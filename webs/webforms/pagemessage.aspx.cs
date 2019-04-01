using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webs.webforms
{
    public partial class pagemessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LMsg.Text = (string)Session["message"];
        }
    }
}