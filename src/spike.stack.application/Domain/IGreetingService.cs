namespace spike.stack.application.Domain
{
    public interface IGreetingService
    {
        string SayHi(string name);
        string SayBye(string name);
    }
}