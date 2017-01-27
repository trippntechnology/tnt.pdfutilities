using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PDFCombine
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

			// Open the first PDF
			PdfReader reader = new PdfReader(parms.FileNames[0]);

			// Create new document with the same page format as the first PDF
			Document document = new Document(reader.GetPageSizeWithRotation(1));

			// Create a copy of the first PDF
			PdfCopy copy = new PdfCopy(document, new FileStream(parms.OutputFile, FileMode.Create));

			document.Open();

			for (int index = 0; index < parms.FileNames.Count; index++)
			{
				reader = new PdfReader(parms.FileNames[index]);

				// Add each page
				for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
				{
					copy.AddPage(copy.GetImportedPage(reader, pageNo));
				}
			}

			// Close the new document
			document.Close();
		}
	}
}
