using System;

namespace log4net.MicrosoftTeams.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = LogManager.GetLogger(typeof(Program));

            logger.Info("Info.");
            logger.Fatal("Fatal!", new ArgumentException("42"));

            Console.ReadKey();
        }
    }
}
