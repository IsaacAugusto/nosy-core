using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NosyCore.BrokerChain
{
    public class ChainMediator<T>
    {
        private readonly HashSet<ChainModifier<T>> _modifiers;
        private uint _version { get; set; }
        private ChainQuery<T> _query;
        
        public int ModifiersCont => _modifiers.Count;
        public bool HasModifiers => _modifiers.Count > 0;
        public bool ContainsModifier(ChainModifier<T> modifier) => _modifiers.Contains(modifier);
        
        public ChainMediator()
        {
            _modifiers = new HashSet<ChainModifier<T>>();
            _version = 0;
            CreateQuery(default);
        }
        
        public ChainMediator(T initialData)
        {
            _modifiers = new HashSet<ChainModifier<T>>();
            _version = 0;
            CreateQuery(initialData);
        }

        public bool AddModifier(ChainModifier<T> modifier)
        {
            var success = _modifiers.Add(modifier);
            UpdateVersion();
            return success;
        }
        
        public bool RemoveModifier(ChainModifier<T> modifier)
        {
            var success = _modifiers.Remove(modifier);
            UpdateVersion();
            return success;
        }
        
        public void ClearModifiers()
        {
            _modifiers.Clear();
            UpdateVersion();
        }
        
        public ChainQuery<T> CreateAndApplyQuery(T data)
        {
            CreateQuery(data);
            return QueryValue();
        }

        public ChainQuery<T> QueryValue()
        {
            if (_query.Version == _version)
            {
                return _query;
            }

            foreach (var modifier in _modifiers)
            {
                _query.Data = modifier.Handle(_query.Data);
            }
            
            _query.Version = _version;

            return _query;
        }

        public void EnsureModifier(ChainModifier<T> modifier)
        {
            if (ContainsModifier(modifier))
            {
                return;
            }
            AddModifier(modifier);
        }
        
        public void EnsureNoModifier(ChainModifier<T> modifier)
        {
            if (ContainsModifier(modifier) == false)
            {
                return;
            }
            RemoveModifier(modifier);
        }

        private void CreateQuery(T data)
        {
            UpdateVersion();
            _query = new ChainQuery<T>(data, _version);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateVersion()
        {
            _version = (_version + 1) % uint.MaxValue;
        }
    }

    public class ChainQuery<T>
    {
        public T Data;
        public uint Version { get; set; }

        internal ChainQuery(T data, uint version)
        {
            Data = data;
            Version = version;
        }
    }
}