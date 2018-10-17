

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.BindingTargets;

namespace SportsStore.Controllers{
    [Route("api/[controller]")]
    public class SupplierController : Controller {
        private StoreAppContext _context;
        public SupplierController(StoreAppContext context){
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Supplier> GetSuppliers(){
            return _context.Suppliers;
        }
        [HttpPost]
        [Route("addSupplier")]
        public IActionResult Create([FromBody] SupplierData supplierData){
            if(ModelState.IsValid){
                Supplier supplier = supplierData.Supplier;
                _context.Add(supplier);
                _context.SaveChanges();
                return Ok(supplier.SupplierId);
            }else{
                return BadRequest(ModelState);
            }
        }
    }
}