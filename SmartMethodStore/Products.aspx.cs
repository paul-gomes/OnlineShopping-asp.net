using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMethodStore
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridViewProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "addToCart")
            {
                int rowClicked = Convert.ToInt32(e.CommandArgument);
                int productId = Convert.ToInt32(GridViewProduct.DataKeys[rowClicked].Value);

                List<int> productsInCart = (List<int>)Session["cart"];

                if (productsInCart == null) 
                {
                    productsInCart = new List<int>();
                }

                productsInCart.Add(productId);
                Session["cart"] = productsInCart;

            }
        }
    }
}