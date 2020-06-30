using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TentaWebApiNWSupplier.BasicAuthentication;
using TentaWebApiNWSupplier.CustomErrors;
using TentaWebApiNWSupplier.Data;
using TentaWebApiNWSupplier.Models;
using TentaWebApiNWSupplier.ViewModels;

//API controller: Api/SuppliersController  

//Authentication: BasicAuthentication/BasicAuthenticationAttribute
//Username == "IliNag-09" && Password == "n@5?*-68[FK_EMP"

//CustomErrors/NotFoundTextPlainActionResult

//MappingProfile for Supplier: Data/SupplierMappingProfile
//MappingModel for supplier: ViewModels/SupplierMapperModel

//SQLSupplierData: Data/SupplierData
//Data/ISupplierData

namespace TentaWebApiNWSupplier.Api
{
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : ApiController
    {
        private readonly ISupplierData _repo;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierData repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get All Suppliers
        //returns: everything||InternalServerError(ex)(for debugging)
        [BasicAuthentication]
        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllSuppliers();

                var mapperResult = _mapper.Map<IEnumerable<SupplierMapperModel>>(result);
                
                return Ok(mapperResult);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Get Supplier by ID
        //returns: supplier||Custom NotFound||InternalServerError(ex)(for debugging)
        [BasicAuthentication]
        [Route("{number}", Name = "GetSupplier")]
        public async Task<IHttpActionResult> Get(int number)
        {
            try
            {
                var result =  await _repo.GetSupplier(number);
                if (result == null)
                {
                    return new NotFoundTextPlainActionResult("There is no such supplier(Get)", Request);
                    //return NotFound();
                }
                return Ok(_mapper.Map<SupplierMapperModel>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Post Supplier
        //returns: ID||BadRequest(ModelState)||InternalServerError(ex)(for debugging)
        [BasicAuthentication]
        [HttpPost]
        [Route()]
        public async Task<IHttpActionResult> Post(SupplierMapperModel model)
        {
            try
            {
                if (await _repo.GetSupplier(model.SupplierID) != null)
                {
                    ModelState.AddModelError("Supplier", "Supplier in use");
                }
                if (ModelState.IsValid)
                {
                    var supplier = _mapper.Map<Supplier>(model);
                    _repo.AddSupplier(supplier);
                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok(new { supplier.SupplierID });
                        //var newModel = _mapper.Map<SupplierMapperModel>(supplier);
                        //return CreatedAtRoute("GetSupplier", new { number = newModel.SupplierID }, newModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        //Edit Supplier by ID
        //returns: ID||Custom NotFound||InternalServerError(ex)(for debugging)
        [BasicAuthentication]
        [Route("{number}")]
        public async Task<IHttpActionResult> Put(int number, SupplierMapperModel model)
        {
            try
            {
                var supplier = await _repo.GetSupplier(number);
                if (supplier == null)
                {
                    return new NotFoundTextPlainActionResult("There is no such supplier(Edit)", Request);
                    //return NotFound();
                }
                model.SupplierID = supplier.SupplierID;
                _mapper.Map(model, supplier);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(new { supplier.SupplierID });
                    //return Ok(_mapper.Map<SupplierMapperModel>(supplier));
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Delete Supplier by ID
        //returns: ID||Custom NotFound||InternalServerError(ex)(for debugging)
        [BasicAuthentication]
        [Route("{number}")]
        public async Task<IHttpActionResult> Delete(int number)
        {
            try
            {
                var supplier = await _repo.GetSupplier(number);
                if (supplier == null)
                {
                    return new NotFoundTextPlainActionResult("There is no such supplier(Delete)", Request);
                    //return NotFound();
                }
                _repo.DeleteSupplier(supplier);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(new {supplier.SupplierID});
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}