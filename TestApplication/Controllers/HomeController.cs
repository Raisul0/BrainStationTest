using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        Database context = new Database();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public string GetPostList(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            string test = string.Empty;
            sSearch = sSearch.ToLower();

            int totalRecord = context.Post.Count();
            var posts = new List<Post>();

            if (!string.IsNullOrEmpty(sSearch))
                posts = context.Post.Where(a =>  a.Text.ToLower().Contains(sSearch) || a.Username.ToLower().Contains(sSearch)  ).OrderBy(a => a.Id).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            else
                posts = context.Post.OrderBy(a => a.Id).Skip(iDisplayStart).Take(iDisplayLength).ToList();


            var result = (from p in posts
                          select new Demo
                          {
                              S = p.Id,
                              A = "<span style='color:blue'>" +p.Text + "</span>",
                              B = "<span style='color:blue'>" + p.Username + "</span>",
                              C = "<span style='color:blue'>" + p.PostDate.ToLocalTime().ToString("dd/MM/yyyy") + "</span>",
                              D = "<span style='color:blue'>" + context.Comment.Count(x=>x.PostId == p.Id).ToString() + " Comment(s)" + "</span>",
                          }
                        ).ToList();

            var comments = (from c in context.Comment.ToList()
                            join p in posts
                            on c.PostId equals p.Id
                            select new Demo
                            {
                                S = c.PostId,
                                A = "&nbsp;&nbsp;&nbsp;&nbsp;" +c.Text,
                                B = c.Username,
                                C = c.CommentDate.ToLocalTime().ToString("dd/MM/yyyy"),
                                D = "&uarr; " +c.UpVote + " &darr; " + c.DownVote,
                            }
                           ).ToList();

            result.AddRange(comments);
            result = result.OrderBy(x => x.S).ToList();
           

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        public class Demo
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public int S { get; set; }
        }
    }
}
