
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Shkolo.Models;
using Shkolo.Enums;
using System.Diagnostics;

var options = new JsonSerializerOptions
{
    WriteIndented = true
};

List<Student> students = LoadData();

string json = File.ReadAllText("data.json");

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
            SaveData(students);
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
    Console.Write("Enter grade value(2-6):");
    if(!double.TryParse(Console.ReadLine(), out double value))
    {
        Console.WriteLine("invalid grade");
        return;
    }
    Console.WriteLine("\nChoose Grade Reason:");
    int index1 = 0;
    
    foreach (var name in Enum.GetNames(typeof(GradeReason)))
    {
        Console.WriteLine($"{index1} + {name}");
        index1++;
    }

    Console.Write("choose");
    string input = Console.ReadLine()!;

    GradeReason reason = new GradeReason();

    switch (input) 
    {
        case "1": reason = GradeReason.Exam; break;

        case "2": reason = GradeReason.ActiveParticipation; break;

        case "3": reason = GradeReason.Homework; break;
        
        case "4": reason = GradeReason.Project; break;

        case "5": reason = GradeReason.OralExamination; break;

        case "6": reason = GradeReason.EntranceExam; break;

        case "7": reason = GradeReason.TermExam; break;

        case "8": reason = GradeReason.FinalExam; break;

        case "9": reason = GradeReason.Other; break;

        default: Console.WriteLine("Invalid option"); break;
    }

    int index2 = 0;

    foreach (var subjectName in Enum.GetNames(typeof(Subject)))
    {
        Console.WriteLine($"{index2} + {subjectName}");
        index2++;
    }

    Console.Write("Choose subject: ");
    string subjectInput = Console.ReadLine()!;

    Subject subject = new Subject();

    switch (subjectInput)
    {
        case "1": subject = Subject.Bulgarian; break;

        case "2": subject = Subject.Mathematics; break;

        case "3": subject = Subject.Physics; break;

        case "4": subject = Subject.Chemistry; break;

        case "5": subject = Subject.Biology; break;

        case "6": subject = Subject.History; break;

        case "7": subject = Subject.Geography; break;

        case "8": subject = Subject.English; break;

        case "9": subject = Subject.Programming; break;

        case "10": subject = Subject.Business; break;

        case "11": subject = Subject.Design; break;

        case "12": subject = Subject.Russian; break;

        case "13": subject = Subject.PhysicalEducation; break;

        case "14": subject = Subject.German; break;

        case "15": subject = Subject.Spanish; break;

        case "16": subject = Subject.Philosophy; break;

        case "17": subject = Subject.Informatics; break;

        default: Console.WriteLine("Invalid option"); break;
    }

    Console.Write("Enter teacher name: ");
    Teacher teacher = new Teacher(Console.ReadLine()!, Console.ReadLine()!);

    Grade grade = new Grade(subject, value, reason, teacher);

    s.AddGrade(grade);

    File.WriteAllText("data.json", JsonSerializer.Serialize(grade, options));
}
void AddPraise(Student s)
{
    Console.Write("Choose praise reason: ");

    string praiseSubject = Console.ReadLine()!;

    Subject subject = new Subject();

    switch (praiseSubject)
    {
        case "1": subject = Subject.Bulgarian; break;

        case "2": subject = Subject.Mathematics; break;

        case "3": subject = Subject.Physics; break;

        case "4": subject = Subject.Chemistry; break;

        case "5": subject = Subject.Biology; break;

        case "6": subject = Subject.History; break;

        case "7": subject = Subject.Geography; break;

        case "8": subject = Subject.English; break;

        case "9": subject = Subject.Programming; break;

        case "10": subject = Subject.Business; break;

        case "11": subject = Subject.Design; break;

        case "12": subject = Subject.Russian; break;

        case "13": subject = Subject.PhysicalEducation; break;

        case "14": subject = Subject.German; break;

        case "15": subject = Subject.Spanish; break;

        case "16": subject = Subject.Philosophy; break;

        case "17": subject = Subject.Informatics; break;

        default: Console.WriteLine("Invalid option"); break;
    }

    Console.Write("Enter praise type: ");
    string praiseType = Console.ReadLine()!;

    PraiseType prType = new PraiseType();

    switch (praiseType)
    {
        case "1": prType = PraiseType.ActiveParticipation; break;

        case "2": prType = PraiseType.ExcellentPerformance; break;

        case "3": prType = PraiseType.Improvement; break;

        case "4": prType = PraiseType.Teamwork; break;

        case "5": prType = PraiseType.Leadership; break;

        case "6": prType = PraiseType.Creativity; break;

        case "7": prType = PraiseType.Consistency; break;

        case "8": prType = PraiseType.Responsibility; break;

        case "9": prType = PraiseType.TaskCompletion; break;

        case "10": prType = PraiseType.Nap; break;

        case "11": prType = PraiseType.Other; break;

        default: Console.WriteLine("Invalid option"); break;
    }
    Praise praise = new Praise(subject, prType);

    s.AddPraise(praise);

    File.WriteAllText("data.json", JsonSerializer.Serialize(praise, options));
}
void AddRemark(Student s)
{
    Console.Write("Choose remark reason: ");

    string remarkSubject = Console.ReadLine()!;

    Subject subject = new Subject();

    switch (remarkSubject)
    {
        case "1": subject = Subject.Bulgarian; break;

        case "2": subject = Subject.Mathematics; break;

        case "3": subject = Subject.Physics; break;

        case "4": subject = Subject.Chemistry; break;

        case "5": subject = Subject.Biology; break;

        case "6": subject = Subject.History; break;

        case "7": subject = Subject.Geography; break;

        case "8": subject = Subject.English; break;

        case "9": subject = Subject.Programming; break;

        case "10": subject = Subject.Business; break;

        case "11": subject = Subject.Design; break;

        case "12": subject = Subject.Russian; break;

        case "13": subject = Subject.PhysicalEducation; break;

        case "14": subject = Subject.German; break;

        case "15": subject = Subject.Spanish; break;

        case "16": subject = Subject.Philosophy; break;

        case "17": subject = Subject.Informatics; break;

        default: Console.WriteLine("Invalid option"); break;
    }

    Console.Write("Enter remark type: ");
    string type = Console.ReadLine()!;

    RemarkType rmType = new RemarkType();

    switch (type)
    {
        case "1": rmType = RemarkType.NoHomework; break;

        case "2": rmType = RemarkType.DisruptiveBehavior; break;

        case "3": rmType = RemarkType.LateSubmission; break;

        case "4": rmType = RemarkType.ConcentrationIssues; break;

        case "5": rmType = RemarkType.BadDiscipline; break;

        case "6": rmType = RemarkType.SleepingInClass; break;

        case "7": rmType = RemarkType.EatingInClass; break;

        case "8": rmType = RemarkType.UnpreparedForClass; break;

        case "9": rmType = RemarkType.RudeBehavior; break;

        case "10": rmType = RemarkType.Other; break;

        default: Console.WriteLine("Invalid option"); break;
    }

    Remark remark = new Remark(subject, rmType);

    s.AddRemark(remark);

    File.WriteAllText("data.json", JsonSerializer.Serialize(remark, options));
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

void AddTeacher()
{
    Console.Write("Teahcer first name: ");
    var teacherFirst = Console.ReadLine() ?? "";

    Console.Write("Teacher last name: ");
    var teacherLast = Console.ReadLine() ?? "";

    Teacher teacher = new Teacher(teacherFirst, teacherLast);

    File.WriteAllText("data.json", JsonSerializer.Serialize(teacher, options));
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



File.WriteAllText("data.json", "");
string str = JsonSerializer.Serialize("black", options);
*/
//>>>>>>> Stashed changes
