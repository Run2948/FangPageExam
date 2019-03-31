using System;
using System.Collections;
using System.IO;
using System.Web;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000004 RID: 4
	public class UpLoad
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002F0E File Offset: 0x0000110E
		public string FileSaveAs(HttpPostedFile postedFile)
		{
			return this.FileSaveAs(postedFile, 0, 0, 0, 0);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002F1C File Offset: 0x0000111C
		public string FileSaveAs(HttpPostedFile postedFile, int isthumbnail, int iswatermark, int imgmaxwidth, int imgmaxheight)
		{
			string result;
			try
			{
				string text = this.GetFileExt(postedFile.FileName).ToLower();
				string fileName = Path.GetFileName(postedFile.FileName);
				int contentLength = postedFile.ContentLength;
				string text2 = this.GetNewFileName() + "." + text;
				string upLoadPath = this.GetUpLoadPath();
				AttachType attachType = AttachBll.GetAttachType(text);
				Hashtable hashtable = new Hashtable();
				if (attachType.id <= 0)
				{
					hashtable["error"] = "系统不允许上传该类型文件！";
					hashtable["name"] = fileName;
					hashtable["filename"] = upLoadPath + text2;
					hashtable["filesize"] = contentLength.ToString();
					hashtable["filetype"] = attachType.extension;
					hashtable["desc"] = attachType.type;
					result = FPJson.ToJson(hashtable);
				}
				else if (postedFile.ContentLength > attachType.maxsize * 1024)
				{
					hashtable["error"] = "该类型附件上传不得超过【" + FPFile.FormatBytesStr((long)(attachType.maxsize * 1024)) + "】";
					hashtable["name"] = fileName;
					hashtable["filename"] = upLoadPath + text2;
					hashtable["filesize"] = contentLength.ToString();
					hashtable["filetype"] = text;
					hashtable["desc"] = attachType.type;
					result = FPJson.ToJson(hashtable);
				}
				else
				{
					string mapPath = FPFile.GetMapPath(upLoadPath);
					if (!Directory.Exists(mapPath))
					{
						Directory.CreateDirectory(mapPath);
					}
					postedFile.SaveAs(mapPath + text2);
					if (imgmaxwidth <= 0)
					{
						imgmaxwidth = this.sysconfig.attachimgmaxwidth;
					}
					if (imgmaxheight <= 0)
					{
						imgmaxheight = this.sysconfig.attachimgmaxheight;
					}
					if (this.IsImage(text) && (imgmaxwidth > 0 || imgmaxheight > 0))
					{
						FPThumb.MakeThumbnailImage(mapPath + text2, mapPath + text2, imgmaxwidth, imgmaxheight);
					}
					string virPath = upLoadPath + Path.GetFileNameWithoutExtension(text2) + "_small." + text;
					if (this.IsImage(text) && isthumbnail == 1 && this.sysconfig.thumbnailwidth > 0 && this.sysconfig.thumbnailheight > 0)
					{
						FPThumb.MakeThumbnailImage(mapPath + text2, FPFile.GetMapPath(virPath), this.sysconfig.thumbnailwidth, this.sysconfig.thumbnailheight);
					}
					if ((this.IsWaterMark(text) && iswatermark == 1) || (this.IsWaterMark(text) && iswatermark == 0 && this.sysconfig.allowwatermark == 1))
					{
						WaterMark.AddImageSignPic(mapPath + text2, mapPath + text2, FPFile.GetMapPath(WebConfig.WebPath + this.sysconfig.watermarkpic), this.sysconfig.watermarkstatus, this.sysconfig.attachimgquality, this.sysconfig.watermarkopacity);
					}
					hashtable["error"] = "";
					hashtable["name"] = fileName;
					hashtable["filename"] = upLoadPath + text2;
					hashtable["filesize"] = contentLength.ToString();
					hashtable["filetype"] = attachType.extension;
					hashtable["desc"] = attachType.type;
					result = FPJson.ToJson(hashtable);
				}
			}
			catch (Exception ex)
			{
				Hashtable hashtable2 = new Hashtable();
				hashtable2["error"] = ex.Message;
				hashtable2["name"] = "";
				hashtable2["filename"] = "";
				hashtable2["filesize"] = "0";
				hashtable2["filetype"] = "";
				hashtable2["desc"] = "";
				result = FPJson.ToJson(hashtable2);
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00003300 File Offset: 0x00001500
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

		// Token: 0x0600000E RID: 14 RVA: 0x00003364 File Offset: 0x00001564
		private string GetNewFileName()
		{
			return DateTime.Now.ToString("yyyyMMddHHmmss") + FPRandom.CreateCodeNum(4);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000338E File Offset: 0x0000158E
		private bool IsImage(string _fileExt)
		{
			return FPArray.InArray(_fileExt.ToLower(), "bmp,jpg,gif,png") >= 0;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000033A6 File Offset: 0x000015A6
		private bool IsWaterMark(string _fileExt)
		{
			return SysConfigs.GetConfig().allowwatermark > 0 && FPArray.InArray(_fileExt.ToLower(), "bmp,jpg,png") >= 0;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000033CD File Offset: 0x000015CD
		private string GetFileExt(string _filepath)
		{
			if (string.IsNullOrEmpty(_filepath))
			{
				return "";
			}
			if (_filepath.LastIndexOf(".") > 0)
			{
				return _filepath.Substring(_filepath.LastIndexOf(".") + 1);
			}
			return "";
		}

		// Token: 0x0400001F RID: 31
		private SysConfig sysconfig = SysConfigs.GetConfig();
	}
}
