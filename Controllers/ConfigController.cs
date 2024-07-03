namespace api.Controllers;


public class ConfigController : BaseApiController
{
    private readonly ILogger<ConfigController> _logger;
    private readonly IConfig _config;

    

    public ConfigController(ILogger<ConfigController> logger, IConfig config)
    {
        _logger = logger;
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


}
