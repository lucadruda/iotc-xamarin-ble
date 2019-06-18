
using iotc_csharp_service.Templates;
using iotc_csharp_service.Types;

public class Application
{
    private string id;
    private string name;
    private string displayName;
    private string subdomain;
    private string type;
    private string location;
    private string template;
    private IoTCTemplate iotcTemplate;
    private User[] users;

    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string DisplayName { get => displayName; set => displayName = value; }
    public string Subdomain { get => subdomain; set => subdomain = value; }
    public string Type { get => type; set => type = value; }
    public string Location { get => location; set => location = value; }
    public string Template { get => template; set => template = value; }
    public IoTCTemplate IotcTemplate { get => iotcTemplate; set => iotcTemplate = value; }
    public User[] Users { get => users; set => users = value; }


    public Application()
    {

    }

    public Application(string name, string displayName, string subdomain, string location, IoTCTemplate template)
    {
        this.name = name;
        this.displayName = displayName;
        this.subdomain = subdomain;
        this.location = location;
        this.iotcTemplate = template;
    }

    public Application(string id, string name, string subdomain)
    {
        this.id = id;
        this.name = name;
        this.subdomain = subdomain;
    }

    public Application(string id, string name, string displayName, string subdomain, string type, string location,
            IoTCTemplate template) : this(id, name, subdomain)
    {
        this.displayName = displayName;
        this.type = type;
        this.location = location;
        this.iotcTemplate = template;
    }


}