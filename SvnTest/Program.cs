using SvnLib;

namespace SvnTest
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length > 1)
			{
				var arguments = args[0];
				var fileName = args[1];
				SvnService.Log(arguments, fileName);
			}
			else
			{
				var fileName = args[0];
				SvnService.LogError(fileName);
			}
		}
	}
}
