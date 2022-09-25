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
    public class EscenariosController : Controller
    {
        private ProyectoContext db = new ProyectoContext();
        // GET: Equipos
        public ActionResult Index()
        {
            //recuperar la relacion con Torneo y con Canchas
            var escenarios = db.Escenarios.Include(p => p.Torneo).Include(p => p.Canchas);
            return View(db.Escenarios.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Llenar un DropDownList con la información de los Torneos y Canchas
            var list = db.Escenarios.ToList();
            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
            ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Escenario escenario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Escenarios.Add(escenario);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Escenario creado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un escenario con ese nombre";
                        ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
                        ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
                        ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");

                    }

                    return View(escenario);

                }

            }
            else
            {
                ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
                ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");
                return View(escenario);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escenario escenario = db.Escenarios.Find(id);// 
            if (escenario.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
            ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");
            return View(escenario);
        }

        [HttpPost]
        public ActionResult Edit(Escenario escenario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(escenario).State = EntityState.Modified;// update
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Escenario modificado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un escenario con ese nombre";

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }

                    return View(escenario);
                }
            }
            else
            {

                ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
                ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");
                return View(escenario);
            }

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escenario escenario = db.Escenarios.Find(id);

            if (escenario.Equals(null))
            {
                return HttpNotFound();
            }
            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
            ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");
            return View(escenario);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escenario escenario = db.Escenarios.Find(id);
            if (escenario.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.TorneoId = new SelectList(db.Torneos, "TorneoId", "Nombre");
            ViewBag.CanchaId = new SelectList(db.Canchas, "CanchasId", "Nombre");

            return View(escenario);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Escenario escenario = db.Escenarios.Find(id);
            if (escenario.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    db.Escenarios.Remove(escenario);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Escenario eliminado exitosamente ....!";

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ViewBag.Error = "No se permite la eliminación de registros con integridad referencial";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(escenario);
                }

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