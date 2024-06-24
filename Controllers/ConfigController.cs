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
    [HttpGet("getCategories")]
    public async Task<IActionResult> Categories()
    {
        var result = await _config.getCategories();
        return Ok(result);
    }


}
