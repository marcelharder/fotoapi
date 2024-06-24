namespace fotoservice.api.data.interfaces;

public interface IUsers
{

      Task<List<AppUser>> GetUsers();
      Task<AppUser> GetChefsByHospital(int center_id);
      Task<AppUser> GetUser(int id);
      Task<AppUser> GetUserByMail(string email);
      Task<bool> GetUserLtk(int id);
      Task<bool> UpdatePayment(DateTime d, int id);
      Task<bool> SaveAll();
      void Update(AppUser p);
      void Delete<T>(T entity) where T : class;
}