using System.Net;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using IronSoftware.Drawing;

namespace api.Controllers;

public class ImagesController : BaseApiController
{
    private readonly IImage _image;
    private readonly IMapper _mapper;
    private readonly IDapperCategoryService _dapper;

    private readonly IConfiguration _conf;



    public ImagesController(IImage image, IMapper mapper, IConfiguration conf, IDapperCategoryService dapper)
    {
        _image = image;
        _mapper = mapper;
        _conf = conf;
        _dapper = dapper;

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

    [HttpGet("getImageFile/{id}")]

    public async Task<IActionResult> getImageFile(int id)
    {
        var locationPrefix = _conf.GetValue<string>("NfsLocation");
        AnyBitmap anyBitmap;

        // in appsettings => "LocationPreFix": "/media/marcel/MSI Harddrive/webimages/",
        // in appsettings => "NfsLocation": "/nfs/mariadb_data/fotos/",
        var selectedImage = await _image.findImage(id.ToString());
        var img = System.IO.File.OpenRead(locationPrefix + selectedImage.ImageUrl);

        using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(img))
        {
            int width = image.Width / 4;
            int height = image.Height / 4;
            image.Mutate(x => x.Resize(width, height));

            anyBitmap = image;
            var help = anyBitmap.ExportBytesAsJpg();
            //MemoryStream stream = anyBitmap.ToStream();

            return File(help, "image/jpg");
        }
    }
    [HttpGet("getFullImageFile/{id}")]
    public async Task<IActionResult> getFullImageFile(int id)
    {
        var locationPrefix = _conf.GetValue<string>("NfsLocation");
        // in appsettings => "LocationPreFix": "/media/marcel/MSI Harddrive/webimages/",
        // in appsettings => "NfsLocation": "/nfs/mariadb_data/fotos/",
        var selectedImage = await _image.findImage(id.ToString());
        var img = System.IO.File.OpenRead(locationPrefix + selectedImage.ImageUrl);

        return File(img, "image/jpg");
    }




    [HttpGet("getImagesByCategory/{cat}")]
    public async Task<ActionResult<List<ImageDto>>> GetImagesByCat(int cat)
    {
        var plImages = await _image.getImagesByCategory(cat);
        return Ok(plImages);
    }

    [HttpPost("addImage")]
    public async Task<ActionResult<int>> AddImage()
    {
      
      await _dapper.SeedImages();
       
      
       return Ok();
          
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
    
     [HttpGet("addImage")]
    public async Task<ActionResult<ImageDto>> FindtwoImage(string Id)
    {
        return await _image.findImage(Id);
    }

   [HttpGet("getCarousel/{Id}")]
    public async Task<ActionResult<CarouselDto>> getCarousel(string Id)
    {
        return await _image.getCarouselData(Id);
    }


}
