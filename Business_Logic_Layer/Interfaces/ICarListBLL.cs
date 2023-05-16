using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface ICarListBLL
    {
        ActionResult<List<CarList>> GetAll();

        ActionResult<CarList> GetCarById(int id);

        Task<bool> Add([FromForm] CarListCreateDTO model);

        bool Update(int id, [FromForm] CarListUpdateDTO model);

        bool RemoveCarById(int id);
    }
}
