using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x02000009 RID: 9
	public class ViewConfigs
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003308 File Offset: 0x00001508
		public static List<ViewConfig> GetViewList()
		{
			object obj = FPCache.Get("FP_VIEWLIST");
			List<ViewConfig> list = new List<ViewConfig>();
			if (obj != null)
			{
				list = (obj as List<ViewConfig>);
			}
			else
			{
				string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/view.config");
				list = FPXml.LoadList<ViewConfig>(mapPath);
				FPCache.Insert("FP_VIEWLIST", list, mapPath);
			}
			return list;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000335C File Offset: 0x0000155C
		public static ViewConfig GetViewInfo(string path)
		{
			List<ViewConfig> list = ViewConfigs.GetViewList().FindAll((ViewConfig item) => item.path.ToLower() == path.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new ViewConfig();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000033A4 File Offset: 0x000015A4
		public static void SaveViewConfig(ViewConfig viewconfig)
		{
			bool flag = false;
			List<ViewConfig> viewList = ViewConfigs.GetViewList();
			for (int i = 0; i < viewList.Count; i++)
			{
				if (viewList[i].path.ToLower() == viewconfig.path.ToLower())
				{
					viewList[i].include = viewconfig.include;
					flag = true;
				}
			}
			if (!flag)
			{
				viewList.Add(viewconfig);
			}
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/view.config");
			FPXml.SaveXml<ViewConfig>(viewList, mapPath);
			FPCache.Remove("FP_VIEWLIST");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003434 File Offset: 0x00001634
		public static void SaveViewConfig(List<ViewConfig> viewlist)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/view.config");
			FPXml.SaveXml<ViewConfig>(viewlist, mapPath);
			FPCache.Remove("FP_VIEWLIST");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003468 File Offset: 0x00001668
		public static void ReSetViewConfig()
		{
			List<ViewConfig> viewList = ViewConfigs.GetViewList();
			for (int i = viewList.Count - 1; i >= 0; i--)
			{
				if (!File.Exists(FPFile.GetMapPath(WebConfig.WebPath + viewList[i].path)))
				{
					viewList.RemoveAt(i);
				}
			}
			ViewConfigs.SaveViewConfig(viewList);
		}
	}
}
