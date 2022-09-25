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
    public class DeportistasController : Controller
    {
        private ProyectoContext db = new ProyectoContext();
        // GET: Equipos
        public ActionResult Index()
        {
            //Recuperar la relacion entre deportista y equipo
            var deportista = db.Deportistas.Include(mun => mun.Equipo);
            return View(db.Deportistas.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Llenar un DropDownList con la información de los Equipos
            var list = db.Deportistas.ToList();
            ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Deportista deportista)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Deportistas.Add(deportista);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Deportista creado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un deportista con este numero de identificacion";
                        ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
                    }

                    return View(deportista);

                }

            }
            else
            {
                ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
                return View(deportista);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deportista deportista = db.Deportistas.Find(id);
            if (deportista.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
            return View(deportista);
        }

        [HttpPost]
        public ActionResult Edit(Deportista deportista)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(deportista).State = EntityState.Modified;// update
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Deportista modificado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un deportista con este numero de identificacion";
                        ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                        ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
                    }

                    return View(deportista);
                }
            }
            else
            {
                ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
                return View(deportista);
            }

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deportista deportista = db.Deportistas.Find(id);
            if (deportista.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
            return View(deportista);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deportista deportista = db.Deportistas.Find(id);// select o from Municipios where MunicipioId=id
            if (deportista.Equals(null))
            {
                return HttpNotFound();
            }

            ViewBag.equipoId = new SelectList(db.Equipos, "EquipoId", "Nombre");
            return View(deportista);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Deportista deportista = db.Deportistas.Find(id);
            if (deportista.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                db.Deportistas.Remove(deportista);
                db.SaveChanges();
                TempData["AlertMessage"] = "¡Deportista eliminado exitosamente ....!";
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