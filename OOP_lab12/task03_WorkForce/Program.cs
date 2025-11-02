using System;
using System.Collections.Generic;

public interface IEmployee
{
    string Name { get; }
    int WorkHoursPerWeek { get; }
}

public class StandardEmployee : IEmployee
{
    public string Name { get; }
    public int WorkHoursPerWeek { get; } = 40;

    public StandardEmployee(string name)
    {
        Name = name;
    }
}

public class PartTimeEmployee : IEmployee
{
    public string Name { get; }
    public int WorkHoursPerWeek { get; } = 20;

    public PartTimeEmployee(string name)
    {
        Name = name;
    }
}

public delegate void JobDoneEventHandler(Job job);

public class Job
{
    public string Name { get; }
    public int HoursRequired { get; private set; }
    public IEmployee Employee { get; }

    public event JobDoneEventHandler? JobDone;

    public Job(string name, int hoursRequired, IEmployee employee)
    {
        Name = name;
        HoursRequired = hoursRequired;
        Employee = employee;
    }

    public void Update()
    {
        HoursRequired -= Employee.WorkHoursPerWeek;
        if (HoursRequired <= 0)
        {
            Console.WriteLine($"Job {Name} done!");
            JobDone?.Invoke(this);
        }
    }

    public override string ToString()
    {
        return $"Job: {Name} Hours Remaining: {HoursRequired}";
    }
}

public class JobList
{
    private List<Job> jobs = new List<Job>();

    public void Add(Job job)
    {
        jobs.Add(job);
        job.JobDone += OnJobDone;
    }

    private void OnJobDone(Job job)
    {
        jobs.Remove(job);
    }

    public void PassWeek()
    {
        foreach (var job in new List<Job>(jobs))
        {
            job.Update();
        }
    }

    public void Status()
    {
        foreach (var job in jobs)
        {
            Console.WriteLine(job);
        }
    }
}

class Program
{
    static void Main()
    {
        var employees = new Dictionary<string, IEmployee>();
        var jobList = new JobList();

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            switch (parts[0])
            {
                case "StandardEmployee":
                    employees[parts[1]] = new StandardEmployee(parts[1]);
                    break;

                case "PartTimeEmployee":
                    employees[parts[1]] = new PartTimeEmployee(parts[1]);
                    break;

                case "Job":
                    string jobName = parts[1];
                    int hours = int.Parse(parts[2]);
                    string empName = parts[3];
                    IEmployee emp = employees[empName];
                    jobList.Add(new Job(jobName, hours, emp));
                    break;

                case "Pass":
                    jobList.PassWeek();
                    break;

                case "Status":
                    jobList.Status();
                    break;
            }
        }
    }
}
