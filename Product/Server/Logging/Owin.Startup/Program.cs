using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using CERTENROLLLib;
using Logger;
using Microsoft.Owin.Hosting;
using Middlewares;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Owin.Startup
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var port = 443;

			var cert = GetCert("localhost", TimeSpan.FromDays(3650), "devpwd", AppDomain.CurrentDomain.BaseDirectory + "cert.pfx");
			ActivateCert((X509Certificate2) cert, port, GetAppId());
			var so = new StartOptions();
			so.Urls.Add($"https://+:{port}/");
			using (WebApp.Start<Startup>(so))
			{
				log.i($"Hosted on port: {port}");
				Console.ReadLine();
			}
		}

		private static string GetAppId()
		{
			var assembly = Assembly.GetExecutingAssembly();

			//The following line (part of the original answer) is misleading.
			//**Do not** use it unless you want to return the System.Reflection.Assembly type's GUID.
			//Console.WriteLine(assembly.GetType().GUID.ToString());

			// The following is the correct code.
			var attribute = (GuidAttribute) assembly.GetCustomAttributes(typeof (GuidAttribute), true)[0];
			var id = attribute.Value;
			return id;
		}

		public static X509Certificate GetCert(string cn, TimeSpan expirationLength, string pwd = "", string filename = null)
		{
			// http://stackoverflow.com/questions/18339706/how-to-create-self-signed-certificate-programmatically-for-wcf-service
			// http://stackoverflow.com/questions/21629395/http-listener-with-https-support-coded-in-c-sharp
			// https://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.storename(v=vs.110).aspx
			// create DN for subject and issuer
			X509Certificate cert = null;
			if (filename != null && File.Exists(filename))
			{
				cert = new X509Certificate2(filename, pwd,
					X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
			}
			else
			{
				var base64encoded = string.Empty;
				base64encoded = CreateCertContent(cn, expirationLength, pwd);
				cert = new X509Certificate2(
					Convert.FromBase64String(base64encoded), pwd,
					// mark the private key as exportable (this is usually what you want to do)
					// mark private key to go into the Machine store instead of the current users store
					X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet
					);
				//File.WriteAllBytes(filename, cert.Export(X509ContentType.Pfx, pwd));
			}
			// instantiate the target class with the PKCS#12 data (and the empty password)

			return cert;
		}

		private static string CreateCertContent(string cn, TimeSpan expirationLength, string pwd)
		{
			var base64encoded = string.Empty;
			var dn = new CX500DistinguishedName();
			dn.Encode("CN=" + cn, X500NameFlags.XCN_CERT_NAME_STR_NONE);

			var privateKey = new CX509PrivateKey();
			privateKey.ProviderName = "Microsoft Strong Cryptographic Provider";
			privateKey.Length = 2048;
			privateKey.KeySpec = X509KeySpec.XCN_AT_KEYEXCHANGE;
			privateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_DECRYPT_FLAG |
			                      X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_KEY_AGREEMENT_FLAG;
			privateKey.MachineContext = true;
			privateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_PLAINTEXT_EXPORT_FLAG;
			privateKey.Create();

			// Use the stronger SHA512 hashing algorithm
			var hashobj = new CObjectId();
			hashobj.InitializeFromAlgorithmName(ObjectIdGroupId.XCN_CRYPT_HASH_ALG_OID_GROUP_ID,
				ObjectIdPublicKeyFlags.XCN_CRYPT_OID_INFO_PUBKEY_ANY,
				AlgorithmFlags.AlgorithmFlagsNone, "SHA256");

			// Create the self signing request
			var cert = new CX509CertificateRequestCertificate();
			cert.InitializeFromPrivateKey(X509CertificateEnrollmentContext.ContextMachine, privateKey, "");
			cert.Subject = dn;
			cert.Issuer = dn; // the issuer and the subject are the same
			cert.NotBefore = DateTime.Now.Date;
			// this cert expires immediately. Change to whatever makes sense for you
			cert.NotAfter = cert.NotBefore + expirationLength;
			cert.HashAlgorithm = hashobj; // Specify the hashing algorithm
			cert.Encode(); // encode the certificate

			// Do the final enrollment process
			var enroll = new CX509Enrollment();
			enroll.InitializeFromRequest(cert); // load the certificate
			enroll.CertificateFriendlyName = cn; // Optional: add a friendly name
			var csr = enroll.CreateRequest(); // Output the request in base64
			// and install it back as the response
			enroll.InstallResponse(InstallResponseRestrictionFlags.AllowUntrustedCertificate,
				csr, EncodingType.XCN_CRYPT_STRING_BASE64, pwd); // no password
			// output a base64 encoded PKCS#12 so we can import it back to the .Net security classes
			base64encoded = enroll.CreatePFX(pwd, // no password, this is for internal consumption
				PFXExportOptions.PFXExportChainWithRoot);
			return base64encoded;
		}

		private static void ActivateCert(X509Certificate2 rlt, int port, string appId)
		{
			var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
			store.Open(OpenFlags.ReadWrite);
			if (!store.Certificates.Contains(rlt))
			{
				store.Add(rlt);

				var psi = new ProcessStartInfo();
				psi.FileName = "netsh";

				psi.Arguments = $"http delete sslcert ipport=0.0.0.0:{port}";
				var procDel = Process.Start(psi);
				procDel.WaitForExit();

				psi.Arguments = $"http add sslcert ipport=0.0.0.0:{port} certhash={rlt.Thumbprint} appid={{{appId}}}";
				var proc = Process.Start(psi);
				proc.WaitForExit();

				psi.Arguments = $"http delete sslcert ipport=[::]:{port}";
				var procDelV6 = Process.Start(psi);
				procDelV6.WaitForExit();

				psi.Arguments = $"http add sslcert ipport=[::]:{port} certhash={rlt.Thumbprint} appid={{{appId}}}";
				var procV6 = Process.Start(psi);
				procV6.WaitForExit();

				psi.Arguments = $"http add urlacl url=https://+:{port}/ user={Environment.UserDomainName}\\{Environment.UserName}";
				var procAcl = Process.Start(psi);
				procAcl.WaitForExit();
			}
			store.Close();
		}
	}


	public class Startup
	{
		private IAppBuilder app;

		public void Configuration(IAppBuilder app)
		{
#if DEBUG
			app.UseErrorPage();
#endif
			//var middleware = CreateMiddleWare(env =>
			//{
			//	log.i("Begin Request");
			//	foreach (var i in env.Keys)
			//	{
			//		var s = $"{i}\t={(env[i] == null ? "null" : env[i].ToString())}\t#\t{(env[i] == null ? "null" : env[i].GetType().FullName)}";
			//		log.i(s);
			//	}
			//}, () =>
			//{
			//	log.i("Request Completed");
			//});
			app.Use(typeof (LogMiddleware));
			app.Use(typeof (HttpMiddleware));
			//app.UseWelcomePage("/");
			this.app = app;
		}


		private Func<AppFunc, AppFunc> CreateMiddleWare(Action<IDictionary<string, object>> worker, Action ender = null)
		{
			if (worker != null)
			{
				var rlt = new Func<AppFunc, AppFunc>(next => (async env =>
				{
					worker(env);
					if (next != null)
					{
						await next.Invoke(env);
					}
					if (ender != null)
					{
						ender();
					}
				}));
				return rlt;
			}
			return null;
		}
	}
}