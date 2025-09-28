using System;
using System.Collections.Generic;
using System.Linq;

class Patient
{
    public string Name { get; set; }
    public Patient(string name) => Name = name;
}

class Doctor
{
    public string Name { get; set; }
    public List<Patient> Patients { get; } = new List<Patient>();
    public Doctor(string name) => Name = name;
}

class Room
{
    public List<Patient> Patients { get; } = new List<Patient>();
    public bool HasFreeBed
    {
        get { return Patients.Count < 3; }
    }
}

class Department
{
    public string Name { get; set; }
    public List<Room> Rooms { get; } = new List<Room>();

    public Department(string name)
    {
        Name = name;
        for (int i = 0; i < 20; i++)
            Rooms.Add(new Room());
    }

    public void AddPatient(Patient patient)
    {
        foreach (var room in Rooms)
        {
            if (room.HasFreeBed)
            {
                room.Patients.Add(patient);
                return;
            }
        }
    }

    public IEnumerable<Patient> GetAllPatients()
    {
        foreach (var room in Rooms)
        {
            foreach (var p in room.Patients)
                yield return p;
        }
    }
}

class Hospital
{
    public List<Department> Departments { get; } = new List<Department>();
    public List<Doctor> Doctors { get; } = new List<Doctor>();

    public Department GetOrCreateDepartment(string name)
    {
        foreach (var dept in Departments)
        {
            if (dept.Name == name)
                return dept;
        }
        var newDept = new Department(name);
        Departments.Add(newDept);
        return newDept;
    }

    public Doctor GetOrCreateDoctor(string name)
    {
        foreach (var doc in Doctors)
        {
            if (doc.Name == name)
                return doc;
        }
        var newDoc = new Doctor(name);
        Doctors.Add(newDoc);
        return newDoc;
    }
}

class Program
{
    static void Main()
    {
        var hospital = new Hospital();
        string input;

        while ((input = Console.ReadLine()) != "Output")
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string departmentName = parts[0];
            string doctorName = parts[1] + " " + parts[2];
            string patientName = parts[3];

            var department = hospital.GetOrCreateDepartment(departmentName);
            var doctor = hospital.GetOrCreateDoctor(doctorName);
            var patient = new Patient(patientName);

            department.AddPatient(patient);
            doctor.Patients.Add(patient);
        }

        while ((input = Console.ReadLine()) != "End")
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1) 
            {
                string departmentName = parts[0];
                var dept = hospital.Departments.First(d => d.Name == departmentName);
                foreach (var p in dept.GetAllPatients())
                    Console.WriteLine(p.Name);
            }
            else if (parts.Length == 2 && int.TryParse(parts[1], out int roomNumber))
            {
                string departmentName = parts[0];
                var dept = hospital.Departments.First(d => d.Name == departmentName);
                var patients = dept.Rooms[roomNumber - 1].Patients.OrderBy(p => p.Name);
                foreach (var p in patients)
                    Console.WriteLine(p.Name);
            }
            else 
            {
                string doctorName = parts[0] + " " + parts[1];
                var doc = hospital.Doctors.First(d => d.Name == doctorName);
                foreach (var p in doc.Patients.OrderBy(p => p.Name))
                    Console.WriteLine(p.Name);
            }
        }
    }
}
