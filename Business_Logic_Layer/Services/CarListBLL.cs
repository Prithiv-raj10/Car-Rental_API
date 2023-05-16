using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Business_Logic_Layer.Services
{
    public class CarListBLL: ICarListBLL
    {
        private readonly ICarListRepository _productRepo;
        private IWebHostEnvironment environment;
        public CarListBLL(ICarListRepository productRepo, IWebHostEnvironment env)
        {

            _productRepo = productRepo;
            environment = env;
        }
        public ActionResult<List<CarList>> GetAll()
        {
            List<CarList> carFromDB = _productRepo.GetAllCars();
            return carFromDB;
        }

        public ActionResult<CarList> GetCarById(int id)
        {
            var res = _productRepo.GetCarById(id);
            return res;
        }



        public async Task<bool> Add([FromForm] CarListCreateDTO model)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(model.ImageFile.FileName).Take(15).ToArray()).Replace(' ', '-');
            imageName = imageName + Path.GetExtension(model.ImageFile.FileName);

            var imagePath = Path.Combine(environment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))

            {

                await model.ImageFile.CopyToAsync(fileStream);

            }

            string fileResult = imageName;

            CarList CarItemToCreate = new()

            {

                Name = model.Name,

                Rent = model.Rent,

                Category = model.Category,

                SpecialTag = model.SpecialTag,

                Description = model.Description,

                Image = fileResult

            };
            var productResult = _productRepo.Add(model, fileResult, CarItemToCreate);
            return productResult;


        }
        public bool Update(int id, [FromForm] CarListUpdateDTO model)
        {
            var result = _productRepo.Update(id, model).Result;
            return result;
        }
        public bool RemoveCarById(int id)
        {
            var result = _productRepo.RemoveCarById(id);
            return result;
        }
    }
}
