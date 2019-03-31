using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000036 RID: 54
	public class DepartmentBll
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0000A138 File Offset: 0x00008338
		public static Department GetDepartInfo(int id)
		{
			List<Department> list = DepartmentBll.GetDepartList().FindAll((Department item) => item.id == id);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new Department();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000A180 File Offset: 0x00008380
		public static Department GetDepartInfo(string markup)
		{
			List<Department> list = DepartmentBll.GetDepartList().FindAll((Department item) => item.markup == markup);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new Department();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A1C8 File Offset: 0x000083C8
		public static List<Department> GetDepartList(int parentid)
		{
			return DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == parentid);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public static List<Department> GetDepartList()
		{
			object obj = FPCache.Get("FP_DEPARTLIST");
			List<Department> list;
			if (obj != null)
			{
				list = (obj as List<Department>);
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				list = DbHelper.ExecuteList<Department>(new SqlParam[]
				{
					sqlParam
				});
				CacheBll.Insert("系统部门信息缓存", "FP_DEPARTLIST", list);
			}
			return list;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000A24C File Offset: 0x0000844C
		public static string GetDepartIdList(int departid)
		{
			return FPArray.Push(DbHelper.ExecuteField<Department>(DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == departid)), departid);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000A28C File Offset: 0x0000848C
		public static string GetDepartIdList(string departids)
		{
			return FPArray.Push(DbHelper.ExecuteField<Department>(DepartmentBll.GetDepartList().FindAll((Department item) => FPArray.InArray(item.parentid, departids) >= 0)), departids);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000A2CC File Offset: 0x000084CC
		public static string GetUserDepartment(int departid)
		{
			string text = "";
			Department departInfo = DepartmentBll.GetDepartInfo(departid);
			if (departInfo.id > 0)
			{
				int[] array = FPArray.SplitInt(departInfo.parentlist);
				for (int i = 0; i < array.Length; i++)
				{
					Department departInfo2 = DepartmentBll.GetDepartInfo(array[i]);
					if (departInfo2.id > 0)
					{
						if (text != "")
						{
							text += ">";
						}
						text += departInfo2.name;
					}
				}
			}
			return text;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000A348 File Offset: 0x00008548
		public static string GetManageDepartList(string username)
		{
			object obj = FPCache.Get("FP_DEPARTMANAGER", username);
			string text;
			if (obj == null)
			{
				text = DbHelper.ExecuteField<Department>(DepartmentBll.GetDepartList().FindAll((Department item) => FPArray.InArray(username, item.departer) >= 0));
				FPCache.InsertAt("FP_DEPARTMANAGER", username, text);
			}
			else
			{
				text = obj.ToString();
			}
			return text;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public static Department CheckDepart(string departname)
		{
			if (string.IsNullOrEmpty(departname))
			{
				return new Department();
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", departname);
			return DbHelper.ExecuteModel<Department>(new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
