
using System.Linq;

namespace api.data.implementations;

public class Dappercategory : IDapperCategoryService
{

    private readonly DapperContext _context;

    public Dappercategory(DapperContext context)
    {
        _context = context;
    }

    public Task<Category> CreateCategory(Category up)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var query = "Select * FROM Categories";
        using (var connection = _context.CreateConnection())
        {
            var documents = await connection.QueryAsync<CategoryDto>(query);

            return documents.ToList();
        }
    }

    public async Task<List<CategoryDto>> GetAllowedCategories(List<int> test)
    {
        var allCategories = await GetAllCategories();
        var _result = new List<CategoryDto>();

        foreach (CategoryDto cat in allCategories)
        {
            if (cat.Description != null)
            {
                var id = cat.Id;
                if (test.Contains((int)id))
                {
                    var help = new CategoryDto();
                    help.Id = id;
                    help.Description = cat.Description;
                    help.MainPhoto = "http://localhost:5123/api/Images/getImageFile/" + cat.MainPhoto;
                    _result.Add(help);
                }
            }
        }
        return _result;
    }

    public async Task<CategoryDto?> getSpecificCategory(int id)
    {

         var query = "Select * FROM Categories WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var document = await connection.QueryFirstOrDefaultAsync<CategoryDto>(query, new { id });
           
                
                return document;
            }
    }

    public Task UpdateCategory(Category up)
    {
        throw new NotImplementedException();
    }
}