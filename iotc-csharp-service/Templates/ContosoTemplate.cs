using iotc_csharp_service.Types;
using System.Collections.Generic;
using static iotc_csharp_service.Types.Measure;

namespace iotc_csharp_service.Templates
{
    public class ContosoTemplate : IoTCTemplate
    {


        public override string id()
        {
            return "iotc-demo@1.0.0";
        }


        public override Dictionary<string, string> models()
        {
            return new Dictionary<string, string>  {
            {"c318d580-39fc-4aca-b995-843719821049", "Refrigerated Vending Machine" }
        };
        }


        public override List<Measure> GetMeasures(string modelId)
        {
            switch (modelId)
            {
                case "c318d580-39fc-4aca-b995-843719821049":
                    return new List<Measure> {
                  {new Measure("humidity", "humidity", MeasureType.TELEMETRY)},
                    {new Measure("temp", "temperature", MeasureType.TELEMETRY)},
                    {new Measure("pressure", "pressure", MeasureType.TELEMETRY)},
                    {new Measure("magnetometerX", "magnetometerX", MeasureType.TELEMETRY)},
                    {new Measure("magnetometerY", "magnetometerY", MeasureType.TELEMETRY)},
                    {new Measure("magnetometerZ", "magnetometerZ", MeasureType.TELEMETRY)},
                    {new Measure("accelerometerX", "accelerometerX", MeasureType.TELEMETRY)},
                    {new Measure("accelerometerY", "accelerometerY", MeasureType.TELEMETRY)},
                    {new Measure("accelerometerZ", "accelerometerZ", MeasureType.TELEMETRY)},
                    {new Measure("gyroscopeX", "gyroscopeX", MeasureType.TELEMETRY)},
                    {new Measure("gyroscopeY", "gyroscopeY", MeasureType.TELEMETRY)},
                    {new Measure("gyroscopeZ", "gyroscopeZ", MeasureType.TELEMETRY)}
            };
                default:
                    return new List<Measure>();
            }
        }
    }
}