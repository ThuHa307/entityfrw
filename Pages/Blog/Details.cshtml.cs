using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using entityfrw.models;

namespace entityfrw.Pages_Blog
{
    public class DetailsModel : PageModel
    {
        private readonly entityfrw.models.MyBlogContext _context;

        public DetailsModel(entityfrw.models.MyBlogContext context)
        {
            _context = context;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.articles.FirstOrDefaultAsync(m => m.ID == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
