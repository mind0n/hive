using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Startup
{
	/// <span class="code-SummaryComment"><summary></span>
	/// Loads an assembly into a new AppDomain and obtains all the
	/// namespaces in the loaded Assembly, which are returned as a 
	/// List. The new AppDomain is then Unloaded.
	/// 
	/// This class creates a new instance of a 
	/// <span class="code-SummaryComment"><c>AssemblyLoader</c> class</span>
	/// which does the actual ReflectionOnly loading 
	/// of the Assembly into
	/// the new AppDomain.
	/// <span class="code-SummaryComment"></summary></span>
	public class DomainLoader
	{
		protected AppDomain childDomain;

		#region Public Methods
		/// <span class="code-SummaryComment"><summary></span>
		/// Loads an assembly into a new AppDomain and obtains all the
		/// namespaces in the loaded Assembly, which are returned as a 
		/// List. The new AppDomain is then Unloaded
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="assemblyLocation">The Assembly file </span>
		/// location<span class="code-SummaryComment"></param></span>
		/// <span class="code-SummaryComment"><returns>A list of found namespaces</returns></span>
		public LoadResult LoadAssembly(FileInfo assemblyLocation)
		{
			LoadResult rlt = new LoadResult();
			List<String> namespaces = new List<String>();

			if (string.IsNullOrEmpty(assemblyLocation.Directory.FullName))
			{
				throw new InvalidOperationException(
					"Directory can't be null or empty.");
			}

			if (!Directory.Exists(assemblyLocation.Directory.FullName))
			{
				throw new InvalidOperationException(
				   string.Format(CultureInfo.CurrentCulture,
				   "Directory not found {0}",
				   assemblyLocation.Directory.FullName));

			}

			childDomain = BuildChildDomain(
				AppDomain.CurrentDomain);

			try
			{
				Type loaderType = typeof (AssemblyLoader);
				if (loaderType.Assembly != null)
				{
					var loader =
						(AssemblyLoader) childDomain.
							CreateInstanceFrom(
								loaderType.Assembly.Location,
								loaderType.FullName).Unwrap();

					rlt.Assembly = loader.LoadAssembly(
						assemblyLocation.FullName);
					namespaces =
						loader.GetNamespaces(
							assemblyLocation.Directory.FullName);
				}
			}
			catch (Exception ex)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
			}
			rlt.Namespaces = namespaces != null ? namespaces.ToArray() : null;
			return rlt;
		}
		#endregion

		#region Private Methods
		/// <span class="code-SummaryComment"><summary></span>
		/// Creates a new AppDomain based on the parent AppDomains 
		/// Evidence and AppDomainSetup
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="parentDomain">The parent AppDomain</param></span>
		/// <span class="code-SummaryComment"><returns>A newly created AppDomain</returns></span>
		private AppDomain BuildChildDomain(AppDomain parentDomain)
		{
			Evidence evidence = new Evidence(parentDomain.Evidence);
			AppDomainSetup setup = parentDomain.SetupInformation;
			return AppDomain.CreateDomain("DiscoveryRegion",
				evidence, setup);
		}
		#endregion


		/// <span class="code-SummaryComment"><summary></span>
		/// Remotable AssemblyLoader, this class 
		/// inherits from <span class="code-SummaryComment"><c>MarshalByRefObject</c> </span>
		/// to allow the CLR to marshall
		/// this object by reference across 
		/// AppDomain boundaries
		/// <span class="code-SummaryComment"></summary></span>
		class AssemblyLoader : MarshalByRefObject
		{
			#region Private/Internal Methods
			
			/// <span class="code-SummaryComment"><summary></span>
			/// Gets namespaces for ReflectionOnly Loaded Assemblies
			/// <span class="code-SummaryComment"></summary></span>
			/// <span class="code-SummaryComment"><param name="path">The path to the Assembly</param></span>
			/// <span class="code-SummaryComment"><returns>A List of namespace strings</returns></span>
			[SuppressMessage("Microsoft.Performance",
				"CA1822:MarkMembersAsStatic")]
			internal List<String> GetNamespaces(string path)
			{

				List<String> namespaces = new List<String>();

				DirectoryInfo directory = new DirectoryInfo(path);
				ResolveEventHandler resolveEventHandler =
					(s, e) => {
						return OnReflectionOnlyResolve(
							e, directory);
					};

				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve
					+= resolveEventHandler;

				Assembly reflectionOnlyAssembly =
					AppDomain.CurrentDomain.
						ReflectionOnlyGetAssemblies().First();

				foreach (Type type in reflectionOnlyAssembly.GetTypes())
				{
					if (!namespaces.Contains(type.Namespace))
						namespaces.Add(type.Namespace);
				}

				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve
					-= resolveEventHandler;
				return namespaces;

			}

			/// <span class="code-SummaryComment"><summary></span>
			/// Attempts ReflectionOnlyLoad of current 
			/// Assemblies dependants
			/// <span class="code-SummaryComment"></summary></span>
			/// <span class="code-SummaryComment"><param name="args">ReflectionOnlyAssemblyResolve </span>
			/// event args<span class="code-SummaryComment"></param></span>
			/// <span class="code-SummaryComment"><param name="directory">The current Assemblies </span>
			/// Directory<span class="code-SummaryComment"></param></span>
			/// <span class="code-SummaryComment"><returns>ReflectionOnlyLoadFrom loaded</span>
			/// dependant Assembly<span class="code-SummaryComment"></returns></span>
			private Assembly OnReflectionOnlyResolve(
				ResolveEventArgs args, DirectoryInfo directory)
			{
				Assembly loadedAssembly =
					AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
						.FirstOrDefault(
						  asm => string.Equals(asm.FullName, args.Name,
							  StringComparison.OrdinalIgnoreCase));

				if (loadedAssembly != null)
				{
					return loadedAssembly;
				}

				AssemblyName assemblyName =
					new AssemblyName(args.Name);
				string dependentAssemblyFilename =
					Path.Combine(directory.FullName,
					assemblyName.Name + ".dll");

				if (File.Exists(dependentAssemblyFilename))
				{
					return Assembly.ReflectionOnlyLoadFrom(
						dependentAssemblyFilename);
				}
				return Assembly.ReflectionOnlyLoad(args.Name);
			}


			/// <span class="code-SummaryComment"><summary></span>
			/// ReflectionOnlyLoad of single Assembly based on 
			/// the assemblyPath parameter
			/// <span class="code-SummaryComment"></summary></span>
			/// <span class="code-SummaryComment"><param name="assemblyPath">The path to the Assembly</param></span>
			[SuppressMessage("Microsoft.Performance",
				"CA1822:MarkMembersAsStatic")]
			internal Assembly LoadAssembly(String assemblyPath)
			{
				try
				{
					return Assembly.ReflectionOnlyLoadFrom(assemblyPath);
				}
				catch (FileNotFoundException)
				{
					/* Continue loading assemblies even if an assembly
                     * can not be loaded in the new AppDomain. */
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
					return null;
				}
			}
			#endregion
		}
	}

	public class LoadResult
	{
		public Assembly Assembly { get; set; }
		public string[] Namespaces { get; set; }

		public bool IsSuccess
		{
			get { return Assembly != null && Namespaces != null && Namespaces.Length > 0; }
		}
	}
}
