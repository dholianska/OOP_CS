using System;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;

class Program
{
    static void Main()
    {
        using var context = new HospitalContext();
        // context.Database.EnsureCreated();

        while (true)
        {
            Console.WriteLine("=== Hospital System ===");
            Console.WriteLine("1. Add patient");
            Console.WriteLine("2. View all patients");
            Console.WriteLine("3. Add doctor");
            Console.WriteLine("4. View all doctors");
            Console.WriteLine("5. Add visitation");
            Console.WriteLine("0. Exit");
            Console.Write("Select: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddPatient(context);
                    break;
                case "2":
                    ViewPatients(context);
                    break;
                case "3":
                    AddDoctor(context);
                    break;
                case "4":
                    ViewDoctors(context);
                    break;
                case "5":
                    AddVisitation(context);
                    break;
                case "0":
                    return;
            }

            Console.WriteLine();
        }
    }

    static void AddPatient(HospitalContext context)
    {
        Console.Write("First name: ");
        string firstName = Console.ReadLine();

        Console.Write("Last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        context.Patients.Add(patient);
        context.SaveChanges();

        Console.WriteLine("Patient added!");
    }

    static void ViewPatients(HospitalContext context)
    {
        foreach (var p in context.Patients)
        {
            Console.WriteLine($"{p.PatientId}: {p.FirstName} {p.LastName} - {p.Email}");

            if (p.Visitations.Count > 0)
            {
                Console.WriteLine("  Visitations:");
                foreach (var v in p.Visitations)
                {
                    string doctorName = v.Doctor != null ? v.Doctor.Name : "No doctor";
                    Console.WriteLine($"    {v.Date}: {doctorName} - {v.Comments}");
                }
            }
        }
    }

    static void AddDoctor(HospitalContext context)
    {
        Console.Write("Doctor name: ");
        string name = Console.ReadLine();

        Console.Write("Specialty: ");
        string specialty = Console.ReadLine();

        var doctor = new Doctor
        {
            Name = name,
            Specialty = specialty
        };

        context.Doctors.Add(doctor);
        context.SaveChanges();

        Console.WriteLine("Doctor added!");
    }

    static void ViewDoctors(HospitalContext context)
    {
        foreach (var d in context.Doctors)
        {
            Console.WriteLine($"{d.DoctorId}: {d.Name} - {d.Specialty}");
        }
    }

    static void AddVisitation(HospitalContext context)
    {
        Console.Write("Patient ID: ");
        int patientId = int.Parse(Console.ReadLine());

        Console.Write("Doctor ID (optional, press Enter to skip): ");
        string doctorInput = Console.ReadLine();
        int? doctorId = string.IsNullOrEmpty(doctorInput) ? null : int.Parse(doctorInput);

        Console.Write("Comments: ");
        string comments = Console.ReadLine();

        var visitation = new Visitation
        {
            PatientId = patientId,
            DoctorId = doctorId,
            Date = DateTime.Now,
            Comments = comments
        };

        context.Visitations.Add(visitation);
        context.SaveChanges();

        Console.WriteLine("Visitation added!");
    }
}
