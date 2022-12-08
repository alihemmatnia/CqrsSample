using ApplicationCore.DTOs.Post;
using ApplicationCore.Services.PostServices.Command;
using ApplicationCore.Services.PostServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto dto)
        {
            var result = await _mediator.Send(new CreatePostCommand(dto));
            return Ok(result.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetPostRequest(id));
            return Ok(result);
        }
    }
}
