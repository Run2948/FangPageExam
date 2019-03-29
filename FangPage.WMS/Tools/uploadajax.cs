using System;
using System.Collections;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS.Tools
{
	// Token: 0x02000044 RID: 68
	public class uploadajax : WMSController
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000AE44 File Offset: 0x00009044
		protected override void View()
		{
			if (this.roleid != 1 && this.role.isupload != 1)
			{
				this.ShowErrMsg("对不起，您所在的用户角色没有上传文件权限!");
			}
			else
			{
				this.EditorFile();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AE88 File Offset: 0x00009088
		private void EditorFile()
		{
			HttpPostedFile httpPostedFile = FPRequest.Files["imgfile"];
			if (httpPostedFile == null)
			{
				this.ShowErrMsg("请选择要上传文件!");
			}
			else
			{
				UpLoad upLoad = new UpLoad();
				string json = upLoad.FileSaveAs(httpPostedFile, this.dir, this.user);
				JsonData jsonData = JsonMapper.ToObject(json);
				string text = jsonData["error"].ToString();
				if (text != "")
				{
					this.ShowErrMsg(text);
				}
				else
				{
					AttachInfo attachInfo = new AttachInfo();
					attachInfo.uid = this.userid;
					attachInfo.sortid = this.sortid;
					attachInfo.filename = jsonData["filename"].ToString();
					attachInfo.filesize = (long)FPUtils.StrToInt(jsonData["filesize"].ToString(), 0);
					attachInfo.originalname = jsonData["originalname"].ToString();
					attachInfo.postdatetime = DbUtils.GetDateTime();
					attachInfo.filetype = this.dir;
					if (DbHelper.ExecuteInsert<AttachInfo>(attachInfo) == 0)
					{
						this.ShowErrMsg("数据库更新失败。");
					}
					else
					{
						Hashtable hashtable = new Hashtable();
						hashtable["error"] = 0;
						hashtable["url"] = attachInfo.filename;
						hashtable["title"] = attachInfo.originalname;
						base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
						base.Response.Write(JsonMapper.ToJson(hashtable));
						base.Response.End();
					}
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B048 File Offset: 0x00009248
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x0400014A RID: 330
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400014B RID: 331
		protected string dir = FPRequest.GetString("dir");
	}
}
