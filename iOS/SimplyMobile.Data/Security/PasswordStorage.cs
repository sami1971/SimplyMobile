using System;
using SimplyMobile.Core;
using MonoTouch.Security;
using MonoTouch.Foundation;

namespace SimplyMobile.Data.Security
{
    public class PasswordStorage : IPasswordStorage
    {
        public static bool Synchronizable = false;

        public static SecStatusCode LastCode { get; private set; }

        public static SecAccessible Accessability { get; set; }

        #region IPasswordStorage implementation
        /// <summary>
        /// Deletes the password.
        /// </summary>
        /// <returns><c>true</c>, if password was deleted, <c>false</c> otherwise.</returns>
        /// <param name="username">Username.</param>
        /// <param name="service">Service.</param>
        public bool DeletePassword(string username, string service)
        {
            if (string.IsNullOrEmpty (username) || string.IsNullOrEmpty (service))
            {
                return false;
            }

            username = username.ToLower();
//          service = service.ToLower();

            var queryRec = new SecRecord ( SecKind.GenericPassword ) 
            { 
                Service = service, 
                Label = service, 
                Account = username, 
                Synchronizable = Synchronizable 
            };

            LastCode = SecKeyChain.Remove ( queryRec );
            return LastCode == SecStatusCode.Success;
        }

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <returns><c>true</c>, if password was set, <c>false</c> otherwise.</returns>
        /// <param name="username">Username.</param>
        /// <param name="service">Service.</param>
        /// <param name="password">Password.</param>
        public bool SetPassword(string username, string service, string password)
        {
            if (string.IsNullOrEmpty (username) || string.IsNullOrEmpty (service))
            {
                return false;
            }

            username = username.ToLower();
//          service = service.ToLower();

            DeletePassword (username, service);

            var rec = new SecRecord (SecKind.GenericPassword) 
            {
                Service = service,
//              Label = service,
                Account = username,
//              Generic = NSData.FromString (password, NSStringEncoding.UTF8),
                ValueData = NSData.FromString (password, NSStringEncoding.UTF8),
                Accessible = Accessability,
                Synchronizable = Synchronizable
            };

            LastCode = SecKeyChain.Add(rec);
            System.Diagnostics.Debug.WriteLine (LastCode);
            return LastCode == SecStatusCode.Success;
        }

        /// <summary>
        /// Tries the get password.
        /// </summary>
        /// <returns><c>true</c>, if get password was tryed, <c>false</c> otherwise.</returns>
        /// <param name="username">Username.</param>
        /// <param name="service">Service.</param>
        /// <param name="password">Password.</param>
        public bool TryGetPassword(string username, string service, out string password)
        {
            password = string.Empty;

            if (string.IsNullOrEmpty (username) || string.IsNullOrEmpty (service))
            {
                return false;
            }

            username = username.ToLower();
//          service = service.ToLower();

            SecStatusCode code;
            // Query the record.
            var queryRec = new SecRecord ( SecKind.GenericPassword ) 
            { 
                Service = service, 
                Label = service, 
                Account = username, 
                Synchronizable = Synchronizable 
            };

            try
            {
                queryRec = SecKeyChain.QueryAsRecord ( queryRec, out code );

                LastCode = code;

                if (LastCode == SecStatusCode.Success && queryRec != null && queryRec.ValueData != null )
                {
                    password = NSString.FromData ( queryRec.ValueData, NSStringEncoding.UTF8 ).ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            System.Diagnostics.Debug.WriteLine (LastCode);
            // Something went wrong.
            return LastCode == SecStatusCode.Success;
        }

        #endregion
    }
}

