using fotoservice.api.data.interfaces;
using Microsoft.AspNetCore.Identity;

namespace fotoservice.data.implementations;
public class UserRepo : IUsers
{
        private readonly UserManager<AppUser> _userManager;

    public UserRepo(UserManager<AppUser> userManager)
    {
            _userManager = userManager;
        
    }

    public void Delete<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetChefsByHospital(int center_id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetUser(int id)
    {
        throw new NotImplementedException();
    }

     public async Task<AppUser> GetUser(string email)
    {
         if (email != null) {
            var result = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            return result;
            }
            return null;
    }

    public Task<bool> GetUserLtk(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<AppUser>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAll()
    {
        throw new NotImplementedException();
    }

    public void Update(AppUser p)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePayment(DateTime d, int id)
    {
        throw new NotImplementedException();
    }
}