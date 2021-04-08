using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Testing.Models;
using Covid19Testing.IRepos;
using PagedList.Core;
using Covid19Testing.ViewModels;
using Covid19Testing.Utils;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;

using Covid19Testing.Utils;


using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Drawing;
using Syncfusion.DocIORenderer;
using Microsoft.AspNetCore.Authorization;

using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mime;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
//using System.Net.Mail;
//using SmtpClient = System.Net.Mail.SmtpClient;
//using System.Net;

namespace Covid19Testing.Controllers
{

    [Authorize(Roles = "dataentry10")]
    public class LabTestsController : Controller
    {
        //private readonly Covid19TestingContext _context;

        private readonly ILabTestRepos tests;
        private readonly IBiodataRepos biodata;
        private readonly IMethodRepos methods;
        private readonly ITestIndicatorRepos indicators;
        private readonly IGenderRepos genders;
        //private readonly IConfiguration configuration;

        //private readonly ITestIndicatorRepos indicators;
        private readonly ISpecimenRepos specimen;

        const int _pageSize = 20;

        private TestsApi _api;

        private readonly List<SelectListItem> results = new List<SelectListItem>
            {
                new SelectListItem{ Text="Positive", Value = "1" },
                new SelectListItem{ Text="Negative", Value = "2" },
            };

        public LabTestsController(IConfiguration _configuration, ILabTestRepos _tests, ITestIndicatorRepos _indicators,ISpecimenRepos _specimen, IBiodataRepos _biodata, IMethodRepos _methods, IGenderRepos _genders)//Covid19TestingContext context)
        {
            tests = _tests;
            biodata = _biodata;
            methods = _methods;
            indicators = _indicators;
            specimen = _specimen;
            genders = _genders;
            _api = new TestsApi(_configuration);
        }

        [HttpGet]
        [Route("Labtests/search")]
        public IActionResult search(int page = 1, int pageSize = _pageSize)
        {
            var date1 = Request.Query["date1"].ToString();
            var date2 = Request.Query["date2"].ToString();

            var clear1 = Request.Query["cleardate1"].ToString();
            var clear2 = Request.Query["cleardate2"].ToString();

            if (clear1 == "on") date1 = "";
            if (clear2 == "on") date2 = "";

            ViewBag.date1 = date1;
            ViewBag.date2 = date2;

            PagedList<LabTestDetailsViewModel> model;

            HttpContext.Session.SetInt32("labtests_search_page", page);
            HttpContext.Session.SetString("labtests_search_date1", date1);
            HttpContext.Session.SetString("labtests_search_date2", date2);

            if (string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
                model = new PagedList<LabTestDetailsViewModel>(tests.GetAllByBeforeDate(DateTime.Parse(date2)).AsQueryable(), page, pageSize);
            else if (string.IsNullOrEmpty(date2) && !string.IsNullOrEmpty(date1))
                model = new PagedList<LabTestDetailsViewModel>(tests.GetAllByAfterDate(DateTime.Parse(date1)).AsQueryable(), page, pageSize);
            else if (!string.IsNullOrEmpty(date2) && !string.IsNullOrEmpty(date1))
                model = new PagedList<LabTestDetailsViewModel>(tests.GetAllByDateRange(DateTime.Parse(date1), DateTime.Parse(date2)).AsQueryable(), page, pageSize);
            else
                model = new PagedList<LabTestDetailsViewModel>(tests.GetAll().AsQueryable(), page, pageSize); //ok

            //ViewBag.keyword = keyword;
            return View("Index", model);
            //return null;
        }

        // GET: LabTests
        public async Task<IActionResult> Index()
        {
            
            //var covid19TestingContext = _context.TblLabTests.Include(t => t.BiodataNavigation).Include(t => t.MethodNavigation);

            //return View(await covid19TestingContext.ToListAsync());
            HttpContext.Session.SetString("biodata_details_token", "");

            int _page = 1;
            try
            {
                _page = HttpContext.Session.GetInt32("labtests_search_page").Value;
            }
            catch { }

            string date1 = HttpContext.Session.GetString("labtests_search_date1");
            string date2 = HttpContext.Session.GetString("labtests_search_date2");

            //PagedList<LabTestDetailsViewModel> model = new PagedList<LabTestDetailsViewModel>(tests.GetAll().AsQueryable(), _page, _pageSize);
            return RedirectToAction(nameof(search), new { page = _page, date1 = date1, date2=date2 });
            //return View(model);
        }

        // GET: LabTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LabTest = tests.GetById(id);/*await _context.TblLabTests
                .Include(t => t.BiodataNavigation)
                .Include(t => t.MethodNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);*/


            //var json = JsonConvert.SerializeObject(LabTest.LabTest, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            if (LabTest == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("biodata_details_token", "");

            var _indicators = indicators.GetAll();

            foreach (var i in LabTest.Indicators)
            {
                i.IndicatorName = _indicators.FirstOrDefault(x => x.Id == i.Indicator).IndicatorName;
            }

            var _specimen = specimen.GetAll();

            foreach (var s in LabTest.Specimen)
            {
                s.SpecimenName = _specimen.FirstOrDefault(x => x.Id == s.Specimen).Type;
            }

            if (LabTest.LabTest.Approved == null)
                LabTest.LabTest.Approved = false;

            ViewData["Ready"] = Ready4Approval(LabTest.LabTest);

            return View(LabTest);
        }



