using fotoservice.data.dtos;

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
        var imageList = new List<ImageDto>();
        var counter = 0;
        var offset = 1;
        var imagecounter = 0;
      
        for (int x = 0; x < catList.Count; x++)
        {
            counter += (int)catList[x].Number_of_images;
            for (int y = offset; y < counter; y++) // get the list of images that belongs to this category
            {
                imagecounter++;
                imageList.Add(
                    new ImageDto
                    {
                        Id = y.ToString(),
                        Location = catList[x].Description,
                        ImageUrl = catList[x].Name + "/" + imagecounter + ".jpg",
                        YearTaken = 1955,
                        Category = (int) catList[x].Id,
                        Familie = "",
                        Quality = "",
                        Series = "",
                        Spare1 = "",
                        Spare2 = "",
                        Spare3 = "",
                    }
                );
            }
            foreach (ImageDto im in imageList){ await AddImage(im); }
            offset = counter + 1;
            imagecounter = 0;
            imageList.Clear();
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
                        "http://localhost:8103/api/Images/getImageFile/" + cat.MainPhoto;
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

    public async Task<int> AddImage(ImageDto test)
    {
        await Task.Run(() =>
        {
            var query =
                "INSERT INTO Images (Id,ImageUrl,YearTaken,Location,Familie,Category,Series,Quality,Spare1,Spare2,Spare3)"
                + "VALUES(@Id,@ImageUrl,@YearTaken,@Location,@Familie,@Category,@Series,@Quality,@Spare1,@Spare2,@Spare3)";

            var parameters = new DynamicParameters();

            parameters.Add("Id", test.Id, DbType.Int32);
            parameters.Add("ImageUrl", test.ImageUrl, DbType.String);
            parameters.Add("YearTaken", 1955, DbType.Int32);
            parameters.Add("Location", test.Location, DbType.String);
            parameters.Add("Familie", "n/a", DbType.String);
            parameters.Add("Category", test.Category, DbType.Int32);
            parameters.Add("Series", "n/a", DbType.String);
            parameters.Add("Quality", "n/a", DbType.String);
            parameters.Add("Spare1", "n/a", DbType.String);
            parameters.Add("Spare2", "n/a", DbType.String);
            parameters.Add("Spare3", "n/a", DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = connection.Execute(query, parameters);
            }
        });
        return 1;
    }
}
