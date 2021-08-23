using donationApi.DAL;
using donationApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace donationApi.Common.BL
{
    public class clsDonationBl
    {

        public Boolean SaveData(clsRequest data)
        {
            //clsDonationDal oDonationDal = new clsDonationDal();
            
                try
                {
                    foreach (var donatin in data.donations)
                    {
                        if (Convert.ToInt32(donatin.amount) > 10000)
                        {
                            sendEmail("kamaz41294@gmail.com", "kamaz41294@gmail.com");//בעקרון אפשר לשלוף נתון של הבנאדם מהפרטים האישיים שלו על האימייל שלו
                        }
                    }
                   
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                dbBl db = new dbBl();
                db.spInserOrUpdateAllDataRequest(data);

            return true;
        }

        public static void sendEmail(string to, string from)
        {
            MailMessage message = new MailMessage(from, to);
            message.Subject = "לידיעתך - סכום התרומה גדול מ 10000";
            message.Body = @"נא לא להשיב למייל זה";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);// "Exception caught in CreateTestMessage2(): {0}")();
            }
        }
    }
}