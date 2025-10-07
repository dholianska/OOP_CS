using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ISoldier
{
    string FirstName { get; }
    string LastName { get; }
    int Id { get; }
} 
public interface IPrivate : ISoldier
{
    decimal Salary { get; }
}
interface ILeutenantGeneral : IPrivate
{
    List<IPrivate> Privates { get; }
}
interface ISpecialisedSoldier : IPrivate
{
    string Corps { get; }
}
interface IEngineer : ISpecialisedSoldier // + додатковий клас для ремонту
{
    List<Repair> Repairs { get; }
}
interface ICommandos : ISpecialisedSoldier // + додатковий клас для місій
{
    List<Mission> Missions { get; }
} 
interface ISpy : ISoldier
{
    int CodeNumber {  get; }
}

public class Repair
{
    public string PartName { get; }
    public int HoursWorked { get; }
    public Repair(string partName, int hoursWorked)
    {
        PartName = partName;
        HoursWorked = hoursWorked;
    }
    public override string ToString()
    {
        return $"Part Name: {PartName}, HoursWorked: {HoursWorked}";
    }
}
public class Mission
{
    public string CodeName { get; }
    public string State { get; private set; }
    public Mission(string codeName, string state)
    {
        CodeName = codeName;
        if (state != "inProgress" && state != "Finished")
        {
            throw new ArgumentException("Invalid state of mission!");
        }
        State = state;
    }
    public string CompleteMission()
    {
        State = "Finished";
        return State;
    }
    public override string ToString()
    {
        return $"Code Name: {CodeName}, State: {State}";
    }
}

public abstract class Soldier : ISoldier
{
    public string FirstName { get; }
    public string LastName { get; }
    public int Id { get; }
    protected Soldier(int id, string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }
    public override string ToString()
    {
        return $"Name: {FirstName} {LastName}, Id: {Id}";
    }
}

public class Private : Soldier, IPrivate
{
    public decimal Salary { get; }
    public Private(int id, string firstName, string lastName, decimal salary) 
        : base(id, firstName, lastName)
    {
        Salary = salary;
    }
    public override string ToString()
    {
        return base.ToString() + $", Salary: {Salary:F2}";
    }
}

public class LeutenantGeneral : Private, ILeutenantGeneral
{
    public List<IPrivate> Privates { get; }

    public LeutenantGeneral(int  id, string firstName, string lastName, decimal salary)
        : base(id, firstName, lastName, salary)
    {
        Privates = new List<IPrivate>();
    }

    public void AddPrivate(IPrivate @private)
        => Privates.Add(@private);

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine("Privates:");
        foreach (var p in Privates.OrderByDescending(p => p.Salary))
            sb.AppendLine("  " + p);
        return sb.ToString().TrimEnd();
    }
}

public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
{
    public string Corps { get; }
    protected SpecialisedSoldier(int id, string firstName, string lastName, decimal salary, string corps) : base(id, firstName, lastName, salary)
    {
        if (corps != "Airforces" && corps != "Marines")
        {
            throw new ArgumentException("Invalid value for corps!");
        }
        Corps = corps;
    }
    public override string ToString()
    {
        return base.ToString() + $"\nCorps: {Corps}";
    }
}

public class Engineer : SpecialisedSoldier, IEngineer
{
    public List<Repair> Repairs { get; }
    public Engineer(int id, string firstName, string lastName, decimal salary, string corps)
        : base(id, firstName, lastName, salary, corps)
    {
        Repairs = new List<Repair>();
    }
    public void AddRepair(Repair repair)
        => Repairs.Add(repair);
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine("Repairs: ");
        foreach (var r in Repairs)
            sb.AppendLine("  " + r);
        return sb.ToString().TrimEnd();
    }
}

public class Commando : SpecialisedSoldier, ICommandos
{
    public List<Mission> Missions { get; }
    public Commando(int id, string firstName, string lastName, decimal salary, string corps)
        : base(id, firstName, lastName, salary, corps)
    {
        Missions = new List<Mission>();
    }
    public void AddMission(Mission mission)
    => Missions.Add(mission);

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine("Missions:");
        foreach (var m in Missions)
            sb.AppendLine("  " + m);
        return sb.ToString().TrimEnd();
    }
}

public class Spy : Soldier, ISpy
{
    public int CodeNumber { get; }

    public Spy(int id, string firstName, string lastName, int codeNumber)
        : base(id, firstName, lastName)
    {
        CodeNumber = codeNumber;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nCode Number: {CodeNumber}";
    }
}

class Program
{
    static void Main()
    {
        List<ISoldier> soldiers = new List<ISoldier>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string type = parts[0];

            try
            {
                switch (type)
                {
                    case "Private":
                        {
                            int id = int.Parse(parts[1]);
                            string firstName = parts[2];
                            string lastName = parts[3];
                            decimal salary = decimal.Parse(parts[4]);

                            soldiers.Add(new Private(id, firstName, lastName, salary));
                            break;
                        }

                    case "LeutenantGeneral":
                        {
                            int id = int.Parse(parts[1]);
                            string firstName = parts[2];
                            string lastName = parts[3];
                            decimal salary = decimal.Parse(parts[4]);

                            LeutenantGeneral general = new LeutenantGeneral(id, firstName, lastName, salary);

                            foreach (var pid in parts.Skip(5))
                            {
                                IPrivate soldier = soldiers.FirstOrDefault(s => s is IPrivate p && p.Id == int.Parse(pid)) as IPrivate;
                                if (soldier != null)
                                    general.AddPrivate(soldier);
                            }

                            soldiers.Add(general);
                            break;
                        }

                    case "Engineer":
                        {
                            int id = int.Parse(parts[1]);
                            string firstName = parts[2];
                            string lastName = parts[3];
                            decimal salary = decimal.Parse(parts[4]);
                            string corps = parts[5];

                            Engineer engineer = new Engineer(id, firstName, lastName, salary, corps);

                            for (int i = 6; i < parts.Length; i += 2)
                            {
                                string partName = parts[i];
                                int hoursWorked = int.Parse(parts[i + 1]);
                                engineer.AddRepair(new Repair(partName, hoursWorked));
                            }

                            soldiers.Add(engineer);
                            break;
                        }

                    case "Commando":
                        {
                            int id = int.Parse(parts[1]);
                            string firstName = parts[2];
                            string lastName = parts[3];
                            decimal salary = decimal.Parse(parts[4]);
                            string corps = parts[5];

                            Commando commando = new Commando(id, firstName, lastName, salary, corps);

                            for (int i = 6; i < parts.Length; i += 2)
                            {
                                string codeName = parts[i];
                                string state = parts[i + 1];
                                commando.AddMission(new Mission(codeName, state));
                            }

                            soldiers.Add(commando);
                            break;
                        }

                    case "Spy":
                        {
                            int id = int.Parse(parts[1]);
                            string firstName = parts[2];
                            string lastName = parts[3];
                            int codeNumber = int.Parse(parts[4]);

                            soldiers.Add(new Spy(id, firstName, lastName, codeNumber));
                            break;
                        }
                }
            }
            catch
            {
                continue;
            }
        }

        foreach (var soldier in soldiers)
        {
            Console.WriteLine(soldier);
        }
    }
}