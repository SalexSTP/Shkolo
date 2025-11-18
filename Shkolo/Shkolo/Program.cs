<<<<<<< Updated upstream
﻿// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
=======
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Shkolo.Models;
using Shkolo.Enums;



var options = new JsonSerializerOptions
{
    WriteIndented = true
};

List<Student> students = LoadData();

// simple menu loop
while (true)
{
    Console.WriteLine("\n===== SHKOLO MENU =====");
    Console.WriteLine("1) Add Student");
    Console.WriteLine("2) List Students");
    Console.WriteLine("3) Student Details");
    Console.WriteLine("4) Search Student");
    Console.WriteLine("5) Delete Student");
    Console.WriteLine("6) Save & Exit");
    Console.WriteLine("7) Find student ID by name");
    Console.Write("Choose: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": AddStudent(students); break;
        case "2": ListStudents(students); break;
        case "3": StudentDetails(); break;
        case "4": SearchStudent(); break;
        case "5": DeleteStudent(); break;
        case "6":
            SaveData(students); // Pass the 'students' list to the SaveData method.
            Console.WriteLine("Saved. Goodbye!");
            return;
        case "7":
            ShowStudentIdByName();
            break;
        default:
            Console.WriteLine("Invalid Option.");
            break;
    }
}

void PrintInfo(Student s)
{
}

void AddGrade(Student s)
{
}
void AddPraise(Student s)
{
}
void AddRemark(Student s)
{
}

void DeleteStudent()
{
}


void SearchStudent()
{
}


void StudentDetails()
{
}


void AddStudent(List<Student> list)
{
    Console.Write("First name: ");
    var first = Console.ReadLine() ?? "";

    Console.Write("Last name: ");
    var last = Console.ReadLine() ?? "";

    var student = new Student(first, last);
    list.Add(student);

    Console.WriteLine("Student added.");
}

void ListStudents(List<Student> list)
{
    if (list.Count == 0)
    {
        Console.WriteLine("No students yet.");
        return;
    }

    Console.WriteLine("\n--- Students ---");
    for (int i = 0; i < list.Count; i++)
    {
        Console.WriteLine($"{i + 1}) {list[i].FirstName} {list[i].LastName}");
    }
}


void GetStudentById()
{
}

void ShowStudentIdByName()
{
}


List<Student> LoadData()
{
    if (!File.Exists("data.json") || new FileInfo("data.json").Length == 0)
        return new List<Student>();

    try
    {
        return JsonSerializer.Deserialize<List<Student>>(File.ReadAllText("data.json"))
               ?? new List<Student>();
    }
    catch
    {
        return new List<Student>();
    }
}

void SaveData(List<Student> list)
{
    File.WriteAllText("data.json", JsonSerializer.Serialize(list, options));
}




/*using System.Text.Json;
using System.Text.Json.Nodes;

JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

File.WriteAllText("data.json", "");
string str = JsonSerializer.Serialize("black", options);
*/
>>>>>>> Stashed changes
