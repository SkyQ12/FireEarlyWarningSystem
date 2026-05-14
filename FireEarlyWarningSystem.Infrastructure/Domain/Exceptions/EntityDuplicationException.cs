using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Exceptions
{
    public class EntityDuplicationException : Exception
    {
        public EntityDuplicationException()
        {
        }

        public EntityDuplicationException(string? message) : base(message)
        {
        }

        public EntityDuplicationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
