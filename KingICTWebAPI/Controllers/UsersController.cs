using BL.Models;
using KingICTWebAPI.DTOModels;
using KingICTWebAPI.Security;
using KingICTWebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KingICTWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        //constructor for the controller that uses product service that acts as a middleware from the dummy api
        //and gets all the needed product information in json format

        private readonly ProductService _productService;
        private readonly IConfiguration _configuration;
        public UsersController(ProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        //get all users method

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                var result = _productService.GetAllUsers();  //first we get all the users from the service through the middleware
                var mappedUsers = result.Value.Select(user => new UserDTO  //then we map it to a userdto to evade using unnecesary info but we also fetch username and password to show that login works
                {
                    firstName = user.firstName,
                    lastName = user.lastName,
                    username = user.username,
                    password = user.password
                }).ToList();

                return Ok(mappedUsers);
            }
            catch (Exception ex)
            {
                return NotFound("Users could not be retrieved");
            }
        }

        //simple login method that gets usernames and passwords from the dummy api and then authenticates if the username and pass typed are the same
        //if they are, a jwt token is provided, else an error occurs

        [HttpPost("Login")]
        public ActionResult Login(UserLoginDto loginDto)
        {
            try
            {
                var genericLoginFail = "Incorrect username or password";    //first we define a generic error

                //then we try to get a user from the service
                var existingUser = _productService.GetAllUsers().Value.FirstOrDefault(u => u.username == loginDto.username);
                if (existingUser == null)
                    return BadRequest(genericLoginFail);

                //check if pass is the same
                var pass = existingUser.password;
                if(pass != loginDto.password)
                    return BadRequest(genericLoginFail);

                // Create and provide JWT token
                var secureKey = _configuration["JWT:SecureKey"];

                var serializedToken =
                    JwtTokenProvider.CreateToken(
                        secureKey,
                        120,
                        loginDto.username);

                return Ok(serializedToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
