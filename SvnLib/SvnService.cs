using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SvnLib
{
	public class SvnService
	{
		public static void Log()
		{
			Process p = new Process();
			p.StartInfo = new ProcessStartInfo("svn", "log --xml -v todo -l 5")
			{
				CreateNoWindow = true, // コンソールを開かない
				UseShellExecute = false, // シェル機能を使用しない
				RedirectStandardOutput = true, // 標準出力をリダイレクト
				StandardOutputEncoding = Encoding.UTF8 // 結果はUTF-8で来る
			};
			p.Start(); // アプリの実行開始
			p.WaitForExit();

			XDocument doc = XDocument.Load(p.StandardOutput);
			foreach (XElement entry in doc.Elements("log").Elements("logentry"))
			{
				// revision 属性の取得
				Console.Write("■r{0} ", entry.Attribute("revision").Value);
				foreach (XElement msg in entry.Elements("msg"))
				{
					Console.WriteLine(Regex.Replace(msg.Value, "\n(?!$)", "\r\n	"));
				}
			}
		}
	}
}
