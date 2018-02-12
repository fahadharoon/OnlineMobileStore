using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Models
{
    public class ShoppingCart
    {
        MobileStoreContext storeDB = new MobileStoreContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Product product)
        {
            
            var cartItem = storeDB.carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.ProductId);

            if (cartItem == null)
            {
                
                cartItem = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.carts.Add(cartItem);
            }
            else
            {
                
                cartItem.Count++;
            }
            
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            
            var cartItem = storeDB.carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.carts.Remove(cartItem);
                }
                
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.carts.Remove(cartItem);
            }
            
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {

            int? count = (from cartItems in storeDB.carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            
            decimal? total = (from cartItems in storeDB.carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(CartOrder order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            
            foreach (var item in cartItems)
            {
                var cartOrderDetail = new CartOrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Product.Price);

                storeDB.orderDetails.Add(cartOrderDetail);

            }
            
            order.Total = orderTotal;

            
            storeDB.SaveChanges();
            
            EmptyCart();
            
            return order.OrderId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    
                    Guid tempCartId = Guid.NewGuid();
                    
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public void MigrateCart(string Email)
        {
            var shoppingCart = storeDB.carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = Email;
            }
            storeDB.SaveChanges();
        }

    }
}