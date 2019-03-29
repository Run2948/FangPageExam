using System;
using System.IO;
using System.Net;
using System.Text;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000052 RID: 82
	public class smsbalance : AdminController
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000F034 File Offset: 0x0000D234
		protected override void View()
		{
			string requestUriString = string.Format("http://www.dxton.com/webservice/sms.asmx/GetNum?account={0}&password={1}", this.account, this.password);
			WebRequest webRequest = WebRequest.Create(requestUriString);
			webRequest.Timeout = 30000;
			webRequest.Headers.Set("Pragma", "no-cache");
			WebResponse response = webRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			Encoding utf = Encoding.UTF8;
			StreamReader streamReader = new StreamReader(responseStream, utf);
			base.Response.Write(streamReader.ReadToEnd());
			streamReader.Close();
			base.Response.End();
		}

		// Token: 0x040000CC RID: 204
		protected string account = FPRequest.GetString("account");

		// Token: 0x040000CD RID: 205
		protected string password = FPRequest.GetString("password");
	}
}
