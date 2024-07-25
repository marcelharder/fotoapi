namespace api.Controllers;

public class ImagesController : BaseApiController
{
    private readonly IImage _image;
    private readonly IMapper _mapper;

    public ImagesController(IImage image, IMapper mapper)
    {
        _image = image;
        _mapper = mapper;
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
        var img = _mapper.Map<Image>(imagedto);
        await _image.addImage(img);
        if (!await _image.SaveChangesAsync())
        {
            return CreatedAtRoute("GetImage", new { id = img.Id }, img);
        }
        return BadRequest("Could not add Image ...");
    }

    [HttpDelete("deleteImage/{id}")]
    public async Task<ActionResult<int>> DeleteImage(string id)
    {
        var result = await _image.deleteImage(id);
        if (await _image.SaveChangesAsync()) { return Ok("Image removed"); }
        return BadRequest("Image was not deleted");
    }

    [HttpPut("updateImage")]
    public async Task<ActionResult<int>> UpdateImage(ImageDto imagedto)
    {
        var result = await _image.updateImage(imagedto);
        if (await _image.SaveChangesAsync()) { return Ok(); }
        return BadRequest("Image was not updated");

    }

    [HttpGet("findImage/{Id}", Name = "GetImage")]
    public async Task<ActionResult<ImageDto>> FindImage(string Id)
    {
        return await _image.findImage(Id);
    }


}
