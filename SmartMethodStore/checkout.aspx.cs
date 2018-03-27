using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMethodStore
{
    public partial class checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!Page.IsPostBack)
            {
                getProductsFromCart();
 
            }
             

        }
        
       protected void GridViewProduct_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           if (e.CommandName == "removeFromCart")
           {
               int rowClicked = Convert.ToInt32(e.CommandArgument);
               int productID = Convert.ToInt32(GridViewProduct.DataKeys[rowClicked].Value);

               List<int> productsInCart = (List<int>)Session["cart"];
               productsInCart.Remove(productID);
               Session["cart"] = productsInCart;
               getProductsFromCart();
           }
       }
       
       private void getProductsFromCart()
       {
           if (Session["cart"] != null) 
           {
               using (StoreDataContext Data = new StoreDataContext())
               {
                   List<int> cart = (List<int>)Session["cart"];
                   var products = Data.Products.Where(product =>
                       cart.Contains(product.ProductID));
                   GridViewProduct.DataSource = products;
                   GridViewProduct.DataBind();
               }
           }
       }

       protected void ButtonContinueToPayment_Click(object sender, EventArgs e)
       {
           Response.Redirect("~/pay/pay.aspx");
       }
    }
}