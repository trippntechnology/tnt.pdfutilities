using iTextSharp.text.pdf;
using System;
using System.IO;
using TNT.Utilities.Console;

namespace PDFMerge
{
	public class Parameters : TNT.Utilities.Console.Parameters
	{
		static string FILE1 = "f1";
		static string FILE2 = "f2";
		static string OUTPUT = "o";
		static string REVERSE = "r";

		public string File1 { get { return (this[FILE1] as FileParameter).Value; } }
		public string File2 { get { return (this[FILE2] as FileParameter).Value; } }
		public string Output { get { return (this[OUTPUT] as FileParameter).Value; } }
		public new bool Reverse { get { return base.FlagExists(REVERSE); } }

		public Parameters()
		{
			this.Add(new FileParameter(FILE2, "File 2", true) { MustExist = true, Validate = ValidatePDF });
			this.Add(new FileParameter(FILE1, "File 1", true) { MustExist = true, Validate = ValidatePDF });
			this.Add(new FileParameter(OUTPUT, "Output file", true) { Validate = ValidatePDF });
			this.Add(new FlagParameter(REVERSE, $"Set to reverse pages in {FILE2}."));
		}

		public override bool ParseArgs(string[] args, Action<TNT.Utilities.Console.Parameters> postValidator = null, bool swallowException = true)
		{
			return base.ParseArgs(args, PostValidator, swallowException);
		}

		private void PostValidator(TNT.Utilities.Console.Parameters obj)
		{
			PdfReader reader1 = new PdfReader(this.File1);
			PdfReader reader2 = new PdfReader(this.File2);

			if (reader1.NumberOfPages < reader2.NumberOfPages || reader1.NumberOfPages > reader2.NumberOfPages + 1)
			{
				throw new ArgumentException(string.Format("{0} ({1}) must have an equal number of pages or no greater than one more than {2} ({3})", this.File1, reader1.NumberOfPages, this.File2, reader2.NumberOfPages));
			}
		}


		private void ValidatePDF(object obj)
		{
			string fileName = obj as string;

			if (Path.GetExtension(fileName).ToLower() != ".pdf")
			{
				throw new ArgumentException(string.Format("Unexpected file, {0}. PDF file expected.", fileName));
			}
		}
	}
}
