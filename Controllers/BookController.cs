using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ConsumerWebApi.Models;

namespace ConsumerWebApi.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            var booklist = GetBooksfromApi();
            return View(booklist);
        }

        // GET: Book
        public ActionResult Create()
        {
            var booklist = GetBooksfromApi();
            return View(booklist);
        }




        private List<Book> GetBooksfromApi()
        {
            try
            {
                var booklist = new List<Book>();
                var client = new HttpClient();
                var getbooksdata = client.GetAsync("http://localhost:51198/api/Books").
                    ContinueWith(response =>
                    {
                        var result = response.Result;
                        var getBookData = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<Book>>();
                            readResult.Wait();
                            booklist = readResult.Result;
                        }
                    }

                );
                getbooksdata.Wait();
                return booklist;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}