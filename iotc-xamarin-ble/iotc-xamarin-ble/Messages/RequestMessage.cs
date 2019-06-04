using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Messages
{
    public class RequestMessage
    {

    }

    public class RequestMessage<T>
    {
        public RequestMessage()
        {

        }
        public RequestMessage(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
