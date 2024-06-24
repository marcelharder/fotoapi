using api.helpers;
using AutoMapper.QueryableExtensions;
using fotoservice.api.helpers;

namespace api.data.implementations
{
    public class ImageImplementation : IImage
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ImageImplementation(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedList<ImageDto>> getImages(ImageParams imgP)
        {
            // get images from dapper query

            var images = _context.Images
            .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();
            return await PagedList<ImageDto>.CreateAsync(images, imgP.PageNumber, imgP.PageSize);
        }

        public async Task<int> addImage(ImageDto imagedto)
        {
            var result = 1;
            var img = _mapper.Map<Image>(imagedto);
            _context.Images.Add(img);
            await _context.SaveChangesAsync();
            return result;
        }


    }
}
