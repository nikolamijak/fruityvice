using System.Runtime.Serialization;

namespace Fruityvice.Api.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        private const string DefaultMessage = "The item can not be found.";

        public NotFoundException() : this(DefaultMessage)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
