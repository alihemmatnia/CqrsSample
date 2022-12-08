using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime Created { get; private set; }

        public Post(string title, string content)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            Created = DateTime.Now;
        }
    }
}
