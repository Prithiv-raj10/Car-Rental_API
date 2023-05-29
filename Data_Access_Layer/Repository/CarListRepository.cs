
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using log4net.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{

    public class CarListRepository : ICarListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarListRepository> _logger;
        private IWebHostEnvironment environment;
        public CarListRepository(ApplicationDbContext context,IWebHostEnvironment env,ILogger<CarListRepository> logger)
        {
            this._context = context;
            _logger = logger;
            this.environment = env;
        }

       
        public bool Add(CarListCreateDTO model,string fileResult,CarList CarItemToCreate)
        {
            try
            {
                _context.CarLists.Add(CarItemToCreate);
                _logger.LogInformation("Added car details to database");
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured");
                return false;
            }
        }

        public List<CarList> GetAllCars()
        {
            _logger.LogInformation("Getting all car from database");
            return _context.CarLists.ToList();
        }
        public CarList GetCarById(int id)
        {
            _logger.LogInformation("Getting car with id {id}", id);
            return _context.CarLists.FirstOrDefault(x => x.Id == id);
        }

        public bool RemoveCarById(int id)
        {
            CarList car = _context.CarLists.FirstOrDefault(y => y.Id == id);
            if(car!=null)
            {
                 DeleteImage(car.Image);
                _context.CarLists.Remove(car);
                
                _context.SaveChanges();
                _logger.LogInformation("Removed car");
                return true;
            }
            return false;
        }

        public async Task<bool> Update(int id, [FromForm] CarListUpdateDTO model)
        {
            CarList car = await _context.CarLists.FindAsync(id);
            if(car==null)
            {
                return false;
            }

            car.Name = (model.Name!=null?model.Name:car.Name);

            car.Rent = (model.Rent!=null?model.Rent:car.Rent);

            car.Category = (model.Category != null ? model.Category : car.Category);

            car.SpecialTag = (model.SpecialTag != null ? model.SpecialTag : car.SpecialTag);

            car.Description = (model.Description!=null ? model.Description:car.Description);

            _context.CarLists.Update(car);
            _context.SaveChanges();

            return true;


        }
  
        public bool DeleteImage(string imageFileName)
        {
            try
            {

                var path = Path.Combine(environment.ContentRootPath, "Images", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    _logger.LogInformation("Car image deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting car image");
                return false;
            }
        }


    }
}
