using iotc_csharp_service.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_csharp_service.Templates
{
    public abstract class IoTCTemplate
    {

        public abstract string id();

        public abstract Dictionary<string, string> models();

        public abstract List<Measure> GetMeasures(string modelName);

        private static Dictionary<string, Type> templates = new Dictionary<string, Type>
                {
                    {"iotc-demo@1.0.0", typeof(ContosoTemplate) },
            { "iotc-devkit-sample@1.0.0", typeof(DevKitTemplate) }
                };

        public static IoTCTemplate GetTemplate(string id)
        {
            return (IoTCTemplate)Activator.CreateInstance(templates[id]);
        }
    }

}
