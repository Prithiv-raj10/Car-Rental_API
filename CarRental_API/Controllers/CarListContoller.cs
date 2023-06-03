using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CarRental_API.Controllers
{
    [Route("api/CarList")]
    [ApiController]
    public class CarListController : ControllerBase
    {
      
        private readonly ICarListBLL _product;
        public CarListController(ICarListBLL product)
        {
        
            this._product = product;
        }
        [HttpGet]
        public ActionResult<List<CarList>> GetAll()
        {
            var status = new Status();
            var res = _product.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public ActionResult<CarList> GetById(int id)
        {
            var status = new Status();
            var res = _product.GetCarById(id);
            if (res!=null)
            {
                return Ok(res);
            }
            else
            {
                status.StatusCode = (int)HttpStatusCode.NotFound;
                status.Message = "No car id exists";
                return Ok(status);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add([FromForm] CarListCreateDTO model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = (int)HttpStatusCode.BadRequest;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.ImageFile != null)
            {
                var productResult = _product.Add(model).Result;
                if (productResult)
                {
                    status.StatusCode = (int)HttpStatusCode.OK;
                    status.Message = "Added successfully";
                }
                else
                {
                    status.StatusCode = (int)HttpStatusCode.BadRequest;
                    status.Message = "Error on adding image of car";

                }
            }
            return Ok(status);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id,[FromForm] CarListUpdateDTO model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = (int)HttpStatusCode.BadRequest;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
                var productResult = _product.Update(id,model);
                if (productResult)
                {
                    status.StatusCode = (int)HttpStatusCode.OK;
                    status.Message = "Updated successfully";
                }
                else
                {
                    status.StatusCode = (int)HttpStatusCode.BadRequest;
                    status.Message = "Error on updating car";

                }
                return Ok(status);
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var result = _product.RemoveCarById(id);
            var status = new Status();
            if (result==false)
            {
                status.StatusCode = (int)HttpStatusCode.BadRequest;
                status.Message = "Error on deleting car";
            }
            else
            {
                status.StatusCode = (int)HttpStatusCode.OK;
                status.Message = "Deleted successfully";
                
            }
            return Ok(status);
        }

        }
    }
