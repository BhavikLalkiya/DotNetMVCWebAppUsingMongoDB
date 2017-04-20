using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetMVCWebAppUsingMongoDB.Models.Home
{
    public class PostModel
    {
        public Post Post { get; set; }

        public NewCommentModel NewComment { get; set; }
    }
}