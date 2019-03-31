using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using Newtonsoft.Json.Linq;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000031 RID: 49
	public class AttachBll
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00008910 File Offset: 0x00006B10
		public static AttachInfo Create(AttachInfo attachinfo, string attachid, int uid, string app)
		{
			return AttachBll.Create(attachinfo, attachid, uid, app, 0);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000891C File Offset: 0x00006B1C
		public static List<AttachInfo> Create(List<AttachInfo> attachlist, string attachid, int uid, string app)
		{
			return AttachBll.Create(attachlist, attachid, uid, app, 0);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00008928 File Offset: 0x00006B28
		public static AttachInfo Create(AttachInfo attachinfo, string attachid, int uid, string app, int postid)
		{
			AttachInfo attachInfo = new AttachInfo();
			if (string.IsNullOrEmpty(attachid))
			{
				attachInfo.error = "附件编号(attachid)不能为空。";
				return attachInfo;
			}
			string text = string.Concat(new string[]
			{
				WebConfig.WebPath,
				"upload/",
				DateTime.Now.ToString("yyyyMM"),
				"/",
				DateTime.Now.ToString("dd"),
				"/"
			});
			string mapPath = FPFile.GetMapPath(text);
			if (!Directory.Exists(mapPath))
			{
				Directory.CreateDirectory(mapPath);
			}
			string str = DateTime.Now.ToString("yyyyMMddHHmmss") + FPRandom.CreateCodeNum(4) + "." + attachinfo.filetype;
			FPFile.FileCoppy(FPFile.GetMapPath(attachinfo.filename), mapPath + str);
			attachInfo.app = app;
			attachInfo.attachid = attachid;
			attachInfo.parentid = attachinfo.id;
			attachInfo.uid = uid;
			attachInfo.name = attachinfo.name;
			attachInfo.filename = text + str;
			attachInfo.filesize = attachinfo.filesize;
			attachInfo.filetype = attachinfo.filetype;
			attachInfo.description = attachinfo.description;
			attachInfo.postid = postid;
			DbHelper.ExecuteInsert<AttachInfo>(attachInfo);
			return attachInfo;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00008A70 File Offset: 0x00006C70
		public static List<AttachInfo> Create(List<AttachInfo> attachlist, string attachid, int uid, string app, int postid)
		{
			List<AttachInfo> list = new List<AttachInfo>();
			if (string.IsNullOrEmpty(attachid))
			{
				return list;
			}
			foreach (AttachInfo attachinfo in attachlist)
			{
				AttachInfo item = AttachBll.Create(attachinfo, attachid, uid, app, postid);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00008ADC File Offset: 0x00006CDC
		public static AttachInfo Upload(HttpPostedFile files, string attachid, int uid, string app)
		{
			return AttachBll.Upload(files, attachid, uid, app, 0);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public static AttachInfo Upload(HttpPostedFile files, string attachid, int uid, string app, int postid)
		{
			AttachInfo attachInfo = new AttachInfo();
			if (string.IsNullOrEmpty(attachid))
			{
				attachInfo.error = "附件编号(attachid)不能为空。";
				return attachInfo;
			}
			attachInfo.attachid = attachid;
			JObject jobject = FPJson.ToModel<JObject>(new UpLoad().FileSaveAs(files));
			attachInfo.error = jobject["error"].ToString();
			if (attachInfo.error == "")
			{
				attachInfo.uid = uid;
				attachInfo.platform = SysConfigs.PlatForm;
				attachInfo.app = app;
				attachInfo.postid = postid;
				attachInfo.name = jobject["name"].ToString();
				attachInfo.filename = jobject["filename"].ToString();
				attachInfo.filesize = (long)FPUtils.StrToInt(jobject["filesize"].ToString(), 0);
				attachInfo.filetype = jobject["filetype"].ToString();
				attachInfo.description = jobject["desc"].ToString();
				DbHelper.ExecuteInsert<AttachInfo>(attachInfo);
			}
			return attachInfo;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00008BF1 File Offset: 0x00006DF1
		public static AttachInfo UploadImg(HttpPostedFile files, string attachid, int uid, string app, int imgmaxwidth, int imgmaxheight)
		{
			return AttachBll.UploadImg(files, attachid, uid, app, 0, imgmaxwidth, imgmaxheight);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00008C04 File Offset: 0x00006E04
		public static AttachInfo UploadImg(HttpPostedFile files, string attachid, int uid, string app, int postid, int imgmaxwidth, int imgmaxheight)
		{
			return AttachBll.UploadImg(files, attachid, uid, app, postid, 0, -1, imgmaxwidth, imgmaxheight);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00008C24 File Offset: 0x00006E24
		public static AttachInfo UploadImg(HttpPostedFile files, string attachid, int uid, string app, int postid, int isthumbnail, int iswatermark, int imgmaxwidth, int imgmaxheight)
		{
			AttachInfo attachInfo = new AttachInfo();
			if (string.IsNullOrEmpty(attachid))
			{
				attachInfo.error = "附件编号(attachid)不能为空。";
				return attachInfo;
			}
			attachInfo.attachid = attachid;
			JObject jobject = FPJson.ToModel<JObject>(new UpLoad().FileSaveAs(files, isthumbnail, iswatermark, imgmaxwidth, imgmaxheight));
			attachInfo.error = jobject["error"].ToString();
			if (attachInfo.error == "")
			{
				attachInfo.uid = uid;
				attachInfo.platform = SysConfigs.PlatForm;
				attachInfo.app = app;
				attachInfo.postid = postid;
				attachInfo.name = jobject["name"].ToString();
				attachInfo.filename = jobject["filename"].ToString();
				attachInfo.filesize = (long)FPUtils.StrToInt(jobject["filesize"].ToString(), 0);
				attachInfo.filetype = jobject["filetype"].ToString();
				attachInfo.description = jobject["desc"].ToString();
				DbHelper.ExecuteInsert<AttachInfo>(attachInfo);
			}
			return attachInfo;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00008D38 File Offset: 0x00006F38
		public static void UpdateAttach(string attachid, int postid)
		{
			if (string.IsNullOrEmpty(attachid))
			{
				return;
			}
			DbHelper.ExecuteUpdate<AttachInfo>(new SqlParam[]
			{
				DbHelper.MakeUpdate("postid", postid),
				DbHelper.MakeAndWhere("attachid", WhereType.In, "'" + attachid + "'")
			});
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00008D8C File Offset: 0x00006F8C
		public static void UpdateOnceAttach(string attachid, int postid)
		{
			if (string.IsNullOrEmpty(attachid))
			{
				return;
			}
			AttachInfo attachInfo = DbHelper.ExecuteModel<AttachInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("attachid", attachid),
				DbHelper.MakeOrderBy("uploadtime", OrderBy.DESC)
			});
			if (attachInfo.id > 0)
			{
				foreach (AttachInfo attachInfo2 in DbHelper.ExecuteList<AttachInfo>(new SqlParam[]
				{
					DbHelper.MakeAndWhere("attachid", attachInfo.attachid),
					DbHelper.MakeAndWhere("id", WhereType.NotEqual, attachInfo.id)
				}))
				{
					DbHelper.ExecuteDelete<AttachInfo>(attachInfo2.id);
					if (File.Exists(FPFile.GetMapPath(attachInfo2.filename)))
					{
						string newFile = string.Concat(new string[]
						{
							Path.GetDirectoryName(FPFile.GetMapPath(WebConfig.WebPath + "cache/attach_deleted" + attachInfo2.filename)),
							"\\",
							Path.GetFileNameWithoutExtension(attachInfo2.filename),
							"_",
							attachInfo2.name
						});
						FPFile.FileCoppy(FPFile.GetMapPath(attachInfo2.filename), newFile);
						File.Delete(FPFile.GetMapPath(attachInfo2.filename));
					}
				}
				DbHelper.ExecuteUpdate<AttachInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("postid", postid),
					DbHelper.MakeAndWhere("attachid", attachid)
				});
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00008F10 File Offset: 0x00007110
		public static AttachInfo GetAttachInfo(int aid)
		{
			return DbHelper.ExecuteModel<AttachInfo>(aid);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00008F18 File Offset: 0x00007118
		public static AttachInfo GetAttachInfo(string attachid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("attachid", attachid);
			return DbHelper.ExecuteModel<AttachInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00008F40 File Offset: 0x00007140
		public static void Delete(string attachid)
		{
			if (string.IsNullOrEmpty(attachid))
			{
				return;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("attachid", WhereType.In, "'" + attachid + "'");
			foreach (AttachInfo attachInfo in DbHelper.ExecuteList<AttachInfo>(new SqlParam[]
			{
				sqlParam
			}))
			{
				if (attachInfo.id > 0)
				{
					DbHelper.ExecuteDelete<AttachInfo>(attachInfo.id);
					if (File.Exists(FPFile.GetMapPath(attachInfo.filename)))
					{
						string newFile = string.Concat(new string[]
						{
							Path.GetDirectoryName(FPFile.GetMapPath(WebConfig.WebPath + "cache/attach_deleted" + attachInfo.filename)),
							"\\",
							Path.GetFileNameWithoutExtension(attachInfo.filename),
							"_",
							attachInfo.name
						});
						FPFile.FileCoppy(FPFile.GetMapPath(attachInfo.filename), newFile);
						File.Delete(FPFile.GetMapPath(attachInfo.filename));
					}
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009060 File Offset: 0x00007260
		public static void DeleteById(int aid)
		{
			AttachInfo attachInfo = DbHelper.ExecuteModel<AttachInfo>(aid);
			if (attachInfo.id > 0)
			{
				DbHelper.ExecuteDelete<AttachInfo>(attachInfo.id);
				if (File.Exists(FPFile.GetMapPath(attachInfo.filename)))
				{
					string newFile = string.Concat(new string[]
					{
						Path.GetDirectoryName(FPFile.GetMapPath(WebConfig.WebPath + "cache/attach_deleted" + attachInfo.filename)),
						"\\",
						Path.GetFileNameWithoutExtension(attachInfo.filename),
						"_",
						attachInfo.name
					});
					FPFile.FileCoppy(FPFile.GetMapPath(attachInfo.filename), newFile);
					File.Delete(FPFile.GetMapPath(attachInfo.filename));
				}
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00009114 File Offset: 0x00007314
		public static void DeleteById(string aidlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, aidlist);
			foreach (AttachInfo attachInfo in DbHelper.ExecuteList<AttachInfo>(new SqlParam[]
			{
				sqlParam
			}))
			{
				DbHelper.ExecuteDelete<AttachInfo>(attachInfo.id);
				if (File.Exists(FPFile.GetMapPath(attachInfo.filename)))
				{
					string newFile = string.Concat(new string[]
					{
						Path.GetDirectoryName(FPFile.GetMapPath(WebConfig.WebPath + "cache/attach_deleted" + attachInfo.filename)),
						"\\",
						Path.GetFileNameWithoutExtension(attachInfo.filename),
						"_",
						attachInfo.name
					});
					FPFile.FileCoppy(FPFile.GetMapPath(attachInfo.filename), newFile);
					File.Delete(FPFile.GetMapPath(attachInfo.filename));
				}
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00009210 File Offset: 0x00007410
		public static void DeleteByFileName(string filename)
		{
			if (filename == "")
			{
				return;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("filename", filename);
			AttachInfo attachInfo = DbHelper.ExecuteModel<AttachInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (attachInfo.id > 0)
			{
				DbHelper.ExecuteDelete<AttachInfo>(attachInfo.id);
				if (File.Exists(FPFile.GetMapPath(attachInfo.filename)))
				{
					string newFile = string.Concat(new string[]
					{
						Path.GetDirectoryName(FPFile.GetMapPath(WebConfig.WebPath + "cache/attach_deleted" + attachInfo.filename)),
						"\\",
						Path.GetFileNameWithoutExtension(attachInfo.filename),
						"_",
						attachInfo.name
					});
					FPFile.FileCoppy(FPFile.GetMapPath(attachInfo.filename), newFile);
					File.Delete(FPFile.GetMapPath(attachInfo.filename));
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000092E8 File Offset: 0x000074E8
		public static List<AttachInfo> GetAttachList(string attachid)
		{
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeOrderBy("id", OrderBy.ASC));
			if (FPUtils.IsNumericArray(attachid))
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, attachid));
			}
			else
			{
				list.Add(DbHelper.MakeAndWhere("attachid", WhereType.In, "'" + attachid + "'"));
			}
			return DbHelper.ExecuteList<AttachInfo>(list.ToArray());
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00009354 File Offset: 0x00007554
		public static List<AttachInfo> GetAttachList(string attachid, params SqlParam[] sqlparam)
		{
			List<SqlParam> list = new List<SqlParam>();
			if (FPUtils.IsNumericArray(attachid))
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, attachid));
			}
			else
			{
				list.Add(DbHelper.MakeAndWhere("attachid", WhereType.In, "'" + attachid + "'"));
			}
			foreach (SqlParam sqlParam in sqlparam)
			{
				if (sqlParam.SqlType != SqlType.Update && sqlParam.SqlType != SqlType.Insert)
				{
					list.Add(sqlParam);
				}
			}
			return DbHelper.ExecuteList<AttachInfo>(list.ToArray());
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000093DC File Offset: 0x000075DC
		public static string DownLoad(int aid)
		{
			return AttachBll.DownLoad(aid.ToString(), "");
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000093EF File Offset: 0x000075EF
		public static string DownLoad(string aidlist)
		{
			return AttachBll.DownLoad(aidlist, "");
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000093FC File Offset: 0x000075FC
		public static string DownLoad(string aidlist, string filename)
		{
			List<AttachInfo> attachList = AttachBll.GetAttachList(aidlist);
			if (attachList.Count == 0)
			{
				return "对不起，没有附件文件。";
			}
			if (filename == "")
			{
				filename = "附件批量下载.zip";
			}
			else
			{
				filename = Path.GetFileNameWithoutExtension(filename) + ".zip";
			}
			if (attachList.Count == 1)
			{
				DbHelper.ExecuteUpdate<AttachInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("downloads", attachList[0].downloads + 1),
					DbHelper.MakeAndWhere("id", attachList[0].id)
				});
				FPResponse.WriteDown(FPFile.GetMapPath(attachList[0].filename), attachList[0].name);
			}
			else
			{
				using (FPZip fpzip = new FPZip())
				{
					string text = string.Format("UPDATE [{0}WMS_AttachInfo] SET [downloads]=[downloads]+1 WHERE [id] IN({1})", DbConfigs.Prefix, aidlist);
					DbHelper.ExecuteSql(text);
					foreach (AttachInfo attachInfo in attachList)
					{
						if (text != "")
						{
							text += ";";
						}
						fpzip.AddFile(FPFile.GetMapPath(attachInfo.filename), attachInfo.name);
					}
					fpzip.ZipDown(FPUtils.UrlEncode(filename));
				}
			}
			return "";
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00009574 File Offset: 0x00007774
		public static AttachType GetAttachType(string type)
		{
			if (type != "")
			{
				if (type.LastIndexOf(".") > 0)
				{
					type = type.Substring(type.LastIndexOf(".") + 1);
				}
				foreach (AttachType attachType in AttachBll.GetAttachTypeList())
				{
					if (attachType.extension.ToLower() == type)
					{
						return attachType;
					}
				}
			}
			return new AttachType();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009610 File Offset: 0x00007810
		public static string GetAttachTypeExts()
		{
			return AttachBll.GetAttachTypeString("*");
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000961C File Offset: 0x0000781C
		public static string GetAttachTypeString()
		{
			return AttachBll.GetAttachTypeString("|");
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00009628 File Offset: 0x00007828
		public static string GetAttachTypeString(string separator)
		{
			List<AttachType> attachTypeList = AttachBll.GetAttachTypeList();
			string text = "";
			if (string.IsNullOrEmpty(separator))
			{
				separator = "*";
			}
			if (separator == "*")
			{
				using (List<AttachType>.Enumerator enumerator = attachTypeList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AttachType attachType = enumerator.Current;
						text = FPArray.Push(text, "*." + attachType.extension, ";");
					}
					return text;
				}
			}
			foreach (AttachType attachType2 in attachTypeList)
			{
				text = FPArray.Push(text, attachType2.extension, separator);
			}
			return text;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000096FC File Offset: 0x000078FC
		public static List<AttachType> GetAttachTypeList()
		{
			object obj = FPCache.Get("FP_ATTACHTYPE");
			List<AttachType> list;
			if (obj != null)
			{
				list = (obj as List<AttachType>);
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeOrderBy("id", OrderBy.ASC);
				list = DbHelper.ExecuteList<AttachType>(new SqlParam[]
				{
					sqlParam
				});
				CacheBll.Insert("附件类型信息缓存", "FP_ATTACHTYPE", list);
			}
			return list;
		}
	}
}
