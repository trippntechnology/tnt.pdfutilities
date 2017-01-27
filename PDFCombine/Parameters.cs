using System.Collections.Generic;
using TNT.Utilities.Console;

namespace PDFCombine
{
	public class Parameters : TNT.Utilities.Console.Parameters
	{
		static string OUTPUT = "o";
		static string FILE_LIST = "f";

		protected List<string> _FileNames = null;
		public string OutputFile { get { return (this[OUTPUT] as FileParameter).Value; } }
		public List<string> FileNames
		{
			get
			{
				if (_FileNames == null)
				{
					_FileNames = new List<string>((this[FILE_LIST] as FileListParameter).Value);
				}
				return _FileNames;
			}
		}

		public Parameters()
		{
			this.Add(new FileParameter(OUTPUT, "Destination file", true));
			this.Add(new FileListParameter(FILE_LIST, "Comma separated listing of files to combine", true));
		}
	}
}
