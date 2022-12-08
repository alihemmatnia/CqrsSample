using ApplicationCore.DTOs.Post;
using Infrastructure;
using MediatR;

namespace ApplicationCore.Services.PostServices.Queries
{
    public class GetPostRequest : IRequest<GetPostDto>
    {
        public Guid PostId { get; set; }

        public GetPostRequest(Guid id)
        {
            PostId = id;
        }

    }

    public class GetPostQuery : IRequestHandler<GetPostRequest, GetPostDto>
    {
        private readonly ApplicationDbContext _context;

        public GetPostQuery(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetPostDto> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Posts.FindAsync(request.PostId);
            if (result is not null)
                return new GetPostDto()
                {
                    Content = result.Content,
                    DateTime = result.Created,
                    Title = result.Title
                };
            return new GetPostDto();
        }
    }
}
