<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.taskmanage" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<%@ Import namespace="FangPage.WMS.Task" %>
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
	ViewBuilder.Append("<title>系统计划任务管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
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
	ViewBuilder.Append("        PageNav(\"系统计划任务管理," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("    function DownLoadFile(tid) {\r\n");
	ViewBuilder.Append("        $(\"#action\").val(\"download\");\r\n");
	ViewBuilder.Append("        $(\"#tid\").val(tid);\r\n");
	ViewBuilder.Append("        $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function TaskRun(tid) {\r\n");
	ViewBuilder.Append("        $(\"#action\").val(\"run\");\r\n");
	ViewBuilder.Append("        $(\"#tid\").val(tid);\r\n");
	ViewBuilder.Append("        $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("    <input id=\"action\" name=\"action\" type=\"hidden\" value=\"\">\r\n");
	ViewBuilder.Append("    <input id=\"tid\" name=\"tid\" type=\"hidden\" value=\"\">\r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("    <td>\r\n");
	ViewBuilder.Append("    <div class=\"newslist\">\r\n");
	ViewBuilder.Append("        <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"> <a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"taskadd.aspx\">新建</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/tasks.gif) 2px 6px no-repeat\"><a href=\"taskinstall.aspx\">安装</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/newapp.gif) 2px 6px no-repeat\"><a href=\"tasklog.aspx\">执行日志</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"" + fullname.ToString() + "\">刷新</a> </li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>系统计划任务管理</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("    <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("                <td width=\"40\"><input id=\"checkall\" name=\"checkall\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  计划任务名称\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  任务执行频率\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  上次执行时间\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  任务执行状态\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td width=\"110\">\r\n");
	ViewBuilder.Append("                  操作\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(TaskInfo item in tasklist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("                <td><input id=\"chkid\" name=\"chkid\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + item.name.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");

	if (item.timeofday==-1)
	{

	ViewBuilder.Append("                     周期执行：每隔" + item.minutes.ToString().Trim() + "分钟执行一次\r\n");

	}
	else
	{

	ViewBuilder.Append("                     定时执行：每天" + (item.timeofday/60).ToString().Trim() + "时" + (item.timeofday%60).ToString().Trim() + "分执行一次\r\n");

	}	//end if

	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");

	if (item.lastexecuted=="")
	{

	ViewBuilder.Append("                    从未执行\r\n");

	}
	else
	{

	ViewBuilder.Append("                   " + FangPage.MVC.FPUtils.GetDate(item.lastexecuted,"yyyy-MM-dd HH:mm:ss") + "\r\n");

	}	//end if

	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");

	if (item.enabled==1)
	{

	ViewBuilder.Append("                      开启\r\n");

	}
	else
	{

	ViewBuilder.Append("                      开闭\r\n");

	}	//end if

	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  <a href=\"taskadd.aspx?id=" + item.id.ToString().Trim() + "\">编辑</a>&nbsp;\r\n");
	ViewBuilder.Append("                  <a href=\"javascript:TaskRun('" + item.id.ToString().Trim() + "')\">执行</a>&nbsp;\r\n");
	ViewBuilder.Append("                  <a href=\"javascript:DownLoadFile('" + item.id.ToString().Trim() + "')\">下载</a>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </td></tr>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
