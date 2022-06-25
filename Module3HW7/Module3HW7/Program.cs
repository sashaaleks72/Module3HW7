namespace Module3HW7
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var starter = new Starter();
            await starter.Run();
        }
    }
}