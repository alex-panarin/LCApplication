using System;

namespace LC.Backend.Common.DB
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
