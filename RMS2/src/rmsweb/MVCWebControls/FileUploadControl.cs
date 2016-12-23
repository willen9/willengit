using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace REGAL.MVC.UI.Bootstrap
{
    public class FileUploadControl
    {
        string name;
        string description;
        string cssClass;

        public FileUploadControl(string name)
        {
            this.name = name;
            this.description = "";
            this.cssClass = "";
        }

        public FileUploadControl(string name, string description, string cssClass)
        {
            this.name = name;
            this.description = description;
            this.cssClass = cssClass;
        }

        public MvcHtmlString RenderHtml()
        {
            StringBuilder objBuilder = new StringBuilder();
            objBuilder.AppendFormat("<div id='{0}' class='modal {1}' tabindex='-1' role='dialog' aria-hidden='true'>", name, cssClass);
            objBuilder.Append(Environment.NewLine);
            objBuilder.AppendLine(" <div class='modal-dialog'>");
            objBuilder.AppendLine("     <div class='modal-content'>");
            objBuilder.AppendLine("         <div class='modal-header'>");
            objBuilder.AppendLine("             <h4 class='modal-title'>Upload</h4>");
            objBuilder.AppendLine("         </div>");
            objBuilder.AppendLine("         <div class='modal-body'>");
            objBuilder.AppendLine("             <div class='form-group'>");
            objBuilder.AppendLine("                 <label for='id_fileUpload' class='control-label'>Attachment</label>");
            objBuilder.AppendLine("                 <div>");
            objBuilder.AppendLine("                     <input type='file' name='fileUpload' id='id_fileUpload' />");
            objBuilder.AppendFormat("                     <p><small>{0}</small></p>", description);
            objBuilder.Append(Environment.NewLine);
            objBuilder.AppendLine("                 </div>");
            objBuilder.AppendLine("             </div>");
            objBuilder.AppendLine("         </div>");
            objBuilder.AppendLine("         <div class='modal-footer'>");
            objBuilder.AppendFormat("             <button type='button' class='btn btn-default' id='submitButton-{0}'>Upload</button>", name);
            objBuilder.Append(Environment.NewLine);
            objBuilder.AppendLine("             <button type='button' class='btn btn-link' data-dismiss='modal' aria-label='Close'>Cancel</button>");
            objBuilder.AppendLine("         </div>");
            objBuilder.AppendLine("     </div>");
            objBuilder.AppendLine(" </div>");
            objBuilder.AppendLine("</div>");

            return new MvcHtmlString(objBuilder.ToString());
        }
    }
}