        [HttpPost]
        [Authorize(Roles = "dataapprover")]
        public async Task<IActionResult> PublishResult(int Id)
        {
            ActionResponse response = new ActionResponse { };

            response.IsSuccessful = false;
            response.Message = "Cannot connect to central server.";

            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Add("ApiKey", "37005211-de90-4b0c-a6f2-f141f5d97070");

                HttpResponseMessage _response = await client.GetAsync(string.Format("api/results/{0}", Id));

                if (_response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //var result = _response.Content.ReadAsStringAsync().Result;
                    response.IsSuccessful = false;
                    response.Message = "Unauthorized access to central server.";
                }
                else if (_response.IsSuccessStatusCode)
                {
                    //var result = _response.Content.ReadAsStringAsync().Result;
                    response.IsSuccessful = false;
                    response.Message = "Covid19 Test Result already published.";
                }
                else
                {
                    var test = tests.GetById(Id);
                    var json_payload = JsonConvert.SerializeObject(test.LabTest, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                    HttpContent content = new StringContent(json_payload, Encoding.UTF8, "application/json");

                    _response = await client.PostAsync("api/results/", content);

                    if (_response.IsSuccessStatusCode)
                    {
                        //var result = _response.Content.ReadAsStringAsync().Result;
                        response.IsSuccessful = true;
                        response.Message = "Covid19 Test Result published successfully.";
                        test.LabTest.PushedSvrBy = User.Identity.Name;
                        tests.Update(test);

                    }
                    else {
                        response.IsSuccessful = false;
                        response.Message = "Covid19 Test Result not published.";
                    }

                }

            }


            return Json(response, new JsonSerializerSettings());
        }

        [HttpPost]
        [Authorize(Roles = "dataapprover")]
        public async Task<IActionResult> ApproveResult(int Id)
        {
            ActionResponse response = new ActionResponse { };
            
            try
            {
                var LabTest = tests.GetById(Id);

                if (LabTest == null)
                {
                    return NotFound();
                }

                LabTest.LabTest.Approved = true;

                tests.Update(LabTest);

                response.IsSuccessful = true;
                response.Message = "n/a";

                response.JSONData = JsonConvert.SerializeObject("Ok");
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = "n/a";
            }
            return Json(response, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> ShareDocument(int Id, string emails)
        {
            ActionResponse response = new ActionResponse { };

            FileContentResult file = (FileContentResult)await GenerateDocument(Id, "");

            var LabTest = tests.GetById(Id);

            string testing_date = Utils.Utils.toShortdate(LabTest.LabTest.TestingDate);

            string filename = string.Format("Covid19-{0}-{1}.pdf",LabTest.BioData.EpidNo, testing_date);

            Attachment data = new Attachment(new MemoryStream(file.FileContents),filename, MediaTypeNames.Application.Octet);

            // Gmail Address from where you send the mail 
            var fromAddress = "abdulguillaume@gmail.com";
            // any address where the email will be sending 
            //var toAddress = "aguillaume@iom.int";
            //Password of your gmail address 
            const string fromPassword = "pigyhssujyscavyv";

            

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(fromAddress);

            foreach (var e in emails.Split(";"))
            {
                message.To.Add(new MailAddress(e));
            }
            //message.To.Add(new MailAddress(toAddress));
            //message.To.Add(new MailAddress("malabi@iom.int"));
            message.Subject = string.Format("Covid19 Test Result, {0}", testing_date);
            string body = "Greetings, <br />";
            body += string.Format("Covid19 Test Result for candicate EPID-No: {0}, Fullname: {1} <br />" , LabTest.BioData.EpidNo, LabTest.BioData.Fullname);
            body += "Regards <br />";
            body += "UN Migration/IOM/MHAC";
            message.Body = body;//@"Using this new feature, you can send an email message from an application very easily.";
            // smtp settings 
            message.Attachments.Add(data);

            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 0;

                try
                {
                    smtp.Send(message);
                    LabTest.LabTest.SentEmailBy = User.Identity.Name;
                    LabTest.LabTest.SentEmailTime = DateTime.Now;
                    tests.Update(LabTest);

                    response.IsSuccessful = true;
                    response.Message = "n/a";
                    //response.JSONData = JsonConvert.SerializeObject(Indicators);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    //    ex.ToString());
                    response.IsSuccessful = false;
                    response.Message = ex.Message;
                    //response.JSONData = JsonConvert.SerializeObject(Indicators);
                }
            }


            return Json(response, new JsonSerializerSettings());
        }

        [HttpGet]
        public async Task<IActionResult> GenerateDocument(int Id, string tokenId)
        {
            var LabTest = tests.GetById(Id);

            if (LabTest == null)
            {
                return NotFound();
            }

            //Loads or opens an existing word document through Open method of WordDocument class
            Stream fs = new FileStream("Templates/result.docx", FileMode.Open, FileAccess.Read, FileShare.Read);

            WordDocument document = new WordDocument(fs, FormatType.Docx);
            if(document.Sections.Count<1)throw new Exception("Result template empty. Please contact your Application support.");
            IWSection section = document.Sections[0];

            IWTable table = section.Tables[0];

            WParagraph p = (WParagraph)table.Rows[0].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BioData.Fullname);

            p = (WParagraph)table.Rows[1].Cells[1].ChildEntities[0];
            string gardian = LabTest.BioData.LegalGardianName;
            gardian = string.IsNullOrEmpty(gardian) ? "-" : gardian;
            p.AppendText(gardian);

            p = (WParagraph)table.Rows[2].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BioData.Dateofbirth.ToShortDateString());

