using System.Collections.Generic;

namespace NosyCore.ChainOfCommand
{
    public interface IHandler<T> where T : IChainMessage
    {
        /// <returns>If it should pass the message for the next handler.</returns>
        public bool Handle(T message);
    }
}