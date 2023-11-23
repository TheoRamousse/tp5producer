using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Http;
using UserApi.Models;
using UserApi.Services;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "GetUser")]
        public IActionResult Get(string email)
        {
            var res = _service.GetUser(email);

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpGet("validate", Name = "Validate")]
        public IActionResult Validate([FromUri] string email, [FromUri] string urlValidation)
        {
            var res = _service.ValidateAccount(email, urlValidation);

            if (res == 1)
                return Ok();

            return BadRequest();
        }

        [HttpPost(Name = "AddUser")]
        public IActionResult Post([FromBody] object data)
        {
            User elementAsDto = null;
            try
            {
                _logger.LogInformation(data.ToString());
                elementAsDto = JsonConvert.DeserializeObject<User>(data.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Le corps de la requête est vide ou une date renseignée est invalide");
            }

            var result = _service.Insert(elementAsDto);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}