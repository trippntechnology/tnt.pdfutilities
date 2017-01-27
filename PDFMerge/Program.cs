using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;


namespace PDFMerge
{
	class Program
	{
		static void Main(string[] args)
		{
			Parameters parms = new Parameters();

			if (!parms.ParseArgs(args))
			{
				return;
			}

			using (MemoryStream ms = new MemoryStream())
			{
				var newDoc = new Document();
				var newWriter = new PdfCopy(newDoc, ms);
				newDoc.Open();

				PdfReader reader1 = new PdfReader(parms.File1);
				PdfReader reader2 = new PdfReader(parms.File2);

				int pageCount = Math.Max(reader1.NumberOfPages, reader2.NumberOfPages);

				for (int f1PgNo = 1; f1PgNo <= pageCount; f1PgNo++)
				{
					newWriter.AddPage(newWriter.GetImportedPage(reader1, f1PgNo));
					try
					{
						int f2PgNo = parms.Reverse ? reader2.NumberOfPages - (f1PgNo - 1) : f1PgNo;
						newWriter.AddPage(newWriter.GetImportedPage(reader2, f2PgNo));
					}
					catch { }
				}

				newDoc.Close();
				File.WriteAllBytes(parms.Output, ms.ToArray());
			}
		}
	}
}
