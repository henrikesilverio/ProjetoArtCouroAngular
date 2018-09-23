using ProjetoArtCouro.Domain.Contracts.IException;
using System;

namespace ProjetoArtCouro.Domain.Exceptions
{
    public class BusinessException : Exception, IBusinessException
    {
        public BusinessException() { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception inner) : base(message, inner) { }
    }
}
