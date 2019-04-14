using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BookmarkManager.Models
{
    public class JsonHelper
    {
    }

    public class CreateJson
    {
        public CreateJson()
        {
        }
        public CreateJson(Bookmark bookmark, string username)
        {
            Bookmark = bookmark;
            Username = username;
        }

        [JsonProperty("bookmark")]
        public Bookmark Bookmark { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public class CreateUserJson
    {
        public CreateUserJson()
        {
        }
        public CreateUserJson(User user)
        {
            User = user;
        }
        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class EditJson
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("bookmark")]
        public Bookmark Bookmark { get; set; }

    }
}