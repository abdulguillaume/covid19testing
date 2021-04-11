using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19TestingServer.Models;
using Microsoft.AspNetCore.Http;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Drawing;
using System.IO;
using Microsoft.Extensions.Configuration;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace CovidTestingServer.Controllers
{
    public class LabTestsController : Controller
    {
        private readonly Covid19TestingSrvContext _context;
        private readonly IConfiguration configuration;

        Dictionary<int, string> specimen = new Dictionary<int, string>();

        Dictionary<int, string> indicators = new Dictionary<int, string>();

        public LabTestsController(Covid19TestingSrvContext context, IConfiguration _configuration)
        {
            _context = context;
            configuration = _configuration;

            //string endpoint = configuration.GetConnectionString("ServerEndpoint");

            foreach (var s in _context.TlkpSpecimen)
            {
                specimen[s.Id] = s.Type;
            }

            foreach (var i in _context.TlkpTestIndicators)
            {
                indicators[i.Id] = i.IndicatorName;
            }

        }

        // GET: LabTests
        public async Task<IActionResult> Index()
        {
            //var covid19TestingSrvContext = _context.TblLabTests.Include(t => t.BiodataNavigation).Include(t => t.MethodNavigation);
            //_context.TblLabTests;

            string _keyword = HttpContext.Session.GetString("testsrv_search_keyword");
            string _date1 = HttpContext.Session.GetString("testsrv_search_date1");

            if (!string.IsNullOrEmpty(_keyword) && !string.IsNullOrEmpty(_date1))
            {

                //return search(_page, _pageSize);
                return RedirectToAction(nameof(search), new {  keyword = _keyword, date1 = _date1 });
            }

            ViewBag.NoResult = true;

            return View(
                            await _context.TblLabTests
                            .Where(x=>x.Id==-1)
                            //.Include(x=>x.BiodataNavigation)
                            .ToListAsync()
                        );
        }


        [HttpGet]
        [Route("Labtests/search")]
        public async Task<IActionResult> search()
        {

            var keyword = Request.Query["keyword"].ToString();

            ViewBag.keyword = keyword;

            var date1 = Request.Query["date1"].ToString();
            
            ViewBag.date1 = date1;

            HttpContext.Session.SetString("testsrv_search_keyword", keyword);
            HttpContext.Session.SetString("testsrv_search_date1", date1);

            return View("Index",
                await _context.TblLabTests
                            .Include(x => x.BiodataNavigation)
                            //.Include(x=> x.TblLabTestsIndicatorsValues)
                            //.Include(x=>x.TblLabTestsSpecimen)
                            .Where(x=>x.BiodataNavigation.EpidNo==keyword && x.TestingDate.Value.Date==DateTime.Parse(date1).Date)
                            .ToListAsync()
                        );
        }

        // GET: LabTests/Details/5
        public async Task<IActionResult> Details(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var tblLabTests = await _context.TblLabTests
                            .Include(t => t.MethodNavigation)
                            .Include(x => x.BiodataNavigation)
                            .Include(x => x.BiodataNavigation.GenderNavigation)
                            .Include(x => x.TblLabTestsIndicatorsValues)
                            .Include(x => x.TblLabTestsSpecimen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblLabTests == null)
            {
                return NotFound();
            }

            ViewBag.specimen = specimen;
            ViewBag.indicators = indicators;

            return View(tblLabTests);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateDocument(int Id, string tokenId)
        {
            var LabTest = _context.TblLabTests
                .Include(x=>x.MethodNavigation)
                .Include(x=>x.BiodataNavigation)
                .Include(x=>x.BiodataNavigation.GenderNavigation)
                .Include(x => x.TblLabTestsIndicatorsValues)
                .Include(x => x.TblLabTestsSpecimen)
                .FirstOrDefault(x=>x.Id==Id);

            if (LabTest == null)
            {
                return NotFound();
            }

            //Loads or opens an existing word document through Open method of WordDocument class
            Stream fs = new FileStream("Templates/result.docx", FileMode.Open, FileAccess.Read, FileShare.Read);

            WordDocument document = new WordDocument(fs, FormatType.Docx);
            if (document.Sections.Count < 1) throw new Exception("Result template empty. Please contact your Application support.");
            IWSection section = document.Sections[0];

            IWTable table = section.Tables[0];

            WParagraph p = (WParagraph)table.Rows[0].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BiodataNavigation.Fullname);

            p = (WParagraph)table.Rows[1].Cells[1].ChildEntities[0];
            string gardian = LabTest.BiodataNavigation.LegalGardianName;
            gardian = string.IsNullOrEmpty(gardian) ? "-" : gardian;
            p.AppendText(gardian);

            p = (WParagraph)table.Rows[2].Cells[1].ChildEntities[0];
            
            p.AppendText(LabTest.BiodataNavigation.Dateofbirth.ToString("dd/MMM/yyyy"));

            //skip 3


            //Repos.GenderRepos genders = new Repos.GenderRepos();


            int g_id = 1;
            foreach (var g in _context.TlkpGenders)
            {
                string tmp = g.Gender.Substring(0, 2).Trim().Trim('=');
                try
                {
                    p = (WParagraph)table.Rows[3].Cells[g_id].ChildEntities[0];
                    WCheckBox checkbox = p.AppendCheckBox();

                    if (LabTest.BiodataNavigation.Gender == g_id)
                        checkbox.Checked = true;

                    p.AppendText(" " + tmp);

                    g_id++;

                }
                catch
                {

                }

            }

            p = (WParagraph)table.Rows[4].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BiodataNavigation.EpidNo);

            //skip 5 for local phone number
            p = (WParagraph)table.Rows[5].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BiodataNavigation.LocalPhone ?? "-");

            p = (WParagraph)table.Rows[6].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BiodataNavigation.HomePhone ?? "-");

            p = (WParagraph)table.Rows[7].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BiodataNavigation.ResidentialAddress);

            //next table
            table = section.Tables[1];

            p = (WParagraph)table.Rows[0].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.MethodNavigation.Methodname);

            int k = 1;


            foreach (var i in LabTest.TblLabTestsIndicatorsValues)
            {
                var iname = _context.TlkpTestIndicators.FirstOrDefault(x => x.Id == i.Indicator).IndicatorName;

                p = (WParagraph)table.Rows[k].Cells[0].ChildEntities[0];
                p.AppendText(iname);

                p = (WParagraph)table.Rows[k].Cells[1].ChildEntities[0];
                p.AppendText(i.IndicatorValue.Value.ToString());

                k++;
            }



            /*Dictionary<int, string> _dict = new Dictionary<int, string> {
                { 1, "POSITIVE"},
                { 2, "NEGATIVE"},
                { 97, "NO RESULT"}
            };*/
            var p1 = (WParagraph)table.Rows[k].Cells[0].ChildEntities[0];

            p = (WParagraph)table.Rows[k].Cells[1].ChildEntities[0];
            WCheckBox checkbox1 = p.AppendCheckBox();
            if (LabTest.Interpretation == 2)
            {
                checkbox1.Checked = true;
                p1.AppendText("INTERPRETATION: NEGATIVE RESULT");
            }
            else if (LabTest.Interpretation > 2)
            {
                p1.AppendText("INTERPRETATION: UNKNOWN");
            }

            p.AppendText(" NEGATIVE");

            p = (WParagraph)table.Rows[k + 1].Cells[1].ChildEntities[0];
            checkbox1 = p.AppendCheckBox();
            if (LabTest.Interpretation == 1)
            {
                checkbox1.Checked = true;
                p1.AppendText("INTERPRETATION: POSITIVE RESULT");
            }

            p.AppendText(" POSITIVE");

            //next table
            table = section.Tables[2];

            DateTime d1 = DateTime.Today;               // any date will do

            p = (WParagraph)table.Rows[0].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.TestingDate.Value.ToString("dd/MMM/yyyy"));

            p = (WParagraph)table.Rows[1].Cells[1].ChildEntities[0];
            TimeSpan t = LabTest.TestingTime.Value;
            string chg = (d1 + t).ToString("hh:mm tt");
            p.AppendText(chg);

            p = (WParagraph)table.Rows[2].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.ReportingDate.Value.ToString("dd/MMM/yyyy"));

            p = (WParagraph)table.Rows[3].Cells[1].ChildEntities[0];
            t = LabTest.ReportingTime.Value;
            chg = (d1 + t).ToString("hh:mm tt");
            p.AppendText(chg);

            //next table
            table = section.Tables[3];

            //Repos.SpecimenRepos _specimen = new Repos.SpecimenRepos();

            k = 1;
            foreach (var s in LabTest.TblLabTestsSpecimen)
            {
                bool other = s.Specimen == 99;

                var sname = _context.TlkpSpecimen.FirstOrDefault(x=>x.Id==s.Specimen).Type;
                p = (WParagraph)table.Rows[k].Cells[0].ChildEntities[0];

                IWTextRange textRange = p.AppendText(sname + (other ? " (Specify)" : ""));
                textRange.CharacterFormat.FontSize = 8;


                p.ApplyStyle(BuiltinStyle.BlockText);
                p = (WParagraph)table.Rows[k].Cells[1].ChildEntities[0];
                if (other && !string.IsNullOrEmpty(s.SpecimenOther))
                {
                    textRange = p.AppendText(s.SpecimenOther);
                    textRange.CharacterFormat.FontSize = 10;
                }

                p = (WParagraph)table.Rows[k].Cells[2].ChildEntities[0];
                checkbox1 = p.AppendCheckBox();
                if (s.Checked)
                    checkbox1.Checked = true;

                k++;
            }


            //
            //table = section.Tables[1];
            // p = (WParagraph)table.Rows[6].Cells[1].ChildEntities[0];
            //p.AppendText(LabTest.BioData.ResidentialAddress);


            DocIORenderer render = new DocIORenderer();
            //Converts Word document to PDF.
            PdfDocument pdfDocument = render.ConvertToPDF(document);
            //Release the resources used by the Word document and DocIO Renderer objects.

            render.Dispose();
            render.Dispose();
            document.Dispose();

            //add barcode
            PdfQRBarcode barcode = new PdfQRBarcode();
            barcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;
            //Set XDimension
            barcode.XDimension = 2.5f;
            //https://localhost:44353/LabTests/Details/
            string endpoint = configuration.GetConnectionString("ServerEndpoint");
            barcode.Text = string.Format("{0}LabTests/Details/{1}", endpoint, Id);
            //Creating new PDF Document
            //PdfDocument doc = new PdfDocument();
            //Adding new page to PDF document
            PdfPage page = pdfDocument.Pages[0];
            //Printing barcode on to the Pdf. 
            //barcode.Draw(page, new PointF(25, 70));
            barcode.Draw(page, new PointF(250, 650));

            if (!string.IsNullOrEmpty(tokenId))
                Response.Cookies.Append("fileDownloadToken", tokenId);

            MemoryStream stream = new MemoryStream();
            //document.Save(stream, FormatType.Docx);
            pdfDocument.Save(stream);
            //return File(stream, "application/msword", "Sample.docx");
            return File(stream.ToArray(), "application/octet-stream", "Draft.pdf");
            //return RedirectToAction(nameof(Index));
        }
    }
}
