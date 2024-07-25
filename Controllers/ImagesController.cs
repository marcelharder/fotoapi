namespace api.Controllers;

public class ImagesController : BaseApiController
{
    private readonly IImage _image;

    public ImagesController(IImage image)
    {
        _image = image;
    }

    //get a Paged list of images per Category
    [HttpGet("getImages")]
    public async Task<ActionResult<PagedList<ImageDto>>> GetImages([FromQuery] ImageParams imgP)
    {
        var plImages = await _image.getImages(imgP);
        Response.AddPaginationHeader(
            new PaginationHeader(
                plImages.CurrentPage,
                plImages.PageSize,
                plImages.TotalCount,
                plImages.TotalPages
            )
        );
        return Ok(plImages);
    }

    [HttpGet("getImagesByCategory/{cat}")]
    public async Task<ActionResult<List<ImageDto>>> GetImagesByCat(int cat)
    {
        var plImages = await _image.getImagesByCategory(cat);
        return Ok(plImages);
    }

    [HttpPost("addImage")]
    public async Task<ActionResult<int>> AddImage(ImageDto imagedto)
    {
        return await _image.addImage(imagedto);
    }

    [HttpDelete("id")]
    public async Task<ActionResult<int>> DeleteImage(int id)
    {
        return await _image.deleteImage(id);
    }

    [HttpPut("updateImage")]
    public async Task<ActionResult<int>> UpdateImage(ImageDto imagedto)
    {
        return await _image.updateImage(imagedto);
    }

    [HttpGet("findImage/{Id}")]
    public async Task<ActionResult<ImageDto>> FindImage(string Id)
    {
        return await _image.findImage(Id);
    }

   
}
