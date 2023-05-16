
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.Extensions.Hosting;
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
        private IWebHostEnvironment environment;
        public CarListRepository(ApplicationDbContext context,IWebHostEnvironment env)
        {
            this._context = context;
            this.environment = env;
        }

       
        public bool Add(CarListCreateDTO model,string fileResult,CarList CarItemToCreate)
        {
            try
            {
                _context.CarLists.Add(CarItemToCreate);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<CarList> GetAllCars()
        {
            return _context.CarLists.ToList();
        }
        public CarList GetCarById(int id)
        {

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
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
