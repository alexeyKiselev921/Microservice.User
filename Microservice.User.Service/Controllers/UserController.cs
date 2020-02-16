using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.User.Service.Models;
using Microservice.User.Service.Resolvers;
using Microservice.User.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservice.User.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _jsonSerializerSettings = new JsonSerializerSettings() { ContractResolver = new LowercaseContractResolver() };
        }

        [HttpGet]
        public Task<IEnumerable<UserModel>> Get()
        {
            return _userService.GetAll();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<string> Get(string id)
        {
            try
            {
                var user = await _userService.Get(id);
                if (user == null)
                {
                    return JsonConvert.SerializeObject("No user found");
                }

                return JsonConvert.SerializeObject(user, Formatting.Indented, _jsonSerializerSettings);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Username))
                    return BadRequest("Please enter user name");
                if (string.IsNullOrEmpty(model.Password))
                    return BadRequest("Please enter user password");
                if (!model.Email.Contains("@"))
                    return BadRequest("Please enter valid email");

                model.CreationDate = DateTime.Now;
                await _userService.Create(model);
                return Ok("User has been added successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Username))
                    return BadRequest("Please enter user name");
                if (!model.Email.Contains("@"))
                    return BadRequest("Please enter valid email");
                var result = await _userService.Update(model);
                if (result)
                {
                    return Ok("User has been updated successfully");
                }

                return BadRequest("No user found to update");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.Remove(id);
                return Ok("User has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("deleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _userService.RemoveAll();
                return Ok("All users has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
