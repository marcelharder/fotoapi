using System.ComponentModel;

namespace fotoservice.data.dtos;
    public class CategoryDto
    {
        public int Id { get; set;}
        public required string Description { get; set;}
        public required string MainPhoto { get; set;}

        public CategoryDto(int i,string d,string m)
        {
           Id = i;
           Description = d;
           MainPhoto = m;
        }
    }