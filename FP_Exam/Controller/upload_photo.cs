using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FP_Exam.Controller
{
	public class upload_photo : LoginController
	{
		protected ExamResult examresult = new ExamResult();

		protected override void Controller()
		{
			int @int = FPRequest.GetInt("resultid");
			this.examresult = ExamBll.GetExamResult(@int);
			bool ispost = this.ispost;
			if (ispost)
			{
				string @string = FPRequest.GetString("type");
				string string2 = FPRequest.GetString("image");
				bool flag = @string == "data";
				if (flag)
				{
					string[] array = FPArray.SplitString(string2, 2);
					string s = this.Base64ToImage(array[1]);
					FPResponse.Write(s, true);
				}
				else
				{
					bool flag2 = @string == "pixel";
					if (flag2)
					{
						string s2 = this.PixelToImage(string2);
						FPResponse.Write(s2, true);
					}
				}
			}
		}

		private string Base64ToImage(string image)
		{
			string upLoadPath = this.GetUpLoadPath();
			string mapPath = FPFile.GetMapPath(upLoadPath);
			string str = this.GetNewFileName() + ".jpg";
			bool flag = !Directory.Exists(mapPath);
			if (flag)
			{
				Directory.CreateDirectory(mapPath);
			}
			string result;
			try
			{
				byte[] buffer = Convert.FromBase64String(image);
				MemoryStream memoryStream = new MemoryStream(buffer);
				Bitmap bitmap = new Bitmap(memoryStream);
				bool flag2 = File.Exists(mapPath + str);
				if (flag2)
				{
					File.Delete(mapPath + str);
				}
				bitmap.Save(mapPath + str, ImageFormat.Jpeg);
				memoryStream.Close();
				FileInfo fileInfo = new FileInfo(mapPath + str);
				AttachInfo attachInfo = new AttachInfo();
				attachInfo.attachid = this.examresult.attachid;
				attachInfo.uid = this.userid;
				attachInfo.app = this.setupinfo.markup;
				attachInfo.postid = this.examresult.id;
				attachInfo.name = this.examresult.examname + "_考试视频图片";
				attachInfo.filename = upLoadPath + str;
				attachInfo.filesize = fileInfo.Length;
				attachInfo.filetype = "jpg";
				attachInfo.description = "考试上传视频图片";
				attachInfo.id = DbHelper.ExecuteInsert<AttachInfo>(attachInfo);
				result = "ok";
			}
			catch (Exception ex)
			{
				result = "Base64ToImage 转换失败\nException：" + ex.Message;
			}
			return result;
		}

		private string PixelToImage(string image)
		{
			string upLoadPath = this.GetUpLoadPath();
			string mapPath = FPFile.GetMapPath(upLoadPath);
			string str = this.GetNewFileName() + ".jpg";
			bool flag = !Directory.Exists(mapPath);
			if (flag)
			{
				Directory.CreateDirectory(mapPath);
			}
			string result;
			try
			{
				Bitmap bitmap = new Bitmap(320, 240);
				string[] array = image.Split(new char[]
				{
					'|'
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries);
					for (int j = 0; j < array2.Length; j++)
					{
						Color baseColor = Color.FromArgb(Convert.ToInt32(array2[j]));
						Color color = Color.FromArgb(255, baseColor);
						bitmap.SetPixel(j, i, color);
					}
				}
				bitmap.Save(mapPath + str, ImageFormat.Jpeg);
				FileInfo fileInfo = new FileInfo(mapPath + str);
				AttachInfo attachInfo = new AttachInfo();
				attachInfo.attachid = this.examresult.attachid;
				attachInfo.uid = this.userid;
				attachInfo.app = this.setupinfo.markup;
				attachInfo.postid = this.examresult.id;
				attachInfo.name = this.examresult.examname + "_考试视频图片";
				attachInfo.filename = upLoadPath + str;
				attachInfo.filesize = fileInfo.Length;
				attachInfo.filetype = "jpg";
				attachInfo.description = "考试上传视频图片";
				attachInfo.id = DbHelper.ExecuteInsert<AttachInfo>(attachInfo);
				result = "ok";
			}
			catch (Exception ex)
			{
				result = "PixelToImage 转换失败\nException：" + ex.Message;
			}
			return result;
		}

		private string GetNewFileName()
		{
			return DateTime.Now.ToString("yyyyMMddHHmmss") + FPRandom.CreateCodeNum(4);
		}

		private string GetUpLoadPath()
		{
			return string.Concat(new string[]
			{
				this.webpath,
				"upload/",
				DateTime.Now.ToString("yyyyMM"),
				"/",
				DateTime.Now.ToString("dd"),
				"/"
			});
		}
	}
}
