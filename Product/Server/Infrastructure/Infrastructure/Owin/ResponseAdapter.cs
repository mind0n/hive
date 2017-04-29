using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
	public abstract class ResponseAdapter
	{
		public ResponseAdapter(object context)
		{
			ProcessContext(context);
		}

		protected abstract void ProcessContext(object context);

		public abstract void Write(string data);

		public abstract void Write(Stream stream);

		public abstract void Status(int statusCode);

		public abstract void Type(string contentType);

		public abstract string Header(string key, string val);
	}

	public class OwinResponseAdapter : ResponseAdapter
	{
		protected IOwinResponse RawResponse { get; private set; }
		public Dobj Headers { get; } = new Dobj();

		public OwinResponseAdapter(IOwinContext context) : base(context)
		{
		}

		protected override void ProcessContext(object context)
		{
			var ctx = (IOwinContext) context;
			RawResponse = ctx.Response;
		}

		public override void Status(int statusCode)
		{
			RawResponse.StatusCode = statusCode;
		}

		public override void Type(string contentType)
		{
			RawResponse.ContentType = contentType;
		}

		public override void Write(string data)
		{
			RawResponse.Write(data);
		}

		public override void Write(Stream stream)
		{
			using (stream)
			{
				stream.BufferedRead((buf, len) =>
				{
					RawResponse.Write(buf, 0, len);
				});
			}
		}

		public override string Header(string key, string val = null)
		{
			if (val == null)
			{
				return RawResponse.Headers.ContainsKey(key) ? RawResponse.Headers[key] : null;
			}
			if (string.Empty.Equals(val))
			{
				if (RawResponse.Headers.ContainsKey(key))
				{
					RawResponse.Headers.Remove(key);
				}
				return null;
			}
			RawResponse.Headers[key] = val;
			return null;
		}
	}
}
