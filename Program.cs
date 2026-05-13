public class Program
{
    static void Main(string[] args)
    {
        using var db = new AppDbContext();

        var bug = new BugTask
        {
            Title = "Error with UI",
            Description = "Button doesn't work",
            Severity = BugSeverity.High
        };

        db.Bugs.Add(bug);
        db.SaveChanges();

        Console.WriteLine("Bug salvato nel database.");
    }
}