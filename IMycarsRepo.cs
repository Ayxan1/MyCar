using System.Collections.Generic;
using Mycars.Models;

namespace Mycars.Data
{
    public interface IMycarsRepo
    {
        bool SaveChanges();

        IEnumerable<Brands> GetAllBrands();
        Brands GetBrandById(int id);
        void CreateBrand(Brands cmd);
        void UpdateBrand(Brands cmd);
        void DeleteBrand(Brands cmd);
    }
}