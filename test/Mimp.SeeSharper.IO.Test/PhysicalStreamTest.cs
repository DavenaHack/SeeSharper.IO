using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.IO.Provide;
using Mimp.SeeSharper.IO.Provide.Abstraction;
using System.IO;

namespace Mimp.SeeSharper.IO.Test
{
    [TestClass]
    public class PhysicalStreamTest
    {


        [TestCleanup]
        public void Clean()
        {
            if (Directory.Exists("path"))
                Directory.Delete("path", true);
        }



        [TestMethod]
        public void TestProvide()
        {
            var dir = Directory.GetCurrentDirectory();
            var provider = new PhysicalStreamProvider(dir);
            Assert.ThrowsException<ProvideIOException>(() => provider.ProvideStream(""));

            Assert.AreEqual(Path.GetFullPath(Path.Combine(dir, "path/to/file.tmp")), ((PhysicalStreamInfo)provider.ProvideStream("path/to/file.tmp")).Path);
        }


        [TestMethod]
        public void TestWrite()
        {
            var provider = new CurrentPhysicalStreamProvider();

            using (var stream = provider.OpenWrite("path/to/file.tmp"))
                stream.Write("temp");

            Assert.AreEqual("temp", File.ReadAllText("path/to/file.tmp"));

            using (var stream = provider.OpenWrite(@"path\to\file.tmp"))
            {
                stream.SeekEnd();
                stream.Write("temp");
            }

            Assert.AreEqual("temptemp", File.ReadAllText(@"path\to\file.tmp"));
        }


        [TestMethod]
        public void TestRead()
        {
            var provider = new CurrentPhysicalStreamProvider();

            using (var stream = provider.OpenRead("path/to/file.tmp"))
                Assert.AreEqual(0, stream.Length);

            Directory.CreateDirectory("path/to");
            File.WriteAllText("path/to/file.tmp", "temp");
            using (var stream = provider.OpenRead("path/to/file.tmp"))
                Assert.AreEqual("temp", stream.ReadString());

            using (var stream = provider.OpenRead(@"path\to\file.tmp"))
                Assert.AreEqual("temp", stream.ReadString());
        }

    }
}
