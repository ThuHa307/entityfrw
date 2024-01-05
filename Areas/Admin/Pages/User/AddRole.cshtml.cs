using System.ComponentModel;
using entityfrw.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Admin.User
{
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppUser User { get; set; }
        public string StatusMessage { get; set; }

        [BindProperty]
        [DisplayName("List of roles")]
        public string[] RoleNames { get; set; }
        public SelectList AllRoles { get; set; }
        public AddRoleModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(id)) {
                return NotFound();
            }
            User = await _userManager.FindByIdAsync(id);
            if (User == null) return NotFound();
            RoleNames = (await _userManager.GetRolesAsync(User)).ToArray<string>();

            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            AllRoles = new SelectList(roleNames);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            User = await _userManager.FindByIdAsync(id);
            if (User == null) return NotFound();

            var oldRoleNames = (await _userManager.GetRolesAsync(User)).ToArray();
            var deleteRoleNames = oldRoleNames.Where(r => !RoleNames.Contains(r));
            var addRoles = RoleNames.Where(r => !oldRoleNames.Contains(r));

            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            AllRoles = new SelectList(roleNames);

            var resultDelete = await _userManager.RemoveFromRolesAsync(User, deleteRoleNames);
            if(!resultDelete.Succeeded) {
                resultDelete.Errors.ToList().ForEach(err => {
                    ModelState.AddModelError(string.Empty, err.Description);
                });
                return Page();
            }
            var resultAdd= await _userManager.AddToRolesAsync(User, addRoles);
            if(!resultAdd.Succeeded) {
                resultAdd.Errors.ToList().ForEach(err => {
                    ModelState.AddModelError(string.Empty, err.Description);
                });
                return Page();
            }

            StatusMessage = $"Updated role successfully for user {User.UserName}";
            return RedirectToPage("./Index");
        }


    }
}
