using Android.Content;
using Xamarin.Forms;

namespace Gym.Services
{
    public class MailingService : IMailingService
    {
        public void Send(string to, string title, string content)
        {
            var email = new Intent(Intent.ActionSend)
                .PutExtra(Intent.ExtraEmail, new string[] { to })
                .PutExtra(Intent.ExtraSubject, title)
                .PutExtra(Intent.ExtraText, content)
                .SetType("message/rfc822");

            Forms.Context.StartActivity(email);
        }
    }
}
