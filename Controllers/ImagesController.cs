namespace api.Controllers;

public class ImagesController : BaseApiController
{

    private static readonly HttpClient client = new HttpClient();
    
    private readonly IConfiguration _config;
    private IUsers _users;
    

    public ImagesController(
        IConfiguration config,
        IUsers users)
    {
        _config = config;
        _users = users;
    }

    //get a Paged list of images that the user can see
    [HttpGet("getImages/{{userId}}")]
    public async Task<ActionResult<List<ImageDto>>> getImages(int UserId)
    {
        var images = await _users.getImages();
        return images;
    }
}
