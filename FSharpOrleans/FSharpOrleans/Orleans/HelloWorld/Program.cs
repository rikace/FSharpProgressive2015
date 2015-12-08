using System;
using Orleans;
using FSharpWorldInterfaces;

namespace HelloWorld
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // The Orleans silo environment is initialized in its own app domain in order to more
            // closely emulate the distributed situation, when the client and the server cannot
            // pass data via shared memory.
            AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo,
                AppDomainInitializerArguments = args,
            });

            GrainClient.Initialize("DevTestClientConfiguration.xml");

            var friend = GrainClient.GrainFactory.GetGrain<FSharpWorldInterfaces.IHello>(0);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Orleans Silo is running...");

            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                Console.Write("Message : ");
                string message = Console.ReadLine();

                if (String.IsNullOrEmpty(message))
                    break;

                Console.WriteLine("\n\n{0}\n\n", friend.SayHello(message).Result);
            }


            // ICalculator

            var calculator = GrainClient.GrainFactory.GetGrain<FSharpWorldInterfaces.ICalculator>(0);
            int state = 0;
            while (true)
            {
                Console.Write("operation ? : ");
                string operation = Console.ReadLine();
                Console.WriteLine();

                Console.Write("value ? : ");
                string valueStr = Console.ReadLine();
                int value = int.Parse(valueStr);
                
                switch (operation)
                {
                    case "+":
                        var addResult = calculator.Add(value).Result;
                        Console.WriteLine("The result of adding {0} and {1} is {2}", state, value, addResult);
                        state = addResult;
                        break;
                    case "-":
                        var subResult = calculator.Subtract(value).Result;
                        Console.WriteLine("The result of subtracting {0} and {1} is {2}", state, value, subResult);
                        state = subResult;
                        break;
                    case "*":
                        int mulResult = calculator.Multiply(value).Result;
                        Console.WriteLine("The result of multipling {0} and {1} is {2}", state, value, mulResult);
                        state = mulResult;
                        break;
                    default:
                        break;
                }



                
            }

            hostDomain.DoCallBack(ShutdownSilo);
        }

        static void InitSilo(string[] args)
        {
            hostWrapper = new OrleansHostWrapper(args);

            if (!hostWrapper.Run())
            {
                Console.Error.WriteLine("Failed to initialize Orleans silo");
            }
        }

        static void ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                hostWrapper.Dispose();
                GC.SuppressFinalize(hostWrapper);
            }
        }

        private static OrleansHostWrapper hostWrapper;
    }
}
