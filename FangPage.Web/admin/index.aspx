<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.index" %>
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
	ViewBuilder.Append("<title>" + pagetitle.ToString() + " V" + wmsver.ToString() + "  - Powered by FangPage.Com</title>\r\n");
	ViewBuilder.Append("<meta name=\"keywords\" content=\"方配网站管理系统(WMS),方配软件技术有限公司,FangPage.Com,方配软件,网站程序,网站源码,网站建设,网建专家,网站模板,ASPX,ASP.NET\">\r\n");
	ViewBuilder.Append("<meta name=\"description\" content=\"方配软件技术有限公司(www.fangpage.com)是一家从事专注于软件技术开发、软件集成的高新技术企业，我们的理念：专注 、执着、 努力、卓越，倾情为用户提供免费的软件技术和软件产品\">\r\n");
	ViewBuilder.Append("<meta name=\"generator\" content=\"方配软件(http://www.fangpage.com)\">\r\n");
	ViewBuilder.Append("<meta content=\"IE=edge,chrome=1\" http-equiv=\"X-UA-Compatible\">\r\n");
	ViewBuilder.Append("<link href=\"css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("<link href=\"images/wms.ico\" type=\"image/x-icon\" rel=\"icon\">\r\n");
	ViewBuilder.Append("<link href=\"images/wms.ico\" type=\"image/x-icon\" rel=\"shortcut icon\">\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<frameset rows=\"118,*\" cols=\"*\" framespacing=\"0\" frameborder=\"no\" border=\"0\" screen_capture_injected=\"true\">\r\n");
	ViewBuilder.Append("  <frame src=\"top.aspx\" name=\"topframe\" scrolling=\"No\" noresize=\"noresize\" id=\"topframe\">\r\n");
	ViewBuilder.Append("  <frameset id=\"attachucp\" framespacing=\"0\" border=\"0\" frameborder=\"no\" cols=\"198,9,*\" rows=\"*\">\r\n");
	ViewBuilder.Append("    <frame scrolling=\"auto\" noresize=\"\" border=\"0\" name=\"leftframe\" src=\"" + lefturl.ToString() + "\">\r\n");
	ViewBuilder.Append("    <frame id=\"switchframe\" scrolling=\"no\" border=\"0\" noresize=\"\" name=\"switchframe\" src=\"swich.aspx\">\r\n");
	ViewBuilder.Append("    <frame scrolling=\"yes\" noresize=\"\" border=\"0\" name=\"mainframe\" src=\"" + righturl.ToString() + "\">\r\n");
	ViewBuilder.Append("  </frameset>\r\n");
	ViewBuilder.Append("</frameset>\r\n");
	ViewBuilder.Append("<noframes>\r\n");
	ViewBuilder.Append("</noframes>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
