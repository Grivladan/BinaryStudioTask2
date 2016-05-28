using System;

class Program
{
    static void Main(string[] args)
    {
        AdressBook book = new AdressBook();
        LoggerProviderFactory factory = LoggerProviderFactory.GetInstance();
        try
        {
            ILogger log = factory.GetLoggingProvider(LoggingProviders.Consol);
            book.UserAdded += log.Info;     //подписка на события
            book.UserRemoved += log.Info;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: "+e.Message);
        }
        User u1 = new User("Ivan", "Ivanov", new DateTime(1993, 4, 5), "Kyiv", "Sechenova,6", "0446785676","mail","ivan93@ukr.net");
        User u2 = new User("Petro", "Petrenko", new DateTime(1995, 6, 7), "Lviv", "Franko,12", "0678945632", "mail","petro95@gmail.com");
        book.AddUser(u1);
        book.AddUser(u2);
        book.AddUser(u1);
        book.RemoveUser(u1);
        book.RemoveUser(u1);
    }
}
