<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.useradd" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<%@ Import namespace="FangPage.WMS" %>
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
	ViewBuilder.Append("<title>添加编辑用户信息</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"../css/tab.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/dateselector/dateselector.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/jsaddress/jsaddress.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"input[name=btncancle]\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"usermanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统用户管理,user/usermanage.aspx|添加编辑用户信息,user/" + pagename.ToString() + "?id=" + id.ToString() + "\")\r\n");
	ViewBuilder.Append("        var myDate = new Date();\r\n");
	ViewBuilder.Append("        $(\"#dateSelector\").DateSelector({\r\n");
	ViewBuilder.Append("            ctlYearId: 'idYear',\r\n");
	ViewBuilder.Append("            ctlMonthId: 'idMonth',\r\n");
	ViewBuilder.Append("            ctlDayId: 'idDay',\r\n");
	ViewBuilder.Append("            minYear: 1900,\r\n");
	ViewBuilder.Append("            maxYear: myDate.getFullYear()\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $.initProv(\"#province\", \"#city\", \"" + fulluserinfo.province.ToString().Trim() + "\", \"" + fulluserinfo.city.ToString().Trim() + "\")\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("    <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul>\r\n");
	ViewBuilder.Append("            <li id=\"one1\" onclick=\"setTab('one',1,3)\" class=\"active\"><a href=\"#\">基本信息</a> </li>\r\n");
	ViewBuilder.Append("            <li id=\"one2\" onclick=\"setTab('one',2,3)\" class=\"normal\"><a href=\"#\">详细信息</a></li>\r\n");
	ViewBuilder.Append("            <li id=\"one3\" onclick=\"setTab('one',3,3)\" class=\"normal\"><a href=\"#\">其他信息</a></li>\r\n");
	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div id=\"con_one_1\" style=\"display:block\">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">用户基本信息</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">用户角色： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"roleid\" name=\"roleid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                <option value=\"0\">--请选择--</option>\r\n");

	loop__id=0;
	foreach(RoleInfo role in rolelist)
	{
	loop__id++;

	ViewBuilder.Append("                    <option value=\"" + role.id.ToString().Trim() + "\" \r\n");

	if (role.id==fulluserinfo.roleid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + role.name.ToString().Trim() + "</option>\r\n");

	}	//end loop

	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("              <span style=\"color:Red\">*</span>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">所在部门： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	string tree = "├";
	
	ViewBuilder.Append("              <select id=\"departid\" name=\"departid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                <option value=\"0\">--请选择--</option>\r\n");

	loop__id=0;
	foreach(Department item in deparlist)
	{
	loop__id++;

	ViewBuilder.Append("                <option value=\"" + item.id.ToString().Trim() + "\" \r\n");

	if (item.id==fulluserinfo.departid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">├" + item.name.ToString().Trim() + "</option>\r\n");
	ViewBuilder.Append("                " + GetChildDepartment(item.id,tree).ToString() + "\r\n");

	}	//end loop

	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">用户级别： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"gradeid\" name=\"gradeid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                <option value=\"0\">--请选择--</option>\r\n");

	loop__id=0;
	foreach(UserGrade itme in usergradelist)
	{
	loop__id++;

	ViewBuilder.Append("                    <option value=\"" + itme.id.ToString().Trim() + "\" \r\n");

	if (itme.id==fulluserinfo.gradeid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + itme.name.ToString().Trim() + "</option>\r\n");

	}	//end loop

	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	if (typelist.Count>0)
	{

	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\">用户类型： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");

	loop__id=0;
	foreach(TypeInfo types in typelist)
	{
	loop__id++;

	ViewBuilder.Append("            <select id=\"type\" name=\"type\">\r\n");
	ViewBuilder.Append("            <option value=\"\">" + types.name.ToString().Trim() + "</option>\r\n");

	loop__id=0;
	foreach(TypeInfo types2 in TypeBll.GetTypeList(types.id))
	{
	loop__id++;

	ViewBuilder.Append("            <option value=\"" + types2.id.ToString().Trim() + "\" \r\n");

	if (ischecked(types2.id,fulluserinfo.type))
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + types2.name.ToString().Trim() + "</option>\r\n");

	}	//end loop

	ViewBuilder.Append("            </select>\r\n");

	}	//end loop

	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	}	//end if

	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">用户名： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"username\" name=\"username\" value=\"" + fulluserinfo.username.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <span style=\"color:Red\">*</span>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">密 码： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input type=\"text\" style=\"width: 200px\" id=\"password1\" name=\"password1\">\r\n");

	if (id>0)
	{

	ViewBuilder.Append("              <span style=\"color:Red\">编辑留空不更改密码</span>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <span style=\"color:Red\">*</span>\r\n");

	}	//end if

	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">真实姓名： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"realname\" name=\"realname\" value=\"" + fulluserinfo.realname.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <input id=\"isreal\" name=\"isreal\" value=\"1\" \r\n");

	if (fulluserinfo.isreal==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">实名已验证\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">手机： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"mobile\" name=\"mobile\" value=\"" + fulluserinfo.mobile.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <input id=\"ismobile\" name=\"ismobile\" value=\"1\" \r\n");

	if (fulluserinfo.ismobile==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">手机已验证\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">Email： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"email\" name=\"email\" value=\"" + fulluserinfo.email.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <input id=\"isemail\" name=\"isemail\" value=\"1\" \r\n");

	if (fulluserinfo.isemail==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">邮箱已验证\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">经验值： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"exp\" name=\"exp\" value=\"" + fulluserinfo.exp.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <input id=\"isgrade\" name=\"isgrade\" value=\"1\" type=\"checkbox\">保存根据经验值更新用户等级\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">昵称： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"nickname\" name=\"nickname\" value=\"" + fulluserinfo.nickname.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">身份证号： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"idcard\" name=\"idcard\" value=\"" + fulluserinfo.idcard.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">性别： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input type=\"radio\" id=\"sex\" name=\"sex\" value=\"0\" \r\n");

	if (fulluserinfo.sex==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">女\r\n");
	ViewBuilder.Append("              <input type=\"radio\" id=\"sex\" name=\"sex\" value=\"1\" \r\n");

	if (fulluserinfo.sex==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">男\r\n");
	ViewBuilder.Append("              <input type=\"radio\" id=\"sex\" name=\"sex\" value=\"-1\" \r\n");

	if (fulluserinfo.sex==-1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">保密\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("            <input id=\"btnsubmit1\" name=\"btnsubmit\" type=\"submit\" value=\"保存\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle1\" name=\"btncancle\" type=\"button\" value=\"返回\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div id=\"con_one_2\" style=\"display:none\">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">用户详细信息</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">出生日期： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"idYear\" name=\"bday\" data=\"" + bday[0].ToString().Trim() + "\"></select>\r\n");
	ViewBuilder.Append("              <select id=\"idMonth\" name=\"bday\" data=\"" + bday[1].ToString().Trim() + "\"></select>\r\n");
	ViewBuilder.Append("              <select id=\"idDay\" name=\"bday\" data=\"" + bday[2].ToString().Trim() + "\"></select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">教育程度： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"education\" name=\"education\">\r\n");
	ViewBuilder.Append("              <option value=\"\">请选择</option>\r\n");
	ViewBuilder.Append("              <option value=\"小学\" \r\n");

	if (fulluserinfo.education=="小学")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">小学</option>\r\n");
	ViewBuilder.Append("              <option value=\"初中\" \r\n");

	if (fulluserinfo.education=="初中")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">初中</option>\r\n");
	ViewBuilder.Append("              <option value=\"高中\" \r\n");

	if (fulluserinfo.education=="高中")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">高中</option>\r\n");
	ViewBuilder.Append("              <option value=\"大学专科\" \r\n");

	if (fulluserinfo.education=="大学专科")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">大学专科</option>\r\n");
	ViewBuilder.Append("              <option value=\"大学本科\" \r\n");

	if (fulluserinfo.education=="大学本科")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">大学本科</option>\r\n");
	ViewBuilder.Append("              <option value=\"硕士\" \r\n");

	if (fulluserinfo.education=="硕士")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">硕士</option>\r\n");
	ViewBuilder.Append("              <option value=\"博士\" \r\n");

	if (fulluserinfo.education=="博士")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">博士</option>\r\n");
	ViewBuilder.Append("              </select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">联系电话： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"phone\" name=\"phone\" value=\"" + fulluserinfo.phone.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">QQ号码： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"qq\" name=\"qq\" value=\"" + fulluserinfo.qq.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">所在地区： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <select id=\"province\" name=\"province\"></select>\r\n");
	ViewBuilder.Append("              <select id=\"city\" name=\"city\"></select>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">详细地址： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"address\" name=\"address\" value=\"" + fulluserinfo.address.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">博客地址： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"blog\" name=\"blog\" value=\"" + fulluserinfo.blog.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">个人签名： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("             <textarea name=\"note\" rows=\"5\" cols=\"30\" id=\"note\" style=\"height:80px;width:200px;\">" + fulluserinfo.note.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("            <input id=\"btnsubmit2\" name=\"btnsubmit\" type=\"submit\" value=\"保存\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle2\" name=\"btncancle\" type=\"button\" value=\"返回\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div id=\"con_one_3\" style=\"display:none\">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">用户基他信息</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">用户积分： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              " + fulluserinfo.credits.ToString().Trim() + "\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">注册日期： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              " + FangPage.MVC.FPUtils.GetDate(fulluserinfo.joindatetime,"yyyy-MM-dd HH:mm:ss") + "\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">注册IP： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              " + fulluserinfo.regip.ToString().Trim() + "\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">最后登录IP： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              " + fulluserinfo.lastip.ToString().Trim() + "\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">最后访问时间： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              " + FangPage.MVC.FPUtils.GetDate(fulluserinfo.lastvisit,"yyyy-MM-dd HH:mm:ss") + "\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">在线状态： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	if (fulluserinfo.onlinestate==1)
	{

	ViewBuilder.Append("              在线\r\n");

	}
	else
	{

	ViewBuilder.Append("              离线\r\n");

	}	//end if

	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">用户头像： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	if (fulluserinfo.avatar!="")
	{

	ViewBuilder.Append("              <img src=\"" + fulluserinfo.avatar.ToString().Trim() + "\" width=\"150\" height=\"150\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"../images/default.gif\" width=\"150\" height=\"150\">\r\n");

	}	//end if

	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
