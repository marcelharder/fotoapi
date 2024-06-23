using api.helpers;

namespace api.data.implementations
{
    public class Image : IImage
    {
        public Task<PagedList<ImageDto>> getImages(ImageParams imgP)
        {
            // get images from dapper query

            

            var images: IQueryable<ImageDto>;

            return await PagedList<ImageDto>.CreateAsync(images, imgP.PageNumber, imgP.PageSize);
        }
    }
}