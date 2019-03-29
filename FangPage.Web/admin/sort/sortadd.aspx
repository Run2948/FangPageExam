<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sortadd" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：4.5*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>添加编辑栏目</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" href=\"" + webpath.ToString() + "plugins/editor/themes/default/default.css\">\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/editor/kindeditor.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/editor/lang/zh_CN.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"sortmanage.aspx?channelid=" + channelid.ToString() + "\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"站点栏目管理,{rawpath}sortmanage.aspx?channelid=" + channelid.ToString() + "|添加编辑栏目," + rawpath.ToString() + "" + pagename.ToString() + "?channelid=" + channelid.ToString() + "&id=" + id.ToString() + "\");\r\n");
	ViewBuilder.Append("        var editor = KindEditor.editor({\r\n");
	ViewBuilder.Append("            uploadJson: '" + webpath.ToString() + "tools/uploadajax.aspx?sortid=" + id.ToString() + "',\r\n");
	ViewBuilder.Append("            fileManagerJson: '" + webpath.ToString() + "tools/filemanagerajax.aspx',\r\n");
	ViewBuilder.Append("            allowFileManager: true\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('#image1').click(function () {\r\n");
	ViewBuilder.Append("            editor.loadPlugin('image', function () {\r\n");
	ViewBuilder.Append("                editor.plugin.imageDialog({\r\n");
	ViewBuilder.Append("                    imageUrl: $('#img').val(),\r\n");
	ViewBuilder.Append("                    clickFn: function (url, title, width, height, border, align) {\r\n");
	ViewBuilder.Append("                        $('#img').val(url);\r\n");
	ViewBuilder.Append("                        $(\"#sortimg\").attr(\"src\", url); \r\n");
	ViewBuilder.Append("                        editor.hideDialog();\r\n");
	ViewBuilder.Append("                    }\r\n");
	ViewBuilder.Append("                });\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\" enctype=\"multipart/form-data\">\r\n");
	ViewBuilder.Append("  <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">添加编辑栏目</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">所属频道： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"channelid\" name=\"channelid\" \r\n");

	if (parentid!=0)
	{

	ViewBuilder.Append(" disabled=\"disabled\" \r\n");

	}	//end if

	ViewBuilder.Append(" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                  <option value=\"0\">请选择栏目频道</option>\r\n");

	loop__id=0;
	foreach(ChannelInfo item in channellist)
	{
	loop__id++;

	ViewBuilder.Append("                  <option value=\"" + item.id.ToString().Trim() + "\" \r\n");

	if (item.id==channelid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + item.name.ToString().Trim() + "</option>\r\n");

	}	//end loop

	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">父级栏目： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	string tree = "├";
	
	ViewBuilder.Append("              <select id=\"parentid\" name=\"parentid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                  <option value=\"0\">根栏目</option>\r\n");

	loop__id=0;
	foreach(SortInfo item in sortlist)
	{
	loop__id++;

	ViewBuilder.Append("                  <option value=\"" + item.id.ToString().Trim() + "\" \r\n");

	if (item.id==parentid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">├" + item.name.ToString().Trim() + "</option>\r\n");
	ViewBuilder.Append("                  " + GetChildSort(item.id,tree).ToString() + "\r\n");

	}	//end loop

	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">栏目功能： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"appid\" name=\"appid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                  <option value=\"0\">无</option>\r\n");

	loop__id=0;
	foreach(SortAppInfo item in sortapplist)
	{
	loop__id++;

	ViewBuilder.Append("                  <option value=\"" + item.id.ToString().Trim() + "\" \r\n");

	if (item.id==appid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + item.name.ToString().Trim() + "</option>\r\n");

	}	//end loop

	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">栏目名称： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"name\" name=\"name\" value=\"" + sortinfo.name.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">栏目标识： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"markup\" name=\"markup\" value=\"" + sortinfo.markup.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">栏目描述： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 199px\" id=\"description\" name=\"description\" value=\"" + sortinfo.description.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">外部链接： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 199px\" id=\"otherurl\" name=\"otherurl\" value=\"" + sortinfo.otherurl.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">栏目图标： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	string hidden = "";
	

	if (sortinfo.hidden==1)
	{

	 hidden = "_hidden";
	

	}	//end if


	if (sortinfo.icon!="")
	{

	ViewBuilder.Append("              <img src=\"" + sortinfo.icon.ToString().Trim() + "\" width=\"16\" height=\"16\">\r\n");

	}
	else if (sortinfo.subcounts>0)
	{

	ViewBuilder.Append("              <img src=\"../images/folders" + hidden.ToString() + ".gif\" width=\"16\" height=\"16\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"../images/folder" + hidden.ToString() + ".gif\" width=\"16\" height=\"16\">\r\n");

	}	//end if

	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">上传图标： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"uploadimg\" name=\"uploadimg\" type=\"file\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">栏目图片： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	if (sortinfo.img!="")
	{

	ViewBuilder.Append("              <img id=\"sortimg\" src=\"" + sortinfo.img.ToString().Trim() + "\" width=\"160\" height=\"160\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img id=\"sortimg\" src=\"../images/default.gif\" width=\"160\" height=\"160\">\r\n");

	}	//end if

	ViewBuilder.Append("              <br>\r\n");
	ViewBuilder.Append("              <input type=\"hidden\" id=\"img\" name=\"img\" value=\"" + sortinfo.img.ToString().Trim() + "\"> \r\n");
	ViewBuilder.Append("              <input type=\"button\" id=\"image1\" value=\"选择图片\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">信息分类： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	loop__id=0;
	foreach(TypeInfo types in typelist)
	{
	loop__id++;

	ViewBuilder.Append("            <input id=\"types\" name=\"types\" value=\"" + types.id.ToString().Trim() + "\" \r\n");

	if (ischecked(types.id,sortinfo.types))
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">" + types.name.ToString().Trim() + "\r\n");

	}	//end loop

	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">隐藏栏目： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input id=\"hidden\" name=\"hidden\" value=\"1\" type=\"checkbox\" \r\n");

	if (sortinfo.hidden==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td><input name=\"submit\" value=\"保存\" type=\"submit\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\"></td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</form>\r\n");


	if (ispost)
	{

	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    layer.msg('" + msg.ToString() + "', 0, 1);\r\n");
	ViewBuilder.Append("    var bar = 0;\r\n");
	ViewBuilder.Append("    count();\r\n");
	ViewBuilder.Append("    function count() {\r\n");
	ViewBuilder.Append("        bar = bar + 4;\r\n");
	ViewBuilder.Append("        if (bar < 80) {\r\n");
	ViewBuilder.Append("            setTimeout(\"count()\", 100);\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        else {\r\n");
	ViewBuilder.Append("            window.location.href = \"" + link.ToString() + "\";\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");

	}	//end if



	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
