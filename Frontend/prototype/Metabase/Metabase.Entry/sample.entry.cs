using Act.Core;
using Foundation;
using System;

namespace Act.Entry
{
	/// <summary>
	/// Please modify module.act.json (will be generated in non-library project such as console application project or windows application project)
	///	from	"pattern": "Act.Infrastructure.dll",
	/// to		"pattern": "Current assembly (.dll)",
	/// </summary>
	public class SampleEntry
	{
		
		public static void Start(dynamic settings)
		{
			log.d($"SampleEntry starting - {AppDomain.CurrentDomain.FriendlyName} -> {settings.test}");
		}

		public static void Stop()
		{
			log.d($"SampleEntry stopping - {AppDomain.CurrentDomain.FriendlyName}");
		}
	}
}
