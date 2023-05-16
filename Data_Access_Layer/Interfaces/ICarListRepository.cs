using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ICarListRepository
    {
        bool Add(CarListCreateDTO model, string fileResult,CarList CarItemToCreate);
        List<CarList> GetAllCars();

        CarList GetCarById(int id);

        bool RemoveCarById(int id);

        Task<bool> Update(int id, [FromForm] CarListUpdateDTO model);

        bool DeleteImage(string imageFileName);

    }

}
