using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Padmate.Allen.Repository;
using Padmate.Allen.Models;

namespace Padmate.Allen.Controllers
{
    //[Route("api/[controller]/[action]")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : Controller
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
        [HttpGet("getbyid/{id:int?}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult PostCreateaaaa([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return HttpBadRequest();
            }
            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { controller = "Todo", id = item.Key }, item);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="item">ITEM</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return HttpBadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }

            TodoItems.Update(item);
            return new NoContentResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TodoItems.Remove(id);
        }
    }
}
