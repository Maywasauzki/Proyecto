using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace Proyecto.Controllers
{
    public class CanchasController : Controller
    {
        //Objeto de tipo contexto
        private ProyectoContext db = new ProyectoContext();

        // GET: Canchas
        public ActionResult Index()
        {
            //Recuperar la relacion entre Cancha y escenario
            var cancha = db.Canchas.Include(mun => mun.Escenario);
            return View(db.Canchas.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Llenar un DropDownList con la información de los Municipios
            var list = db.Canchas.ToList();
            ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Cancha cancha)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Canchas.Add(cancha);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Cancha creada exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrada una cancha con este nombre";
                        ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
                    }

                    return View(cancha);

                }

            }
            else
            {
                ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
                return View(cancha);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancha cancha = db.Canchas.Find(id);
            if (cancha.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
            return View(cancha);
        }

        [HttpPost]
        public ActionResult Edit(Cancha cancha)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(cancha).State = EntityState.Modified;// update
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Cancha modificada exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrada una cancha con este nombre";
                        ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
                    }

                    return View(cancha);
                }
            }
            else
            {
                ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
                return View(cancha);
            }

        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancha cancha = db.Canchas.Find(id);
            if (cancha.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
            return View(cancha);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancha cancha = db.Canchas.Find(id);// select o from Municipios where MunicipioId=id
            if (cancha.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.EscenarioId = new SelectList(db.Escenarios, "EscenarioId", "Nombre");
            return View(cancha);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Cancha cancha = db.Canchas.Find(id);
            if (cancha.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                db.Canchas.Remove(cancha);
                db.SaveChanges();
                TempData["AlertMessage"] = "¡Cancha eliminada exitosamente ....!";
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