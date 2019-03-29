<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Controller.login" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS" %>
<%@ Import namespace="FangPage.WMS.Model" %>

<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.9*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html>\r\n");
	ViewBuilder.Append("<html lang=\"zh-CN\" class=\"default-layout\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n");
	ViewBuilder.Append("    <meta name=\"renderer\" content=\"webkit\">\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge,chrome=1\">\r\n");
	ViewBuilder.Append("    <title>" + siteconfig.sitetitle.ToString().Trim() + "登录 " + siteconfig.version.ToString().Trim() + " - Powered By FangPage.COM</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"" + webpath.ToString() + "sites/exam/statics/css/login.css\" rel=\"stylesheet\">\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-1.8.2.min.js\"></");
	ViewBuilder.Append("script>\r\n");

	ViewBuilder.Append("<!--[if lte IE 8]><link id=\"ie8Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie8.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if lte IE 7]><link id=\"ie7Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie7.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if gte IE 9]><link id=\"ie9Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/gte-ie9.css\")}\"/><![endif]-->\r\n");


	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#register\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href = \"register.aspx\";\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#sumitlogin\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        var _hmt = _hmt || [];\r\n");
	ViewBuilder.Append("        (function () {\r\n");
	ViewBuilder.Append("            var hm = document.createElement(\"script\");\r\n");
	ViewBuilder.Append("            hm.src = \"//hm.baidu.com/hm.js?35483845f92e384129fb5d03f9d7c3cf\";\r\n");
	ViewBuilder.Append("            var s = document.getElementsByTagName(\"script\")[0];\r\n");
	ViewBuilder.Append("            s.parentNode.insertBefore(hm, s);\r\n");
	ViewBuilder.Append("        })();\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <div class=\"wrap\">\r\n");
	ViewBuilder.Append("        <div class=\"repeat-x default-t\">\r\n");
	ViewBuilder.Append("            <div class=\"header-left layout default-lt\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"header-right layout default-rt\"></div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"default-left repeat-y default-l\"></div>\r\n");
	ViewBuilder.Append("        <div class=\"default-right repeat-y default-r\"></div>\r\n");
	ViewBuilder.Append("        <div class=\"header simple-header\">\r\n");
	ViewBuilder.Append("            <div class=\"container\"><a href=\"http://fpexam.fangpage.com\" target=\"_blank\" class=\"logo\">\r\n");
	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/logo.png\" alt=\"方配在线考试系统\"></a></div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"container body-wrap main\">\r\n");
	ViewBuilder.Append("            <h1 class=\"page-header first\"><span class=\"text\"><span class=\"sprite sprite-notepad\"></span>登录</span>\r\n");
	ViewBuilder.Append("                <hr>\r\n");
	ViewBuilder.Append("            </h1>\r\n");
	ViewBuilder.Append("            <div class=\"row\">\r\n");
	ViewBuilder.Append("                <div class=\"span8 offset4 login-form-wrap form-wrap\">\r\n");
	ViewBuilder.Append("                    <form id=\"frmpost\" name=\"frmpost\" method=\"post\" action=\"\" class=\"form\">\r\n");
	ViewBuilder.Append("                        <div class=\"form-content\">\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap text-item-wrap error\">\r\n");
	ViewBuilder.Append("                                <label for=\"username\">用户：</label>\r\n");
	ViewBuilder.Append("                                <span class=\"text-wrap\">\r\n");
	ViewBuilder.Append("                                    <input id=\"username\" name=\"username\" type=\"text\" value=\"\" placeholder=\"请输入用户名/邮箱/手机号\">\r\n");
	ViewBuilder.Append("                                </span><span class=\"item-message help-inline\"></span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap text-item-wrap\">\r\n");
	ViewBuilder.Append("                                <label for=\"password\">密码：</label>\r\n");
	ViewBuilder.Append("                                <span class=\"text-wrap\">\r\n");
	ViewBuilder.Append("                                    <input id=\"password\" name=\"password\" type=\"password\" value=\"\" placeholder=\"请输入密码\">\r\n");
	ViewBuilder.Append("                                </span><span class=\"item-message help-inline\"></span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                        <div class=\"button-row\">\r\n");
	ViewBuilder.Append("                            <button type=\"submit\" class=\"visible-hidden\"></button>\r\n");
	ViewBuilder.Append("                            <!--<button tabindex=\"-1\" type=\"button\" class=\"btn btn-link link-button\">找回密码</button>-->\r\n");
	ViewBuilder.Append("                            <span id=\"sumitlogin\" class=\"btn btn-primary submit-button\"><span class=\"btn-inner\">登&nbsp;录</span></span>\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap\">\r\n");
	ViewBuilder.Append("                              <label for=\"persistent\">\r\n");
	ViewBuilder.Append("                                <input id=\"persistent\" name=\"persistent\" type=\"checkbox\" value=\"true\">\r\n");
	ViewBuilder.Append("                                下次自动登录<span class=\"item-message help-inline\"></span></label>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                        <div class=\"form-message text-red\"></div>\r\n");
	ViewBuilder.Append("                    </form>\r\n");

	if (regconfig.regstatus==1)
	{

	ViewBuilder.Append("                    <span id=\"register\" class=\"btn btn-paper btn-paper-xxlarge link-button signup-button\"><span class=\"fir fir-btn-paper-signup\"></span></span>\r\n");

	}	//end if

	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"footer repeat-x default-b\">\r\n");
	ViewBuilder.Append("            <div class=\"footer-left layout default-lb\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"footer-right layout default-rb\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"container\">\r\n");
	ViewBuilder.Append("            <div class=\"links\">\r\n");
	ViewBuilder.Append("            <p class=\"text-center\">\r\n");
	ViewBuilder.Append("               Copyright &copy; 2013-" + FangPage.MVC.FPUtils.GetDate(datetime,"yyyy") + " <a target=\"_blank\" href=\"http://www.fangpage.com\">方配软件(FangPage.Com)</a>&nbsp;&nbsp;版权所有，版本：" + siteconfig.version.ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </p>\r\n");
	ViewBuilder.Append("            <p class=\"text-center\">\r\n");
	ViewBuilder.Append("                官方网站：<a href=\"http://www.fangpage.com\" target=\"_blank\">http://www.fangpage.com</a>，QQ：12677206，电话：13481092810\r\n");
	ViewBuilder.Append("            </p>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
