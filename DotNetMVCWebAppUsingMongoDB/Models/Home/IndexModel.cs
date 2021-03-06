﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetMVCWebAppUsingMongoDB.Models.Home
{
    public class IndexModel
    {
        public List<Post> RecentPosts { get; set; }

        public List<TagProjection> Tags { get; set; }
    }
}