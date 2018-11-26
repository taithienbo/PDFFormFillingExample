using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using PDFFormFillingExample.Domain;

namespace PDFFormFillingExample.Services
{
    public class SamplePDFFormService : IPDFFormService<PDFSampleForm>
    {
        public SamplePDFFormService()
        {
        }

        public Stream FillForm(Stream inputStream, PDFSampleForm model)
        {
            Stream outStream = new MemoryStream();
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            Stream inStream = null; 
            try
            {
                pdfReader = new PdfReader(inputStream);
                pdfStamper = new PdfStamper(pdfReader, outStream);
                AcroFields form = pdfStamper.AcroFields;
                form.SetField(SampleFormFieldNames.FirstName, model.FirstName);
                form.SetField(SampleFormFieldNames.LastName, model.LastName);
                form.SetField(SampleFormFieldNames.IAmAwesomeCheck, model.AwesomeCheck ? "Yes" : "Off");
                // set this if you want the result PDF to be uneditable. 
                pdfStamper.FormFlattening = true;
                return outStream; 
            }
            finally
            {
                pdfStamper?.Close();
                pdfReader?.Close();
                inStream?.Close(); 
            }
        }

        public ICollection GetFormFields(Stream pdfStream)
        {
            PdfReader reader = null; 
            try 
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                AcroFields acroFields = pdfReader.AcroFields;
                return acroFields.Fields.Keys;
            }
            finally
            {
                reader?.Close(); 
            }
        }
    }
}
