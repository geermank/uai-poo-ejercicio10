namespace KindergardenLib
{
    public class OperationResult<T> where T : class
    {
        private string message;
        private bool success;
        private T data;

        public string Message
        {
            get { return message; }
        }

        public bool Success
        {
            get { return success; }
        }

        public T Data
        {
            get { return data; }
        }

        public OperationResult(bool success, string message, T data)
        {
            this.success = success;
            this.message = message;
            this.data = data;
        }

        public OperationResult(bool success, string message)
        {
            this.success = success;
            this.message = message;
            this.data = null;
        }

        public OperationResult(bool success, T data)
        {
            this.success = success;
            this.message = null;
            this.data = data;
        }

        public OperationResult(bool success)
        {
            this.success = success;
            this.message = null;
            this.data = null;
        }
    }
}