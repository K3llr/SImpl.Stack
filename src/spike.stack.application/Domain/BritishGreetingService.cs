namespace spike.stack.application.Domain
{
    public class BritishGreetingService : IGreetingService
    {
        public string SayHi(string name)
        {
            return $"Hello my love {name}, how are you today?";
        }

        public string SayBye(string name)
        {
            return $"Goodbye my love {name}, see you soon.";

        }
    }
}