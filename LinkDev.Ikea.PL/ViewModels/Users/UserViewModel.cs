
namespace LinkDev.Ikea.PL.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }

        internal static void Add(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
