using api.helpers;
using fotoservice.api.helpers;
using fotoservice.extensions;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

public class ImagesController : BaseApiController
{

    private static readonly HttpClient client = new HttpClient();
    
    private readonly IConfiguration _config;
    private IImage _image;
    

    public ImagesController(
        IConfiguration config,
        IImage image)
    {
        _config = config;
        _image = image;
    }

    //get a Paged list of images that the user can see
    [HttpGet("getImages")]
    public async Task<ActionResult<PagedList<ImageDto>>> getImages([FromQuery]ImageParams imgP)
    {
        var plImages = await _image.getImages(imgP);
        Response.AddPaginationHeader(new PaginationHeader(plImages.CurrentPage, plImages.PageSize, plImages.TotalCount, plImages.TotalPages));
        return Ok(plImages);
    }

     [HttpPost("addImage")]
     public async Task<ActionResult<int>> addImage(ImageDto imagedto){

        return await _image.addImage(imagedto);
     }

     [HttpGet("findImage/{Id}")]
     public async Task<ActionResult<ImageDto>> findImage(string Id){
        return await _image.findImage(Id);
     }

    [Authorize]
    [HttpGet("findImagesByUser/{email}")]
     public async Task<ActionResult<List<ImageDto>>> findImagesByUser(string email){
        return await _image.findImagesByUser(email);
     }
}
