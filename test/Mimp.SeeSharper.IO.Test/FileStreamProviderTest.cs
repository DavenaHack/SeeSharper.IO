using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.IO.Provide;
using Mimp.SeeSharper.IO.Provide.Abstraction;
using Mimp.SeeSharper.IO.Provide.File;
using System;
using System.IO;
using System.Linq;

namespace Mimp.SeeSharper.IO.Test
{
    [TestClass]
    public class FileStreamProviderTest
    {

        private static readonly string _root = Path.GetFullPath("path");
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


        [TestMethod]
        public void TestProvideChildrenRecursive()
        {
            var provider = new CurrentFileStreamProvider();

            var other0 = Path.Combine(_dir, "other0");
            var other1 = Path.Combine(_dir, "other1");

            Directory.CreateDirectory(other0);
            Directory.CreateDirectory(other1);

            Assert.IsTrue(new[] {
                _path,
                _dir,
                other0,
                other1
            }.SequenceEqual(provider.ProvideChildrenRecursive(_path)
                .Select(p => ((FileStreamParentInfo)p).Path)));
        }


        [TestMethod]
        public void TestGetStreamsRecursive()
        {
            var provider = new CurrentFileStreamProvider();

            var other0 = Path.Combine(_dir, "other0");
            var other1 = Path.Combine(_dir, "other1");

            var file0 = Path.Combine(other0, "temp.tmp");
            var file1 = Path.Combine(other1, "temp.tmp");

            Directory.CreateDirectory(other0);
            Directory.CreateDirectory(other1);
            File.Create(_temp).Dispose();
            File.Create(file0).Dispose();
            File.Create(file1).Dispose();

            System.Console.WriteLine(string.Join(", ", provider.GetStreamsRecursive(_path)
                .Select(u => Path.GetFullPath(u.LocalPath)).ToArray()));

            Assert.IsTrue(new[] {
                _temp,
                file0,
                file1
            }.SequenceEqual(provider.GetStreamsRecursive(_path)
                .Select(u => Path.GetFullPath(u.LocalPath))));
        }


    }
}
