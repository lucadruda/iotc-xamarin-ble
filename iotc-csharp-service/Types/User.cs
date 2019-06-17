namespace iotc_csharp_service.Types
{
    public class User
    {
        private string user;
        private string role;

        public User(string User, string role)
        {
            this.user = User;
            this.role = role;
        }

        public string UserName { get => user; set => user = value; }
        public string Role { get => role; set => role = value; }
    }
}