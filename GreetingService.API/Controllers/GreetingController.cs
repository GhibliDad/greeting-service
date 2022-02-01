using GreetingService.API.Authentication;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreetingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BasicAuth]
    public class GreetingController : ControllerBase
    {
        //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        //public class BasicAuthAttribute : TypeFilterAttribute
        //{
        //    public BasicAuthAttribute(string realm = @"My Realm") : base(typeof(BasicAuthFilter))
        //    {
        //        Arguments = new object[] { realm };
        //    }
        //}

        private readonly IGreetingRepository _greetingRepository;

        public GreetingController(IGreetingRepository greetingRepository)
        {
            _greetingRepository = greetingRepository;
        }

        // GET: api/<GreetingController>
        [HttpGet]
        public IEnumerable<Greeting> Get()
        {
            return _greetingRepository.Get();
            //return new Greeting[] 
            //{
            //    new Greeting()
            //    {
            //    Timestamp = DateTime.Now,
            //        From = "Towa",
            //        To = "Help Desk",
            //        Message = "Hello!",
            //    },
            //    new Greeting()
            //    {
            //        Timestamp = DateTime.Now,
            //        From = "Towa",
            //        To = "Help Desk",
            //        Message = "Hello! I need help!!",
            //    },
            //};
        }

        // GET api/<GreetingController>/5
        [HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Greeting))]        //when we return IActionResult instead of Greeting, there is no way for swagger to know what the return type is, we need to explicitly state what it will return
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult Get(Guid id)
        //{
        //    var greeting = _greetingRepository.Get(id);
        //    return Ok();
        //    if (greeting == null)
        //    {
        //        return NotFound();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        public Greeting Get(Guid id)
        {
            return _greetingRepository.Get(id);
        }

        // POST api/<GreetingController>
        [HttpPost]
        public void Post([FromBody] Greeting greeting)
        {
            _greetingRepository.Create(greeting);
        }

        // PUT api/<GreetingController>/5
        [HttpPut]
        public void Put([FromBody] Greeting greeting)
        {
            _greetingRepository.Update(greeting);
        }

        // DELETE api/<GreetingController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _greetingRepository.Delete(id);
        }

        // DELETE api/<GreetingController>/5
        [HttpDelete]
        public void DeleteAll()
        {
            _greetingRepository.DeleteAll();
        }
    }
}
