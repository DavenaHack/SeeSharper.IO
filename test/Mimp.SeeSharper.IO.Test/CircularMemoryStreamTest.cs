using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Mimp.SeeSharper.IO.Test
{
    [TestClass]
    public class CircularMemoryStreamTest
    {


        [TestMethod]
        public void TestRead()
        {
            var buffer = new byte[16];
            var stream = new CircularMemoryStream(buffer, 4, 8);

            var expect = new byte[4] { 1, 0, 1, 0 };
            stream.Write(expect);
            stream.SeekStart();
            Assert.IsTrue(stream.ReadBytes(4).SequenceEqual(expect));

            expect = new byte[8] { 0, 1, 0, 1, 0, 1, 0, 1 };
            stream.Write(expect);
            stream.SeekStart();
            Assert.IsTrue(stream.ReadBytes(8).SequenceEqual(expect));
        }


        [TestMethod]
        public void TestWrite()
        {
            var buffer = new byte[16];
            var streamBuffer = buffer.Skip(4).Take(8);
            var stream = new CircularMemoryStream(buffer, 4, 8);

            stream.Write(new byte[4] { 1, 0, 1, 0 });
            Assert.IsTrue(new byte[8] { 1, 0, 1, 0, 0, 0, 0, 0 }.SequenceEqual(streamBuffer));

            stream.Write(new byte[6] { 1, 1, 1, 1, 0, 0 });
            Assert.IsTrue(new byte[8] { 0, 0, 1, 0, 1, 1, 1, 1 }.SequenceEqual(streamBuffer));

            stream.Write(new byte[12] { 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0 });
            Assert.IsTrue(new byte[8] { 1, 1, 0, 1, 0, 0, 0, 0 }.SequenceEqual(streamBuffer));

            Assert.IsTrue(buffer.Take(4).All(b => b == 0) && buffer.Skip(12).All(b => b == 0));
        }


    }
}
