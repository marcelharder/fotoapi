namespace fotoservice.data;

public class Seed
{
    private readonly IDapperCategoryService _dapper;
    public Seed(IDapperCategoryService dapper)
    {
        _dapper = dapper;
    }
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

    public async Task SeedImages(ApplicationDbContext context)
    {
        var counter = 0;
        //var categories = new List<CategoryDto>();
        if (await context.Images.AnyAsync())
            return;
        // get all the categories
        var cats = await _dapper.GetAllCategories();
        if(cats != null){
            foreach(CategoryDto cat in cats){
             

            }
        }
        
        


    }
}
