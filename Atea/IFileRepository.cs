namespace Atea
{
    public interface IFileRepository
    {
        OperationResult WriteToFile(int id, string message);

        string ReadFromFile(int id);

        OperationResult DeleteFile(int id);

        OperationResult UpdateFile(int id, string message);
    }
}
