<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.usercheckmanage" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：4.5*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>用户实名认证</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"../css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("            $('input[name=chkdel]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitdel\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"你确定要删除吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统用户管理,user/usermanager.aspx|用户实名认证," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"" + pagename.ToString() + "\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/return.gif) 2px 6px no-repeat\"> <a href=\"usermanage.aspx\">返回</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>用户实名认证</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"40\"><input id=\"checkall\" name=\"checkall\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("        	  <td>用户名</td>\r\n");
	ViewBuilder.Append("              <td>真实姓名</td>\r\n");
	ViewBuilder.Append("        	  <td>身份证号</td>\r\n");
	ViewBuilder.Append("              <td>性别</td>\r\n");
	ViewBuilder.Append("              <td>身份证正面</td>\r\n");
	ViewBuilder.Append("              <td>身份证反面</td>\r\n");
	ViewBuilder.Append("              <td>手持身份证</td>\r\n");
	ViewBuilder.Append("              <td>状态</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">操作</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(FullUserInfo item in userinfolist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.username.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.realname.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.idcard.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.sex==1)
	{

	ViewBuilder.Append("                男\r\n");

	}
	else if (item.sex==0)
	{

	ViewBuilder.Append("                女\r\n");

	}
	else if (item.sex==-1)
	{

	ViewBuilder.Append("                保密\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.idcardface!="")
	{

	ViewBuilder.Append("                  <img src=\"../images/have.png\">\r\n");

	}
	else
	{

	ViewBuilder.Append("                  <img src=\"../images/no.png\">\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.idcardback!="")
	{

	ViewBuilder.Append("                  <img src=\"../images/have.png\">\r\n");

	}
	else
	{

	ViewBuilder.Append("                  <img src=\"../images/no.png\">\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.idcardper!="")
	{

	ViewBuilder.Append("                  <img src=\"../images/have.png\">\r\n");

	}
	else
	{

	ViewBuilder.Append("                  <img src=\"../images/no.png\">\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.isidcard==1)
	{

	ViewBuilder.Append("              <span style=\"color:Blue\">已审核</span>\r\n");

	}
	else if (item.isidcard==-1)
	{

	ViewBuilder.Append("              <span style=\"color:red\">审核失败</span>\r\n");

	}
	else
	{

	ViewBuilder.Append("              审核中\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("                <a style=\"color: #1317fc\" href=\"usercheckinfo.aspx?uid=" + item.id.ToString().Trim() + "\">审核</a>&nbsp;\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("       <td align=\"left\">共有" + pager.total.ToString().Trim() + "条记录，页次：" + pager.pageindex.ToString().Trim() + "/" + pager.pagecount.ToString().Trim() + "，" + pager.pagesize.ToString().Trim() + "条每页</td>\r\n");
	ViewBuilder.Append("       <td align=\"right\"><div class=\"pages\">" + pager.pagenum.ToString().Trim() + "</div></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("  </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
