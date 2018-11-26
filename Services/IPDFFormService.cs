using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PDFFormFillingExample.Services
{
    public interface IPDFFormService<T> where T : class
    {
        Stream FillForm(Stream inputStream, T formModel);
        ICollection GetFormFields(Stream pdfStream);
    }
}
