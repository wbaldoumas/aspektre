using Aspektre.Business;

namespace Aspektre
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new AspektreImageProcessor(args[0], int.Parse(args[1]), int.Parse(args[2])).Process();
        }
    }
}