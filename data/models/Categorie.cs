namespace fotoservice.data.models;

public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string MainPhoto { get; set; }
    }
  
