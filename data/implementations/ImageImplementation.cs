using api.helpers;
using AutoMapper.QueryableExtensions;
using fotoservice.api.data.interfaces;
using fotoservice.api.helpers;

namespace api.data.implementations
{
    public class ImageImplementation : IImage
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUsers _user;
        public ImageImplementation(ApplicationDbContext context, IMapper mapper, IUsers user)
        {
            _user = user;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedList<ImageDto>> getImages(ImageParams imgP)
        {
            IQueryable<ImageDto> images;


            if (imgP.Category == 1)
            {
                images = _context.Images
                         .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                         .AsNoTracking();
            }
            else
            {
                images = _context.Images
                       .Where(x => x.Category == imgP.Category)
                       .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking();
            }

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

        public async Task<ImageDto> findImage(string Id)
        {
            var selectedImage = await _context.Images
                 .FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<ImageDto>(selectedImage);
        }

        public async Task<ActionResult<List<ImageDto>>> findImagesByUser(string email)
        {
            // get the categories that his user can see
            string[] cararray = {};
            IQueryable<ImageDto> images;
            var l = new List<ImageDto>();

            var selectedUser = await _user.GetUserByMail(email);

          
            if(selectedUser != null){
                var categories = selectedUser.AllowedToSee;
                if(categories != null){cararray = categories.Split(",");}
                }
            foreach(string s in cararray){
                 images = _context.Images
                       .Where(x => x.Category == Convert.ToInt32(s))
                       .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking();

                l.AddRange(await images.ToListAsync());
            }
            return l;
        }
    }
}
