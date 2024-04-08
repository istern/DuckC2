using DuckC2.Server.Abstractions.Factories;
using DuckC2.Server.Abstractions.Models;
using DuckC2.Server.Abstractions.Repositories;
using DuckC2.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Text.Json;

namespace DuckC2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListenerController(IListenerRepository listenerRepository, IEnumerable<IListenerFactory> listenerFactories) : ControllerBase
    {
        private readonly IListenerRepository repository = listenerRepository;
        private readonly IEnumerable<IListenerFactory> factories = listenerFactories;

        [HttpGet("GetTypes")]
        public IActionResult GetListenersTypes()
        {
            var types = factories.Select(x => x.Name);
            return Ok(types);
        }

        [HttpGet]
        public IActionResult GetListeners()
        {
            var listeners = repository.Get();
            return Ok(listeners);
        }

        [HttpGet("(name)")]
        public IActionResult GetListener(string name)
        {
            var listener = repository.Get(name);
            if (listener == null)
                return NotFound();
            return Ok(listener);
        }

        [HttpDelete("{name}")]
        public IActionResult StopListener(string name)
        {
            var listener = repository.Get(name);
            if (listener == null)
                return NotFound(name);
            listener.Stop();
            repository.Remove(listener);
            return NoContent();
        }


        [HttpPut]
        public IActionResult CreateListener(CreateListnerRequest request)
        {
            var factory = factories.FirstOrDefault(x => x.Name.Equals(request.TypeName, StringComparison.InvariantCultureIgnoreCase));
            if (factory == null)
                return NotFound();

            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            dynamic arguments = JsonSerializer.Deserialize<ExpandoObject>(request.Arguments);
            var listener = factory.Create(arguments);
            listener.Start();

            repository.Add(listener);

             var root = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            var path = $"{root}/{listener.Name}";
            return Ok(listener);
        }


    }
}
