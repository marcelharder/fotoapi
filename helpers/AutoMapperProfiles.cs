

namespace fotoservice.api.helpers;

    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<fotoservice.data.models.Image, ImageDto>().ReverseMap();
        }


    }
