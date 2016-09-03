using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS422.Tests
{
    internal class TestWriter : System.IO.TextWriter
    {
        public string Line { get; set; }

        public override void WriteLine(string value)
        {
            Line += value + '\n';
        }

        public override System.Text.Encoding Encoding
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    [TestClass]
    public class NUnitTestClass
    {
        // public void <MethodTesting>_<Condition Testing>_<What you expect to happen>
        [TestMethod]
        public void NumberedTextWriter_OneLine_ExpectPass()
        {
            //arrange
            TestWriter test = new TestWriter();
            NumberedTextWriter tw = new NumberedTextWriter(test);
            //act
            tw.WriteLine("Hello");
            //assert
            Assert.AreEqual("1: Hello\n", test.Line);
        }

        [TestMethod]
        public void NumberedTextWriter_MultiLine_ExpectPass()
        {
            //arrange
            TestWriter test = new TestWriter();
            NumberedTextWriter tw = new NumberedTextWriter(test);
            //act
            tw.WriteLine("Hello");
            tw.WriteLine("World");
            //assert
            Assert.AreEqual("1: Hello\n2: World\n", test.Line);
        }

        [TestMethod]
        public void NumberedTextWriter_EmptyString_ExpectPass()
        {
            //arrange
            TestWriter test = new TestWriter();
            NumberedTextWriter tw = new NumberedTextWriter(test);
            //act
            tw.WriteLine("");
            //assert
            Assert.AreEqual("1: \n", test.Line);
        }

        [TestMethod]
        public void IndexedNumsStream_Regular256Buff_ExpectPass()
        {
            //arrange
            byte[] buffer = new byte[256];
            IndexedNumsStream test = new IndexedNumsStream(256);
            //act
            test.Read(buffer, 0, 256);
            //assert
            for (int i = 0; i < 256; i++)
                Assert.AreEqual(i % 256, buffer[i]);
        }
        [TestMethod]
		public void IndexedNumsStream_TooBigCount_ExpectPass()
		{
			//arrange
			byte[] buffer = new byte[256];
			IndexedNumsStream test = new IndexedNumsStream (long.MaxValue);
			//act
			test.Read(buffer, 0, 2086165146);
			//assert
			Assert.AreEqual(2086165146 % 256, buffer[2086165145]);
		}
		
        [TestMethod]
        public void IndexedNumsStream_negativeCount_Expect0()
        {
            //arrange
            byte[] buffer = new byte[256];
            IndexedNumsStream test = new IndexedNumsStream(256);
            //act
            test.Read(buffer, 0, -1);
            //assert
            Assert.AreEqual(0, buffer[0]);
        }

        [TestMethod]
        public void IndexedNumsStream_negativeOffset_Expect0()
        {
            //arrange
            byte[] buffer = new byte[256];
            IndexedNumsStream test = new IndexedNumsStream(256);
            //act
            test.Read(buffer, -1, 256);
            //assert
            Assert.AreEqual(0, buffer[0]);
        }
    }
}

