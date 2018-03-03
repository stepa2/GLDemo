
namespace GLDemo
{
    internal static class Program
    {
        private static void Main()
        {
            using (var window = new DemoWindow())
                window.Run(120,120);
        }
    }
    
}
