using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace oxmldocgen_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        /// <summary>
        /// Generates a document in openxml.
        /// </summary>
        /// <returns>OpenXML document</returns>
        [HttpPost]
        [Description("Generates a document in openxml.")]
        public ActionResult<string> Create([FromBody] string html)
        {
            return "generated open xml document, placeholder.";
        }
    }
}
