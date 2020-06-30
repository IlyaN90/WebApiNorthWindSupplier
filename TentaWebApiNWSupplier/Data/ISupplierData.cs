using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TentaWebApiNWSupplier.Models;

namespace TentaWebApiNWSupplier.Data
{
    public interface ISupplierData
    {
        Task<Supplier> GetSupplier(int id);
        void AddSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
        Task<bool> SaveChangesAsync();
        Task<Supplier[]> GetAllSuppliers();
    }
}