            //skip 3
            

            //Repos.GenderRepos genders = new Repos.GenderRepos();


            int g_id = 1;
            foreach (var g in genders.GetAll())
            {
                string tmp = g.Gender.Substring(0, 2).Trim().Trim('=');
                try
                {
                    p = (WParagraph)table.Rows[3].Cells[g_id].ChildEntities[0];
                    WCheckBox checkbox = p.AppendCheckBox();

                    if (LabTest.BioData.Gender == g_id)
                        checkbox.Checked = true;

                    p.AppendText(" " + tmp);

                    g_id++;

                }
                catch {

                }
                
            }

            p = (WParagraph)table.Rows[4].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BioData.EpidNo);

            //skip 5 for local phone number
            p = (WParagraph)table.Rows[5].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BioData.LocalPhone ?? "-");

            p = (WParagraph)table.Rows[6].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BioData.HomePhone??"-");

            p = (WParagraph)table.Rows[7].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.BioData.ResidentialAddress);
            
            //next table
            table = section.Tables[1];

            p = (WParagraph)table.Rows[0].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.LabTest.MethodNavigation.Methodname);

            int k = 1;

            var _indicators = indicators.GetAll();

            foreach (var i in LabTest.Indicators)
            {
                var iname = _indicators.FirstOrDefault(x => x.Id == i.Indicator).IndicatorName;

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
            var p1= (WParagraph)table.Rows[k].Cells[0].ChildEntities[0];

            p = (WParagraph)table.Rows[k].Cells[1].ChildEntities[0];
            WCheckBox checkbox1 = p.AppendCheckBox();
            if (LabTest.LabTest.Interpretation == 2)
            {
                checkbox1.Checked = true;
                p1.AppendText("INTERPRETATION: NEGATIVE RESULT");
            }
            else if(LabTest.LabTest.Interpretation > 2 ) {
                p1.AppendText("INTERPRETATION: UNKNOWN");
            }
                
            p.AppendText(" NEGATIVE");

            p = (WParagraph)table.Rows[k+1].Cells[1].ChildEntities[0];
            checkbox1 = p.AppendCheckBox();
            if (LabTest.LabTest.Interpretation == 1)
            {
                checkbox1.Checked = true;
                p1.AppendText("INTERPRETATION: POSITIVE RESULT");
            }
                
            p.AppendText(" POSITIVE");

            //next table
            table = section.Tables[2];

            DateTime d1 = DateTime.Today;               // any date will do

            p = (WParagraph)table.Rows[0].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.LabTest.TestingDate.Value.ToString("dd/MMM/yyyy"));

            p = (WParagraph)table.Rows[1].Cells[1].ChildEntities[0];
            TimeSpan t = LabTest.LabTest.TestingTime.Value;
            string chg = (d1 + t).ToString("hh:mm tt");
            p.AppendText(chg);

            p = (WParagraph)table.Rows[2].Cells[1].ChildEntities[0];
            p.AppendText(LabTest.LabTest.ReportingDate.Value.ToString("dd/MMM/yyyy"));

            p = (WParagraph)table.Rows[3].Cells[1].ChildEntities[0];
            t = LabTest.LabTest.ReportingTime.Value;
            chg = (d1 + t).ToString("hh:mm tt");
            p.AppendText(chg);

            //next table
            table = section.Tables[3];

            //Repos.SpecimenRepos _specimen = new Repos.SpecimenRepos();

            k = 1;
            foreach (var s in LabTest.Specimen)
            {
                bool other = s.Specimen == 99;

                var sname = specimen.GetById(s.Specimen).Type;
                p = (WParagraph)table.Rows[k].Cells[0].ChildEntities[0];

                IWTextRange textRange =p.AppendText(sname+(other?" (Specify)":""));
                textRange.CharacterFormat.FontSize = 8;


                p.ApplyStyle(BuiltinStyle.BlockText);
                p = (WParagraph)table.Rows[k].Cells[1].ChildEntities[0];
                if (other && !string.IsNullOrEmpty(s.SpecimenOther))
                {
                    textRange =p.AppendText(s.SpecimenOther);
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
            document.Dispose();

            //add barcode
            PdfQRBarcode barcode = new PdfQRBarcode();
            barcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;
            //Set XDimension
            barcode.XDimension = 2.5f;
            barcode.Text = "https://dtm.iom.int/";
            //Creating new PDF Document
            //PdfDocument doc = new PdfDocument();
            //Adding new page to PDF document
            PdfPage page = pdfDocument.Pages[0];
            //Printing barcode on to the Pdf. 
            //barcode.Draw(page, new PointF(25, 70));
            barcode.Draw(page, new PointF(250, 650));

            if(!string.IsNullOrEmpty(tokenId))
                Response.Cookies.Append("fileDownloadToken", tokenId);

            MemoryStream stream = new MemoryStream();
            //document.Save(stream, FormatType.Docx);
            pdfDocument.Save(stream);
            //return File(stream, "application/msword", "Sample.docx");
            return File(stream.ToArray(), "application/octet-stream", "Draft.pdf");
            //return RedirectToAction(nameof(Index));
        }

        // GET: LabTests/Create
        [Authorize(Roles = "dataentry10")]
        public IActionResult Create(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("biodata_details_token");

            if(string.IsNullOrEmpty(token))
                return RedirectToAction(nameof(Index), "Biodata");

            TblBiodata _Biodata = biodata.GetById(id);

            LabTestDetailsViewModel _labTest = new LabTestDetailsViewModel(_Biodata, methods, specimen);

            ViewData["Results"] = results;

            ViewData["Method"] = new SelectList(methods.GetAll(), "Id", "Methodname");

            /* ViewData["Biodata"] = new SelectList(_context.TblBiodata, "Id", "Fullname");
             ViewData["Method"] = new SelectList(_context.TlkpTestMethods, "Id", "InsertBy");*/
            return View(_labTest);
        }

        //string admin = "admin";

        // POST: LabTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabTestDetailsViewModel _labTest)//[Bind("Id,Biodata,Method,Interpretation,TestingDate,TestingTime,ReportingDate,ReportingTime,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblLabTests tblLabTests)
        {

            //var errors = ModelState.Values;
            //foreach (var e in errors)
            //{
            //    if (e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            //        ;
                
            //}

            if (ModelState.IsValid)
            {
                _labTest.LabTest.TblLabTestsIndicatorsValues = _labTest.Indicators;
                _labTest.LabTest.TblLabTestsSpecimen = _labTest.Specimen;
                _labTest.LabTest.InsertBy =
                    _labTest.LabTest.UpdateBy = User.Identity.Name;
                tests.Create(_labTest);
                //_context.Add(tblLabTests);
                //await _context.SaveChangesAsync();
                HttpContext.Session.SetString("biodata_details_token", "");

                return RedirectToAction(nameof(Index));
            }

            ViewData["Results"] = results;

            ViewData["Method"] = new SelectList(methods.GetAll(), "Id", "Methodname", _labTest.LabTest.Method);
            /*ViewData["Biodata"] = new SelectList(_context.TblBiodata, "Id", "Fullname", tblLabTests.Biodata);
            ViewData["Method"] = new SelectList(_context.TlkpTestMethods, "Id", "InsertBy", tblLabTests.Method);*/
            TblBiodata _Biodata = biodata.GetById(_labTest.BioData.Id);
            _labTest.BioData = _Biodata;
            //_labTest.Indicators = 
            return View(_labTest);
        }

        // GET: LabTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var LabTest = tests.GetById(id);

            if (LabTest == null)
            {
                return NotFound();
            }

            if(LabTest.LabTest.Approved!=null && LabTest.LabTest.Approved.Value)
                return RedirectToAction(nameof(Index));

            HttpContext.Session.SetString("biodata_details_token", "");


            var _indicators = indicators.GetAll();

            foreach (var i in LabTest.Indicators)
            {
                i.IndicatorName = _indicators.FirstOrDefault(x => x.Id == i.Indicator).IndicatorName;
            }

            var _specimen = specimen.GetAll();

            foreach (var s in LabTest.Specimen)
            {
                s.SpecimenName = _specimen.FirstOrDefault(x => x.Id == s.Specimen).Type;
            }

            ViewData["Results"] = results;
            ViewData["Method"] = new SelectList(methods.GetAll(), "Id", "Methodname");

            //ViewData["Indicators"] = new SelectList(indicators.GetAll(), "Id", "IndicatorName"); ;

            return View(LabTest);
        }

        // POST: LabTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LabTestDetailsViewModel test)
        {
            
            if (id != test.LabTest.Id)
            {
                return NotFound();
            }


            var errors = ModelState.Values;
            foreach (var e in errors)
            {
                if (e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    test.LabTest.UpdateBy = User.Identity.Name;
                    tests.Update(test);
                    /*_context.Update(tblLabTests);
                    await _context.SaveChangesAsync();*/
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLabTestsExists(test.LabTest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Results"] = results;
            ViewData["Method"] = new SelectList(methods.GetAll(), "Id", "Methodname");

            TblBiodata _Biodata = biodata.GetById(test.BioData.Id);
            test.BioData = _Biodata;

            return View(test);
        }

        // GET: LabTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpContext.Session.SetString("biodata_details_token", "");
            return NotFound();
            /*if (id == null)
            {
                return NotFound();
            }

            var tblLabTests = await _context.TblLabTests
                .Include(t => t.BiodataNavigation)
                .Include(t => t.MethodNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblLabTests == null)
            {
                return NotFound();
            }

            return View(tblLabTests);*/
        }

        // POST: LabTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var tblLabTests = await _context.TblLabTests.FindAsync(id);
            _context.TblLabTests.Remove(tblLabTests);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool Ready4Approval(TblLabTests test)
        {
            if (test.Approved != null && test.Approved.Value) return true;

            bool condition1 = (test.ReportingDate != null && test.ReportingTime != null);
            bool condition2 = false, condition3 = false;

            decimal sum = 0;

            foreach (var i in test.TblLabTestsIndicatorsValues)
            {
                if (i.IndicatorValue.HasValue)
                    sum += i.IndicatorValue.Value;
                else break;
            }

            if (sum > 0) condition2 = true;

            foreach (var s in test.TblLabTestsSpecimen)
            {
                if (s.Checked) {
                    condition3 = true;break;
                }
            }

            return condition1 && condition2 && condition3;
        }

        private bool TblLabTestsExists(int id)
        {
            return tests.GetById(id) != null;
        }

        public ActionResult GetBiodata(int _id)
        {
            ActionResponse response = new ActionResponse { };
            try
            {
                TblBiodata subject =
                biodata.GetById(_id);

                subject.TblLabTests = null;
                subject._genderName = subject.GenderNavigation.Gender;
                subject.GenderNavigation = null;
                subject._dob = subject.Dateofbirth.ToString("dd/MMM/yy");

                response.IsSuccessful = true;
                response.Message = "n/a";
                
                response.JSONData = JsonConvert.SerializeObject(subject, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = "n/a";
            }
            return Json(response, new JsonSerializerSettings());
        }

        public ActionResult GetLabTestIndicators(int _method)
        {
            ActionResponse response = new ActionResponse { };
            try
            {
                List<TblLabTestsIndicatorsValues> Indicators = new List<TblLabTestsIndicatorsValues>();

                foreach (var i in methods.GetById(_method).TlkpTestIndicators)
                {
                    TblLabTestsIndicatorsValues v = new TblLabTestsIndicatorsValues();
                    v.Indicator = i.Id;
                    v.Method = _method;
                    v.IndicatorName = i.IndicatorName;

                    Indicators.Add(v);
                }

                response.IsSuccessful = true;
                response.Message = "n/a";
                response.JSONData = JsonConvert.SerializeObject(Indicators);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = "n/a";
            }
            return Json(response, new JsonSerializerSettings());
        }
    }
}
