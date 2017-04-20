using DotNetMVCWebAppUsingMongoDB.Models.Home;
using DotNetMVCWebAppUsingMongoDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DotNetMVCWebAppUsingMongoDB.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var recentPosts = await BlogServices.GetPosts();

            var tags = await BlogServices.GetTags();

            var model = new IndexModel
            {
                RecentPosts = recentPosts,
                Tags = tags
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult NewPost()
        {
            return View(new NewPostModel());
        }

        [HttpPost]
        public async Task<ActionResult> NewPost(NewPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var postId = BlogServices.AddPost(User.Identity.Name, model.Title, model.Content, model.Tags);

            return RedirectToAction("Post", new { id = postId });
        }

        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var post = await BlogServices.GetPostsById(id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }

            var model = new PostModel
            {
                Post = post,
                NewComment = new NewCommentModel
                {
                    PostId = id
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var posts = await BlogServices.GetPostsByTag(tag);
            return View(posts);
        }

        [HttpPost]
        public async Task<ActionResult> NewComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = model.PostId });
            }

            BlogServices.AddComment(User.Identity.Name, model.Content, model.PostId);

            return RedirectToAction("Post", new { id = model.PostId });
        }

        [HttpPost]
        public async Task<ActionResult> CommentLike(CommentLikeModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = model.PostId });
            }

            BlogServices.CommentLike(model.Index, model.PostId);

            return RedirectToAction("Post", new { id = model.PostId });
        }

    }
}
