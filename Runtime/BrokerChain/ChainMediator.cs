using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NosyCore.BrokerChain
{
    public class ChainMediator<T>
    {
        private readonly LinkedList<ChainModifier<T>> _modifiers;
        private uint _version { get; set; }
        private ChainQuery<T> _query;
        
        public ChainMediator()
        {
            _modifiers = new LinkedList<ChainModifier<T>>();
            _version = 0;
            CreateQuery(default);
        }
        
        public ChainMediator(T initialData)
        {
            _modifiers = new LinkedList<ChainModifier<T>>();
            _version = 0;
            CreateQuery(initialData);
        }

        public void AddModifier(ChainModifier<T> modifier)
        {
            _modifiers.AddLast(modifier);
            UpdateVersion();
        }
        
        public void RemoveModifier(ChainModifier<T> modifier)
        {
            _modifiers.Remove(modifier);
            UpdateVersion();
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
            
            var current = _modifiers.First;
            while (current != null)
            {
                _query.Data = current.Value.Handle(_query.Data);
                current = current.Next;
            }
            
            _query.Version = _version;

            return _query;
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