using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ShopBridge.Models;
using System.Web.Mvc;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Net.Mail;

namespace ShopBridge.HelperUtility
{
    public static class HTMLHelper
    {
        
        public static List<T> ToListIfNotNull<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable != null ? new List<T>(enumerable) : null);
        }


        public static int ToGeturlpatternId(this string str)
        {
            if (str != null)
            {
                int extractId = 0;
                string[] words = str.Split('-');
                string getId = words[1];
                extractId = Convert.ToInt32(getId);
                return extractId;
            }
            else
            {
                return 0;
            }
        }

        public static string tocalculateAge(this DateTime? dt, string strFormat = "dd MMM,yyyy")
        {
            string Getcalagelength = "";
            DateTime now = DateTime.Today;
            DateTime agecal = Convert.ToDateTime(dt.Value.toString());
            int age = now.Year - agecal.Year;
            Getcalagelength = age.ToString();
            //if (now < agecal.AddYears(age))
            //{
            //    Getcalagelength = age.ToString();
            //}
            return Getcalagelength;
        }

        public static string toString(this DateTime? dt, string strFormat = "dd MMM,yyyy")
        {
            string strConvertedDT = "";
            if (dt.HasValue)
            {
                strConvertedDT = dt.Value.ToString(strFormat, CultureInfo.InvariantCulture);
            }
            return strConvertedDT;
        }

        public static string toStringCalenderfromat(this DateTime? dt, string strFormat = "yyyy-MM-dd HH:mm:ss tt")
        {
            string strConvertedDT = "";
            if (dt.HasValue)
            {
                strConvertedDT = dt.Value.ToString(strFormat, CultureInfo.InvariantCulture);
            }
            return strConvertedDT;
        }


        public static string toString(this DateTime dt, string strFormat = "dd MMM,yyyy")
        {
            return dt.ToString(strFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime? toDateTime(this string strDT, string strFormat = "dd-MM-yyyy")
        {
            DateTime convertedDT;
            if (!DateTime.TryParseExact(strDT, strFormat, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out convertedDT))
            {
                DateTime.TryParseExact(strDT, new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd-MM-yyyy HH:mm", "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm:ss tt" }, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out convertedDT);
            }
            return (DateTime?)convertedDT;
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            str = str.Replace(' ', '_');
            StringBuilder sb = new StringBuilder(str.Length);
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        
        public static string GenerateRandomPassword(int passwordLength = 8, int numberOfNonAlphaNumericCharactersInPassword = 3)
        {
            if (passwordLength >= 128)
                passwordLength = 8;
            if (numberOfNonAlphaNumericCharactersInPassword >= passwordLength)
                numberOfNonAlphaNumericCharactersInPassword = passwordLength - 2;
            return System.Web.Security.Membership.GeneratePassword(passwordLength, numberOfNonAlphaNumericCharactersInPassword);
        }

        public static string replaceSpecialCharachters(this string str, char replaceBy = '_')
        {
            return str = Regex.Replace(str, "[^a-zA-Z0-9_]+", " ");
        }

        public static string AmountToWord(int number)
        {
            if (number == 0) return "Zero";

            if (number == -2147483648)
                return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }

            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };

            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };

            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };

            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs

            //You can increase as per your need.

            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }


            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {
                    if (h == 0)
                        sb.Append("");
                    else
                        if (h > 0 || i == 0) sb.Append("and ");


                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }

                if (i != 0) sb.Append(words3[i - 1]);

            }
            return sb.ToString().TrimEnd() + " Rupees only";
        }

        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }

        public static decimal roundOfDecimalPlaces(this decimal decimal_value, int places = 2)
        {
            return Math.Round(decimal_value, places);
        }

        public static Dictionary<int, string> EnumDictionary<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be an enum");
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => (int)(object)t, t => t.ToString());
        }

        public static string SendMail(string pTO, string pCC = null, bool UseDefaultCC = false, string pBCC = null, bool UseDefaultBCC = false, string pSubject = "Letstute Automated Mail", string pBody = null, List<string> pListAttachedFilePath = null)
        {
            int Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            string From = ConfigurationManager.AppSettings["MailUserId"];
            string Password = ConfigurationManager.AppSettings["MailPassword"];

            string CC = ConfigurationManager.AppSettings["CCUser"];
            string BCC = ConfigurationManager.AppSettings["BCCUser"];

            try
            {
                MailMessage Msg = new MailMessage();
                SmtpClient smtpcli;
                if (From == null || From.Equals(string.Empty))
                    return "ERROR:FROM MISSING";
                if (Password == null || Password.Equals(string.Empty))
                    return "ERROR:PASSWORD MISSING";
                //if (pTO == null || pTO.Equals(string.Empty))
                //    return "ERROR:TO MISSING";
                System.Net.NetworkCredential uInfo = new System.Net.NetworkCredential(From, Password);
                Msg.From = new MailAddress(From);
                if (pTO != null && pTO != string.Empty)
                    Msg.To.Add(pTO);

                if (UseDefaultCC == true && CC != null && CC != string.Empty)
                    Msg.CC.Add(CC);
                if (pCC != null && pCC != string.Empty)
                    Msg.CC.Add(pCC);

                if (UseDefaultBCC == true && BCC != null && BCC != string.Empty)
                    Msg.Bcc.Add(BCC);
                if (pBCC != null && pBCC != string.Empty)
                    Msg.Bcc.Add(pBCC);

                if (pListAttachedFilePath != null && pListAttachedFilePath.Count > 0)
                    pListAttachedFilePath.ForEach(o => { if (o != null && o != string.Empty) Msg.Attachments.Add(new Attachment(o)); });

                Msg.Subject = pSubject;
                Msg.IsBodyHtml = true;
                Msg.Body = pBody ?? "";
                smtpcli = new SmtpClient("smtp.gmail.com", Port);
                smtpcli.Host = "smtp.gmail.com";
                smtpcli.EnableSsl = true;
                smtpcli.UseDefaultCredentials = false;
                smtpcli.Credentials = uInfo;
                if ((pTO == null || pTO == string.Empty) && UseDefaultCC == false && UseDefaultBCC == false && (pCC == null || pCC == string.Empty) && (pBCC == null || pBCC == string.Empty))
                    return "ERROR:RECEIPIENT NOT SPECIFIED";
                smtpcli.Send(Msg);
                return "Success";
            }
            catch (Exception ex)
            {
                return "ERROR:" + ex.Message;
            }
        }

    }

    public static class NotificationMessage
    {
        public static ConstantEnums.MessageType MessageType;
        public static string Message;

        public static void ShowMessage(this Controller controller, ConstantEnums.MessageType messageType, string message)
        {
            MessageType = messageType;
            Message = message;
            //controller.TempData[messageTypeKey] = message;
        }

        /// <summary>
        /// Render all messages that have been set during execution of the controller action.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static HtmlString RenderMessages(this HtmlHelper htmlHelper)
        {
            var messages = String.Empty;
            if (Message != null)
            {
                string cssClass = "alert alert-danger";
                if (MessageType.ToString().ToLowerInvariant().Equals("success"))
                {
                    cssClass = "alert alert-success";

                }

                string oMessage = "<div class='" + cssClass + " alert-dismissable'>";
                oMessage += Message.ToString();
                oMessage += "</div>"; // End of original Div

                messages += oMessage;
            }
            Message = null;
            return MvcHtmlString.Create(messages);
        }


    }

    public static class ConvertToDataSet
    {
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }
    }
    
}