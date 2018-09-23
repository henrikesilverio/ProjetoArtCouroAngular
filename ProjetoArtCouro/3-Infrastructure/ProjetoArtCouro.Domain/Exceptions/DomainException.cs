using ProjetoArtCouro.Domain.Contracts.IException;
using System;

namespace ProjetoArtCouro.Domain.Exceptions
{
    public class DomainException : Exception, IDomainException
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}
