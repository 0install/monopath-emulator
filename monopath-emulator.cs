using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

static class MonoPathEmulator
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static int Main(string[] args)
	{
		if (args.LongLength == 0) throw new ArgumentException("Missing target");

		string targetPath = args[0];
		var targetArgs = new string[args.LongLength - 1];
		Array.Copy(args, 1, targetArgs, 0, args.LongLength - 1);

		AppDomain domain = AppDomain.CreateDomain("target");
		domain.AssemblyResolve += OnAssemblyResolve;
		try
		{
			return domain.ExecuteAssembly(targetPath, null, targetArgs);
		}
		catch (TypeInitializationException ex)
		{
			throw ex.InnerException;
		}
	}

	private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs e)
	{
		var assembly = new AssemblyName(e.Name);
		string[] prefixCandidates = (assembly.CultureInfo == null)
			? new[]
				{
					assembly.Name,
					Path.Combine(assembly.Name, assembly.Name)
				}
			: new[]
				{
					assembly.Name,
					Path.Combine(assembly.Name, assembly.Name),
					Path.Combine(assembly.CultureInfo.TwoLetterISOLanguageName, assembly.Name),
					Path.Combine(assembly.CultureInfo.TwoLetterISOLanguageName, Path.Combine(assembly.Name, assembly.Name))
				};

		string[] directories = (Environment.GetEnvironmentVariable("MONO_PATH") ?? "").Split(Path.PathSeparator);
		foreach (string directory in directories)
		{
			foreach (string prefix in prefixCandidates)
			{
				string assemblyPath = Path.Combine(directory, prefix + ".dll");
				if (File.Exists(assemblyPath))
					return Assembly.LoadFile(assemblyPath);
			}
			foreach (string prefix in prefixCandidates)
			{
				string assemblyPath = Path.Combine(directory, prefix + ".exe");
				if (File.Exists(assemblyPath))
					return Assembly.LoadFile(assemblyPath);
			}
		}
		return null;
	}
}
