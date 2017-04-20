using DotNetMVCWebAppUsingMongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace DotNetMVCWebAppUsingMongoDB.Services
{
    public static class BlogServices
    {
        public static Task<List<Post>> GetPosts()
        {
            var blogContext = new BlogContext();
            return blogContext.Posts.Find(x => true).SortByDescending(x => x.CreatedAtUtc).Limit(10).ToListAsync();
        }

        public static Task<List<TagProjection>> GetTags()
        {
            var blogContext = new BlogContext();
            return blogContext.Posts.Aggregate()
                .Project(x => new { _id = x.Id, Tags = x.Tags })
                .Unwind(x => x.Tags)
                .Group<TagProjection>("{ _id: '$Tags', Count: { $sum: 1 } }")
                .ToListAsync();
        }


        public static string AddPost(string Name, string Title, string Content, string Tags)
        {
            var blogContext = new BlogContext();
            var post = new Post
            {
                Author = Name,
                Title = Title,
                Content = Content,
                Tags = Tags.Split(' ', ',', ';'),
                CreatedAtUtc = DateTime.UtcNow,
                Comments = new List<Comment>()
            };

            blogContext.Posts.InsertOneAsync(post);
            return post.Id;
        }
        public static Task<Post> GetPostsById(string PostId)
        {
            var blogContext = new BlogContext();
            return blogContext.Posts.Find(x => x.Id == PostId).SingleOrDefaultAsync();
        }

        public static Task<List<Post>> GetPostsByTag(string tag)
        {

            var blogContext = new BlogContext();

            Expression<Func<Post, bool>> filter = x => true;

            if (tag != null)
            {
                filter = x => x.Tags.Contains(tag);
            }

            return blogContext.Posts.Find(filter)
                .SortByDescending(x => x.CreatedAtUtc)
                .ToListAsync();


        }


        public static void AddComment(string Name, string Content, string PostId)
        {
            var comment = new Comment
            {
                Author = Name,
                Content = Content,
                CreatedAtUtc = DateTime.UtcNow
            };

            var blogContext = new BlogContext();

            blogContext.Posts.UpdateOneAsync(
                x => x.Id == PostId,
                Builders<Post>.Update.Push(x => x.Comments, comment));
        }

        public static void CommentLike(int Index, string PostId)
        {
            var blogContext = new BlogContext();

            var fieldName = string.Format("Comments.{0}.Likes", Index);

            UpdateDefinition<Post> updateDef = Builders<Post>.Update.Inc(fieldName, 1);

            blogContext.Posts.UpdateOneAsync(x => x.Id == PostId, updateDef);
        }
    }
}