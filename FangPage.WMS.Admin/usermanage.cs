using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using NPOI.HSSF.UserModel;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200005A RID: 90
	public class usermanage : SuperController
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00010CBC File Offset: 0x0000EEBC
		protected override void Controller()
		{
			if (this.ispost)
			{
				string text = FPRequest.GetString("chkid");
				if (text == "" && this.action == "delete")
				{
					this.ShowErr("对不起，您没有选择任何用户。");
					return;
				}
				text = FPArray.Remove(text, "1");
				if (this.action == "delete")
				{
					if (text != "")
					{
						DbHelper.ExecuteDelete<UserInfo>(text);
					}
					base.Response.Redirect(this.webpath + this.cururl);
				}
				else if (this.action == "batch")
				{
					if (text != "")
					{
						base.Response.Redirect("userbatch.aspx?idlist=" + text + "&backurl=" + FPUtils.UrlEncode(this.pageurl));
					}
					else if (this.query != "")
					{
						base.Response.Redirect("userbatch.aspx?" + this.query + "&backurl=" + FPUtils.UrlEncode(this.pageurl));
					}
					else
					{
						base.Response.Redirect("userbatch.aspx?backurl=" + FPUtils.UrlEncode(this.pageurl));
					}
				}
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("roleid", WhereType.NotEqual, 3));
			list.Add(DbHelper.MakeOrderBy("display", OrderBy.ASC));
			if (this.s_roleid > 0)
			{
				list.Add(DbHelper.MakeAndWhere("roleid", this.s_roleid));
			}
			if (this.s_departid > 0)
			{
				if (this.s_depts == 1)
				{
					string departIdList = DepartmentBll.GetDepartIdList(this.s_departid);
					list.Add(DbHelper.MakeAndWhere("departid", WhereType.In, departIdList));
				}
				else
				{
					list.Add(DbHelper.MakeAndWhere("departid", this.s_departid));
				}
			}
			if (this.s_gradeid > 0)
			{
				list.Add(DbHelper.MakeAndWhere("gradeid", this.s_gradeid));
			}
			if (this.s_types != "")
			{
				foreach (int num in FPArray.SplitInt(this.s_types))
				{
					if (num > 0)
					{
						list.Add(DbHelper.MakeAndWhere("types", WhereType.Contain, num));
					}
				}
			}
			if (this.s_sso == 1)
			{
				list.Add(DbHelper.MakeAndWhere("issso", 1));
			}
			else if (this.s_sso == 2)
			{
				list.Add(DbHelper.MakeAndWhere("issso", 0));
			}
			if (this.s_state == 1)
			{
				list.Add(DbHelper.MakeAndWhere("state", 1));
			}
			else if (this.s_state == 2)
			{
				list.Add(DbHelper.MakeAndWhere("state", 0));
			}
			string text2 = "";
			if (this.keyword != "")
			{
				if (this.s_username == 1)
				{
					text2 = "[username] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_realname == 1)
				{
					if (text2 != "")
					{
						text2 += " OR ";
					}
					text2 = text2 + "[realname] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_mobile == 1)
				{
					if (text2 != "")
					{
						text2 += " OR ";
					}
					text2 = text2 + "[mobile] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_email == 1)
				{
					if (text2 != "")
					{
						text2 += " OR ";
					}
					text2 = text2 + "[email] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_idcard == 1)
				{
					if (text2 != "")
					{
						text2 += " OR ";
					}
					text2 = text2 + "[idcard] LIKE '%" + this.keyword + "%'";
				}
				if (text2 == "")
				{
					text2 = string.Concat(new string[]
					{
						"[username] LIKE '%",
						this.keyword,
						"%' OR [realname] LIKE '%",
						this.keyword,
						"%'"
					});
				}
				list.Add(DbHelper.MakeAndWhere("(" + text2 + ")", WhereType.Custom, ""));
			}
			if (this.ispost && this.action == "export")
			{
				this.userlist = DbHelper.ExecuteList<UserInfo>(list.ToArray());
				List<UserExtend> list2 = FPXml.LoadList<UserExtend>(FPFile.GetMapPath(this.webpath + "config/user_extend.config"));
				HSSFWorkbook hssfworkbook = new HSSFWorkbook();
				HSSFSheet hssfsheet = hssfworkbook.CreateSheet("Sheet1");
				HSSFCellStyle hssfcellStyle = hssfworkbook.CreateCellStyle();
				hssfcellStyle.Alignment = CellHorizontalAlignment.CENTER;
				hssfcellStyle.VerticalAlignment = CellVerticalAlignment.CENTER;
				hssfcellStyle.BorderTop = CellBorderType.THIN;
				hssfcellStyle.BorderRight = CellBorderType.THIN;
				hssfcellStyle.BorderLeft = CellBorderType.THIN;
				hssfcellStyle.BorderBottom = CellBorderType.THIN;
				hssfcellStyle.DataFormat = 0;
				HSSFFont hssffont = hssfworkbook.CreateFont();
				hssffont.Boldweight = short.MaxValue;
				hssfcellStyle.SetFont(hssffont);
				HSSFRow hssfrow = hssfsheet.CreateRow(0);
				hssfrow.CreateCell(0).SetCellValue("用户名");
				hssfrow.CreateCell(1).SetCellValue("姓名");
				hssfrow.CreateCell(2).SetCellValue("性别");
				hssfrow.CreateCell(3).SetCellValue("所属部门");
				hssfrow.CreateCell(4).SetCellValue("身份证号码");
				hssfrow.CreateCell(5).SetCellValue("手机号码");
				hssfrow.CreateCell(6).SetCellValue("电子邮箱");
				int num2 = 6;
				foreach (UserExtend userExtend in list2)
				{
					num2++;
					hssfrow.CreateCell(num2).SetCellValue(userExtend.name);
				}
				num2++;
				hssfrow.CreateCell(num2).SetCellValue("");
				hssfrow.Height = 400;
				hssfsheet.SetColumnWidth(3, 6000);
				hssfsheet.SetColumnWidth(4, 6000);
				for (int j = 0; j < num2; j++)
				{
					hssfrow.Cells[j].CellStyle = hssfcellStyle;
				}
				HSSFCellStyle hssfcellStyle2 = hssfworkbook.CreateCellStyle();
				hssfcellStyle2.Alignment = CellHorizontalAlignment.CENTER;
				hssfcellStyle2.VerticalAlignment = CellVerticalAlignment.CENTER;
				hssfcellStyle2.BorderTop = CellBorderType.THIN;
				hssfcellStyle2.BorderRight = CellBorderType.THIN;
				hssfcellStyle2.BorderLeft = CellBorderType.THIN;
				hssfcellStyle2.BorderBottom = CellBorderType.THIN;
				hssfcellStyle2.DataFormat = 0;
				int num3 = 1;
				foreach (UserInfo userInfo in this.userlist)
				{
					HSSFRow hssfrow2 = hssfsheet.CreateRow(num3);
					hssfrow2.Height = 300;
					hssfrow2.CreateCell(0).SetCellValue(userInfo.username);
					hssfrow2.CreateCell(1).SetCellValue(userInfo.realname);
					hssfrow2.CreateCell(2).SetCellValue(userInfo.sex);
					hssfrow2.CreateCell(3).SetCellValue(userInfo.departname);
					hssfrow2.CreateCell(4).SetCellValue(userInfo.idcard);
					hssfrow2.CreateCell(5).SetCellValue(userInfo.mobile);
					hssfrow2.CreateCell(6).SetCellValue(userInfo.email);
					num2 = 6;
					foreach (UserExtend userExtend2 in list2)
					{
						num2++;
						hssfrow2.CreateCell(num2).SetCellValue(userInfo.extend[userExtend2.markup]);
					}
					num2++;
					hssfrow2.CreateCell(num2).SetCellValue("");
					num3++;
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					hssfworkbook.Write(memoryStream);
					memoryStream.Flush();
					memoryStream.Position = 0L;
					hssfsheet.Dispose();
					hssfworkbook.Dispose();
					base.Response.ContentType = "application/vnd.ms-excel";
					base.Response.ContentEncoding = Encoding.UTF8;
					base.Response.Charset = "";
					base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("导出用户表.xls"));
					base.Response.BinaryWrite(memoryStream.GetBuffer());
					base.Response.Flush();
					base.Response.End();
					goto IL_8AC;
				}
			}
			this.userlist = DbHelper.ExecuteList<UserInfo>(this.pager, list.ToArray());
			IL_8AC:
			this.typelist = TypeBll.GetTypeListByMarkup("user_type");
		}

		// Token: 0x040000FA RID: 250
		protected List<UserInfo> userlist = new List<UserInfo>();

		// Token: 0x040000FB RID: 251
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x040000FC RID: 252
		protected int s_roleid = FPRequest.GetInt("s_roleid");

		// Token: 0x040000FD RID: 253
		protected int s_departid = FPRequest.GetInt("s_departid");

		// Token: 0x040000FE RID: 254
		protected int s_depts = FPRequest.GetInt("s_depts");

		// Token: 0x040000FF RID: 255
		protected int s_gradeid = FPRequest.GetInt("s_gradeid");

		// Token: 0x04000100 RID: 256
		protected string s_types = FPArray.Remove(FPRequest.GetString("s_types"), "");

		// Token: 0x04000101 RID: 257
		protected int s_username = FPRequest.GetInt("s_username", 1);

		// Token: 0x04000102 RID: 258
		protected int s_realname = FPRequest.GetInt("s_realname", 1);

		// Token: 0x04000103 RID: 259
		protected int s_mobile = FPRequest.GetInt("s_mobile");

		// Token: 0x04000104 RID: 260
		protected int s_email = FPRequest.GetInt("s_email");

		// Token: 0x04000105 RID: 261
		protected int s_idcard = FPRequest.GetInt("s_idcard");

		// Token: 0x04000106 RID: 262
		protected int s_sso = FPRequest.GetInt("s_sso");

		// Token: 0x04000107 RID: 263
		protected int s_state = FPRequest.GetInt("s_state");

		// Token: 0x04000108 RID: 264
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x04000109 RID: 265
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
