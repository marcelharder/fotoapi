
namespace fotoservice.data;
public class UserRepo : IUsers
{
    

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