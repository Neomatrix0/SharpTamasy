using Microsoft.EntityFrameworkCore;

public class Program
{
    static void Main(string[] args)
    {
        using var db = new AppDbContext();
        db.Database.Migrate();

        var service = new TaskService(db);
        bool rimani = true;

        while (rimani)
        {
            Console.WriteLine();
            Console.WriteLine("Menu SharpTamasy");
            Console.WriteLine("1. Aggiungi task");
            Console.WriteLine("2. Mostra tutte le task");
            Console.WriteLine("3. Cerca task per id");
            Console.WriteLine("4. Elimina task");
            Console.WriteLine("5. Modifica task");
            Console.WriteLine("0. Esci");
            Console.Write("Scegli un'opzione: ");

            string? opzione = Console.ReadLine();

            switch (opzione)
            {
                case "1":
                    Console.Write("Titolo: ");
                    string title = Console.ReadLine() ?? string.Empty;

                    Console.Write("Descrizione: ");
                    string description = Console.ReadLine() ?? string.Empty;

                    Console.Write("Assegnato a: ");
                    string assignedTo = Console.ReadLine() ?? string.Empty;

                    Console.WriteLine("Tipo di task:");
                    Console.WriteLine("1. Task normale");
                    Console.WriteLine("2. Bug task");
                    Console.WriteLine("3. Feature task");
                    Console.Write("Scelta: ");

                    string? tipoScelta = Console.ReadLine();

                    TaskCreator? creator = null;

                    var data = new TaskCreationData
                    {
                        Title = title,
                        Description = description,
                        AssignedTo = assignedTo
                    };

                    switch (tipoScelta)
                    {
                        case "1":
                            creator = new BasicTaskCreator();
                            break;

                        case "2":
                            Console.Write("Severita (1.Low, 2.Medium, 3.High): ");
                            string? severityChoice = Console.ReadLine();

                            data.Severity = severityChoice switch
                            {
                                "1" => BugSeverity.Low,
                                "2" => BugSeverity.Medium,
                                "3" => BugSeverity.High,
                                _ => BugSeverity.Low
                            };

                            creator = new BugTaskCreator();
                            break;

                        case "3":
                            Console.Write("Priorita (1.Low, 2.Normal, 3.Urgent): ");
                            string? priorityChoice = Console.ReadLine();

                            data.Priority = priorityChoice switch
                            {
                                "1" => FeaturePriority.Low,
                                "2" => FeaturePriority.Normal,
                                "3" => FeaturePriority.Urgent,
                                _ => FeaturePriority.Normal
                            };

                            creator = new FeatureTaskCreator();
                            break;

                        default:
                            Console.WriteLine("Tipo non valido.");
                            break;
                    }

                    if (creator == null)
                    {
                        break;
                    }

                    TaskItem createdTask = creator.CreateTask(data);
                    service.AddTask(createdTask);

                    Console.WriteLine("Task aggiunta con successo.");
                    break;

                case "2":
                    var tasks = service.GetAllTasks();

                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("Nessuna task presente.");
                    }
                    else
                    {
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"{task.Id} - {task.Title} - {task.Description} - {task.CreatedAt} - {task.Status} - {task.Type} - Assegnato a: {task.AssignedTo}");
                        }
                    }
                    break;

                case "3":
                    Console.Write("Inserisci Id: ");

                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var taskById = service.GetTaskById(id);

