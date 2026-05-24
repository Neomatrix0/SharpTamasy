using System.ComponentModel.Design;
using System.Reflection;


public class Program
{
    static void Main(string[] args)
    {
        

        var service = new TaskService();

        bool rimani = true;

        while (rimani)
        {


            Console.WriteLine("Menu SharpTamasy");
            Console.WriteLine("1. Aggiungi task");
            Console.WriteLine("2. Mostra tutte le task");
            Console.WriteLine("3. Cerca task per id");
            Console.WriteLine("4. Elimina task");
            Console.WriteLine("0. Esci");
            Console.WriteLine("Scegli un opzione: ");

            string? opzione = Console.ReadLine();

            switch (opzione)
            {
                case "1":
                    Console.WriteLine("Titolo: ");
                    string title = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Descrizione: ");
                    string description = Console.ReadLine() ?? string.Empty;
                    service.AddTask(title, description);
                    Console.WriteLine("Task aggiunta con successo.");
                    break;


                case "2":
                    var tasks = service.GetAllTasks();
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("Nessun task presente.");
                    }
                    else
                    {
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"{task.Id}-{task.Title}-{task.Description}-{task.CreatedAt}-{task.Status}-{task.Type}");
                        }
                    }

                    break;


                case "3":

                    Console.WriteLine("Inserici Id: ");
                    int id = int.Parse(Console.ReadLine() ?? "0");
                    var taskById = service.GetTaskById(id);
                    if (taskById != null)
                    {
                        Console.WriteLine($"{taskById.Id}-{taskById.Title}-{taskById.Description}-{taskById.CreatedAt}-{taskById.Status}-{taskById.Type}");
                    }
                    else
                    {
                        Console.WriteLine("Task non trovato\n");
                    }
                    break;



                case "4":

                    Console.WriteLine("Inserisci Id da eliminare: ");
                    int deleteId = int.Parse(Console.ReadLine() ?? "0");
                    var existingTask = service.GetTaskById(deleteId);
                    if (existingTask == null)
                    {
                        Console.WriteLine($"Task id non trovato");
                    }
                    else
                    {
                        service.DeleteTask(deleteId);
                        Console.WriteLine("Task eliminato con successo\n");

                    }


                    break;



                case "0":
                    rimani = false;
                    Console.WriteLine("Uscita dal programma.");
                    break;

                default:
                    Console.WriteLine("Opzione non valida");
                    break;



            }



        }
    }
}