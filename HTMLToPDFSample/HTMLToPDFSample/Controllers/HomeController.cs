using HTMLToPDFSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace HTMLToPDFSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //<summary>
        ///Gets or sets DateTime.
        ///</summary>
        private DateTime DateTime { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Invoice()
        {
            var nwDt = DateTime.Now.ToString("MM/dd/yyyy");
            ViewData["nowDt"] = nwDt;
            var nxtDt = DateTime.Now.AddMonths(1).ToString("MM/dd/yyyy");
            ViewData["nextDt"] = nxtDt;
            return View();
        }
        public ActionResult Index()
        {   
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ExportToPDF()
        {
            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();
            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(800, 0);
            converter.ConverterSettings = blinkConverterSettings;
            using (PdfDocument document = converter.Convert("https://localhost:7156/Home/Invoice"))
            {
                MemoryStream stream = new MemoryStream();
                document.Save(stream);
                stream.Position = 0;
                return File(stream, "application/pdf", "HTML-to-PDF.pdf");
            }
        }
                
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}