using entityfrw.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Admin.Roles {
    public class RolePageModel : PageModel 
    {
        protected readonly MyBlogContext _context;
        protected readonly RoleManager<IdentityRole> _roleManager;

        [TempData]
        public string StatusMessage { get; set; }
        public RolePageModel(RoleManager<IdentityRole> roleManager, MyBlogContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
    }
}