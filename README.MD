# OpenXML Document Generator
![travis build](https://travis-ci.com/sjefvanleeuwen/openxml-document-generator.svg?branch=master)

Generates documents from html into openxml standard.

The goal of this generator is to improve efficiency in the UX process.

* Use an open standard XML
* Use open standard HTML/CSS
* Generate all document types from one central point
    * Web site
    * Print formats
* Databind from one central point to generate these documents
* Showcase the document process flow in BPMN Camunda tooling using ZDS.

More information on ZDS can be found at: 
https://github.com/VNG-Realisatie/gemma-zaken

## Status of this project

The openAPI has a create document function and an /enqueue function, which uses hangfire for queing jobs in case you need to asynchronously generate documents. https://www.hangfire.io/

The default implementation is currently set to : LiteDB. LiteDB can be found at: https://github.com/mbdavid/LiteDB

more about hangfire can be found at: 

Currently there's a "unit" test in place which generates a docx openxml document and showes that images are resolved in that document.

```csharp

        public void can_convert_html_to_openxml_document()
        {
            var doc = File.ReadAllText(@".\data\letter.html");
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
                File.WriteAllBytes(@".\test.docx", generatedDocument.ToArray());
            }
        }    
    }
```

More information on the camunda bpmn project (samples) can be found at:
https://github.com/sjefvanleeuwen/camunda-process-examples

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

* .NET core https://github.com/dotnet/core/releases
* nuget command line tools available at https://www.nuget.org/downloads

### Building Docker Images

```
docker-compose up --build -d
```

The API should now be reachable from:

* [SWAGGER](http://localhost:5080/swagger) - Swagger API documentation page
* [REDOC](http://localhost:5080/redoc) - Redoc API documentation page

## Built With

* [VSCODE](https://code.visualstudio.com/) - The IDE used
* [DOCKER](https://www.docker.com/) - Build, Ship, and Run Any App, Anywhere

## Contributing

Pull requests are accepted

## Authors

* **Sjef van Leeuwen** - *Initial work* - [github](https://github.com/sjefvanleeuwen)

## License

This project is licensed under the GPL-V3 License - see the [LICENSE.md](LICENSE.md) file for details