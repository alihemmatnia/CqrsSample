using ApplicationCore.DTOs.Post;
using Infrastructure;
using Infrastructure.Entities;
using MediatR;

namespace ApplicationCore.Services.PostServices.Command
{
    public class CreatePostCommand : IRequest<PostResponse>
    {
        public CreatePostDto CreatePostDto { get; set; }

        public CreatePostCommand(CreatePostDto createPostDto)
        {
            CreatePostDto = createPostDto;
        }
    }


    public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreatePostHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post(request.CreatePostDto.Title, request.CreatePostDto.Content);
            await _context.Posts.AddAsync(post, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new PostResponse() { Id = post.Id };
        }
    }
}
