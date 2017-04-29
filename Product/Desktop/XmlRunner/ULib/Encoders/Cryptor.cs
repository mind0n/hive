using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using ULib.Exceptions;

namespace ULib.Encoders
{
    public class Cryptor
    {
        public static string certInfoConfigFile = @"Encoders\CertificateInfo.xml";
        private static X509Certificate2 cert;

        /// <summary>
        /// Encrypts string with the public key stored in the certificate.
        /// </summary>
        /// <param name="toBeEncrypted">The string to be encrypted.</param>
        /// <returns>The encrypted base 64 string.</returns>
        /// <exception cref="RSACryptoException">
        /// Throws when failed to encrypt the string.
        /// </exception>
        public static string RSAEncrypt(string toBeEncrypted)
        {
            string encrypted = toBeEncrypted;

            if (!string.IsNullOrEmpty(toBeEncrypted))
            {
                FetchCertificate();
                try
                {
                    byte[] dataToEncrypt = Encoding.UTF8.GetBytes(toBeEncrypted);
                    RSACryptoServiceProvider provider = cert.PublicKey.Key as RSACryptoServiceProvider;
                    byte[] encryptedData = provider.Encrypt(dataToEncrypt, false);
                    encrypted = Convert.ToBase64String(encryptedData);
                }
                catch (Exception e)
                {
                    e.Handle();
                }
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypts string with the private key stored in the certificate.
        /// </summary>
        /// <param name="toBeDecrypted">The base 64 string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="RSACryptoException">
        /// Throws when failed to decrypt the string.
        /// </exception>
        public static string RSADecrypt(string toBeDecrypted)
        {
            string decrypted = toBeDecrypted;

            if (!string.IsNullOrEmpty(toBeDecrypted))
            {
                FetchCertificate();
                try
                {
                    RSACryptoServiceProvider decryptedProvider = cert.PrivateKey as RSACryptoServiceProvider;
                    byte[] encryptedByte = Convert.FromBase64String(toBeDecrypted);
                    decryptedProvider.FromXmlString(decryptedProvider.ToXmlString(true));
                    byte[] decryptedByte = decryptedProvider.Decrypt(encryptedByte, false);
                    decrypted = Encoding.UTF8.GetString(decryptedByte);
                }
                catch (Exception e)
                {
                    e.Handle();
                }

            }
            return decrypted;
        }
        /// <summary>
        /// Fetches <code>cert</code> from certificate storage.
        /// </summary>
        /// <exception cref="RSACryptoException">
        /// Throws when failed to decrypt the string.
        /// </exception>
        private static void FetchCertificate()
        {
                if (cert != null)
                {
                    return;
                }

                string xmlData = null;
                CertificateInfo certInfo = null;
                StreamReader sr = null;
                StoreLocation storeLocation = StoreLocation.CurrentUser;
                try
                {
                    sr = new StreamReader(certInfoConfigFile, UTF8Encoding.UTF8);
                    xmlData = sr.ReadToEnd();
                    certInfo = (CertificateInfo)XmlConvertor.XmlToObject(xmlData, typeof(CertificateInfo));
                    if ("LocalMachine".Equals(certInfo.StoreLocation))
                    {
                        storeLocation = StoreLocation.LocalMachine;
                    }
                }
                catch (Exception e)
                {
                    e.Handle();
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }

                X509Store store = new X509Store(certInfo.StoreName, storeLocation);
                store.Open(OpenFlags.ReadOnly);

                X509FindType findType = X509FindType.FindBySubjectDistinguishedName;

                if ("FindBySubjectName".Equals(certInfo.FindType))
                {
                    findType = X509FindType.FindBySubjectName;
                }
                else if ("FindByIssuerName".Equals(certInfo.FindType))
                {
                    findType = X509FindType.FindByIssuerName;
                }
                else if ("FindByIssuerDistinguishedName".Equals(certInfo.FindType))
                {
                    findType = X509FindType.FindByIssuerDistinguishedName;
                }

                // Place the selected certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection =
                    store.Certificates.Find(findType, certInfo.FindValue, false);

                if (certCollection.Count > 0)
                {
                    cert = certCollection[0];
                }
                else
                {
                    ExceptionHandler.Handle(new Exception("Fetch certificate error (Certificate count must > 0)"));
                }
        }

    }
}
