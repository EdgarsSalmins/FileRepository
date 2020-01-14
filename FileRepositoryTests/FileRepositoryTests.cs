using System;
using System.IO;
using Atea;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileRepositoryTests
{
    [TestClass]
    public class FileRepositoryTests
    {
        [TestMethod]
        public void FileRepositoryInitTest()
        {
            Assert.ThrowsException<ArgumentException>(() => new FileRepository(new Logger(), null, null));
        }

        [TestMethod]
        public void WriteFileTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            var result = repository.WriteToFile(1, "abc");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1,result.Id);
            //test cleanup
            repository.DeleteFile(1);
        }

        [TestMethod]
        public void WriteExistingFileTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            var result = repository.WriteToFile(1, "abc");
            Assert.IsTrue(result.Success);
            Assert.ThrowsException<InvalidOperationException>(() =>repository.WriteToFile(1, "kkk"));
            //test cleanup
            repository.DeleteFile(1);
        }

        [TestMethod]
        public void ReadFileTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            var result = repository.WriteToFile(1, "abc");
            Assert.IsTrue(result.Success);
            var content = repository.ReadFromFile(1);
            Assert.AreEqual("abc",content);
            //test cleanup
            repository.DeleteFile(1);
        }
        [TestMethod]
        public void ReadNonExistingFileTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            Assert.ThrowsException<FileNotFoundException>(() => repository.ReadFromFile(55));
        }
        
        [TestMethod]
        public void WriteAndUpdateFileTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            var result = repository.WriteToFile(1, "abc");
            Assert.IsTrue(result.Success);
            result = repository.UpdateFile(1, "def");
            Assert.IsTrue(result.Success);
            var content = repository.ReadFromFile(1);
            Assert.AreEqual("def", content);
            //cleanup
            repository.DeleteFile(1);
        }

        [TestMethod]
        public void UpdateNonExistingFileTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            Assert.ThrowsException<FileNotFoundException>(() => repository.UpdateFile(1, "def"));
        }

        [TestMethod]
        public void WriteAndDeleteTest()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            var result = repository.WriteToFile(1, "gggg");
            Assert.IsTrue(result.Success);
            result = repository.DeleteFile(1);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void DeleteNonExistingFile()
        {
            IFileRepository repository = new FileRepository(new Logger(), "AA", null);
            Assert.ThrowsException<FileNotFoundException>(() => repository.DeleteFile(1));
        }
    }
}
