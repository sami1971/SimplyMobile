using System;
using Java.Security;
using System.IO;
using Javax.Crypto;

namespace SimplyMobile.Data
{
    using Core;

    public class PasswordStorage : IPasswordStorage
    {
        private readonly string filename;
        private readonly char[] PassKey;

        private readonly KeyStore keystore;
        private readonly KeyStore.PasswordProtection protection;

        private readonly object locker = new object();

        #region IPasswordStorage implementation

        public bool DeletePassword(string username, string service)
        {
            this.keystore.DeleteEntry(GetAlias (username, service));

            return Save();
        }

        public bool SetPassword(string username, string service, string password)
        {
            this.keystore.SetEntry (
                GetAlias (username, service), 
                new KeyStore.SecretKeyEntry(new SecretPassword (password)), 
                this.protection);

            return Save();
        }

        public bool TryGetPassword(string username, string service, out string password)
        {
            var aliases = keystore.Aliases ();
            while (aliases.HasMoreElements) 
            {
                var alias = aliases.NextElement().ToString();
                if (alias.Equals(GetAlias(username, service)))
                {
                    var e = keystore.GetEntry (alias, this.protection) as KeyStore.SecretKeyEntry;
                    if (e != null) 
                    {
                        var bytes = e.SecretKey.GetEncoded ();
                        password = System.Text.Encoding.UTF8.GetString (bytes);
                        return true;
                    }
                }
            }

            password = string.Empty;
            return false;
        }

        #endregion

        public PasswordStorage (string fileName, string passkey) : this(fileName, passkey.ToCharArray())
        {

        }

        public PasswordStorage(string fileName, char[] passkey)
        {
            this.filename = fileName;
            this.PassKey = passkey;

            this.keystore = KeyStore.GetInstance (KeyStore.DefaultType);
            this.protection = new KeyStore.PasswordProtection (this.PassKey);

            if (File.Exists (this.filename))
            {
                lock (this.locker)
                {
                    using (var stream = new FileStream (fileName, FileMode.Open))
                    {
                        keystore.Load (stream, passkey);
                    }
                }
            } 
            else
            {
                keystore.Load (null, passkey);
            }
        }

        private static string GetAlias(string username, string service)
        {
            return string.Format ("{0}_{1}", username, service);
        }

        private bool Save()
        {
            try
            {
                lock (this.locker)
                {
                    using (var stream = new FileStream (this.filename, FileMode.Truncate))
                    {
                        keystore.Store(stream, this.PassKey);
                    }
                }

                return true;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }
    }

    internal sealed class SecretPassword : Java.Lang.Object, ISecretKey
    {
        private readonly byte[] bytes;

        public SecretPassword(string password)
        {
            this.bytes = System.Text.Encoding.UTF8.GetBytes(password);
        }

        public byte[] GetEncoded()
        {
            return this.bytes;
        }

        public string Algorithm
        {
            get
            {
                return "RAW";
            }
        }

        public string Format
        {
            get
            {
                return "RAW";
            }
        }
    }
}

