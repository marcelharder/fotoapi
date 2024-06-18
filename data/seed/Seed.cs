

namespace fotoservice.data;
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> manager, RoleManager<AppRole> roleManager)
         {
             if (await manager.Users.AnyAsync()) return;
             var userData = await System.IO.File.ReadAllTextAsync("data/seed/UserSeedData.json");
             var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
             if (users == null) return;

             var roles = new List<AppRole>{
                 new AppRole{Name = "Surgery"},
                 new AppRole{Name = "Moderator"},
                 new AppRole{Name = "Sponsor"},
                 new AppRole{Name = "Refcard"},
                 new AppRole{Name = "Admin"},
                 new AppRole{Name = "Cardiologist"},
                 new AppRole{Name = "Chef"}
             };
             foreach (var role in roles) { await roleManager.CreateAsync(role); }
             foreach (AppUser ap in users)
             {
                 ap.UserName = ap.UserName.ToLower();
                 await manager.CreateAsync(ap, "Pa$$w0rd");
                 await manager.AddToRoleAsync(ap, "Surgery");
             }

             var admin = new AppUser{
                  UserName = "admin@gfancy.nl",
                  Email = "admin@gfancy.nl",
                  Gender = "male",
                  PaidTill = new DateTime().AddYears(2250)};
             await manager.CreateAsync(admin, "Pa$$w0rd");
             await manager.AddToRoleAsync(admin, "Admin");
           


         } 
        }
