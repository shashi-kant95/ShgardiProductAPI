using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Repository.Models;
using ShgardiProductAPI.Repository.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShgardiProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(DBContext dBContext, ILogger<ProductsController> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() //Returns all products from Product table, currently there are no limits set
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Products");
                return StatusCode(500, "Server Error: " + ex.Message);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id) // will return the product with given id from product table
        {
            try
            {
                // if (id == null) // id will never be null
                // {
                //     return BadRequest("Id is required");
                // }
                var prod = await _dbContext.Products.FindAsync(id); // FindAsync will search based on primary key id
                if (prod == null)
                {
                    _logger.LogInformation("No Product found with ID {Id} while fetching", id);
                    return NotFound("No Record Found");
                }
                return prod;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Product with ID {Id}", id);
                return StatusCode(500, "Server Error: " + ex.Message);
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] Product product) // update the Name, Desc, and Price
        {
            try
            {
                if (id != product.Id) // id needs to be same
                {
                    return BadRequest("Invalid Input");
                }
                var existingProduct = await _dbContext.Products.FindAsync(id);
                if (existingProduct == null) // No record found with given id
                {
                    _logger.LogInformation("No Product with ID {Id} while updating", id);
                    return NotFound($"Product with ID {id} not found.");
                }
                // Update properties
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                await _dbContext.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Product with ID {Id}", id);
                return StatusCode(500, "Server Error: " + ex.Message);
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product) // Add record to table, Id will be incremental
        {
            try
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                return CreatedAtRoute("GetProduct", new { id = product.Id }, product); // return a path to access newly added product
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Adding Product {val}", JsonSerializer.Serialize(product));
                return StatusCode(500, "Server Error: " + ex.Message);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) // Hard delete the record, It won't keep record in table
        {
            try
            {
                var prod = await _dbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (prod == null) // no record
                {
                    _logger.LogInformation("No Product with ID {Id} while deleting", id);
                    return NotFound("No Record Found");
                }
                _dbContext.Products.Remove(prod);
                await _dbContext.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Product {id}", id);
                return StatusCode(500, "Server Error: " + ex.Message);
            }
        }
    }

}

