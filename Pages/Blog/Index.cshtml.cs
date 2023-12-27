using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using entityfrw.models;
using Microsoft.AspNetCore.Authorization;

namespace entityfrw.Pages_Blog
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private const int ITEMS_PER_PAGE = 10;
        public int countPages { set; get;}

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { set; get; }
        private readonly MyBlogContext _context;

        public IndexModel(MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync(string search)
        {
            int totalPages = await _context.articles.CountAsync();
            countPages = (int)Math.Ceiling((double)totalPages / ITEMS_PER_PAGE);
            
            if(currentPage < 1) currentPage = 1;
            if(currentPage > countPages) currentPage = countPages;

            var query = (from a in _context.articles 
                        orderby a.Created descending
                        select a)
                        .Skip((currentPage - 1) * ITEMS_PER_PAGE)
                        .Take(ITEMS_PER_PAGE);
            if (!string.IsNullOrEmpty(search)) {
                Article = await query.Where(q => q.Title.Contains(search)).ToListAsync();
            }
            else Article = await query.ToListAsync();
        }
    }
}
