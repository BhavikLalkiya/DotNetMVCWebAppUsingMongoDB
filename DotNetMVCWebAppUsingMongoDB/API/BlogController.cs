using DotNetMVCWebAppUsingMongoDB.Models;
using DotNetMVCWebAppUsingMongoDB.Models.Home;
using DotNetMVCWebAppUsingMongoDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
//using System.Web.Mvc;

namespace DotNetMVCWebAppUsingMongoDB.API
{
    public class BlogController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public HttpResponseMessage GetPost()
        {
            var recentPosts = BlogServices.GetPosts().Result;

            var tags = BlogServices.GetTags().Result;

            var model = new IndexModel
            {
                RecentPosts = recentPosts.ToList(),
                Tags = tags.ToList()
            };
            return Request.CreateResponse(HttpStatusCode.OK, new { Data = model, Status = HttpStatusCode.OK });
        }

    
        [HttpPost]
        public HttpResponseMessage NewPost(PostData objData)
        {
            var postId = BlogServices.AddPost(objData.Name, objData.Title, objData.Content, objData.Tags);
            return Request.CreateResponse(HttpStatusCode.OK, new { Data = postId });
        }


        [HttpGet]
        public HttpResponseMessage GetPostById(string id)
        {
            var post = BlogServices.GetPostsById(id).Result;

            if (post == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, new { Data = "No post", Status = HttpStatusCode.NoContent });
            }

            var model = new PostModel
            {
                Post = post,
                NewComment = new NewCommentModel
                {
                    PostId = id
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, new { Data = model, Status = HttpStatusCode.OK });
        }

        [HttpGet]
        public HttpResponseMessage GetPostBytag(string id = null)
        {
            var post = BlogServices.GetPostsByTag(id).Result;

            return Request.CreateResponse(HttpStatusCode.OK, new { Data = post, Status = HttpStatusCode.OK });
        }


        [HttpPost]
        public HttpResponseMessage NewComment(NewCommentAPIModel objData)
        {
            BlogServices.AddComment(objData.Name, objData.Content, objData.PostId);
            return Request.CreateResponse(HttpStatusCode.OK, new { Data = objData.PostId });
        }


        [HttpPost]
        public HttpResponseMessage CommentLike(CommentLikeModel objData)
        {
            BlogServices.CommentLike(objData.Index, objData.PostId);
            return Request.CreateResponse(HttpStatusCode.OK, new { Data = objData.PostId });
        }

    }
}