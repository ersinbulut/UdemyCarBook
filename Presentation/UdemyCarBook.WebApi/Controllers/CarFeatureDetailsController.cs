using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediator.Commands.CarFeatureCommands;
using UdemyCarBook.Application.Features.Mediator.Handlers.CarFeatureHandlers;
using UdemyCarBook.Application.Features.Mediator.Handlers.FeatureHandlers;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeatureDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarFeatureDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarFeatureByCarCommand command)
        {
            command.Available = true;
            await _mediator.Send(command);
            return Ok(new { message = "Özellik başarıyla eklendi." });
        }
    }
}
