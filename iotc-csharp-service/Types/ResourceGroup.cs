namespace iotc_csharp_service.Types
{

    public class ResourceGroup
    {

        private string location;
        private string name;

        public ResourceGroup()
        {

        }

        public ResourceGroup(string name, string location)
        {
            this.location = location;
            this.name = name;
        }

        public string Location { get => location; set => location = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return name;
        }
    }
}