using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileStore.Models;
using System.IO;

namespace MobileStore.Controllers
{
    public class ProductController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.Brand).Include(p => p.Category);
            return View(products.ToList());
        }

        //For Each category(Mobiles,tab,notebook)
        public ActionResult Category(int? id)
        {
            int productId = id == null ? 1 : (int)id;  //if requested id is null, set is as 1
            if (db.categories.Find(productId) == null)  //if category does not exist, return Error
                return View("Error");
            var model = db.categories.Find(productId).products.ToList(); //return list of projects in requested category
            return View(model);
        }

        //For Each category(Mobiles,tab,notebook)
        public ActionResult Brand(int? id)
        {
            int brandId = id == null ? 1 : (int)id;  //if requested id is null, set is as 1
            if (db.categories.Find(brandId) == null)  //if category does not exist, return Error
                return View("Error");
            var model = db.brands.Find(brandId).products.ToList(); //return list of projects in requested category
            return View(model);
        }

        //Featured Product User
        public ActionResult FeaturedProduct()
        {
            var model = db.products.Include(p => p.Brand).Include(p => p.Category);
            return PartialView(model.ToList());
        }
        //New Product User
        public ActionResult NewProduct()
        {
            var model = db.products.Include(p => p.Brand).Include(p => p.Category);
            return PartialView(model.ToList());
        }

        
     

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult DetailsForUserProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {   
                return HttpNotFound();
            }
            return View(product);
        }
        //Product Details User

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.brands, "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(db.categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Create([Bind(Include ="ProductId,Title,Description,Price,ImageUrl,AvailableProduct,SoldProduct,CategoryId,BrandId")] Product product, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {

                //image url 

                if (ImageUrl != null)
                {
                    FileInfo file = new FileInfo(ImageUrl.FileName);
                    string newFile = Guid.NewGuid().ToString("N") + file.Extension;
                    ImageUrl.SaveAs(Server.MapPath("~/Content/Upload/ProductImages/" + newFile));

                    product.ImageUrl = newFile;
                }//end

                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.brands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.brands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Title,Description,Price,ImageUrl,AvailableProduct,SoldProduct,CategoryId,BrandId")] Product product, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {


                //if (ImageUrl != null)
                //{
                //    FileInfo file = new FileInfo(ImageUrl.FileName);
                //    string newFile = Guid.NewGuid().ToString("N") + file.Extension;
                //    ImageUrl.SaveAs(Server.MapPath("~/Content/Upload/ProductImages/" + newFile));

                //    product.ImageUrl = newFile;
                //}//end


                if (ImageUrl != null)
                {
                    FileInfo file = new FileInfo(ImageUrl.FileName);
                    string newFile = Guid.NewGuid().ToString("N") + file.Extension;

                    if (System.IO.File.Exists(Server.MapPath("~/Content/Upload/ProductImages/" + product.ImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/Upload/ProductImages/" + product.ImageUrl));

                    }
                    ImageUrl.SaveAs(Server.MapPath("~/Content/Upload/ProductImages/" + newFile));

                    product.ImageUrl = newFile;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.brands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.products.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Content/Upload/ProductImages/" + product.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Upload/ProductImages/" + product.ImageUrl));

            }
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
