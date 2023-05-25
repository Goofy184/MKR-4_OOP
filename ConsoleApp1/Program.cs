using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
public class Employee : IComparable<Employee>
{
    public string LastName { get; set; }
    public string PassportNumber { get; set; }
    public decimal Salary { get; set; }
    public int WorkExperience { get; set; }
    public Employee(string lastName, string passportNumber, decimal salary, int workExperience)
    {
        LastName = lastName;
        PassportNumber = passportNumber;
        Salary = salary;
        WorkExperience = workExperience;
    }
    public int CompareTo(Employee other)
    {
        return LastName.CompareTo(other.LastName);
    }
    public static void Serialize(List<Employee> employees, string fileName)
    {
        using (FileStream stream = new FileStream(fileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, employees);
        }
    }
    public static List<Employee> Deserialize(string fileName)
    {
        using (FileStream stream = new FileStream(fileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (List<Employee>)formatter.Deserialize(stream);
        }
    }
}
class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee("Melnyk", "AA111111", 1000m, 5),
            new Employee("Shevchenko", "BB222222", 2000m, 10),
            new Employee("Boyko", "CC333333", 1500m, 7)

        };
        Console.WriteLine("Enter new employee data:");
        Console.Write("Last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Passport number: ");
        string passportNumber = Console.ReadLine();
        Console.Write("Salary: ");
        decimal salary = decimal.Parse(Console.ReadLine());
        Console.Write("Work experience: ");
        int workExperience = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        employees.Add(new Employee(lastName, passportNumber, salary, workExperience));
        Employee.Serialize(employees, "employees.dat");
        List<Employee> deserializedEmployees = Employee.Deserialize("employees.dat");
        foreach (Employee employee in deserializedEmployees)
        {
            Console.WriteLine($"{employee.LastName} {employee.PassportNumber} {employee.Salary} {employee.WorkExperience}");

        }
        Console.ReadLine();
    }
}