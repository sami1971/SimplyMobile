using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace TwitterSample
{
    public partial class TwitterTableCell : UITableViewCell
    {
        private Datum datum;

        public static readonly UINib Nib = UINib.FromName ("TwitterTableCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString ("TwitterTableCell");

        public TwitterTableCell (IntPtr handle) : base (handle)
        {
        }

        public static TwitterTableCell Create()
        {
            return (TwitterTableCell)Nib.Instantiate (null, null) [0];
        }

        public void Bind(Datum datum)
        {
            this.datum = datum;
            if (this.datum == null || this.datum.caption == null)
            {
                this.labelCaption.Text = "null";
                return;
            }
            else
            {
                this.labelCaption.Text = datum.caption.text;
            }

            this.labelUser.Text = this.datum.user == null ? "user is null" : datum.user.full_name;

            this.labelType.Text = datum.type;

            Task.Factory.StartNew (async () =>
            {
                var d = this.datum;
                var fileName = Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.Personal), d.id);
                if (File.Exists(fileName))
                {
                    SetImage(fileName);
                }
                else
                {
                    await new WebClient ().DownloadFileTaskAsync (new Uri(datum.images.thumbnail.url), fileName);

                    if (this.datum == d)
                    {
                        SetImage(fileName);
                    }
                }
            });
        }

        private void SetImage(string fileName)
        {
            this.image.InvokeOnMainThread(()=> this.image.Image = UIImage.FromFile(fileName));
        }
//      public void Unbind()
//      {
//
//      }
    }
}

