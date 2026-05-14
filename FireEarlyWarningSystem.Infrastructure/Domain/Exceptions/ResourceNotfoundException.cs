using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Exceptions
{
    public class ResourceNotfoundException : Exception
    {
        public string ResourceType { get; } = "";
        public string ResourceId { get; } = "";
        public ResourceNotfoundException()
        {
        }

        public ResourceNotfoundException(string? message) : base(message)
        {
        }

        public ResourceNotfoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
        public ResourceNotfoundException(string resourceType, string resourceId) : base($"The entity of type '{resourceType}' with ID '{resourceId}' cannot be found.")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
    }
}
