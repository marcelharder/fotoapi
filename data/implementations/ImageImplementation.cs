using System.Globalization;
using api.helpers;
using AutoMapper.QueryableExtensions;
using fotoservice.api.data.interfaces;
using fotoservice.api.helpers;

namespace api.data.implementations
{
    public class ImageImplementation : IImage
    {
        private ApplicationDbContext _context;
        private DapperContext _dap;
        private UserManager<AppUser> _userManager;
        private IHttpContextAccessor _ht;
        private readonly IMapper _mapper;
        private readonly IUsers _user;

        private readonly IConfiguration _conf;

        public ImageImplementation(
            IConfiguration conf,
            DapperContext dap,
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor ht,
            IUsers user
        )
        {
            _user = user;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _ht = ht;
            _dap = dap;
            _conf = conf;
        }

        public async Task<PagedList<ImageDto>> getImages(ImageParams imgP)
        {
            IQueryable<ImageDto> images;

            if (imgP.Category == 1)
            {
                images = _context
                    .Images.ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
            }
            else
            {
                images = _context
                    .Images.Where(x => x.Category == imgP.Category)
                    .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
            }

            return await PagedList<ImageDto>.CreateAsync(images, imgP.PageNumber, imgP.PageSize);
        }

       

        public async Task<ImageDto> findImage(string Id)
        {
            var selectedImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<ImageDto>(selectedImage);
        }

        public async Task<ActionResult<List<ImageDto>>> findImagesByUser(string email)
        {
            // get the categories that his user can see
            string[] cararray = { };
            IQueryable<ImageDto> images;
            var l = new List<ImageDto>();

            var selectedUser = await _user.GetUserByMail(email);

            if (selectedUser != null)
            {
                var categories = selectedUser.AllowedToSee;
                if (categories != null)
                {
                    cararray = categories.Split(",");
                }
            }
            foreach (string s in cararray)
            {
                images = _context
                    .Images.Where(x => x.Category == Convert.ToInt32(s))
                    .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();

                l.AddRange(await images.ToListAsync());
            }
            return l;
        }

        public async Task<ActionResult<List<ImageDto>>> getImagesByCategory(int category)
        {
            string[] cararray = { };
            var l = new List<ImageDto>();
            // get all the images from this category
            var images = await _context.Images.Where(x => x.Category == category).ToArrayAsync();
            // return image DTO
            foreach (fotoservice.data.models.Image im in images)
            {
                l.Add(_mapper.Map<ImageDto>(im));
            }
            return l;
        }

        public async Task<int> deleteImage(string id)
        {
            var selectedImage = await _context.Images.FirstOrDefaultAsync(x =>
                x.Id == id
            );
            if (selectedImage != null)
            {
                _context.Images.Remove(selectedImage);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> updateImage(ImageDto imagedto)
        {
            await Task.Run(() =>
            {
                _context.Images.Update(_mapper.Map<fotoservice.data.models.Image>(imagedto));
            });

            return 1;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> addImage(ImageDto imDto)
        {
             var result = 1;
            var img = _mapper.Map<fotoservice.data.models.Image>(imDto);
            _context.Images.Add(img);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
