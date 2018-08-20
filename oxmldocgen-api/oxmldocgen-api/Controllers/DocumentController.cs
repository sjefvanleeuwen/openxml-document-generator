using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml;
using Hangfire;

namespace oxmldocgen_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        /// <summary>
        /// Generates a document in openxml asynchronously.
        /// </summary>
        /// <returns>OpenXML document</returns>
        [HttpPost]
        [Route("Enqueue")]
        [Description("Generates a document in openxml asynchronously using hangfire. Returns a jobid for correlation.")]
        public string Enqueue(string html)
        {
            var client = new BackgroundJobServer();
            return BackgroundJob.Enqueue(() => Create(html));
        }

        /// <summary>
        /// Generates a document in openxml synchronously.
        /// </summary>
        /// <returns>OpenXML document</returns>
        [HttpPost]
        [Description("Generates a document in openxml.")]
        public ActionResult<byte[]> Create(string html)
        {
            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                            mainPart = package.AddMainDocumentPart();
                            new Document(new Body()).Save(mainPart);
                    }
                    HtmlConverter converter = new HtmlConverter(mainPart);
                    converter.ParseHtml(html);
                    mainPart.Document.Save();
                }
                return generatedDocument.ToArray();
            }
           
        }
    }
}
