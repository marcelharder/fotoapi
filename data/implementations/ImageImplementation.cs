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
        private UserManager<AppUser> _userManager;
        private IHttpContextAccessor _ht;
        private readonly IMapper _mapper;
        private readonly IUsers _user;

        public ImageImplementation(
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

        public async Task<int> addImage(Image imagedto)
        {
            var result = 1;
            var img = _mapper.Map<Image>(imagedto);
            _context.Images.Add(img);
            await _context.SaveChangesAsync();
            return result;
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
            foreach (Image im in images)
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
                _context.Images.Update(_mapper.Map<Image>(imagedto));
            });

            return 1;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CategoryDto>> getCategories()
        {
            List<CategoryDto> list = new List<CategoryDto>();
            List<CategoryDto> test = new List<CategoryDto>();

            await Task.Run(() =>
           {
               var cat = new CategoryDto();
               cat.Id = 1;
               cat.Description = "Baden-Baden";
               cat.MainPhoto = "../../assets/dias/baden-baden/10.jpg";
               list.Add(cat);

               var cat1 = new CategoryDto();
               cat1.Id = 2;
               cat1.Description = "Blitterswijk";
               cat1.MainPhoto = "../../assets/dias/baden-baden/10.jpg";
               list.Add(cat1);

               var cat2 = new CategoryDto();
               cat2.Id = 3;
               cat2.Description = "Baarn";
               cat2.MainPhoto = "../../assets/dias/baden-baden/10.jpg";
               list.Add(cat2);

               var cat3 = new CategoryDto();
               cat3.Id = 4;
               cat3.Description = "Beaufortlaan";
               cat3.MainPhoto = "../../assets/dias/baden-baden/10.jpg";
               list.Add(cat3);

               var cat4 = new CategoryDto();
               cat4.Id = 5;
               cat4.Description = "Birkenheuvelweg";
               cat4.MainPhoto = "../../assets/dias/birkenheuvelweg/0.jpg";
               list.Add(cat4);

               var cat5 = new CategoryDto();
               cat5.Id = 6;
               cat5.Description = "Jong Beatrix";
               cat5.MainPhoto = "../../assets/dias/baden-baden/10.jpg";
               list.Add(cat5);

               var cat6 = new CategoryDto();
               cat6.Id = 7;
               cat6.Description = "Engeland 1976";
               cat6.MainPhoto = "../../assets/dias/baden-baden/10.jpg";
               list.Add(cat6);

           }

           );

            // show only the categories that this user is allowed to see
            var allowed = "";
            var loggedInUser = _ht.HttpContext?.User;
            var userdetails = await _userManager.FindByNameAsync(loggedInUser.Identity.Name);


            if (userdetails != null)
            {
                if (userdetails.AllowedToSee != null) { allowed = userdetails.AllowedToSee; }
            }
            // make array from this string
            var help = allowed.Split(',');
            var help2 = help.ToList();
            foreach (string r in help2)
            {
                if (list.Exists(x => x.Id == Convert.ToInt32(r)))
                {
                    test.Add(list.FirstOrDefault(x => x.Id == Convert.ToInt32(r)));
                }
            }
            return test;
        }

        
    }
}
