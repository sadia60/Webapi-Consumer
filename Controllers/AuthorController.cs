using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ConsumerWebApi.Models;

namespace ConsumerWebApi.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            var authorlist = GetBooksfromApi();
            return View(authorlist);
        }
        private List<Author> GetBooksfromApi()
        {
            try
            {
                var autherlist = new List<Author>();
                var client = new HttpClient();
                var getbooksdata = client.GetAsync("http://localhost:51198/api/Authors").
                    ContinueWith(response =>
                    {
                        var result = response.Result;
                        var getBookData = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<Author>>();
                            readResult.Wait();
                            autherlist = readResult.Result;
                        }
                    }

                );
                getbooksdata.Wait();
                return autherlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}