using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers {
    
    [Route("api/[controller]")]
    public class HelloWorldController: Controller {


        // 
        // GET: /HelloWorld/
        
        [HttpGet]
        public string Index() {
            return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        [HttpGet("welcome/{id?}")]
        public string Welcome(string name, int ID = 1) {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }
    }
}