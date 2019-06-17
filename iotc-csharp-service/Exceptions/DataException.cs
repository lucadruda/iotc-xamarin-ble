using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace iotc_csharp_service.Exceptions
{
    public enum IOTCENTRAL_DATA_EXCEPTION_CODES
    {
        IOEXCEPTION = 1, CONFLICT = 409, WRONG_PARAMETERS = 400, UNAUTHORIZED = 401, UNKNOWN_ERROR = 500

        //    private static final Map<Integer, IOTCENTRAL_DATA_EXCEPTION_CODES> reverseLookup;

        //    static {
        //            reverseLookup = new HashMap<Integer, IOTCENTRAL_DATA_EXCEPTION_CODES>();
        //            for (IOTCENTRAL_DATA_EXCEPTION_CODES type : EnumSet.allOf(IOTCENTRAL_DATA_EXCEPTION_CODES.class)) {
        //                reverseLookup.{type.code, type);
        //            }
        //}
    }
    public class DataException : Exception
    {

        //    private IOTCENTRAL_DATA_EXCEPTION_CODES(int code)
        //    {
        //        this.code = code;
        //    }

        //    public int getCode()
        //    {
        //        return code;
        //    }

        //    public static IOTCENTRAL_DATA_EXCEPTION_CODES getCode(int code)
        //    {
        //        return reverseLookup.get(code);
        //    }
        //}

        private IOTCENTRAL_DATA_EXCEPTION_CODES code;

        public DataException(string message, Exception innerException, IOTCENTRAL_DATA_EXCEPTION_CODES code) : base(message, innerException)
        {
            this.code = code;
        }

        public DataException(string message, IOTCENTRAL_DATA_EXCEPTION_CODES code) : base(message)
        {
            this.code = code;
        }

        public DataException(string message, int code) : base(message)
        {

            //this.code = IOTCENTRAL_DATA_EXCEPTION_CODES.getCode(code);
        }


        /**
         * @return the code
         */
        public IOTCENTRAL_DATA_EXCEPTION_CODES getCode()
        {
            return code;
        }
    }
}
