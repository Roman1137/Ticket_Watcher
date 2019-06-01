using System;
using System.Net;
using System.Net.Mail;
using TrainTicketWatcher.models.Response;
using TrainTicketWatcher.models.UserInput;

namespace TrainTicketWatcher.Helpers
{
    public class EmailHelper
    {
        public static void NotifyByEmail(CustomResponse customResponse)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("example123456789example1@gmail.com", "Roman");
            // кому отправляем
            MailAddress to = new MailAddress("poma.borodavka@gmail.com"); //didenckonyura@gmail.com
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Нашлись билеты";
            // текст письма
            m.Body = $"<h2>Привет, кабанам! Вот что нашлось: {customResponse.ResponseFullModel.Data.List}</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("example123456789example1@gmail.com", "123456789p");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
