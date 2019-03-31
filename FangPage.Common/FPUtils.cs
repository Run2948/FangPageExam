using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FangPage.Common
{
    // Token: 0x02000013 RID: 19
    public class FPUtils
    {
        // Token: 0x060000D7 RID: 215 RVA: 0x00005B54 File Offset: 0x00003D54
        public static string MD5(string str)
        {
            byte[] array = Encoding.Default.GetBytes(str);
            array = new MD5CryptoServiceProvider().ComputeHash(array);
            string text = "";
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i].ToString("x").PadLeft(2, '0');
            }
            return text;
        }

        // Token: 0x060000D8 RID: 216 RVA: 0x00005BBC File Offset: 0x00003DBC
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str, Encoding.UTF8);
        }

        // Token: 0x060000D9 RID: 217 RVA: 0x00005BDC File Offset: 0x00003DDC
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str, Encoding.UTF8);
        }

        // Token: 0x060000DA RID: 218 RVA: 0x00005BFC File Offset: 0x00003DFC
        public static string CutString(string str, int len)
        {
            Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$", RegexOptions.Compiled);
            char[] array = str.ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            for (int i = 0; i < array.Length; i++)
            {
                bool flag = regex.IsMatch(array[i].ToString());
                if (flag)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                bool flag2 = num <= len;
                if (!flag2)
                {
                    break;
                }
                stringBuilder.Append(array[i]);
            }
            bool flag3 = stringBuilder.ToString() != str;
            if (flag3)
            {
                stringBuilder.Append("...");
            }
            return stringBuilder.ToString();
        }

        // Token: 0x060000DB RID: 219 RVA: 0x00005CB4 File Offset: 0x00003EB4
        public static string RemoveHtml(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase).Trim();
        }

        // Token: 0x060000DC RID: 220 RVA: 0x00005CE0 File Offset: 0x00003EE0
        public static string GetTxtFromHTML(string HTML)
        {
            Regex regex = new Regex("</?(?!br|img)[^>]*>", RegexOptions.IgnoreCase);
            return regex.Replace(HTML, "");
        }

        // Token: 0x060000DD RID: 221 RVA: 0x00005D0C File Offset: 0x00003F0C
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Token: 0x060000DE RID: 222 RVA: 0x00005D30 File Offset: 0x00003F30
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Token: 0x060000DF RID: 223 RVA: 0x00005D54 File Offset: 0x00003F54
        public static string FormatDateTime(string datetime, string format)
        {
            bool flag = string.IsNullOrEmpty(datetime);
            string result;
            if (flag)
            {
                result = "";
            }
            else
            {
                try
                {
                    datetime = Convert.ToDateTime(datetime).ToString(format).Replace("1900-01-01", "");
                }
                catch
                {
                }
                result = datetime;
            }
            return result;
        }

        // Token: 0x060000E0 RID: 224 RVA: 0x00005DB4 File Offset: 0x00003FB4
        public static string FormatDateTime(string datetime)
        {
            return FPUtils.FormatDateTime(datetime, "yyyy-MM-dd HH:mm:ss");
        }

        // Token: 0x060000E1 RID: 225 RVA: 0x00005DD4 File Offset: 0x00003FD4
        public static string FormatDateTime(DateTime datetime)
        {
            return FPUtils.FormatDateTime(datetime, "yyyy-MM-dd HH:mm:ss");
        }

        // Token: 0x060000E2 RID: 226 RVA: 0x00005DF4 File Offset: 0x00003FF4
        public static string FormatDateTime(DateTime datetime, string format)
        {
            bool flag = string.IsNullOrEmpty(format);
            string result;
            if (flag)
            {
                result = datetime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                result = datetime.ToString(format);
            }
            return result;
        }

        // Token: 0x060000E3 RID: 227 RVA: 0x00005E28 File Offset: 0x00004028
        public static string FormatDateTime(DateTime? datetime)
        {
            return FPUtils.FormatDateTime(datetime, "yyyy-MM-dd HH:mm:ss");
        }

        // Token: 0x060000E4 RID: 228 RVA: 0x00005E48 File Offset: 0x00004048
        public static string FormatDateTime(DateTime? datetime, string format)
        {
            bool flag = datetime != null;
            string result;
            if (flag)
            {
                result = FPUtils.FormatDateTime(datetime.Value, format);
            }
            else
            {
                result = "";
            }
            return result;
        }

        // Token: 0x060000E5 RID: 229 RVA: 0x00005E7C File Offset: 0x0000407C
        public static string DateDiff(DateTime? starttime, DateTime? endtime)
        {
            bool flag = starttime == null || endtime == null;
            string result;
            if (flag)
            {
                result = "";
            }
            else
            {
                TimeSpan ts = new TimeSpan(starttime.Value.Ticks);
                TimeSpan timeSpan = new TimeSpan(endtime.Value.Ticks);
                TimeSpan timeSpan2 = timeSpan.Subtract(ts).Duration();
                string text = "";
                bool flag2 = timeSpan2.Days > 0;
                if (flag2)
                {
                    text = text + timeSpan2.Days.ToString() + "天";
                }
                bool flag3 = timeSpan2.Hours > 0;
                if (flag3)
                {
                    text = text + timeSpan2.Hours.ToString() + "时";
                }
                bool flag4 = timeSpan2.Minutes > 0;
                if (flag4)
                {
                    text = text + timeSpan2.Minutes.ToString() + "分";
                }
                bool flag5 = timeSpan2.Seconds > 0;
                if (flag5)
                {
                    text = text + timeSpan2.Seconds.ToString() + "秒";
                }
                bool flag6 = text == "";
                if (flag6)
                {
                    text = "1秒";
                }
                result = text;
            }
            return result;
        }

        // Token: 0x060000E6 RID: 230 RVA: 0x00005FD4 File Offset: 0x000041D4
        public static bool StrToBool(object Expression, bool defValue)
        {
            bool flag = Expression != null;
            if (flag)
            {
                bool flag2 = string.Compare(Expression.ToString(), "true", true) == 0;
                if (flag2)
                {
                    return true;
                }
                bool flag3 = string.Compare(Expression.ToString(), "false", true) == 0;
                if (flag3)
                {
                    return false;
                }
            }
            return defValue;
        }

        // Token: 0x060000E7 RID: 231 RVA: 0x0000602C File Offset: 0x0000422C
        public static int StrToInt(object Expression)
        {
            return FPUtils.StrToInt(Expression, 0);
        }

        // Token: 0x060000E8 RID: 232 RVA: 0x00006048 File Offset: 0x00004248
        public static int StrToInt(object Expression, int defValue)
        {
            bool flag = Expression != null;
            if (flag)
            {
                string text = Expression.ToString();
                bool flag2 = text.Length > 0 && text.Length <= 11 && Regex.IsMatch(text, "^[-]?[0-9]*$");
                if (flag2)
                {
                    bool flag3 = text.Length < 10 || (text.Length == 10 && text[0] == '1') || (text.Length == 11 && text[0] == '-' && text[1] == '1');
                    if (flag3)
                    {
                        return Convert.ToInt32(text);
                    }
                }
            }
            return defValue;
        }

        // Token: 0x060000E9 RID: 233 RVA: 0x000060EC File Offset: 0x000042EC
        public static float StrToFloat(object strValue)
        {
            return FPUtils.StrToFloat(strValue, 0f);
        }

        // Token: 0x060000EA RID: 234 RVA: 0x0000610C File Offset: 0x0000430C
        public static float StrToFloat(object strValue, float defValue)
        {
            bool flag = strValue == null || strValue.ToString().Length > 10;
            float result;
            if (flag)
            {
                result = defValue;
            }
            else
            {
                float num = defValue;
                bool flag2 = strValue != null;
                if (flag2)
                {
                    bool flag3 = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        num = Convert.ToSingle(strValue);
                    }
                }
                result = num;
            }
            return result;
        }

        // Token: 0x060000EB RID: 235 RVA: 0x0000616C File Offset: 0x0000436C
        public static decimal StrToDecimal(object strValue)
        {
            return FPUtils.StrToDecimal(strValue, 0.00m);
        }

        // Token: 0x060000EC RID: 236 RVA: 0x00006190 File Offset: 0x00004390
        public static decimal StrToDecimal(object strValue, decimal defValue)
        {
            bool flag = strValue == null || strValue.ToString().Length > 10;
            decimal result;
            if (flag)
            {
                result = defValue;
            }
            else
            {
                decimal num = defValue;
                bool flag2 = strValue != null;
                if (flag2)
                {
                    bool flag3 = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        num = Convert.ToDecimal(strValue);
                    }
                }
                result = num;
            }
            return result;
        }

        // Token: 0x060000ED RID: 237 RVA: 0x000061F0 File Offset: 0x000043F0
        public static double StrToDouble(object strValue)
        {
            return FPUtils.StrToDouble(strValue, 0.0);
        }

        // Token: 0x060000EE RID: 238 RVA: 0x00006214 File Offset: 0x00004414
        public static double StrToDouble(object strValue, double defValue)
        {
            bool flag = strValue == null || strValue.ToString().Length > 10;
            double result;
            if (flag)
            {
                result = defValue;
            }
            else
            {
                double num = defValue;
                bool flag2 = strValue != null;
                if (flag2)
                {
                    bool flag3 = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        num = Convert.ToDouble(strValue);
                    }
                }
                result = num;
            }
            return result;
        }

        // Token: 0x060000EF RID: 239 RVA: 0x00006274 File Offset: 0x00004474
        public static DateTime StrToDateTime(string strValue)
        {
            DateTime result;
            try
            {
                result = Convert.ToDateTime(strValue);
            }
            catch
            {
                result = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return result;
        }

        // Token: 0x060000F0 RID: 240 RVA: 0x000062BC File Offset: 0x000044BC
        public static DateTime StrToDateTime(string strValue, string format)
        {
            DateTime result;
            try
            {
                strValue = FPUtils.FormatDateTime(strValue, format);
                result = Convert.ToDateTime(strValue);
            }
            catch
            {
                bool flag = !string.IsNullOrEmpty(format);
                if (flag)
                {
                    result = Convert.ToDateTime(DateTime.Now.ToString(format));
                }
                else
                {
                    result = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            return result;
        }

        // Token: 0x060000F1 RID: 241 RVA: 0x00006330 File Offset: 0x00004530
        public static DateTime? StrToDateTime2(string strValue)
        {
            bool flag = string.IsNullOrEmpty(strValue);
            DateTime? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                try
                {
                    result = new DateTime?(Convert.ToDateTime(strValue));
                }
                catch
                {
                    result = null;
                }
            }
            return result;
        }

        // Token: 0x060000F2 RID: 242 RVA: 0x00006384 File Offset: 0x00004584
        public static DateTime? StrToDateTime2(string strValue, string format)
        {
            bool flag = string.IsNullOrEmpty(strValue);
            DateTime? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                try
                {
                    strValue = FPUtils.FormatDateTime(strValue, format);
                    result = new DateTime?(Convert.ToDateTime(strValue));
                }
                catch
                {
                    result = null;
                }
            }
            return result;
        }

        // Token: 0x060000F3 RID: 243 RVA: 0x000063E4 File Offset: 0x000045E4
        public static FPData StrToFPData(string strArray)
        {
            FPData fpdata = new FPData();
            int num = 0;
            foreach (string value in FPArray.SplitString(strArray))
            {
                fpdata[num] = value;
                num++;
            }
            return fpdata;
        }

        // Token: 0x060000F4 RID: 244 RVA: 0x0000642C File Offset: 0x0000462C
        public static Version StrToVersion(string str)
        {
            int[] array = FPArray.SplitInt(str, ".", 4);
            return new Version(string.Format("{0,1}.{1,1}.{2,1}.{3,1}", new object[]
            {
                array[0].ToString(),
                array[1].ToString(),
                array[2].ToString(),
                array[3].ToString()
            }));
        }

        // Token: 0x060000F5 RID: 245 RVA: 0x0000649C File Offset: 0x0000469C
        public static Version GetVersion(string filename)
        {
            Version result = FPUtils.StrToVersion("0.0.0.0");
            bool flag = Path.GetExtension(filename).ToLower() == ".dll" && File.Exists(filename);
            if (flag)
            {
                Assembly assembly = FPUtils.GetAssembly(filename);
                bool flag2 = assembly != null;
                if (flag2)
                {
                    result = assembly.GetName().Version;
                }
            }
            return result;
        }

        // Token: 0x060000F6 RID: 246 RVA: 0x00006500 File Offset: 0x00004700
        public static Assembly GetAssembly(string filename)
        {
            bool flag = !File.Exists(filename);
            Assembly result;
            if (flag)
            {
                result = null;
            }
            else
            {
                byte[] rawAssembly = File.ReadAllBytes(filename);
                Assembly assembly = Assembly.Load(rawAssembly);
                result = assembly;
            }
            return result;
        }

        // Token: 0x060000F7 RID: 247 RVA: 0x00006534 File Offset: 0x00004734
        public static string FormatVersion(string str)
        {
            int[] array = FPArray.SplitInt(str, ".", 4);
            return string.Format("{0,1}.{1,1}.{2,1}", array[0].ToString(), array[1].ToString(), array[2].ToString());
        }

        // Token: 0x060000F8 RID: 248 RVA: 0x00006584 File Offset: 0x00004784
        public static bool IsContain(string content, string value)
        {
            return content.IndexOf(value) >= 0;
        }

        // Token: 0x060000F9 RID: 249 RVA: 0x000065A4 File Offset: 0x000047A4
        public static bool IsContain(StringBuilder content, string value)
        {
            int length = content.Length;
            content = content.Replace(value, value + "1");
            bool flag = length != content.Length;
            bool result;
            if (flag)
            {
                content = content.Replace(value + "1", value);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000FA RID: 250 RVA: 0x000065FC File Offset: 0x000047FC
        public static bool IsNumeric(object Expression)
        {
            bool flag = Expression != null;
            if (flag)
            {
                string text = Expression.ToString();
                bool flag2 = text.Length > 0 && text.Length <= 11 && Regex.IsMatch(text, "^[-]?[0-9]*[.]?[0-9]*$");
                if (flag2)
                {
                    bool flag3 = text.Length < 10 || (text.Length == 10 && text[0] == '1') || (text.Length == 11 && text[0] == '-' && text[1] == '1');
                    if (flag3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Token: 0x060000FB RID: 251 RVA: 0x00006698 File Offset: 0x00004898
        public static bool IsNumericArray(string[] strNumber)
        {
            bool flag = strNumber == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = strNumber.Length < 1;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    foreach (string expression in strNumber)
                    {
                        bool flag3 = !FPUtils.IsNumeric(expression);
                        if (flag3)
                        {
                            return false;
                        }
                    }
                    result = true;
                }
            }
            return result;
        }

        // Token: 0x060000FC RID: 252 RVA: 0x000066FC File Offset: 0x000048FC
        public static bool IsNumericArray(string strNumber)
        {
            bool flag = strNumber == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = strNumber.Length < 1;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    foreach (string expression in strNumber.Split(new char[]
                    {
                        ','
                    }))
                    {
                        bool flag3 = !FPUtils.IsNumeric(expression);
                        if (flag3)
                        {
                            return false;
                        }
                    }
                    result = true;
                }
            }
            return result;
        }

        // Token: 0x060000FD RID: 253 RVA: 0x00006770 File Offset: 0x00004970
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

        // Token: 0x060000FE RID: 254 RVA: 0x00006790 File Offset: 0x00004990
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
        }

        // Token: 0x060000FF RID: 255 RVA: 0x000067B0 File Offset: 0x000049B0
        public static bool IsValidDomain(string host)
        {
            bool flag = host.IndexOf(".") == -1;
            return !flag && !new Regex("^\\d+$").IsMatch(host.Replace(".", string.Empty));
        }

        // Token: 0x06000100 RID: 256 RVA: 0x00006800 File Offset: 0x00004A00
        public static bool IsEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }

        // Token: 0x06000101 RID: 257 RVA: 0x00006820 File Offset: 0x00004A20
        public static string ImgToBase64(string filename)
        {
            string result;
            try
            {
                Bitmap bitmap = new Bitmap(filename);
                MemoryStream memoryStream = new MemoryStream();
                ImageFormat format = ImageFormat.Png;
                string str = "data:image/png;base64,";
                bool flag = Path.GetExtension(filename) == ".jpg";
                if (flag)
                {
                    format = ImageFormat.Jpeg;
                    str = "data:image/jpeg;base64,";
                }
                else
                {
                    bool flag2 = Path.GetExtension(filename) == ".gif";
                    if (flag2)
                    {
                        format = ImageFormat.Gif;
                        str = "data:image/gif;base64,";
                    }
                }
                bitmap.Save(memoryStream, format);
                byte[] array = new byte[memoryStream.Length];
                memoryStream.Position = 0L;
                memoryStream.Read(array, 0, (int)memoryStream.Length);
                memoryStream.Close();
                string text = str + Convert.ToBase64String(array);
                result = text;
            }
            catch (Exception ex)
            {
                result = "转换失败:" + ex.Message;
            }
            return result;
        }

        // Token: 0x06000102 RID: 258 RVA: 0x0000690C File Offset: 0x00004B0C
        public static string Base64ToImg(string base64str, string filename)
        {
            string result;
            try
            {
                string directoryName = Path.GetDirectoryName(filename);
                bool flag = !Directory.Exists(directoryName);
                if (flag)
                {
                    Directory.CreateDirectory(directoryName);
                }
                bool flag2 = base64str.StartsWith("data:image/");
                if (flag2)
                {
                    base64str = FPArray.SplitString(base64str, 2)[1];
                }
                byte[] buffer = Convert.FromBase64String(base64str);
                MemoryStream memoryStream = new MemoryStream(buffer);
                Bitmap bitmap = new Bitmap(memoryStream);
                bool flag3 = File.Exists(filename);
                if (flag3)
                {
                    File.Delete(filename);
                }
                ImageFormat format = ImageFormat.Png;
                bool flag4 = base64str.StartsWith("data:image/jpeg");
                if (flag4)
                {
                    format = ImageFormat.Jpeg;
                }
                else
                {
                    bool flag5 = base64str.StartsWith("data:image/gif");
                    if (flag5)
                    {
                        format = ImageFormat.Gif;
                    }
                }
                bitmap.Save(filename, format);
                memoryStream.Close();
                result = "";
            }
            catch (Exception ex)
            {
                result = "转换失败:" + ex.Message;
            }
            return result;
        }

        // Token: 0x06000103 RID: 259 RVA: 0x00006A04 File Offset: 0x00004C04
        public static string TxtToImg(string txt, int width, int height, string filename)
        {
            string result;
            try
            {
                bool flag = width == 0;
                if (flag)
                {
                    width = 500;
                }
                bool flag2 = height == 0;
                if (flag2)
                {
                    height = 400;
                }
                string directoryName = Path.GetDirectoryName(filename);
                bool flag3 = !Directory.Exists(directoryName);
                if (flag3)
                {
                    Directory.CreateDirectory(directoryName);
                }
                Bitmap bitmap = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(bitmap);
                Font font = new Font("宋体", 11f);
                SizeF sizeF = graphics.MeasureString(txt, font, width);
                bitmap = new Bitmap(width, Convert.ToInt32(sizeF.Height));
                graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.DrawString(txt, font, Brushes.Black, new Rectangle(0, 0, Convert.ToInt32(sizeF.Width), Convert.ToInt32(sizeF.Height)));
                bitmap.Save(filename, ImageFormat.Jpeg);
                result = "";
            }
            catch (Exception ex)
            {
                result = "转换失败:" + ex.Message;
            }
            return result;
        }

        // Token: 0x06000104 RID: 260 RVA: 0x00006B1C File Offset: 0x00004D1C
        public static string GetIntranetIp()
        {
            string text = string.Empty;
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipaddress in hostEntry.AddressList)
            {
                bool flag = ipaddress.AddressFamily == AddressFamily.InterNetwork;
                if (flag)
                {
                    text = ipaddress.ToString();
                    break;
                }
            }
            bool flag2 = string.IsNullOrEmpty(text);
            if (flag2)
            {
                text = "127.0.0.1";
            }
            return text;
        }

        // Token: 0x06000105 RID: 261 RVA: 0x00006B90 File Offset: 0x00004D90
        public static string Trim(string content, string trim)
        {
            bool flag = string.IsNullOrEmpty(trim);
            string result;
            if (flag)
            {
                result = content;
            }
            else
            {
                bool flag2 = content.StartsWith(trim);
                if (flag2)
                {
                    content = content.Remove(0, trim.Length);
                }
                bool flag3 = content.EndsWith(trim);
                if (flag3)
                {
                    content = content.Remove(content.Length - trim.Length, trim.Length);
                }
                result = content;
            }
            return result;
        }

        // Token: 0x06000106 RID: 262 RVA: 0x00006BF8 File Offset: 0x00004DF8
        public static string TrimStart(string content, string trim)
        {
            bool flag = string.IsNullOrEmpty(trim);
            string result;
            if (flag)
            {
                result = content;
            }
            else
            {
                bool flag2 = content.StartsWith(trim);
                if (flag2)
                {
                    content = content.Remove(0, trim.Length);
                }
                result = content;
            }
            return result;
        }

        // Token: 0x06000107 RID: 263 RVA: 0x00006C38 File Offset: 0x00004E38
        public static string TrimEnd(string content, string trim)
        {
            bool flag = string.IsNullOrEmpty(trim);
            string result;
            if (flag)
            {
                result = content;
            }
            else
            {
                bool flag2 = content.EndsWith(trim);
                if (flag2)
                {
                    content = content.Remove(content.Length - trim.Length, trim.Length);
                }
                result = content;
            }
            return result;
        }

        public static uint ComputeStringHash(string s)
        {
            uint num = new uint();
            if (s != null)
            {
                num = 0x811c9dc5;
                for (int i = 0; i < s.Length; i++)
                {
                    num = (s[i] ^ num) * 0x1000193;
                }
            }
            return num;
        }
    }
}
