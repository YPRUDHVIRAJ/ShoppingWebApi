using Microsoft.AspNetCore.Mvc;
using ShoppingWebApi.EfCore;
using ShoppingWebApi.Models; // Ensure this exists and contains OrderModel and ProductModel
using ShoppingWebApi.Helpers; // Assuming DbHelper is in the Helpers namespace

namespace ShoppingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingApiController : ControllerBase
    {
        private readonly DbHelper _db;

        public ShoppingApiController(AppDbContext context) // Assuming AppDbContext is your EF Core context
        {
            _db = new DbHelper(context);
        }

        // GET: api/ShoppingApi/GetProducts
        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<Product> data = _db.GetProducts();

                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/ShoppingApi/GetProductById/{id}
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                Product data = _db.GetProductById(id);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/ShoppingApi/SaveOrder
        [HttpPost("SaveOrder")]
        public IActionResult SaveOrder([FromBody] Order model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/ShoppingApi/UpdateOrder
        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] Order model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/ShoppingApi/DeleteOrder/{id}
        [HttpDelete("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteOrder(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
