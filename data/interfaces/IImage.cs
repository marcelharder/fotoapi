using System.Globalization;
using api.helpers;
using fotoservice.api.helpers;

namespace fotoservice.data.interfaces;

public interface IImage
    {
        Task<PagedList<ImageDto>> getImages(ImageParams imgP );
       // Task<List<CategoryDto>> getCategories( );
        Task<int> addImage(ImageDto imdto);
        Task<int> deleteImage(string id);
        Task<int> updateImage(ImageDto image);
        Task<ImageDto> findImage(string Id);
        Task<ActionResult<List<ImageDto>>> findImagesByUser(string email);
        Task<ActionResult<List<ImageDto>>> getImagesByCategory( int category);
    Task<bool> SaveChangesAsync();
}
