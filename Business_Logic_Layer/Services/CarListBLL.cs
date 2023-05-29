using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Business_Logic_Layer.Services
{
    public class CarListBLL: ICarListBLL
    {
        private readonly ICarListRepository _productRepo;
        private IWebHostEnvironment environment;
        private readonly ILogger<CarListBLL> _logger;
        public CarListBLL(ICarListRepository productRepo, IWebHostEnvironment env,ILogger<CarListBLL> logger)
        {

            _productRepo = productRepo;
            environment = env;
            _logger = logger;
        }
        public ActionResult<List<CarList>> GetAll()
        {

            _logger.LogInformation("Retrieving all cars from the database.");
            List<CarList> carFromDB = _productRepo.GetAllCars();
            return carFromDB;
        }

        public ActionResult<CarList> GetCarById(int id)
        {
            _logger.LogInformation("Attempting to retrieve car with ID {id}", id);
            var res = _productRepo.GetCarById(id);
            return res;
        }



        public async Task<bool> Add([FromForm] CarListCreateDTO model)
        {
            _logger.LogInformation("Starting the process of adding a new car.");
            string imageName = new string(Path.GetFileNameWithoutExtension(model.ImageFile.FileName).Take(15).ToArray()).Replace(' ', '-');
            imageName = imageName + Path.GetExtension(model.ImageFile.FileName);

            var imagePath = Path.Combine(environment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))

            {

                await model.ImageFile.CopyToAsync(fileStream);

            }
            _logger.LogInformation("Image file saved as: {imageName}", imageName);

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
            _logger.LogInformation("New car added successfully.");
            return productResult;


        }
        public bool Update(int id, [FromForm] CarListUpdateDTO model)
        {
         
            try
            {
                var result = _productRepo.Update(id, model).Result;
                _logger.LogInformation("Car update was made.");
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the car.");
                throw new ArgumentException("Error occurred while updating the car.", ex);
            }
        }
        public bool RemoveCarById(int id)
        {
         
            try
            {
                var result = _productRepo.RemoveCarById(id);
                if (result == true)
                {
                    _logger.LogInformation("The car with {id} was removed", id);
                }
                else
                {
                    _logger.LogError("There was no car with ID {id}", id);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the car with ID {id}", id);
                throw new ArgumentException("An error occurred while removing the car.", ex);
            }
        }
    }
}
