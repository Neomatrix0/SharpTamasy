using System.ComponentModel.Design;
using System.Reflection;

public class Program
{
    static void Main(string[] args)
    {
        using var db = new AppDbContext();

        var service = new TaskService();

        var bug = new BugTask
        {
            Title = "Error with UI",
            Description = "Button doesn't work",
            Severity = BugSeverity.High
        };

        db.Bugs.Add(bug);
        db.SaveChanges();

        Console.WriteLine("Bug salvato nel database.");



        bool rimani = true;
        string? opzione = Console.ReadLine();
        

        while (rimani)
        {

            
            Console.WriteLine("Menu SharpTamasy" );
            Console.WriteLine("1. Aggiungi task");
            Console.WriteLine("2. Mostra tutte le task");
            Console.WriteLine("3. Cerca task per id");
            Console.WriteLine("4. Elimina task");
            Console.WriteLine("0. Esci");

            switch (opzione)
            {
              case "1":
              Console.WriteLine("Titolo: ");
              string title = Console.ReadLine();
              Console.WriteLine("Descrizione: ");
              string description = Console.ReadLine();
              service.AddTask(title,description);
              break;


             case "2":
            var tasks = service.GetAllTasks();
            foreach(var task in tasks)
                    {
                        Console.WriteLine($"{task.Id}-{task.Title}-{task.Description}-{task.CreatedAt}-{task.Status}-{task.Type}");
                    }
            break;

            }



        }
    }
}