using api.helpers;
using CloudinaryDotNet.Actions;

namespace fotoservice.data.interfaces;

public interface IImage
    {
        Task<PagedList<ImageDto>> getImages(ImageParams imgP );
    }
