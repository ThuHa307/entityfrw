using System.ComponentModel.DataAnnotations;
using entityfrw.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Admin.Roles
{
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, MyBlogContext context) : base(roleManager, context)
        {
        }

        public class InputModel {

            [Required(ErrorMessage = "Need to enter {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must be at least {2} characters and at most {1} characters")]
            public string Name { get; set; }
        }


        [BindProperty]
        public InputModel Input {set; get;}
        public IdentityRole Role {set; get;}
        public async Task<IActionResult> OnGet(string roleid)
        {
            if(roleid == null) return NotFound();
            Role = await _roleManager.FindByIdAsync(roleid);
            if(Role != null) {
                
                Input = new InputModel() {Name = Role.Name};
                return Page();
            }
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(string roleid) {
            if(roleid == null) return NotFound();
            Role = await _roleManager.FindByIdAsync(roleid);
            if(Role == null) {
                return NotFound();
            }
            if(!ModelState.IsValid)
                return Page();
            
            Role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(Role);
            if (result.Succeeded) {
                StatusMessage = $"Role {Input.Name} updatted successfully";
                return RedirectToPage("./Index");
            }
            else {
                result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err.Description));
            }
            return Page();
        }
    }
}
