using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Messages
{
    public class ResultMessage
    {

    }
    public class ResultMessage<T>
    {
        public ResultMessage()
        {

        }
        public ResultMessage(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
