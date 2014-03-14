using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;

namespace SimplyMobile.Data
{
    public class Emailer : IEmailer
    {
        public bool CanSend
        {
            get { return MFMailComposeViewController.CanSendMail; }
        }

        public Emailer()
        {
               
        }

        public void ShowDraft(string subject, string body, bool html, string[] to, string[] cc, string[] bcc)
        {
            var mailer = new MFMailComposeViewController();
            mailer.SetMessageBody(body ?? string.Empty, html);
            mailer.SetSubject(subject ?? string.Empty);
            mailer.SetCcRecipients(cc);
            mailer.SetToRecipients(to);
            mailer.Finished += (s, e) =>
                {
                    ((MFMailComposeViewController)s).DismissViewController(true, () => { });
                };

            //_modalHost.PresentModalViewController(_mail, true);
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}