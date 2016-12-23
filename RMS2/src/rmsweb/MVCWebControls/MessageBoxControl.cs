using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace REGAL.MVC.UI.Bootstrap
{
    public class MessageBoxControl
    {
        string name;
        string bodyText;
        string cssClass;
        MessageBoxButtons buttons;

        public MessageBoxControl(string name, string bodyText, MessageBoxButtons buttons, string cssClass)
        {
            this.name = name;
            this.bodyText = bodyText;
            this.buttons = buttons;
            this.cssClass = cssClass;
        }

        public MvcHtmlString RenderHtml()
        {
            StringBuilder objBuilder = new StringBuilder();
            objBuilder.AppendFormat("<div id='{0}' class='modal {1}' tabindex='-1' role='dialog' aria-hidden='true'>", name, cssClass);
            objBuilder.Append(Environment.NewLine);
            objBuilder.AppendLine(" <div class='modal-dialog'>");
            objBuilder.AppendLine("     <div class='modal-content'>");
            objBuilder.AppendLine("         <div class='modal-header''>");
            objBuilder.AppendLine("             <h4 class='modal-title'><i class='fa fa-info-circle'></i> Upload</h4>");
            objBuilder.AppendLine("         </div>");
            objBuilder.AppendLine("         <div class='modal-body' style='font-size:14px;'>");
            objBuilder.AppendFormat("            <p>{0}</p>", bodyText);
            objBuilder.Append(Environment.NewLine);
            objBuilder.AppendLine("         </div>");
            objBuilder.AppendLine("         <div class='modal-footer'>");

            if (buttons == MessageBoxButtons.OK)
            {
                objBuilder.AppendFormat("             <button type='button' id='{0}-button-ok' class='btn btn-default' data-dismiss='modal' aria-label='Close'>OK</button>", name);
                objBuilder.Append(Environment.NewLine);
            }
            else if (buttons == MessageBoxButtons.OKCannel)
            {
                objBuilder.AppendFormat("             <button type='button' id='{0}-button-ok' class='btn btn-default'>OK</button>", name);
                objBuilder.Append(Environment.NewLine);
                objBuilder.AppendFormat("             <button type='button' id='{0}-button-cancel' class='btn btn-default' data-dismiss='modal' aria-label='Close'>Cancel</button>", name);
                objBuilder.Append(Environment.NewLine);
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {
                objBuilder.AppendFormat("             <button type='button' id='{0}-button-yes' class='btn btn-default'>Yes</button>", name);
                objBuilder.Append(Environment.NewLine);
                objBuilder.AppendFormat("             <button type='button' id='{0}-button-no' class='btn btn-default' data-dismiss='modal' aria-label='Close'>No</button>", name);
                objBuilder.Append(Environment.NewLine);
            }
            objBuilder.AppendLine("         </div>");
            objBuilder.AppendLine("     </div> <!-- /.modal-content -->");
            objBuilder.AppendLine(" </div> <!-- /.modal-dialog -->");
            objBuilder.AppendLine("</div> <!-- /.modal -->");

            return new MvcHtmlString(objBuilder.ToString());
        }
    }
}
