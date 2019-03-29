using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004F RID: 79
	public class userimport : SuperController
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		protected override void View()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.ispost)
			{
				if (!this.isfile)
				{
					this.ShowErr("请选择要导入的本地Excel表文件");
				}
				else
				{
					string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
					string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(fileName).ToLower();
					if (a != ".xls")
					{
						this.ShowErr("该文件不是Excel表文件类型");
					}
					else
					{
						if (!Directory.Exists(mapPath))
						{
							Directory.CreateDirectory(mapPath);
						}
						if (File.Exists(mapPath + "\\" + fileName))
						{
							File.Delete(mapPath + "\\" + fileName);
						}
						FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
						DataTable excelTable = FPExcel.GetExcelTable(mapPath + "\\" + fileName);
						if (excelTable.Rows.Count > 0)
						{
							int num = excelTable.Rows.Count - 1;
							for (int i = 0; i < excelTable.Rows.Count; i++)
							{
								DataRow dataRow = excelTable.Rows[num - i];
								string text = dataRow.ItemArray[0].ToString().Trim();
								if (!(text == ""))
								{
									if (!this.InRestrictArray(text, this.regconfig.restrict))
									{
										if (!UserBll.CheckUserName(text))
										{
											DbHelper.ExecuteInsert<UserInfo>(new UserInfo
											{
												username = text,
												password = FPUtils.MD5(dataRow.ItemArray[1].ToString().Trim()),
												realname = dataRow.ItemArray[2].ToString().Trim(),
												mobile = dataRow.ItemArray[3].ToString().Trim(),
												email = dataRow.ItemArray[4].ToString().Trim(),
												roleid = this.GetRoleId(dataRow.ItemArray[5].ToString().Trim()),
												departid = this.GetDepartId(dataRow.ItemArray[6].ToString().Trim())
											});
										}
									}
								}
							}
						}
						if (File.Exists(mapPath + "\\" + fileName))
						{
							File.Delete(mapPath + "\\" + fileName);
						}
						base.Response.Redirect("usermanage.aspx");
					}
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		private bool InRestrictArray(string usernametxt, string restrict)
		{
			bool result;
			if (restrict == null || restrict == "")
			{
				result = false;
			}
			else
			{
				restrict = Regex.Escape(restrict).Replace("\\*", "[\\w-]*");
				foreach (string text in FPUtils.SplitString(restrict.ToLower(), "|"))
				{
					Regex regex = new Regex(string.Format("^{0}$", text));
					if (regex.IsMatch(usernametxt.ToLower()) && !text.Trim().Equals(""))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000EB68 File Offset: 0x0000CD68
		private int GetRoleId(string rolename)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", rolename);
			RoleInfo roleInfo = DbHelper.ExecuteModel<RoleInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (roleInfo.id == 0)
			{
				roleInfo.id = 5;
			}
			return roleInfo.id;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		private int GetDepartId(string departname)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", departname);
			Department department = DbHelper.ExecuteModel<Department>(new SqlParam[]
			{
				sqlParam
			});
			return department.id;
		}

		// Token: 0x040000C0 RID: 192
		protected RegConfig regconfig = new RegConfig();
	}
}
