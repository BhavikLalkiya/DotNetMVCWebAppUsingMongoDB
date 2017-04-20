using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetMVCWebAppUsingMongoDB.Models.Home
{
    public class NewCommentModel
    {
        [HiddenInput(DisplayValue = false)]
        public string PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }
    }

    public class NewCommentAPIModel
    {
        public string PostId { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
    }
}