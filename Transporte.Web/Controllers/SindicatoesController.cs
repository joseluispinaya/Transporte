using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Transporte.Web.Data;
using Transporte.Web.Data.Entities;
using Transporte.Web.Models;

namespace Transporte.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SindicatoesController : Controller
    {
        private readonly DataContext _context;

        public SindicatoesController(DataContext context)
        {
            _context = context;
        }

        // GET: Sindicatoes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Sindicatos.ToListAsync());
        //}
        public IActionResult Index()
        {
            return View(_context.Sindicatos
                .Include(o => o.Afiliados));
        }

        //public async Task<IActionResult> Reporte(int? id)
        public IActionResult Reporte()
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.Letter);
            doc.SetMargins(40f, 40f, 40f, 40f);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.AddAuthor("CodeStack");
            doc.AddTitle("Reporte Afiliados");
            doc.Open();

            // Estilos de letra colores 
            BaseFont helvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);

            iTextSharp.text.Font titulo = new iTextSharp.text.Font(helvetica, 30f, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(0, 0, 0));
            iTextSharp.text.Font texto_blanco = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(255, 255, 255));

            iTextSharp.text.Font subtitulo = new iTextSharp.text.Font(helvetica, 12f, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(0, 0, 0));

            iTextSharp.text.Font negrita = new iTextSharp.text.Font(helvetica, 9f, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(0, 0, 0));

            iTextSharp.text.Font parrafo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            iTextSharp.text.Font detalle = new iTextSharp.text.Font(helvetica, 15f, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(255, 255, 255));
            // Fin de Estilos de letra colores 

            doc.Add(iTextSharp.text.Chunk.Newline);
            iTextSharp.text.Chunk barra = new iTextSharp.text.Chunk(new iTextSharp.text.pdf.draw.LineSeparator(5f, 30f, new iTextSharp.text.BaseColor(0, 69, 161), iTextSharp.text.Element.ALIGN_RIGHT, -1));
            doc.Add(barra);
            doc.Add(new iTextSharp.text.Phrase(" "));

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\images",
                "gopher_head-min.png");

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(path);
            logo.ScaleAbsolute(100, 50);

            var tbl = new PdfPTable(new float[] { 40f, 60f }) { WidthPercentage = 100 };
            //tbl.AddCell(new PdfPCell(new iTextSharp.text.Phrase("EMI UA RIB", titulo)) { Border = 0, Rowspan = 3, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            tbl.AddCell(new PdfPCell(logo) { Border = 0, Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
            tbl.AddCell(new PdfPCell(new iTextSharp.text.Phrase("CALLE PLACIDO MOLINA Y PLACIDO MENDEZ, RIBERALTA", parrafo)) { Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT });
            tbl.AddCell(new PdfPCell(new iTextSharp.text.Phrase("+(591) 71299860 FedeRib@gmail.com", parrafo)) { Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT });
            tbl.AddCell(new PdfPCell(new iTextSharp.text.Phrase(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), parrafo)) { Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT });

            doc.Add(tbl);
            doc.Add(new iTextSharp.text.Phrase(" "));
            //var ruta = Path.Combine("/images/", "gopher_head-min.png");
            //var ruta = Path.Combine("/images/", "gopher_head-min.png");

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(ruta);
            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Path.Combine(env.WebRootPath, "images", "gopher_head-min.png"));
            doc.Add(new Phrase("REPORTE SINDICATOS"));

            writer.Close();
            doc.Close();
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/pdf");
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var sindicato = await _context.Sindicatos
            //    .Include(a => a.Afiliados)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (sindicato == null)
            //{
            //    return NotFound();
            //}
        }

        // GET: Sindicatoes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sindicato = await _context.Sindicatos
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (sindicato == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sindicato);
        //}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos
                .Include(a=> a.Afiliados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return View(sindicato);
        }

        public async Task<IActionResult> AddAfiliado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sindica = await _context.Sindicatos.FindAsync(id);
            if (sindica == null)
            {
                return NotFound();
            }
            var model = new AfiliadoViewModel
            {
                SindicatoId = sindica.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAfiliado(AfiliadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var afili = await ToAfiliadoAsync(model, true);
                _context.Afiliados.Add(afili);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.SindicatoId}");
            }
            return View(model);
        }

        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        public async Task<Afiliado> ToAfiliadoAsync(AfiliadoViewModel model, bool isNew)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(model.NroDocumento,QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, null, 15,6,false);

            var bitmapBytes = BitmapToBytesCode(qrCodeImage);
            var rutaqr = UploadimgArray(bitmapBytes, "Afiqr");

            return new Afiliado
            {
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                Direccion = model.Direccion,
                Id = isNew ? 0 : model.Id,
                NroDocumento = model.NroDocumento,
                Celular = model.Celular,
                Foto = await UploadImageAsync(model.ImageFile),
                //Imgqr = "nada",
                Imgqr = rutaqr,
                Sindicato = await _context.Sindicatos.FindAsync(model.SindicatoId)

            };
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\images\\Afili",
                file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/Afili/{file}";
        }

        public string UploadimgArray(byte[] pictureArray, string folder)
        {
            MemoryStream stream = new MemoryStream(pictureArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";

            try
            {
                stream.Position = 0;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", file);
                //File.WriteAllBytes(path, stream.ToArray());
                System.IO.File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {

                return string.Empty;
            }
            return $"~/images/{folder}/{file}";
        }

        // GET: Sindicatoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sindicatoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nomsindica,Responsable,Ubicacion,Fechafundacion,Celular")] Sindicato sindicato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sindicato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sindicato);
        }

        // GET: Sindicatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos.FindAsync(id);
            if (sindicato == null)
            {
                return NotFound();
            }
            return View(sindicato);
        }

        // POST: Sindicatoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nomsindica,Responsable,Ubicacion,Fechafundacion,Celular")] Sindicato sindicato)
        {
            if (id != sindicato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sindicato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SindicatoExists(sindicato.Id))
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
            return View(sindicato);
        }

        public async Task<IActionResult> Mybusqueda(string texto="")
        {
            //Afiliado af = new Afiliado();
            Afiliado af = null;
            if (texto == "")
            {
                return View(af);
            }
            //var afili = await _context.Afiliados
            //    .FirstOrDefaultAsync(a => a.NroDocumento.Equals(texto));
            var afili = await _context.Afiliados
                .Include(o => o.Sindicato)
                .FirstOrDefaultAsync(a => a.NroDocumento.Equals(texto));
            if (afili == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Mybusqueda));
            }

            return View(afili);
        }

        public async Task<IActionResult> DetailsAfili(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var afiliado = await _context.Afiliados
                .Include(o => o.Sindicato)
                .Include(v => v.Vehiculos)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (afiliado == null)
            {
                return NotFound();
            }

            return View(afiliado);
        }

        public async Task<IActionResult> AddVehiculo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var afili = await _context.Afiliados.FindAsync(id);
            if (afili == null)
            {
                return NotFound();
            }

            var model = new VehiculoViewModel { AfiliadoId = afili.Id };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehiculo(VehiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehi = await ToVehiculoAsync(model, true);
                _context.Vehiculos.Add(vehi);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"DetailsAfili/{model.AfiliadoId}");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicada"))
                    {
                        ModelState.AddModelError(string.Empty, "El numero de placa ya existe.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

               
            }
            return View(model);
        }

        public async Task<Vehiculo> ToVehiculoAsync(VehiculoViewModel model, bool isNew)
        {
            return new Vehiculo
            {
                Nroplaca = model.Nroplaca.ToUpper(),
                Nrochasis = model.Nrochasis,
                Nromotor = model.Nromotor,
                Marca = model.Marca,
                Id = isNew ? 0 : model.Id,
                Afiliado = await _context.Afiliados.FindAsync(model.AfiliadoId)
            };
        }
        // GET: Sindicatoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return View(sindicato);
        }

        // POST: Sindicatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sindicato = await _context.Sindicatos.FindAsync(id);
            _context.Sindicatos.Remove(sindicato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SindicatoExists(int id)
        {
            return _context.Sindicatos.Any(e => e.Id == id);
        }
    }
}
