using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using System.IO;

namespace xmldocgen_api_tests
{
    [TestClass]
    public class DocumentControllerTests
    {
        [TestMethod]
        public void can_convert_html_to_openxml_document()
        {
            var doc = File.ReadAllText("./data/letter.html");
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
                    converter.ParseHtml(doc);
                    mainPart.Document.Save();
                }
                // visually test this.
                File.WriteAllBytes(@"./test.docx", generatedDocument.ToArray());
            }
        }    
    }
}
