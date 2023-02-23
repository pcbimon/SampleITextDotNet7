using System.Diagnostics;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SampleIText.Models;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Path = System.IO.Path;


namespace SampleIText.Controllers
{
    public class HomeController : Controller
    {
        public static String REGULAR = "Fonts/THSarabunNew.ttf";
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PDF()
        {
            MemoryStream stream = new MemoryStream();
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(stream);
            // writer.SetCloseStream(false);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4);
            // Initialize document
            FontProgram fontProgram = FontProgramFactory.CreateFont(Path.Combine(_hostingEnvironment.WebRootPath, REGULAR));
            PdfFont font = PdfFontFactory.CreateFont(fontProgram, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
            Document document = new Document(pdf).SetFont(font);
            
            //Add paragraph to the document
            document.Add(new Paragraph("Hello World!"));
            document.Add(new Paragraph("สวัสดี ชาวโลก"));
            //Add Table
            Table table = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();

            for (int i = 0; i < 16; i++)
            {
                table.AddCell("Hi I'm สวัสดี");
            }
            document.Add(table);
            //Close document
            writer.SetCloseStream(false);
            document.Close();
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}