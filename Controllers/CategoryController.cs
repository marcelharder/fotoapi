using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Controllers;

public class CategoryController : BaseApiController
{
    private readonly IDapperCategoryService _cat;
    private UserManager<AppUser> _userManager;
    private IHttpContextAccessor _ht;

    public CategoryController(IDapperCategoryService cat, UserManager<AppUser> userManager, IHttpContextAccessor ht)
    {
        _cat = cat;
        _userManager = userManager;
        _ht = ht;
    }

    [Authorize]
    [HttpGet("getAllCategories")]
    public async Task<IActionResult> Categories()
    {
        var result = await _cat.GetAllCategories();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getAllowedCategories")]
    public async Task<IActionResult> AllowedCategories()
    {
        // get the allowed list from the server based on who is loggedin
        List<CategoryDto> _result = new List<CategoryDto>();
        // get the user that is loggedIn
        var loggedinUser = await _userManager.FindByNameAsync(_ht.HttpContext.User.Identity.Name);

        if (loggedinUser != null)
        {
            if (loggedinUser.AllowedToSee != null)
            {
                List<int> catArray = loggedinUser.AllowedToSee.Split(',')
                 .Select(t => int.Parse(t))
                 .ToList();
                var result = await _cat.GetAllowedCategories(catArray);
                return Ok(result);
            }
        }
        return BadRequest("");
    }

    [HttpGet("getDescription/{category}")]
    public async Task<IActionResult> GetDescription(int category)
    {
        var result = await _cat.getSpecificCategory(category);
        if(result == null){return BadRequest("");}

        return Ok(result.Description);
    }
}
