using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Oxage.Fonts
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			string directory = (args != null && args.Length > 0 ? args[0] : null);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(directory));
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Log(e.ExceptionObject as Exception);
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Log(e.Exception);
		}

		public static void Log(Exception ex)
		{
			try
			{
				//Append content "{Message}\n{Type}\n{StackTrace}\n\n" to the log file
				string content = ex.Message + Environment.NewLine + Environment.NewLine + ex.GetType() + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine;
				File.AppendAllText("error.log", content);
			}
			catch
			{
			}
		}
	}
}
