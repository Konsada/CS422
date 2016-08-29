using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace CS422
{
    public class NumberedTextWriter : TextWriter
    {
        private int currentLineNumber;
        private TextWriter _wrapThis;
        private string someString;
        NumberedTextWriter(TextWriter wrapThis)
        {
            currentLineNumber = 1;
        }
        NumberedTextWriter(TextWriter wrapThis, int startingLineNumber)
        {
            currentLineNumber = startingLineNumber;
        }
        public override Encoding Encoding
        {
            get { return _wrapThis.Encoding; }
        }
        public override void WriteLine(string value)
        {
            _wrapThis.Write(currentLineNumber.ToString() + ": " + value);
            currentLineNumber++;
        }
    }
}