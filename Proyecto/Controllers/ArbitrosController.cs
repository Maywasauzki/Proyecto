using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Proyecto.Controllers
{
    public class ArbitrosController : Controller
    {
        private ProyectoContext db = new ProyectoContext();
        // GET: Equipos
        public ActionResult Index()
        {
            //recuperar la relacion con ColegioArbitral y con Torneo
            var arbitro = db.Arbitros.Include(p => p.ColegioArbitral).Include(p => p.Torneo);
            return View(db.Arbitros.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Llenar un DropDownList con la información de los Colegios Arbitrales y Torneos
            var list = db.Arbitros.ToList();
            ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Arbitro arbitro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Arbitros.Add(arbitro);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Arbitro creado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un arbitro con ese numero de identificacion";
                        ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
                        ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
                        ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");

                    }

                    return View(arbitro);

                }

            }
            else
            {
                ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
                ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
                return View(arbitro);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbitro arbitro = db.Arbitros.Find(id);// 
            if (arbitro.Equals(null))
            {
                return HttpNotFound();
            }
            ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
            return View(arbitro);
        }

        [HttpPost]
        public ActionResult Edit(Arbitro arbitro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(arbitro).State = EntityState.Modified;// update
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Arbitro modificado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un arbitro con esa identificacion";

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }

                    return View(arbitro);
                }
            }
            else
            {

                ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
                ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
                return View(arbitro);
            }

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbitro arbitro = db.Arbitros.Find(id);

            if (arbitro.Equals(null))
            {
                return HttpNotFound();
            }
            ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
            return View(arbitro);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbitro arbitro = db.Arbitros.Find(id);
            if (arbitro.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.ColegioArbitralId = new SelectList(db.ColegioArbitrals, "ColegioArbitralId", "Nombre");
            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");

            return View(arbitro);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Arbitro arbitro = db.Arbitros.Find(id);
            if (arbitro.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                db.Arbitros.Remove(arbitro);
                db.SaveChanges();
                TempData["AlertMessage"] = "¡Arbitro eliminado exitosamente ....!";
            }
            return RedirectToAction("Index");

        }


        //Metodo para cerrar la conexion con la base de datos

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}