using System;
using NSE.Core.DomainObjects;

namespace NSE.Core.Data
{
    public interface IRepositoty<T> : IDisposable where T : IAggregateRoot
    {

    }
}
