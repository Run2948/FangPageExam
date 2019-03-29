<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.siteadd" %>
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
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("<title>添加编辑站点</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("<link href=\"../css/tab.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("calendar") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"input[name=btncancle]\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"sitemanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统站点管理," + rawpath.ToString() + "sitemanage.aspx|添加编辑站点," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("    function Save(tab)\r\n");
	ViewBuilder.Append("    {\r\n");
	ViewBuilder.Append("        $(\"#tab\").val(tab);\r\n");
	ViewBuilder.Append("        $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"tab\" id=\"tab\" value=\"\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("     <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul>\r\n");

	if (tab==0)
	{

	ViewBuilder.Append("             <li id=\"one1\" onclick=\"setTab('one',1,4)\" class=\"active\"><a href=\"#\">站点基本信息</a> </li>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <li id=\"one1\" onclick=\"setTab('one',1,4)\" class=\"normal\"><a href=\"#\">站点基本信息</a> </li>\r\n");

	}	//end if


	if (tab==1)
	{

	ViewBuilder.Append("              <li id=\"one2\" onclick=\"setTab('one',2,4)\" class=\"active\"><a href=\"#\">站点SEO信息</a></li>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <li id=\"one2\" onclick=\"setTab('one',2,4)\" class=\"normal\"><a href=\"#\">站点SEO信息</a></li>\r\n");

	}	//end if


	if (tab==2)
	{

	ViewBuilder.Append("            <li id=\"one3\" onclick=\"setTab('one',3,4)\" class=\"active\"><a href=\"#\">站点安全配置</a></li>\r\n");

	}
	else
	{

	ViewBuilder.Append("            <li id=\"one3\" onclick=\"setTab('one',3,4)\" class=\"normal\"><a href=\"#\">站点安全配置</a></li>\r\n");

	}	//end if


	if (tab==3)
	{

	ViewBuilder.Append("            <li id=\"one4\" onclick=\"setTab('one',4,4)\" class=\"active\"><a href=\"#\">站点访问角色</a></li>\r\n");

	}
	else
	{

	ViewBuilder.Append("            <li id=\"one4\" onclick=\"setTab('one',4,4)\" class=\"normal\"><a href=\"#\">站点访问角色</a></li>\r\n");

	}	//end if

	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("     <div id=\"con_one_1\" \r\n");

	if (tab==0)
	{

	ViewBuilder.Append(" style=\"display:block\" \r\n");

	}
	else
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">添加编辑站点</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点名称： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"name\" name=\"name\" type=\"text\" value=\"" + siteinfo.name.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点目录： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"dirpath\" name=\"dirpath\" type=\"text\" value=\"" + siteinfo.sitepath.ToString().Trim() + "\" style=\"height:21px;width:400px;\">&nbsp;以英文、数字或下划线组成，首字不能为数字</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点作者： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"author\" name=\"author\" type=\"text\" value=\"" + siteinfo.author.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点版本： </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                <input name=\"version\" type=\"text\" value=\"" + siteinfo.version.ToString().Trim() + "\" id=\"version\" style=\"width:400px;\">\r\n");
	ViewBuilder.Append("                &nbsp;<a onclick=\"document.getElementById('version').value='" + sysversion.ToString() + "'\" href=\"javascript:void(0);\">系统当前版本(V" + sysversion.ToString() + ")</a>\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 创建日期： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"createdate\" name=\"createdate\" type=\"text\" value=\"" + siteinfo.createdate.ToString().Trim() + "\" style=\"height:21px;width:400px;\" onfocus=\"WdatePicker({el:'createdate',dateFmt:'yyyy-MM-dd'})\" readonly=\"readonly\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 默认控制器： </td>\r\n");
	ViewBuilder.Append("            <td><input name=\"inherits\" type=\"text\" value=\"" + siteinfo.inherits.ToString().Trim() + "\" id=\"inherits\" style=\"width:400px;\">&nbsp;视图默认使用控制器</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 默认引用类库： </td>\r\n");
	ViewBuilder.Append("            <td><textarea name=\"import\" rows=\"5\" cols=\"30\" id=\"import\" style=\"height:80px;width:400px;\">" + siteinfo.import.ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点文件路径： </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("            <input id=\"urltype\" name=\"urltype\" type=\"radio\" \r\n");

	if (siteinfo.urltype==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"0\">原始\r\n");
	ViewBuilder.Append("            <input id=\"urltype\" name=\"urltype\" type=\"radio\" \r\n");

	if (siteinfo.urltype==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">相对\r\n");
	ViewBuilder.Append("            <input id=\"urltype\" name=\"urltype\" type=\"radio\" \r\n");

	if (siteinfo.urltype==2)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"2\">绝对\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("            <input type=\"button\" onclick=\"Save(0)\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle1\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("     <div id=\"con_one_2\" \r\n");

	if (tab==1)
	{

	ViewBuilder.Append(" style=\"display:block\" \r\n");

	}
	else
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">站点SEO信息</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点显示标题： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"sitetitle\" name=\"sitetitle\" type=\"text\" value=\"" + siteinfo.sitetitle.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点SEO关键词： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"keywords\" name=\"keywords\" type=\"text\" value=\"" + siteinfo.keywords.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 站点SEO描述： </td>\r\n");
	ViewBuilder.Append("            <td><textarea name=\"description\" rows=\"5\" cols=\"30\" id=\"description\" style=\"height:80px;width:400px;\">" + siteinfo.description.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 其他头部信息： </td>\r\n");
	ViewBuilder.Append("            <td><textarea name=\"otherhead\" rows=\"5\" cols=\"30\" id=\"otherhead\" style=\"height:80px;width:400px;\">" + siteinfo.otherhead.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("            <input type=\"button\" onclick=\"Save(1)\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle2\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("     <div id=\"con_one_3\" \r\n");

	if (tab==2)
	{

	ViewBuilder.Append(" style=\"display:block\" \r\n");

	}
	else
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">站点安全配置</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 访问IP白名单： </td>\r\n");
	ViewBuilder.Append("            <td><textarea name=\"ipaccess\" id=\"ipaccess\" rows=\"5\" cols=\"30\" style=\"height:80px;width:400px;\">" + siteinfo.ipaccess.ToString().Trim() + "</textarea>&nbsp;每行输入一个IP，可以使用通配符*</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 访问IP黑名单：</td>\r\n");
	ViewBuilder.Append("            <td><textarea name=\"ipdenyaccess\" id=\"ipdenyaccess\" rows=\"5\" cols=\"30\" style=\"height:80px;width:400px;\">" + siteinfo.ipdenyaccess.ToString().Trim() + "</textarea>&nbsp;每行输入一个IP，可以使用通配符*</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 是否关闭站点： </td>\r\n");
	ViewBuilder.Append("            <td> \r\n");
	ViewBuilder.Append("            <input id=\"closed\" name=\"closed\" type=\"radio\" \r\n");

	if (siteinfo.closed==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">关闭\r\n");
	ViewBuilder.Append("            <input id=\"closed\" name=\"closed\" type=\"radio\" \r\n");

	if (siteinfo.closed==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"0\">开通\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 显示关闭原因： </td>\r\n");
	ViewBuilder.Append("            <td><textarea name=\"closedreason\" id=\"closedreason\" rows=\"5\" cols=\"30\" style=\"height:80px;width:400px;\">" + siteinfo.closedreason.ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("            <input type=\"button\" onclick=\"Save(2)\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle3\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("     <div id=\"con_one_4\" \r\n");

	if (tab==3)
	{

	ViewBuilder.Append(" style=\"display:block\" \r\n");

	}
	else
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">站点访问角色</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("                <td colspan=\"2\">\r\n");
	ViewBuilder.Append("                    <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                    <tr>\r\n");

	loop__id=0;
	foreach(RoleInfo item in rolelist)
	{
	loop__id++;


	if (ischecked(item.id,siteinfo.roles))
	{

	ViewBuilder.Append("                        <td><input id=\"roles\" name=\"roles\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\" checked=\"checked\">" + item.name.ToString().Trim() + "</td>\r\n");

	}
	else
	{

	ViewBuilder.Append("                        <td><input id=\"roles\" name=\"roles\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\">" + item.name.ToString().Trim() + "</td>\r\n");

	}	//end if


	if (loop__id%4==0)
	{

	ViewBuilder.Append("                        </tr>\r\n");
	ViewBuilder.Append("                        <tr>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    </table>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("            <input type=\"button\" onclick=\"Save(3)\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle4\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
