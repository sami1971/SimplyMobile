using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimplyMobile.Data
{
    public interface IEmailer
    {
        bool CanSend { get; }
        void ShowDraft(string subject, string body, bool html, string[] to, string[] cc, string[] bcc);
    }
}
