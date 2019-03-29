<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Controller.register" %>
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
	ViewBuilder.Append("    <title>用户注册 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge, chrome = 1\">\r\n");
	ViewBuilder.Append("    <link href=\"" + webpath.ToString() + "sites/exam/statics/css/login.css\" rel=\"stylesheet\">\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-1.8.2.min.js\"></");
	ViewBuilder.Append("script>\r\n");

	ViewBuilder.Append("<!--[if lte IE 8]><link id=\"ie8Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie8.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if lte IE 7]><link id=\"ie7Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie7.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if gte IE 9]><link id=\"ie9Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/gte-ie9.css\")}\"/><![endif]-->\r\n");


	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#login\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href = \"login.aspx\";\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#sumitreg\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("        });\r\n");
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
	ViewBuilder.Append("            <div class=\"container\"><a href=\"index.aspx\" class=\"logo\">\r\n");
	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/logo.png\" alt=\"方配在线考试系统\"></a></div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"container body-wrap main\">\r\n");
	ViewBuilder.Append("            <h1 class=\"page-header first\"><span class=\"text\"><span class=\"sprite sprite-notepad\"></span>注册</span>\r\n");
	ViewBuilder.Append("                <hr>\r\n");
	ViewBuilder.Append("            </h1>\r\n");
	ViewBuilder.Append("            <div class=\"row\">\r\n");
	ViewBuilder.Append("                <div class=\"span8 offset4 signup-form-wrap form-wrap\">\r\n");

	if (ispost)
	{

	ViewBuilder.Append("                      " + msg.ToString() + "<br>\r\n");
	ViewBuilder.Append("                      <a href=\"index.aspx\">首页</a>|<a href=\"login.aspx\">登录</a>|<a href=\"" + reurl.ToString() + "\">返回</a>\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <form id=\"frmpost\" name=\"frmpost\" method=\"post\" action=\"\" class=\"form\">\r\n");
	ViewBuilder.Append("                        <div class=\"form-content\">\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap text-item-wrap\">\r\n");
	ViewBuilder.Append("                                <label for=\"signupFormEmailInput\">登录帐号：</label>\r\n");
	ViewBuilder.Append("                                <span class=\"text-wrap\">\r\n");
	ViewBuilder.Append("                                    <input id=\"username\" name=\"username\" type=\"text\" placeholder=\"请输入用户名\">\r\n");
	ViewBuilder.Append("                                </span><span class=\"item-message help-inline\"></span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap text-item-wrap\">\r\n");
	ViewBuilder.Append("                                <label for=\"password\">登录密码：</label>\r\n");
	ViewBuilder.Append("                                <span class=\"text-wrap\">\r\n");
	ViewBuilder.Append("                                    <input id=\"password\" name=\"password\" type=\"password\" placeholder=\"请输入 6 个以上字符\">\r\n");
	ViewBuilder.Append("                                </span><span class=\"item-message help-inline\"></span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap text-item-wrap\">\r\n");
	ViewBuilder.Append("                                <label for=\"repeat\">确认密码：</label>\r\n");
	ViewBuilder.Append("                                <span class=\"text-wrap\">\r\n");
	ViewBuilder.Append("                                    <input id=\"repeat\" name=\"repeat\" type=\"password\" placeholder=\"请输入 6 个以上字符\">\r\n");
	ViewBuilder.Append("                                </span><span class=\"item-message help-inline\"></span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"item-wrap text-item-wrap\">\r\n");
	ViewBuilder.Append("                                <label for=\"realname\">真实姓名：</label>\r\n");
	ViewBuilder.Append("                                <span class=\"text-wrap\">\r\n");
	ViewBuilder.Append("                                    <input id=\"realname\" name=\"realname\" type=\"text\" placeholder=\"请输入您的真实姓名\">\r\n");
	ViewBuilder.Append("                                </span><span class=\"item-message help-inline\"></span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                        <div class=\"button-row\">\r\n");
	ViewBuilder.Append("                            <button type=\"submit\" class=\"visible-hidden\"></button>\r\n");
	ViewBuilder.Append("                            <span id=\"sumitreg\" class=\"btn btn-primary fir-wrap submit-button\"><span class=\"btn-inner\"><span class=\"fir fir-btn-normal-submit\"><span class=\"fir-text\">注册</span></span></span></span>\r\n");
	ViewBuilder.Append("                            <div id=\"signupFormAgree\" class=\"item-wrap\">\r\n");
	ViewBuilder.Append("                                <label for=\"signupFormAgreeInput\">\r\n");
	ViewBuilder.Append("                                    <input id=\"rules\" name=\"rules\" type=\"checkbox\" value=\"1\" checked=\"\">\r\n");
	ViewBuilder.Append("                                    已阅读并同意<a href=\"http://www.fangpage.com\" target=\"_blank\">使用条款和隐私策略</a><span class=\"item-message help-inline\"></span></label>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                    </form>\r\n");
	ViewBuilder.Append("                    <span id=\"login\" class=\"btn btn-paper btn-paper-xxlarge link-button login-button\"><span class=\"fir fir-btn-paper-login\"></span></span>\r\n");

	}	//end if

	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"footer repeat-x default-b\">\r\n");
	ViewBuilder.Append("            <div class=\"footer-left layout default-lb\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"footer-right layout default-rb\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"container\">\r\n");
	ViewBuilder.Append("                <div class=\"links\">\r\n");
	ViewBuilder.Append("                    <p class=\"text-center\">\r\n");
	ViewBuilder.Append("                       Copyright &copy; 2014 <a target=\"_blank\" href=\"http://www.fangpage.com\">方配软件(FangPage.Com)</a>&nbsp;&nbsp;版权所有\r\n");
	ViewBuilder.Append("                    </p>\r\n");
	ViewBuilder.Append("                    <p class=\"text-center\">方配官方网站：<a target=\"_blank\" href=\"http://www.fangpage.com\">http://www.fangpage.com</a></p>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
