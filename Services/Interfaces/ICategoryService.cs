using Services.DTO;
using System.Collections.Generic;


namespace Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDTO> Get();
        CategoryDTO GetById(int id);
        bool Save(CategoryDTO categoryDTO);

        bool Update(CategoryDTO categoryDTO);
        bool Delete(int id);
    }
}
