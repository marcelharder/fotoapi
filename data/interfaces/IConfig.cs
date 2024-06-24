namespace fotoservice.data.interfaces;

public interface IConfig
    {
        Task<List<String>> getCategories();
    }

