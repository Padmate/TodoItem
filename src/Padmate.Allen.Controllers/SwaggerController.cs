using Microsoft.AspNet.Mvc;
using Padmate.Allen.Models;
using Padmate.Allen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padmate.Allen.Controllers
{
    [Route("api/[controller]")]
    public class SwaggerController:Controller
    {
        [FromServices]
        public ITodoRepository TodoItems { get; set; }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }
    }
}
