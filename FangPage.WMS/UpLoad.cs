using System;
using System.IO;
using System.Web;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200000C RID: 12
	public class UpLoad
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003738 File Offset: 0x00001938
		public string FileSaveAs(HttpPostedFile postedFile, string filetype, UserInfo user)
		{
			return this.FileSaveAs(postedFile, filetype, user, false, false, 0, 0);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003758 File Offset: 0x00001958
		public string FileSaveAs(HttpPostedFile postedFile, string filetype, UserInfo user, bool isthumbnail, bool iswatermark, int imgmaxwidth, int imgmaxheight)
		{
			string result;
			try
			{
				string fileExt = UpLoad.GetFileExt(postedFile.FileName);
				string fileName = Path.GetFileName(postedFile.FileName);
				string text = this.GetNewFileName() + "." + fileExt;
				int contentLength = postedFile.ContentLength;
				string[] array = FPUtils.SplitString(AttachBll.GetAttachTypeArray(filetype), "\r\n");
				string[] array2 = new string[array.Length];
				int[] array3 = new int[array.Length];
				string text2 = "";
				for (int i = 0; i < array.Length; i++)
				{
					string[] array4 = FPUtils.SplitString(array[i], ",", 2);
					array2[i] = array4[0];
					array3[i] = FPUtils.StrToInt(array4[1], 0);
					if (text2 != "")
					{
						text2 += "、";
					}
					text2 += array4[0];
				}
				int inArrayID = FPUtils.GetInArrayID(fileExt, array2, true);
				if (inArrayID < 0)
				{
					SysBll.InsertLog(user.id, user.username, "上传文件", "上传文件：" + fileName + ",类型不合法", false);
					result = string.Concat(new string[]
					{
						"{\"error\": \"只允许上传【",
						text2,
						"】类型的文件！\", \"filename\": \"\", \"filesize\": \"",
						contentLength.ToString(),
						"\", \"originalname\": \"",
						fileName,
						"\"}"
					});
				}
				else if (postedFile.ContentLength > array3[inArrayID] * 1024)
				{
					SysBll.InsertLog(user.id, user.username, "上传文件", "上传文件：" + fileName + ",文件大小超过范围", false);
					result = string.Concat(new object[]
					{
						"{\"error\": \"该类型文件上传不得超过【",
						array3[inArrayID],
						"KB】\", \"filename\": \"\", \"filesize\": \"",
						contentLength.ToString(),
						"\", \"originalname\": \"",
						fileName,
						"\"}"
					});
				}
				else
				{
					string upLoadPath = this.GetUpLoadPath();
					string mapPath = FPUtils.GetMapPath(upLoadPath);
					if (!Directory.Exists(mapPath))
					{
						Directory.CreateDirectory(mapPath);
					}
					postedFile.SaveAs(mapPath + text);
					if (imgmaxwidth <= 0)
					{
						imgmaxwidth = this.sysconfig.attachimgmaxwidth;
					}
					if (imgmaxheight <= 0)
					{
						imgmaxheight = this.sysconfig.attachimgmaxheight;
					}
					if (this.IsImage(fileExt) && (imgmaxwidth > 0 || imgmaxheight > 0))
					{
						FPThumb.MakeThumbnailImage(mapPath + text, mapPath + text, imgmaxwidth, imgmaxheight);
					}
					string strPath = upLoadPath + Path.GetFileNameWithoutExtension(text) + "_small." + fileExt;
					if (this.IsImage(fileExt) && isthumbnail && this.sysconfig.thumbnailwidth > 0 && this.sysconfig.thumbnailheight > 0)
					{
						FPThumb.MakeThumbnailImage(mapPath + text, FPUtils.GetMapPath(strPath), this.sysconfig.thumbnailwidth, this.sysconfig.thumbnailheight);
					}
					if (this.IsWaterMark(fileExt) && iswatermark)
					{
						WaterMark.AddImageSignPic(mapPath + text, mapPath + text, FPUtils.GetMapPath(WebConfig.WebPath + this.sysconfig.watermarkpic), this.sysconfig.watermarkstatus, this.sysconfig.attachimgquality, this.sysconfig.watermarkopacity);
					}
					SysBll.InsertLog(user.id, user.username, "上传文件", "上传文件：" + fileName, true);
					result = string.Concat(new string[]
					{
						"{\"error\": \"\", \"filename\": \"",
						upLoadPath,
						text,
						"\", \"filesize\": \"",
						contentLength.ToString(),
						"\", \"originalname\": \"",
						fileName,
						"\"}"
					});
				}
			}
			catch (Exception ex)
			{
				SysBll.InsertLog(user.id, user.username, "上传文件", "错误：" + ex.Message, false);
				result = "{\"error\": \"上传过程中发生意外错误！\", \"filename\": \"\", \"filesize\": \"0\", \"originalname\": \"\"}";
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private string GetUpLoadPath()
		{
			return string.Concat(new string[]
			{
				WebConfig.WebPath,
				"upload/",
				DateTime.Now.ToString("yyyyMM"),
				"/",
				DateTime.Now.ToString("dd"),
				"/"
			});
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003C34 File Offset: 0x00001E34
		private string GetNewFileName()
		{
			return DateTime.Now.ToString("yyyyMMddHHmmssffff");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003C58 File Offset: 0x00001E58
		private bool IsImage(string _fileExt)
		{
			return FPUtils.InArray(_fileExt.ToLower(), "bmp,jpg,gif,png");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003C7C File Offset: 0x00001E7C
		private bool IsWaterMark(string _fileExt)
		{
			return SysConfigs.GetConfig().allowwatermark > 0 && FPUtils.InArray(_fileExt.ToLower(), "bmp,jpg,png");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public static string GetFileExt(string _filepath)
		{
			string result;
			if (string.IsNullOrEmpty(_filepath))
			{
				result = "";
			}
			else if (_filepath.LastIndexOf(".") > 0)
			{
				result = _filepath.Substring(_filepath.LastIndexOf(".") + 1);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0400001C RID: 28
		private SysConfig sysconfig = SysConfigs.GetConfig();
	}
}
