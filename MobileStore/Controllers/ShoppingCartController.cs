using MobileStore.Models;
using MobileStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        MobileStoreContext storeDB = new MobileStoreContext();

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);


            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            var addedItem = storeDB.products
                .Single(item => item.ProductId == id);


            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);


            string itemName = storeDB.carts
                .Single(item => item.RecordId == id).Product.Title;


            int itemCount = cart.RemoveFromCart(id);


            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}