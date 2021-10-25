namespace CompileTech.Respond
{
    public class ErrorResult
    {
        public ErrorResultType Type { get; protected set; } = ErrorResultType.Unhandled;
        public string Subject { get; protected set; }
        public string Message { get; protected set; }
        public object TranslationData { get; protected set; }
    }
}