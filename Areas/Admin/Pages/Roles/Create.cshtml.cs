using System.ComponentModel.DataAnnotations;
using entityfrw.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Admin.Roles
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, MyBlogContext context) : base(roleManager, context)
        {
        }

        public class InputModel {

            [Required(ErrorMessage = "Need to enter {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must be at least {2} characters and at most {1} characters")]
            public string Name { get; set; }
        }


        [BindProperty]
        public InputModel Input {set; get;}

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {
            if(!ModelState.IsValid)
                return Page();
            var role = new IdentityRole(Input.Name);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) {
                StatusMessage = $"Role {Input.Name} created successfully";
                return RedirectToPage("./Index");
            }
            else {
                result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err.Description));
            }
            return Page();
        }
    }
}
