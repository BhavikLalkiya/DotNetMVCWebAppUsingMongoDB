using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetMVCWebAppUsingMongoDB.Models
{
    public class TagProjection
    {
        [BsonElement("_id")]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}