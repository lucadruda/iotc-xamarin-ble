namespace iotc_csharp_service.Types
{

    public class Measure
    {
        private string fieldName;
        private string displayName;

        private MeasureType measureType;

        public Measure(string fieldName, string displayName, MeasureType measureType)
        {
            this.fieldName = fieldName;
            this.displayName = displayName;
            this.measureType = measureType;
        }

        public string FieldName { get => fieldName; set => fieldName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public override string ToString()
        {
            return this.displayName;
        }

        public enum MeasureType
        {
            TELEMETRY, STATE, EVENT
        }
    }
}