                        if (taskById == null)
                        {
                            Console.WriteLine("Task non trovata.");
                        }
                        else
                        {
                            Console.WriteLine($"{taskById.Id} - {taskById.Title} - {taskById.Description} - {taskById.CreatedAt} - {taskById.Status} - {taskById.Type} - Assegnato a: {taskById.AssignedTo}");

                            if (taskById is BugTask bug)
                            {
                                Console.WriteLine($"Severity: {bug.Severity}");
                            }
                            else if (taskById is FeatureTask feature)
                            {
                                Console.WriteLine($"Priority: {feature.Priority}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Id non valido.");
                    }
                    break;

                case "4":
                    Console.Write("Inserisci Id da eliminare: ");

                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        var existingTask = service.GetTaskById(deleteId);

                        if (existingTask == null)
                        {
                            Console.WriteLine("Task non trovata.");
                        }
                        else
                        {
                            service.DeleteTask(deleteId);
                            Console.WriteLine("Task eliminata con successo.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Id non valido.");
                    }
                    break;

                case "5":
                    Console.Write("Inserisci Id da modificare: ");

                    if (int.TryParse(Console.ReadLine(), out int editId))
                    {
                        var existingTask = service.GetTaskById(editId);

                        if (existingTask == null)
                        {
                            Console.WriteLine("Task non trovata.");
                        }
                        else
                        {
                            Console.WriteLine($"{existingTask.Id} - {existingTask.Title} - {existingTask.Description} - {existingTask.CreatedAt} - {existingTask.Status} - {existingTask.Type} - Assegnato a: {existingTask.AssignedTo}");

                            Console.Write("Nuovo titolo (lascia vuoto per mantenere): ");
                            string newTitle = Console.ReadLine() ?? string.Empty;

                            Console.Write("Nuova descrizione (lascia vuoto per mantenere): ");
                            string newDescription = Console.ReadLine() ?? string.Empty;

                            Console.Write("Nuovo assegnatario (lascia vuoto per mantenere): ");
                            string newAssignedTo = Console.ReadLine() ?? string.Empty;

                            if (!string.IsNullOrWhiteSpace(newAssignedTo))
                            {
                                service.UpdateAssignedTo(editId, newAssignedTo);
                            }

                            Console.Write("Nuovo stato (1.Opened, 2.Completed, lascia vuoto per mantenere): ");
                            string? statusChoice = Console.ReadLine();

                            string updatedTitle = string.IsNullOrWhiteSpace(newTitle)
                                ? existingTask.Title
                                : newTitle;

                            string updatedDescription = string.IsNullOrWhiteSpace(newDescription)
                                ? existingTask.Description
                                : newDescription;

                            TaskStatus updatedStatus = statusChoice switch
                            {
                                "1" => TaskStatus.Opened,
                                "2" => TaskStatus.Completed,
                                _ => existingTask.Status
                            };

                            service.UpdateTask(editId, updatedTitle, updatedDescription, updatedStatus);

                            if (existingTask is BugTask)
                            {
                                Console.Write("Nuova severita (1.Low, 2.Medium, 3.High, lascia vuoto per mantenere): ");
                                string? severityChoice = Console.ReadLine();

                                BugSeverity? newSeverity = severityChoice switch
                                {
                                    "1" => BugSeverity.Low,
                                    "2" => BugSeverity.Medium,
                                    "3" => BugSeverity.High,
                                    _ => null
                                };

                                if (newSeverity.HasValue)
                                {
                                    service.UpdateBugSeverity(editId, newSeverity.Value);
                                }
                            }

                            if (existingTask is FeatureTask)
                            {
                                Console.Write("Nuova priorita (1.Low, 2.Normal, 3.Urgent, lascia vuoto per mantenere): ");
                                string? priorityChoice = Console.ReadLine();

                                FeaturePriority? newPriority = priorityChoice switch
                                {
                                    "1" => FeaturePriority.Low,
                                    "2" => FeaturePriority.Normal,
                                    "3" => FeaturePriority.Urgent,
                                    _ => null
                                };

                                if (newPriority.HasValue)
                                {
                                    service.UpdateFeaturePriority(editId, newPriority.Value);
                                }
                            }

                            Console.WriteLine("Task aggiornata con successo.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Id non valido.");
                    }
                    break;

                case "0":
                    rimani = false;
                    Console.WriteLine("Uscita dal programma.");
                    break;

                default:
                    Console.WriteLine("Opzione non valida.");
                    break;
            }
        }
    }
}
