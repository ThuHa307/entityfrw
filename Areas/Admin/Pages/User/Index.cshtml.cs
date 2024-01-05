using entityfrw.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace App.Admin.User
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }
        public List<UserAndRole> Users { get; set; }
        public class UserAndRole : AppUser
        {
            public string RoleNames {set;get;}
        }
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            //var qr = user from _userManager.Users
            Users = await _userManager.Users.OrderBy(u => u.UserName).Select(u => new UserAndRole() {
                Id = u.Id,
                UserName = u.UserName
            }).ToListAsync();

            foreach (var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }
        }

        public void OnPost() => RedirectToPage();
    }
}
//dotnet new page -n AddRole -o Areas/Admin/Pages/User -na App.Admin.User
