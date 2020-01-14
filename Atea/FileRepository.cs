using System;
using System.IO;

namespace Atea
{
    public class FileRepository : IFileRepository
    {
        private readonly string _workingDirectory;
        private readonly ILogger _logger;
        private readonly string _fileType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="workingDirectory">working directory - created if does not exist.</param>
        /// <param name="fileType">file extension default as .txt</param>
        public FileRepository(ILogger logger, string workingDirectory, string fileType)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                throw new ArgumentException("Working directory can't be null.");
            }

            if (!Directory.Exists(workingDirectory))
            {
                Directory.CreateDirectory(workingDirectory);
            }

            _workingDirectory = workingDirectory;
            _logger = logger;
            _fileType = string.IsNullOrEmpty(fileType) ? "txt" : fileType;
        }

        public OperationResult WriteToFile(int id, string message)
        {
            _logger.LogInformation($"Writing to file: {GetFileFullPath(id)}");
            File.WriteAllText(GetFileFullPath(id), message);
            return new OperationResult(true, id);
        }

        public string ReadFromFile(int id)
        {
            _logger.LogInformation($"Reading from file: {GetFileFullPath(id)}");
            if (!File.Exists(GetFileFullPath(id)))
            {
                throw new FileNotFoundException($"File {GetFileFullPath(id)} not found.");
            }
            return File.ReadAllText(GetFileFullPath(id));
        }
        public OperationResult DeleteFile(int id)
        {
            if (!File.Exists(GetFileFullPath(id)))
            {
                throw new FileNotFoundException($"File {Path.Combine(_workingDirectory, $"{id}.txt")} not found.");
            }
            File.Delete(GetFileFullPath(id));

            return new OperationResult(true, id);
        }
        public OperationResult UpdateFile(int id, string message)
        {
            if (!File.Exists(GetFileFullPath(id)))
            {
                throw new FileNotFoundException($"File {GetFileFullPath(id)} not found.");
            }
            File.WriteAllText(GetFileFullPath(id), message);
            return new OperationResult(true, id);
        }

        private string GetFileFullPath(int id)
        {
            return Path.Combine(_workingDirectory, $"{id}.{_fileType}");
        }
    }
}
