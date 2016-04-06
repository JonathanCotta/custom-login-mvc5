using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TestLogin.Models
{
    public class Safe  : IDisposable
    {
        #region Variables and Constructor
        private bool disposed = false;
        private HashAlgorithm sha;
        public enum Algorithm { sha1, sha256, sha384, sha512 }

        public Safe(Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.sha1:
                    sha = SHA1.Create();
                    break;
                case Algorithm.sha256:
                    sha = SHA256.Create();
                    break;
                case Algorithm.sha384:
                    sha = SHA384.Create();
                    break;
                case Algorithm.sha512:
                    sha = SHA512.Create();
                    break;
                default:
                    break;
            }
        } 
        #endregion

        public string HashGen(string p)
        {
            byte[] hashBytes = Encoding.UTF8.GetBytes(p);
            hashBytes = sha.ComputeHash(hashBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }                 

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    sha.Dispose();
            disposed = true;
        } 
        #endregion
    }
}