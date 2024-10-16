namespace api.data.implementations;

public class Dappercategory : IDapperCategoryService
{
    private readonly DapperContext _context;

    public Dappercategory(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SeedImages()
    {
        var catList = await GetAllCategories();
        var counter = 0;
        ImageDto test;
        if (catList != null)
        {
            for (int x = 0; x < catList.Count; x++)
            {
                counter += (int)catList[x].Number_of_images;

                for (int y = 0; y < counter; y++)
                {
                    string? url = catList[x].Name + "/" + (x + 1).ToString() + ".jpg";
                    test = new ImageDto
                    {
                        Id = (y + 1).ToString(),
                        ImageUrl = url,
                        YearTaken = 1995,
                        Location = "mijn test",
                        Familie = "",
                        Category = (int)catList[y].Id,
                        Series = "",
                        Spare1 = "",
                        Spare2 = "",
                        Spare3 = "",
                    };
                    await AddImage(test);
                }
            }
        }

        return 1;
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
                    help.MainPhoto =
                        "http://localhost:5123/api/Images/getImageFile/" + cat.MainPhoto;
                    help.Number_of_images = 0;
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
            var document = await connection.QueryFirstOrDefaultAsync<CategoryDto>(
                query,
                new { id }
            );

            return document;
        }
    }

    public Task UpdateCategory(Category up)
    {
        throw new NotImplementedException();
    }

    public Task AddImage(ImageDto test)
    {
        var query =
            "INSERT INTO Images (Id,ImageUrl,YearTaken,Location,Familie,Category,Series,Spare1,Spare2,Spare3)"
            + "VALUES(@Id,@ImageUrl,@YearTaken,@Location,@Familie,@Category,@Series,@Spare1,@Spare2,@Spare3)";

        var parameters = new DynamicParameters();

        parameters.Add("Id", test.Id, DbType.Int32);
        parameters.Add("ImageUrl", test.ImageUrl, DbType.String);
        parameters.Add("YearTaken", 1955, DbType.Int32);
        parameters.Add("Location", test.Location, DbType.String);
        parameters.Add("Familie", "n/a", DbType.String);
        parameters.Add("Category", test.Id, DbType.Int32);
        parameters.Add("Series", "n/a", DbType.String);
        parameters.Add("Spare1", "n/a", DbType.String);
        parameters.Add("Spare2", "n/a", DbType.String);
        parameters.Add("Spare3", "n/a", DbType.String);

        using (var connection = _context.CreateConnection())
        {
            var id = connection.Execute(query, parameters);
        }
        return null;
    }
}
