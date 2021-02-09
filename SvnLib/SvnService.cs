using System;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml.Linq;

namespace SvnLib
{
	/// <summary>
	/// SVNサービス
	/// </summary>
	public class SvnService
	{
		/// <summary>
		/// SVNログを出力する。
		/// </summary>
		/// <param name="arguments">パラメータ</param>
		/// <param name="fileName">SVNファイルのパス</param>
		public static void Log(string arguments, string fileName)
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo(fileName, arguments)
				{
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					StandardOutputEncoding = Encoding.UTF8
				}
			};

			process.Start();
			process.WaitForExit();

			var document = XDocument.Load(process.StandardOutput);
			foreach (var entry in document.Elements("log").Elements("logentry"))
			{
				var revision = entry.Attribute("revision").Value;
				var message = entry.Elements("msg").FirstOrDefault().Value;
				Console.WriteLine("{0} : {1}", revision, message);
			}
		}

		/// <summary>
		/// Linuxから実行した場合、例外をスローするかを確認するためのテストメソッド
		/// </summary>
		/// <param name="fileName">SVNファイルのパス</param>
		public static void LogError(string fileName)
		{
			Process.Start(fileName, "", new SecureString(), "");
		}
	}
}
