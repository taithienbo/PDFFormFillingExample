using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDFFormFillingExample.Domain;
using PDFFormFillingExample.Services;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PDFFormFillingExampleTests.Services
{

    public class SamplePDFFormServiceTests
    {
        private const string SamplePDFFilePath = "SamplePDFForm.pdf";
        
        private readonly IPDFFormService<PDFSampleForm> _samplePDFFormService;
        private readonly ITestOutputHelper _output; 

        public SamplePDFFormServiceTests(ITestOutputHelper output)
        {
            _samplePDFFormService = new SamplePDFFormService();
            _output = output; 
        }

        /// <summary>
        /// Test retrieving field names of a PDF form. 
        /// @<see cref="IPDFFormService{T}.GetFormFields(System.IO.Stream)"/>
        /// </summary>
        [Fact]
        public void GetFieldNames()
        {
            Stream pdfStream = new FileStream(path:SamplePDFFilePath, 
                                              mode:FileMode.Open);
            ICollection fieldNames = _samplePDFFormService.GetFormFields(pdfStream);
            foreach (var fieldName in fieldNames)
            {
                _output.WriteLine(fieldName.ToString());
            }
            pdfStream.Close(); 
        }

        /// <summary>
        /// Test setting values for the fields in a PDF.
        /// @<see cref="IPDFFormService{T}.FillForm(System.IO.Stream, T)"/>
        /// </summary>
        [Fact]
        public void FillForm()
        {
            PDFSampleForm sampleFormModel = new PDFSampleForm()
            {
                FirstName = "John",
                LastName = "Doe",
                AwesomeCheck = true

            };
            using (Stream pdfInputStream = new FileStream(path: "SamplePDFForm.pdf", mode: FileMode.Open))
            using(Stream resultPDFOutputStream = new FileStream(path: "SamplePDFFormFilled.pdf", mode: FileMode.Create))
            using(Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, sampleFormModel))
            {
                resultPDFStream.Position = 0; 
                _output.WriteLine(resultPDFStream.Position.ToString());
                resultPDFStream.CopyTo(resultPDFOutputStream);
            }
        }
    }
}
