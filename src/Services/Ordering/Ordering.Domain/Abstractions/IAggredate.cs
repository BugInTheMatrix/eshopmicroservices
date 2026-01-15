using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public interface IAggredate<T>:IAggredate, IEntity<T>
    {
    }
    public interface IAggredate:IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvent();
    }
}
