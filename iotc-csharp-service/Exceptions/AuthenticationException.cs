using System;
namespace iotc_csharp_service.Exceptions
{
    public class AuthenticationException : Exception
    {

        public AuthenticationException()
        {
        }

        public AuthenticationException(string message, string innercode) : base(message)
        {
            Innercode = innercode;
        }

        public AuthenticationException(string message) : base(message)
        {
        }

        public string Innercode { get; }
    }
}
