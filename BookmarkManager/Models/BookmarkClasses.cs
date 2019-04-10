using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BookmarkManager.Models
{
    public class BookmarkClasses
    {
    }

    public class BookmarkClass1
    {
        [JsonProperty("bookmark")]
        public Bookmark Bookmark1 { get; set; }
        [JsonProperty("username")]
        public string Username1 { get; set; }
    }
}