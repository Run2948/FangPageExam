<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.usermanage" %>
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
	ViewBuilder.Append("<title>系统用户管理</title>\r\n");
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
	ViewBuilder.Append("        $(\"#submitcredit\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"credit\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统用户管理,user/" + pagename.ToString() + "\");\r\n");
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
	ViewBuilder.Append("              <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"javascript:void();\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"useradd.aspx?url=" + cururl.ToString() + "\">添加</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/search.gif) 2px 6px no-repeat\"><a href=\"usersearch.aspx?s_roleid=" + s_roleid.ToString() + "&s_departid=" + s_departid.ToString() + "&s_username=" + s_username.ToString() + "&s_realname=" + s_realname.ToString() + "&s_mobile=" + s_mobile.ToString() + "&s_email=" + s_email.ToString() + "&keyword=" + keyword.ToString() + "\">搜索</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/xls.gif) 2px 6px no-repeat\"><a href=\"userimport.aspx\">导入用户</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/user.gif) 2px 6px no-repeat\"><a href=\"usercheckmanage.aspx\">实名认证</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/score.gif) 2px 6px no-repeat\"><a href=\"creditmanage.aspx\">积分详情</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/credit.png) 2px 6px no-repeat\"><a id=\"submitcredit\" href=\"javascript:void();\">积分充值</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"usermanage.aspx\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>系统用户管理</strong></li>\r\n");
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
	ViewBuilder.Append("        	  <td width=\"60\">用户名</td>\r\n");
	ViewBuilder.Append("              <td width=\"80\">用户角色</td>\r\n");
	ViewBuilder.Append("              <td width=\"50\">姓名</td>\r\n");
	ViewBuilder.Append("              <td>手机号码</td>\r\n");
	ViewBuilder.Append("              <td>电子邮箱</td>\r\n");
	ViewBuilder.Append("              <td>所在部门</td>\r\n");
	ViewBuilder.Append("              <td>用户级别</td>\r\n");
	ViewBuilder.Append("              <td width=\"50\">经验值</td>\r\n");
	ViewBuilder.Append("              <td width=\"40\">积分</td>\r\n");
	ViewBuilder.Append("        	  <td width=\"120\">最后活动时间</td>\r\n");
	ViewBuilder.Append("              <td width=\"40\">状态</td>\r\n");
	ViewBuilder.Append("              <td width=\"100\">操作</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(UserInfo item in userlist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.username.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.RoleInfo.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.realname.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.mobile.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.email.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.Department.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.UserGrade.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.exp.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.credits.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + FangPage.MVC.FPUtils.GetDate(item.lastvisit,"yyyy-MM-dd HH:mm:ss") + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.onlinestate==1)
	{

	ViewBuilder.Append("              <span style=\"color:Blue\">在线</span>\r\n");

	}
	else
	{

	ViewBuilder.Append("              离线\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("              <a style=\"color: #1317fc\" href=\"useradd.aspx?id=" + item.id.ToString().Trim() + "&url=" + cururl.ToString() + "\">编辑</a>&nbsp;\r\n");
	ViewBuilder.Append("              <a style=\"color: #1317fc\" href=\"creditmanage.aspx?uid=" + item.id.ToString().Trim() + "\">积分详情</a>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("       <td align=\"left\">共有" + pager.total.ToString().Trim() + "个用户，页次：" + pager.pageindex.ToString().Trim() + "/" + pager.pagecount.ToString().Trim() + "，" + pager.pagesize.ToString().Trim() + "个每页</td>\r\n");
	ViewBuilder.Append("       <td align=\"right\"><div class=\"pages\">" + pager.pagenum.ToString().Trim() + "</div></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("  </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
