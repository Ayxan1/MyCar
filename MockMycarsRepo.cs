using System.Collections.Generic;
using Mycars.Models;

namespace Mycars.Data
{
    public class MockMycarsRepo : IMycarsRepo
    {
        public void CreateBrand(Brands cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBrand(Brands cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Brands> GetAllBrands()
        {
            var brands = new List<Brands>
            {
                new Brands{Id=0, brand="Nissan", model="Patrol", year=2015},
                new Brands{Id=1, brand="Ford", model="Fusion", year=2018},
                new Brands{Id=2, brand="Kia", model="Stinger", year=2017}
            };

            return brands;
        }

        public Brands GetBrandById(int id)
        {
            return new Brands { Id = 0, brand = "Nissan", model = "Patrol", year = 2015 };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBrand(Brands cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
