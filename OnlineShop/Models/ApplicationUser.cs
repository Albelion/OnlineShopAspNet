using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstNmae { get; set; }
        public string LastName { get; set; }
    }
}
