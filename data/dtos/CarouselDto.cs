using System.ComponentModel;

namespace fotoservice.data.dtos;
    public class CarouselDto
    {
        public int? numberOfImages { get; set;}
        public bool ShowR { get; set;}
        public bool ShowL{ get; set;}
        public string? nextImageIdR {get; set;}
        public string? nextImageIdL {get; set;}
    }