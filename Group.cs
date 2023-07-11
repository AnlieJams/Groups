using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

public class Group
{
    private List<Student> students;
    private string groupName;
    private string specialization;
    private int courseNumber;

    public Group()
    {
        students = GenerateDefaultStudents(10);
        groupName = "Default Group";
        specialization = "General";
        courseNumber = 1;
    }

    public Group(Student[] studentArray) : this()
    {
        students = studentArray.ToList();
    }

    public Group(List<Student> studentList) : this()
    {
        students = studentList;
    }

    public Group(Group otherGroup)
    {
        students = otherGroup.students.Select(s => new Student(s)).ToList();
        groupName = otherGroup.groupName;
        specialization = otherGroup.specialization;
        courseNumber = otherGroup.courseNumber;
    }

    public void DisplayAllStudents()
    {
        Console.WriteLine($"Group Name: {groupName}");
        Console.WriteLine($"Specialization: {specialization}");
        Console.WriteLine($"Course Number: {courseNumber}");
        Console.WriteLine("Students:");

        var sortedStudents = students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName);

        int index = 1;
        foreach (var student in sortedStudents)
        {
            Console.WriteLine($"{index}. {student.LastName}, {student.FirstName}");
            index++;
        }
    }

    public void AddStudentFromGroup(Student student, Group otherGroup)
    {
        if (otherGroup.students.Contains(student))
        {
            students.Add(student);
            otherGroup.students.Remove(student);
            Console.WriteLine($"Student {student.LastName}, {student.FirstName} added to {groupName} from {otherGroup.groupName}.");
        }
        else
        {
            Console.WriteLine($"Student {student.LastName}, {student.FirstName} does not exist in {otherGroup.groupName}.");
        }
    }

    public void ExpelUnderperformingStudents()
    {
        var underperformingStudents = students.Where(s => s.GetAverageGrade() < 60).ToList();

        foreach (var student in underperformingStudents)
        {
            students.Remove(student);
            Console.WriteLine($"Student {student.LastName}, {student.FirstName} has been expelled from {groupName} for poor performance.");
        }
    }

    public void ExpelWorstStudent()
    {
        var worstStudent = students.OrderBy(s => s.GetAverageGrade()).FirstOrDefault();

        if (worstStudent != null)
        {
            students.Remove(worstStudent);
            Console.WriteLine($"Student {worstStudent.LastName}, {worstStudent.FirstName} has been expelled from {groupName} for being the worst performer.");
        }
    }

    private List<Student> GenerateDefaultStudents(int count)
    {
        List<Student> defaultStudents = new List<Student>();

        for (int i = 0; i < count; i++)
        {
            string firstName = GenerateRandomName();
            string lastName = GenerateRandomName();
            DateTime dateOfBirth = GenerateRandomDateOfBirth();
            Address address = GenerateRandomAddress();
            string phoneNumber = GenerateRandomPhoneNumber();

            Student student = new Student(firstName, lastName, dateOfBirth, address, phoneNumber);
            defaultStudents.Add(student);
        }

        return defaultStudents;
    }

    private string GenerateRandomName()
    {
        string[] names = { "John", "Emma", "Michael", "Sophia", "James", "Olivia", "William", "Ava", "Joseph", "Isabella" };
        Random random = new Random();
        int index = random.Next(names.Length);
        return names[index];
    }

    private DateTime GenerateRandomDateOfBirth()
    {
        Random random = new Random();
        DateTime start = new DateTime(1990, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(random.Next(range));
    }

    private Address GenerateRandomAddress()
    {
        string[] streets = { "Main St", "Park Ave", "Broadway", "Elm St", "Oak St" };
        string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Philadelphia" };
        string[] postalCodes = { "10001", "90001", "60601", "77001", "19101" };

        Random random = new Random();
        int streetIndex = random.Next(streets.Length);
        int cityIndex = random.Next(cities.Length);
        int postalCodeIndex = random.Next(postalCodes.Length);

        string street = streets[streetIndex];
        string city = cities[cityIndex];
        string postalCode = postalCodes[postalCodeIndex];

        return new Address(street, city, postalCode);
    }

    private string GenerateRandomPhoneNumber()
    {
        Random random = new Random();
        long number = random.Next(100_000_000, 999_999_999);
        return number.ToString();
    }
}
