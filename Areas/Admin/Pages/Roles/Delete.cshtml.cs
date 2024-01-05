using System.ComponentModel.DataAnnotations;
using entityfrw.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Admin.Roles
{
    public class DeleteModel : RolePageModel
    {
        public DeleteModel(RoleManager<IdentityRole> roleManager, MyBlogContext context) : base(roleManager, context)
        {
        }

        public IdentityRole Role {set; get;}
        public async Task<IActionResult> OnGet(string roleid)
        {
            if(roleid == null) return NotFound();
            Role = await _roleManager.FindByIdAsync(roleid);
            if(Role == null) {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string roleid) {
            if(roleid == null) return NotFound();
            Role = await _roleManager.FindByIdAsync(roleid);
            if(Role == null) {
                return NotFound();
            }
            
            var result = await _roleManager.DeleteAsync(Role);
            if (result.Succeeded) {
                StatusMessage = $"Role {Role.Name} deleted successfully";
                return RedirectToPage("./Index");
            }
            else {
                result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err.Description));
            }
            return Page();
        }
    }
}
