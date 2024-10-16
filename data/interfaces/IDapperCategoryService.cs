public interface IDapperCategoryService
    {
        public Task<List<CategoryDto>> GetAllCategories();
        public Task<List<CategoryDto>> GetAllowedCategories(List<int> test);
        public Task<CategoryDto?> getSpecificCategory(int id);
        public Task UpdateCategory(Category up);
        public Task DeleteCategory(int id);
        public Task<Category> CreateCategory(Category up);
        public Task AddImage(ImageDto im);
        public Task<int> SeedImages();
    }