namespace api.Controllers;

public class ConfigController : BaseApiController
{
    private readonly IConfig _config;

    public ConfigController(IConfig config)
    {
        _config = config;
    }

    [Authorize]
    [HttpGet("getAllCategories")]
    public async Task<IActionResult> Categories()
    {
        var result = await _config.getAllCategories();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getAllowedCategories")]
    public async Task<IActionResult> AllowedCategories()
    {
        var result = await _config.getAllowedCategories();
        return Ok(result);
    }

    [HttpGet("getDescription/{category}")]
    public async Task<IActionResult> GetDescription(int category)
    {
          var result = await _config.getDescriptionFromCategory(category);
        return Ok(result);
    }
}
