

namespace fotoservice.api.helpers;

    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Image, ImageDto>().ReverseMap();
        }


    }
