using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MemoGlobalTest.Modles;
using MemoGlobalTest.Services.Reqres;
using System.Net;
using MemoGlobalTest.Services;
using MemoGlobalTest.Data.Entities.MetadataConfiguration;
using MemoGlobalTest.Interface;
using MemoGlobalTest.Data.Entities;

namespace MemoGlobalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Entities _dbContext;
        private readonly IHttpClientService _client;
        private readonly ILogger<UserController> _logger;

        public UserController(Entities dbContext, IHttpClientService client, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _client = client;
            _logger = logger;
        }

        [HttpGet("/getUsers/{page}")]
        public async Task<IActionResult> GetUsers(int page)
        {
            try
            {
                var response = await _client.Get($"api/users?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ReqresListUsersResponse>(json);
                    UserCache.AddUsers(res.data, _dbContext);
                    return Ok(res);
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return NotFound();
                        case HttpStatusCode.BadRequest:
                            return BadRequest();
                        case HttpStatusCode.Unauthorized:
                            return Unauthorized();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"get users endpoint failed",ex);
                throw new Exception("get users failed", ex);
            }
        }

        [HttpGet("/getUser/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var user = UserCache.GetUser(userId, _dbContext);
                if (user != null)
                {
                    return Ok(user);
                }

                var response = await _client.Get($"api/users/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ReqresResponse>(json);

                    UserCache.AddUser(res.data, _dbContext);

                    return Ok(res.data);
                }
                else
                { 
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            // code block
                            return NotFound($"User with id {userId} dosen't exists");
                        case HttpStatusCode.BadRequest:
                            return BadRequest();
                        case HttpStatusCode.Unauthorized:
                            return Unauthorized();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError);
                            
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"get User {userId} failed", ex);
                throw new Exception($"get User {userId} failed", ex);
            }
        }


        [HttpPost("/createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDetails newUser)
        {
            if (newUser.email == null ||
                newUser.last_name == null ||
                newUser.first_name == null)
            {
                return BadRequest($"Missing Details in request, pls send email, last name and first name");
            }

            if (!Validation.EmailIsValid(newUser.email))
            {
                return BadRequest($"Invalid email");
            }
                
            try
            {
                var response = await _client.Post($"api/users", newUser);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ReqresUser>(json);

                    UserCache.AddUser(res, _dbContext);

                    return Ok(res);
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            // code block
                            return NotFound();
                        case HttpStatusCode.BadRequest:
                            return BadRequest();
                        case HttpStatusCode.Unauthorized:
                            return Unauthorized();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create user failed", ex);
                throw new Exception("Create user failed", ex);
            }
        }

        [HttpPut("/updateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDetails data)
        {
            if (data.email == null ||
                data.last_name == null ||
                data.first_name == null)
            {
                return BadRequest($"Missing Details in request, pls send email, last name and first name");
            }

            if (!Validation.EmailIsValid(data.email))
            {
                return BadRequest($"Invalid email");
            }

            var res = await _client.Get($"api/users/{userId}");

            if (!res.IsSuccessStatusCode) 
            {
                return BadRequest($"user {userId} dosent exsits");
            }

            try
            {
                var response = await _client.Put($"api/users/{userId}", data);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<ReqresPutResponse>(json);
                    UserCache.UpdateUser(userId, data, _dbContext);

                    return Ok(user);
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return NotFound();
                        case HttpStatusCode.BadRequest:
                            return BadRequest();
                        case HttpStatusCode.Unauthorized:
                            return Unauthorized();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update User {userId} failed", ex);
                throw new Exception($"Update User {userId} failed", ex);
            }
        }

        [HttpDelete("/deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var client = new ReqresClient();

            try
            {
                var response = await client.Delete($"api/users/{userId}");
                
                if (response.IsSuccessStatusCode)
                {
                    UserCache.DeleteUser(userId, _dbContext);
                    return NoContent();
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return NotFound();
                        case HttpStatusCode.BadRequest:
                            return BadRequest();
                        case HttpStatusCode.Unauthorized:
                            return Unauthorized();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"delete User {userId} failed", ex);
                throw new Exception($"delete User {userId} failed", ex);
            }
        }
    }
}
