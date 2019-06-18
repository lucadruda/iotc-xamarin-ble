using iotc_csharp_service.Types;
using System.Collections.Generic;
using static iotc_csharp_service.Types.Measure;

namespace iotc_csharp_service.Templates
{
    public class DevKitTemplate : IoTCTemplate
    {


        public override string Id
        {
            get
            {
                return "iotc-devkit-sample@1.0.0";
            }
        }


        public override Dictionary<string, string> Models
        {
            get
            {
                return new Dictionary<string, string>  {
            {
                "130772c7-97dd-4a76-bbdb-9209888293f6", "mxchip" },
                { "4982cf0d-dfe2-461f-864e-948bc3e82124", "raspberrypi" },
                { "8067a3f6-ca7e-4d76-99c9-12d06d14a1ce", "winIoTCore" }
        };
            }
        }


        public override List<Measure> GetMeasures(string modelId)
        {
            switch (modelId)
            {
                case "130772c7-97dd-4a76-bbdb-9209888293f6":
                    return new List<Measure> {
                    {new Measure("humidity", "humidity", MeasureType.TELEMETRY)},
                    { new Measure("temp", "temperature", MeasureType.TELEMETRY)},
                    { new Measure("pressure", "pressure", MeasureType.TELEMETRY)},
                    { new Measure("magnetometerX", "magnetometerX", MeasureType.TELEMETRY)},
                    { new Measure("magnetometerY", "magnetometerY", MeasureType.TELEMETRY)},
                    { new Measure("magnetometerZ", "magnetometerZ", MeasureType.TELEMETRY)},
                    { new Measure("accelerometerX", "accelerometerX", MeasureType.TELEMETRY)},
                    { new Measure("accelerometerY", "accelerometerY", MeasureType.TELEMETRY)},
                    { new Measure("accelerometerZ", "accelerometerZ", MeasureType.TELEMETRY)},
                    { new Measure("gyroscopeX", "gyroscopeX", MeasureType.TELEMETRY)},
                    { new Measure("gyroscopeY", "gyroscopeY", MeasureType.TELEMETRY)},
                    { new Measure("gyroscopeZ", "gyroscopeZ", MeasureType.TELEMETRY)},
                    { new Measure("ButtonBPressed", "Button B Pressed", MeasureType.EVENT)},
                    { new Measure("deviceState", "Device State", MeasureType.STATE)}
            };
                case "4982cf0d-dfe2-461f-864e-948bc3e82124":
                    return new List<Measure>
                {
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
                case "8067a3f6-ca7e-4d76-99c9-12d06d14a1ce":
                    return new List<Measure> {
                    {new Measure("humidity", "humidity", MeasureType.TELEMETRY)},
                    {new Measure("temp", "temperature", MeasureType.TELEMETRY)},
                    {new Measure("pressure", "pressure", MeasureType.TELEMETRY)}
                };
                default:
                    return new List<Measure>();
            }
        }
    }
}