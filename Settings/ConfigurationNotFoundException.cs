using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MercuryWebsite.Tests.UI.Settings
{
    [Serializable]
    public class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException()
        {
        }

        public ConfigurationNotFoundException(string configurationType)
            : base($"Configuration section for {configurationType} was not found. Please add the section!")
        {
        }

        public ConfigurationNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConfigurationNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
        }
    }
}
