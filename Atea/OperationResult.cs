namespace Atea
{
    public class OperationResult
    {
        public OperationResult(bool success, int id)
        {
            Success = success;
            Id = id;
        }
        public bool Success { get; }

        public int Id { get; }
    }
}
