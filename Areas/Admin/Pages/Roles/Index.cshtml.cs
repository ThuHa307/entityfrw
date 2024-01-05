using entityfrw.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace App.Admin.Roles
{
    public class IndexModel : RolePageModel
    {
        public List<IdentityRole> Roles { get; set; }
        public IndexModel(RoleManager<IdentityRole> roleManager, MyBlogContext context) : base(roleManager, context)
        {
        }

        public async Task OnGet()
        {
            Roles = await _roleManager.Roles.OrderByDescending(role => role.Name).ToListAsync();
        }

        public void OnPost() => RedirectToPage();
    }
}
//dotnet new page -n Create -o Areas/Admin/Pages/Roles -na App.Admin.Roles
