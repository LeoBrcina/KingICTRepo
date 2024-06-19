using System.Text.Json;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace KingICTWebAPI.Service
{
    public class ProductService
    {
        //product service constructor that connects to our wanted http or https client, in this case dummy api and acts as a middleware between the 2 apis

        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        //get all products method from the original dummy api

        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var data = _httpClient.GetStringAsync($"https://dummyjson.com/products/").Result;   //first we store the result products json array 

                var products = JsonSerializer.Deserialize<Rootobject>(data);    //then we deserialize it into rootobjects and return it to later use them in our own api

                return products.products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                var data = _httpClient.GetStringAsync($"https://dummyjson.com/products/{id}").Result;   //first we store the result product json

                var product = JsonSerializer.Deserialize<Product>(data);    //then we deserialize it into a single product and return it for later use

                return product;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //get all products method from the original dummy api

        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var data = _httpClient.GetStringAsync($"https://dummyjson.com/users/").Result;   //first we store the result products json array 

                var users = JsonSerializer.Deserialize<Rootobject2>(data);    //then we deserialize it into rootobjects and return it to later use them in our own api

                return users.users;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
