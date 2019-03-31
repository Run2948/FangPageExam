using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200003D RID: 61
	public class TypeBll
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000B31C File Offset: 0x0000951C
		public static TypeInfo GetTypeInfo(int id)
		{
			List<TypeInfo> list = TypeBll.GetTypeList().FindAll((TypeInfo item) => item.id == id);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new TypeInfo();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B364 File Offset: 0x00009564
		public static TypeInfo GetTypeInfo(string markup)
		{
			List<TypeInfo> list = TypeBll.GetTypeList().FindAll((TypeInfo item) => FPArray.Contain(item.markup.ToLower(), markup.ToLower()));
			if (list.Count > 0)
			{
				return list[0];
			}
			return new TypeInfo();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B3AC File Offset: 0x000095AC
		public static List<TypeInfo> GetTypeList()
		{
			object obj = FPCache.Get("FP_TYPELIST");
			List<TypeInfo> list;
			if (obj != null)
			{
				list = (obj as List<TypeInfo>);
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				list = DbHelper.ExecuteList<TypeInfo>(new SqlParam[]
				{
					sqlParam
				});
				CacheBll.Insert("系统分类信息缓存", "FP_TYPELIST", list);
			}
			return list;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B400 File Offset: 0x00009600
		public static List<TypeInfo> GetTypeList(int parentid)
		{
			return TypeBll.GetTypeList().FindAll((TypeInfo item) => item.parentid == parentid);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B430 File Offset: 0x00009630
		public static List<TypeInfo> GetTypeList(string parentid)
		{
			return TypeBll.GetTypeList().FindAll((TypeInfo item) => FPArray.InArray(item.parentid, parentid) >= 0);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000B460 File Offset: 0x00009660
		public static List<TypeInfo> GetTypeListById(string idlist)
		{
			return TypeBll.GetTypeList().FindAll((TypeInfo item) => FPArray.InArray(item.id, idlist) >= 0);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000B490 File Offset: 0x00009690
		public static List<TypeInfo> GetTypeListByMarkup(string markup)
		{
			return TypeBll.GetTypeList().FindAll((TypeInfo item) => FPArray.Contain(item.markup, markup) && item.parentid == 0);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public static List<TypeInfo> GetSubTypeByMarkup(string markup)
		{
			List<TypeInfo> result = new List<TypeInfo>();
			TypeInfo typeinfo = TypeBll.GetTypeInfo(markup);
			if (typeinfo.id > 0)
			{
				result = TypeBll.GetTypeList().FindAll((TypeInfo item) => item.parentid == typeinfo.id);
			}
			return result;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000B50C File Offset: 0x0000970C
		public static void UpdateTypePosts(int typeid, int sortid, int posts)
		{
			TypeInfo typeInfo = TypeBll.GetTypeInfo(typeid);
			int num = FPArray.InArray(sortid, typeInfo.sortids);
			if (num >= 0)
			{
				typeInfo.posts = FPArray.Update(typeInfo.posts, posts, num);
			}
			else if (posts > 0)
			{
				typeInfo.sortids = FPArray.Append(typeInfo.sortids, sortid);
				typeInfo.posts = FPArray.Append(typeInfo.posts, posts);
			}
			DbHelper.ExecuteUpdate<TypeInfo>(new SqlParam[]
			{
				DbHelper.MakeUpdate("sortids", typeInfo.sortids),
				DbHelper.MakeUpdate("posts", typeInfo.posts),
				DbHelper.MakeAndWhere("id", typeInfo.id)
			});
			FPCache.Remove("FP_TYPELIST");
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public static void UpdateTypePosts(string types, int sortid, int posts)
		{
			if (types == "")
			{
				return;
			}
			string text = "";
			List<TypeInfo> typeListById = TypeBll.GetTypeListById(types);
			for (int i = 0; i < typeListById.Count; i++)
			{
				int num = FPArray.InArray(sortid, typeListById[i].sortids);
				if (num >= 0)
				{
					typeListById[i].posts = FPArray.Update(typeListById[i].posts, posts, num);
				}
				else if (posts > 0)
				{
					typeListById[i].sortids = FPArray.Append(typeListById[i].sortids, sortid);
					typeListById[i].posts = FPArray.Append(typeListById[i].posts, posts);
				}
				if (text != "")
				{
					text += "|";
				}
				text += string.Format("UPDATE [{0}WMS_TypeInfo] SET [sorts]='{1}',[posts]='{2}' WHERE [id]={3}", new object[]
				{
					DbConfigs.Prefix,
					typeListById[i].sortids,
					typeListById[i].posts,
					typeListById[i].id
				});
			}
			if (text != "")
			{
				DbHelper.ExecuteSql(text);
			}
			FPCache.Remove("FP_TYPELIST");
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000B704 File Offset: 0x00009904
		public static void ResetTypePosts(int typeid, int sortid)
		{
			TypeInfo typeInfo = TypeBll.GetTypeInfo(typeid);
			int num = FPArray.InArray(sortid, typeInfo.sortids);
			if (num >= 0)
			{
				typeInfo.posts = FPArray.Replace(typeInfo.posts, 0, num);
				DbHelper.ExecuteUpdate<TypeInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("posts", typeInfo.posts),
					DbHelper.MakeAndWhere("id", typeInfo.id)
				});
				FPCache.Remove("FP_TYPELIST");
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000B780 File Offset: 0x00009980
		public static void ResetTypePosts(string types, int sortid)
		{
			if (types == "")
			{
				return;
			}
			string text = "";
			List<TypeInfo> typeList = TypeBll.GetTypeList(types);
			for (int i = 0; i < typeList.Count; i++)
			{
				int num = FPArray.InArray(sortid, typeList[i].sortids);
				if (num >= 0)
				{
					typeList[i].posts = FPArray.Replace(typeList[i].posts, 0, num);
					if (text != "")
					{
						text += "|";
					}
					text += string.Format("UPDATE [{0}WMS_TypeInfo] SET [posts]='{1}' WHERE [id]={2}", DbConfigs.Prefix, typeList[i].posts, typeList[i].id);
				}
			}
			if (text != "")
			{
				DbHelper.ExecuteSql(text);
				FPCache.Remove("FP_TYPELIST");
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000B860 File Offset: 0x00009A60
		public static void ResetTypePosts<T>(string types, int sortid)
		{
			if (types == "")
			{
				return;
			}
			string text = "";
			List<TypeInfo> typeList = TypeBll.GetTypeList(types);
			for (int i = 0; i < typeList.Count; i++)
			{
				int num = DbHelper.ExecuteCount<T>(new SqlParam[]
				{
					DbHelper.MakeAndWhere("typelist", WhereType.Contain, typeList[i].id),
					DbHelper.MakeAndWhere("sortid", sortid)
				});
				int num2 = FPArray.InArray(sortid, typeList[i].sortids);
				if (num2 >= 0)
				{
					typeList[i].posts = FPArray.Replace(typeList[i].posts, num, num2);
				}
				else if (num > 0)
				{
					typeList[i].sortids = FPArray.Append(typeList[i].sortids, sortid);
					typeList[i].posts = FPArray.Append(typeList[i].posts, num);
				}
				if (text != "")
				{
					text += "|";
				}
				text += string.Format("UPDATE [{0}WMS_TypeInfo] SET [sortids]='{1}',[posts]='{2}' WHERE [id]={3}", new object[]
				{
					DbConfigs.Prefix,
					typeList[i].sortids,
					typeList[i].posts,
					typeList[i].id
				});
			}
			if (text != "")
			{
				DbHelper.ExecuteSql(text);
				FPCache.Remove("FP_TYPELIST");
			}
		}
	}
}
