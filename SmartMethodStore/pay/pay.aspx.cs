using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMethodStore.pay
{
    public partial class pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonContinueToPayment_Click(object sender, EventArgs e)
        {
            using (StoreDataContext Data = new StoreDataContext())
            {
                Order newOrder = new Order();
                newOrder.OrderAddress1 = TextBoxOrderAddress1.Text;
                newOrder.OrderAddress2 = TextBoxOrderAddress2.Text;
                newOrder.OrderTown = TextBoxOrderTown.Text;
                newOrder.OrderRegion = TextBoxOrderRegion.Text;
                newOrder.OrderPostCode = TextBoxOrderPostCode.Text;
                newOrder.CountryID = Convert.ToInt32(DropDownListCountry.SelectedValue);
                newOrder.OrderDate = DateTime.Now;
                newOrder.OrderPaid = false;
                newOrder.OrderSent = false;
                Data.Orders.InsertOnSubmit(newOrder);
                Data.SubmitChanges();

                List<int> products = (List<int>)Session["chart"];
                foreach (int productId in products)
                {
                    OrderProduct newOrderProduct = new OrderProduct();
                    newOrderProduct.OrderID = newOrder.OrderID;
                    newOrderProduct.ProductID = productId;
                    Data.OrderProducts.InsertOnSubmit(newOrderProduct);
                }

                Data.SubmitChanges();


                //sends user to payment system
            }
        }
    }
}