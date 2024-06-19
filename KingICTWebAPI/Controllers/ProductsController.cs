using BL.Models;
using KingICTWebAPI.DTOModels;
using KingICTWebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace KingICTWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        //constructor for the controller that uses product service that acts as a middleware from the dummy api
        //and gets all the needed product information in json format

        private readonly ProductService _productService;  
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        //get all products method

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var result = _productService.GetAllProducts();  //first we get all the products from the service through the middleware
                var mappedProducts = result.Value.Select(product => new ProductDTO  //then we map it to a productdto to evade using unnecesary info
                {
                    id = product.id,
                    title = product.title,
                    description = product.description,
                    category = product.category,
                    price = product.price,
                    images = product.images
                }).ToList();

                return Ok(mappedProducts);
            }
            catch (Exception ex)
            { 
                return NotFound("Products could not be retrieved");
            }
        }

        //get method for a single product by its id

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            try
            {
                var result = _productService.GetProductById(id);    //first we get the product through middleware
                var mappedProduct = new ProductDTO  //then we again map it to a data transfer object or dto
                {
                    id = result.Value.id,
                    title = result.Value.title,
                    description = result.Value.description,
                    category = result.Value.category,
                    price = result.Value.price,
                    images = result.Value.images,
                };

                if (mappedProduct == null)
                {
                    return NotFound($"Product with id {id} not found.");
                }

                return Ok(mappedProduct);
            }
            catch (Exception ex)
            {
                return NotFound($"Product with id {id} not found.");
            }
        }

        //filter method that allows the user to filter products through categories and price ranges

        [HttpGet("Filter")]
        public ActionResult<IEnumerable<Product>> FilterProducts([FromQuery] string category, [FromQuery] string priceRange)
        {
            try
            {
                var products = _productService.GetAllProducts();    //fist we get all products then we split the users price range string and then find filtered products 
                var prices = priceRange.Split('-').Select(decimal.Parse).ToList();
                var filteredProducts = products.Value.Where(p => p.category.ToLower() == category.ToLower() && p.price >= prices[0] && p.price <= prices[1]);

                if(filteredProducts == null)
                {
                    return NotFound("No products with said filter were found.");
                }

                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //search method that allows the user to search products by title

        [HttpGet("Search")]
        public ActionResult<IEnumerable<Product>> SearchProducts([FromQuery] string title)
        {
            try
            {
                var products = _productService.GetAllProducts();    //first get all products
                var searchedProducts = products.Value.Where(p => p.title.Contains(title, StringComparison.OrdinalIgnoreCase));  //return products with the same or similiar name

                if(searchedProducts == null)
                {
                return NotFound("No products with said title were found.");
                }

                return Ok(searchedProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
