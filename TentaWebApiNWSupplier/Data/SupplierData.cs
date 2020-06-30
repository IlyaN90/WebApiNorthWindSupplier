using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TentaWebApiNWSupplier.Models;

namespace TentaWebApiNWSupplier.Data
{
    public class SupplierData : ISupplierData
    {
        private readonly NorthWindDataBaseContext _db;

        public SupplierData(NorthWindDataBaseContext db)
        {
            _db = db;
        }

        public void AddSupplier(Supplier supplier)
        {

            _db.Suppliers.Add(supplier);
        }

        public void DeleteSupplier(Supplier supplier)
        {
            _db.Suppliers.Remove(supplier);
        }

        public async Task <Supplier> GetSupplier(int id)
        {
            var result = await _db.Suppliers.FindAsync(id);
            return result;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }

        public async Task<Supplier[]> GetAllSuppliers()
        {
            IQueryable<Supplier> query =  _db.Suppliers
                .OrderBy(t => t.SupplierID);
            return await query.ToArrayAsync();
        }
    }
}