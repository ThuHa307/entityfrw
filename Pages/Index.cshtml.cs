using entityfrw.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace entityfrw.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly MyBlogContext _myContext;

    public IndexModel(ILogger<IndexModel> logger, MyBlogContext myContext)
    {
        _logger = logger;
        _myContext = myContext;
    }

    public void OnGet()
    {
        var posts = (from article in _myContext.articles
                    select article).ToList();
        ViewData["posts"] = posts;
    }
}
