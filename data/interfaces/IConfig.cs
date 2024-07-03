using System.ComponentModel;

namespace fotoservice.data.interfaces;

public interface IConfig
    {
        Task<List<CategoryDto>> getAllCategories();

        Task<List<CategoryDto>> getAllowedCategories();
    }

