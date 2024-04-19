using Microsoft.AspNetCore.Identity;

namespace Index_Libri.Server.BLL.Model
{
    public class ApplicationUser
    {
        public string UserEmail { get; set; }
        public string? Token { get; set; }

        // Navigation property
        internal BookList BookList { get; set; }

        public ApplicationUser()
        {
        }
    }
}
