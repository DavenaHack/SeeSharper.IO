using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.IO.Provide;
using Mimp.SeeSharper.IO.Provide.Abstraction;
using Mimp.SeeSharper.IO.Provide.File;
using System.IO;

namespace Mimp.SeeSharper.IO.Test
{
    [TestClass]
    public class FileStreamProviderTest
    {

        private static readonly string _root = "path";
        private static readonly string _path = Path.Combine(_root, "to");
        private static readonly string _dir = Path.Combine(_path, "directory");
        private static readonly string _temp = Path.Combine(_path, "file.tmp");
        private static readonly string _unixTemp = "path/to/file.tmp";
        private static readonly string _windowsTemp = @"path\to\file.tmp";

        [TestCleanup]
        public void Clean()
        {
            if (Directory.Exists(_root))
                Directory.Delete(_root, true);
        }


        [TestMethod]
        public void TestProvide()
        {
            var dir = Directory.GetCurrentDirectory();
            var provider = new FileStreamProvider(dir);
            Assert.ThrowsException<ProvideIOException>(() => provider.ProvideStream(""));

            Assert.AreEqual(Path.GetFullPath(Path.Combine(dir, _temp)), ((FileStreamInfo)provider.ProvideStream(_temp)).Path);
        }


        [TestMethod]
        public void TestWrite()
        {
            var provider = new CurrentFileStreamProvider();

            using (var stream = provider.OpenWrite(_unixTemp))
                stream.Write("temp");

            Assert.AreEqual("temp", File.ReadAllText(_temp));

            using (var stream = provider.OpenWrite(_windowsTemp))
            {
                stream.SeekEnd();
                stream.Write("temp");
            }

            Assert.AreEqual("temptemp", File.ReadAllText(_temp));
        }


        [TestMethod]
        public void TestRead()
        {
            var provider = new CurrentFileStreamProvider();

            using (var stream = provider.OpenRead(_temp))
                Assert.AreEqual(0, stream.Length);

            Directory.CreateDirectory(_path);
            File.WriteAllText(_temp, "temp");
            using (var stream = provider.OpenRead(_unixTemp))
                Assert.AreEqual("temp", stream.ReadString());

            using (var stream = provider.OpenRead(_windowsTemp))
                Assert.AreEqual("temp", stream.ReadString());
        }


        [TestMethod]
        public void TestDelete()
        {
            var provider = new CurrentFileStreamProvider();

            Assert.IsFalse(provider.Delete(_temp));

            Directory.CreateDirectory(_path);
            File.Create(_temp).Dispose();
            Assert.IsTrue(provider.Delete(_temp));
            Assert.IsFalse(File.Exists(_temp));
        }


        [TestMethod]
        public void TestCreateParent()
        {
            var provider = new CurrentFileStreamProvider();

            Assert.IsTrue(provider.CreateParent(_dir));
            Assert.IsTrue(Directory.Exists(_dir));
            Assert.IsFalse(provider.CreateParent(_dir));
        }


        [TestMethod]
        public void TestDeleteParent()
        {
            var provider = new CurrentFileStreamProvider();

            Assert.IsFalse(provider.DeleteParent(_dir));

            Directory.CreateDirectory(_dir);
            Assert.IsTrue(provider.DeleteParent(_dir));
            Assert.IsFalse(Directory.Exists(_dir));
        }


    }
}
