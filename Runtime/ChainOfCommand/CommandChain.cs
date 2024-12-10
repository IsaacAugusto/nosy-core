using System.Collections.Generic;

namespace NosyCore.ChainOfCommand
{
    /// <summary>
    /// This class should be used to store a chain of handlers for a message/command of type T.
    /// </summary>
    /// <typeparam name="T">IChainMessage implementation</typeparam>
    public class CommandChain<T> where T : IChainMessage
    {
        private LinkedList<IHandler<T>> _handlers = new();
        
        /// <summary>
        /// Handle message with the handlers defined.
        /// </summary>
        public void Handle(T message)
        {
            var handler = _handlers.First;
            if (handler == null) return;

            while (handler != null)
            {
                if (handler.Value.Handle(message) == false)
                {
                    break;
                }

                handler = handler.Next;
            }
        }

        public bool HasHandler(IHandler<T> handler) => _handlers.Contains(handler);
        public void ClearHandlers() => _handlers.Clear();
        public void AddHandler(IHandler<T> handler) => _handlers.AddLast(handler);
        public bool RemoveHandler(IHandler<T> handler) => _handlers.Remove(handler);
    }
}