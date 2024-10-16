namespace fotoservice.data;

public class Seed
{
    public static async Task SeedUsers(
        UserManager<AppUser> manager,
        RoleManager<AppRole> roleManager
    )
    {
        if (await manager.Users.AnyAsync())
            return;
        var userData = await System.IO.File.ReadAllTextAsync("data/seed/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        if (users == null)
            return;

        var roles = new List<AppRole>
        {
            new AppRole { Name = "Surgery" },
            new AppRole { Name = "Moderator" },
            new AppRole { Name = "Sponsor" },
            new AppRole { Name = "Refcard" },
            new AppRole { Name = "Admin" },
            new AppRole { Name = "Cardiologist" },
            new AppRole { Name = "Chef" }
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        foreach (AppUser ap in users)
        {
            ap.UserName = ap.UserName.ToLower();
            await manager.CreateAsync(ap, "Pa$$w0rd");
            await manager.AddToRoleAsync(ap, "Surgery");
        }

        var admin = new AppUser
        {
            UserName = "admin@gfancy.nl",
            Email = "admin@gfancy.nl",
            Gender = "male",
            PaidTill = new DateTime().AddYears(2250)
        };
        await manager.CreateAsync(admin, "Pa$$w0rd");
        await manager.AddToRoleAsync(admin, "Admin");
    }

    /* public static async Task SeedImages(ApplicationDbContext context)
    {
        if (await context.Images.AnyAsync()) return;
        var diaData = await System.IO.File.ReadAllTextAsync("data/seed/ImageData.json");
        var images = JsonSerializer.Deserialize<List<models.Image>>(diaData);
        
        
        if (images != null)
        {
            foreach (models.Image im in images)
            {
                // save image to database
                var result = context.Images.Add(im);
            }
            await context.SaveChangesAsync();
        }



    } */
    public static async Task SeedCategories(ApplicationDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;
        var catData = await System.IO.File.ReadAllTextAsync("data/seed/CategoryData.json");
        var categories = JsonSerializer.Deserialize<List<Category>>(catData);

        if (categories != null)
        {
            foreach (Category im in categories)
            {
                // save image to database
                var result = context.Categories.Add(im);
            }
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedImages(ApplicationDbContext context, IImage image)
    {
        var counter = 0;
        var catList = new List<Category>();
        ImageDto test;

        if (await context.Images.AnyAsync())
            return;

        catList = await image.getCategories();

        if (catList != null)
        {
            for (int x = 1; x < catList.Count; x++)
            {
                if (catList[x].Number_of_images != 0)
                {
                    counter += (int)catList[x].Number_of_images;
                }
                string? url = catList[x].Name + "/" + x.ToString() + ".jpg";

                test = new ImageDto
                {
                    Id = counter.ToString(),
                    ImageUrl = url,
                    YearTaken = 1995,
                    Location = "",
                    Familie = "",
                    Category = catList[x].Id,
                    Series = "",
                    Spare1 = "",
                    Spare2 = "",
                    Spare3 = "",
                };
                await image.addImage(test);

                //addImage(catList[x],counter,url,_dapper);
            }
        }
    }

   /*  private static async void addImage(
        CategoryDto up,
        int Counter,
        string ImageUrl,
        DapperContext _context
    )
    {
        var query =
            "INSERT INTO Images (Id,ImageUrl,YearTaken,Location,Familie,Category,Series,Spare1,Spare2,Spare3)"
            + "VALUES(@Id,@ImageUrl,@YearTaken,@Location,@Familie,@Category,@Series,@Spare1,@Spare2,@Spare3)";

        var parameters = new DynamicParameters();

        parameters.Add("Id", Counter, DbType.Int32);
        parameters.Add("ImageUrl", ImageUrl, DbType.String);
        parameters.Add("YearTaken", 1955, DbType.Int32);
        parameters.Add("Location", up.Description, DbType.String);
        parameters.Add("Familie", "n/a", DbType.String);
        parameters.Add("Category", up.Id, DbType.Int32);
        parameters.Add("Series", "n/a", DbType.String);
        parameters.Add("Spare1", "n/a", DbType.String);
        parameters.Add("Spare2", "n/a", DbType.String);
        parameters.Add("Spare3", "n/a", DbType.String);

        using (var connection = _context.CreateConnection())
        {
            //var id =  connection.QuerySingleAsync<int>(query,parameters);
            var image = new models.Image()
            {
                Id = "Id",
                ImageUrl = ImageUrl,
                YearTaken = 1955,
                Location = up.Description,
                Familie = "n/a",
                Category = (int)up.Id,
                Quality = "n/a",
                Series = "n/a",
                Spare1 = "",
                Spare2 = "",
                Spare3 = ""
            };
            var rowsAffected = await connection.ExecuteAsync(query, image);
            Console.WriteLine($"{rowsAffected} rows inserted");
        }
    }
 */
 
 }
