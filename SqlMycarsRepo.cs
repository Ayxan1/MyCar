using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Mycars.Models;

namespace Mycars.Data
{
    public class SqlMycarsRepo : IMycarsRepo
    {
        private readonly MycarsContext _context;

        public SqlMycarsRepo(MycarsContext context)
        {
            _context = context;
        }

        public void CreateBrand(Brands cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Brands.Add(cmd);
        }

        public void DeleteBrand(Brands cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Brands.Remove(cmd);
        }

        public IEnumerable<Brands> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public Brands GetBrandById(int id)
        {
            return _context.Brands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateBrand(Brands cmd)
        {
            //Nothing
        }
    }
}