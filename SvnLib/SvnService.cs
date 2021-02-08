using System;
using System.Diagnostics;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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
		public static void Log(string arguments, string fileName = "svn")
		{
			var p = new Process
			{
				StartInfo = new ProcessStartInfo(fileName, arguments)
				{
					CreateNoWindow = true, // コンソールを開かない
					UseShellExecute = false, // シェル機能を使用しない
					RedirectStandardOutput = true, // 標準出力をリダイレクト
					StandardOutputEncoding = Encoding.UTF8 // 結果はUTF-8で来る
				}
			};
			p.Start();
			p.WaitForExit();
			var doc = XDocument.Load(p.StandardOutput);
			foreach (var entry in doc.Elements("log").Elements("logentry"))
			{
				Console.Write("{0} : ", entry.Attribute("revision")?.Value);
				foreach (var msg in entry.Elements("msg"))
				{
					Console.WriteLine(Regex.Replace(msg.Value, "\n(?!$)", "\r\n	"));
				}
			}
		}

		/// <summary>
		/// Linuxから実行した場合、例外をスローするかを確認するためのテストメソッド
		/// </summary>
		/// <param name="fileName">SVNファイルのパス</param>
		public static void LogError(string fileName = "svn")
		{
			Process.Start(fileName, "", new SecureString(), "");
		}
	}
}
