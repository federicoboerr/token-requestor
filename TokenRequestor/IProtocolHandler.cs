namespace GetToken
{
    public interface IProtocolHandler
    {
        bool CanProcess(string response);

        string Process(string response);
    }
}
