using Microsoft.EntityFrameworkCore;

public class Program
{
    static void Main(string[] args)
    {
        using var db = new AppDbContext();
        db.Database.Migrate();

        var service = new TaskService(db);
        var menu = new ConsoleMenu(service);
        menu.ShowMenu();
    }
}