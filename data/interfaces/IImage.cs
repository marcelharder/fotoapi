using api.helpers;
using fotoservice.api.helpers;

namespace fotoservice.data.interfaces;

public interface IImage
    {
        Task<PagedList<ImageDto>> getImages(ImageParams imgP );

        Task<int> addImage(ImageDto image);
        Task<ImageDto> findImage(string Id);
    Task<ActionResult<List<ImageDto>>> findImagesByUser(string email);
}
