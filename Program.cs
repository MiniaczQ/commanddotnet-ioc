using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace commanddotnet_ioc;

public class Program
{
    public interface IMyService
    {
        public string Foo(string a, int b);
    }

    public class MyService : IMyService
    {
        public string Foo(string a, int b)
        {
            return a.PadLeft(b);
        }
    }

    public class Arguments : IArgumentModel
    {
        public string Arg1 = default!;
        public int Arg2;
    }

    static int Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IMyService>(new MyService());
        return new AppRunner<Program>()
            .UseMicrosoftDependencyInjection(services.BuildServiceProvider())
            .Run(args);
    }

    public void Test(
        Arguments args,
        IConsole consoleService,
        IMyService myService)
    {
        consoleService.WriteLine(myService.Foo(args.Arg1, args.Arg2));
    }
}
