
using System;
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
        default:
            Console.WriteLine("Invalid Option.");
            break;
    }
}

void PrintInfo(Student s)
{
    Console.WriteLine($"\n===== INFO FOR {s.FirstName} {s.LastName} =====");

    Console.WriteLine("\n--- Grades ---");
    if (s.Grades != null && s.Grades.Count > 0)
    {
        for (int i = 0; i < s.Grades.Count; i++)
            Console.WriteLine($"{i + 1}) {s.Grades[i]}");
    }
    else
    {
        Console.WriteLine("No grades.");
    }

    Console.WriteLine("\n--- Praises ---");
    if (s.Praises != null && s.Praises.Count > 0)
    {
        for (int i = 0; i < s.Praises.Count; i++)
            Console.WriteLine($"{i + 1}) {s.Praises[i]}");
    }
    else
    {
        Console.WriteLine("No praises.");
    }

    Console.WriteLine("\n--- Remarks ---");
    if (s.Remarks != null && s.Remarks.Count > 0)
    {
        for (int i = 0; i < s.Remarks.Count; i++)
            Console.WriteLine($"{i + 1}) {s.Remarks[i]}");
    }
    else
    {
        Console.WriteLine("No remarks.");
    }

    Console.WriteLine("\n==============================\n");
}

void AddGrade(Student s)
{
    Console.Write("Enter grade value (2-6): ");
    if (!double.TryParse(Console.ReadLine(), out double value) || value < 2 || value > 6)
    {
        Console.WriteLine("Invalid grade");
        return;
    }

    Console.WriteLine("\nChoose Grade Reason:");

    var reasons = Enum.GetNames(typeof(GradeReason)).ToList();

    for (int i = 0; i < reasons.Count; i++)
        Console.WriteLine($"{i + 1}) {reasons[i]}");


    Console.Write("Choose reason (number or name): ");

    var reasonInput = (Console.ReadLine() ?? "").Trim();

    GradeReason reason;

    if (int.TryParse(reasonInput, out int rIdx) && rIdx >= 1 && rIdx <= reasons.Count)
        reason = Enum.Parse<GradeReason>(reasons[rIdx - 1]);

    else if (!Enum.TryParse<GradeReason>(reasonInput, true, out reason))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    Console.WriteLine("\nChoose Subject:");

    var subjects = Enum.GetNames(typeof(Subject)).ToList();

    for (int i = 0; i < subjects.Count; i++)
        Console.WriteLine($"{i + 1}) {subjects[i]}");

    Console.Write("Choose subject (number or name): ");

    var subjectInput = (Console.ReadLine() ?? "").Trim();

    Subject subject;

    if (int.TryParse(subjectInput, out int sIdx) && sIdx >= 1 && sIdx <= subjects.Count)
        subject = Enum.Parse<Subject>(subjects[sIdx - 1]);

    else if (!Enum.TryParse<Subject>(subjectInput, true, out subject))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    Console.Write("Teacher first name: ");

    var tFirst = Console.ReadLine() ?? "";

    Console.Write("Teacher last name: ");

    var tLast = Console.ReadLine() ?? "";

    var teacher = new Teacher(tFirst, tLast);

    var grade = new Grade(subject, value, reason, teacher);
    s.AddGrade(grade);

    
    SaveData(students);
    Console.WriteLine("Grade added and data saved.");
}
void AddPraise(Student s)
{
    Console.WriteLine("\nChoose Subject:");

    var subjects = Enum.GetNames(typeof(Subject)).ToList();

    for (int i = 0; i < subjects.Count; i++)
        Console.WriteLine($"{i + 1}) {subjects[i]}");

    Console.Write("Choose subject (number or name): ");

    var subjectInput = (Console.ReadLine() ?? "").Trim();

    Subject subject;

    if (int.TryParse(subjectInput, out int sIdx) && sIdx >= 1 && sIdx <= subjects.Count)
        subject = Enum.Parse<Subject>(subjects[sIdx - 1]);

    else if (!Enum.TryParse<Subject>(subjectInput, true, out subject))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    Console.WriteLine("\nChoose Praise Type:");

    var types = Enum.GetNames(typeof(PraiseType)).ToList();

    for (int i = 0; i < types.Count; i++)
        Console.WriteLine($"{i + 1}) {types[i]}");

    Console.Write("Choose type (number or name): ");

    var typeInput = (Console.ReadLine() ?? "").Trim();

    PraiseType ptype;

    if (int.TryParse(typeInput, out int pIdx) && pIdx >= 1 && pIdx <= types.Count)
        ptype = Enum.Parse<PraiseType>(types[pIdx - 1]);

    else if (!Enum.TryParse<PraiseType>(typeInput, true, out ptype))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    var praise = new Praise(subject, ptype);

    s.AddPraise(praise);

    SaveData(students);

    Console.WriteLine("Praise added and data saved.");
}
void AddRemark(Student s)
{
    Console.WriteLine("\nChoose Subject:");

    var subjects = Enum.GetNames(typeof(Subject)).ToList();

    for (int i = 0; i < subjects.Count; i++)
        Console.WriteLine($"{i + 1}) {subjects[i]}");

    Console.Write("Choose subject (number or name): ");

    var subjectInput = (Console.ReadLine() ?? "").Trim();

    Subject subject;

    if (int.TryParse(subjectInput, out int sIdx) && sIdx >= 1 && sIdx <= subjects.Count)
        subject = Enum.Parse<Subject>(subjects[sIdx - 1]);

    else if (!Enum.TryParse<Subject>(subjectInput, true, out subject))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    Console.WriteLine("\nChoose Remark Type:");

    var types = Enum.GetNames(typeof(RemarkType)).ToList();

    for (int i = 0; i < types.Count; i++)
        Console.WriteLine($"{i + 1}) {types[i]}");

    Console.Write("Choose type (number or name): ");

    var typeInput = (Console.ReadLine() ?? "").Trim();
    RemarkType rtype;

    if (int.TryParse(typeInput, out int rIdx) && rIdx >= 1 && rIdx <= types.Count)
        rtype = Enum.Parse<RemarkType>(types[rIdx - 1]);

    else if (!Enum.TryParse<RemarkType>(typeInput, true, out rtype))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    var remark = new Remark(subject, rtype);

    s.AddRemark(remark);

    SaveData(students);

    Console.WriteLine("Remark added and data saved.");
}

void DeleteStudent()
{
    Console.Write("Search student to delete (first, last or full name): ");
    var term = (Console.ReadLine() ?? "").Trim();
    if (string.IsNullOrEmpty(term)) { Console.WriteLine("Empty input."); return; }

    var matches = students
        .Select((st, idx) => new { Student = st, Index = idx })
        .Where(x =>
            x.Student.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            x.Student.LastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            $"{x.Student.FirstName} {x.Student.LastName}".Contains(term, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (matches.Count == 0) { Console.WriteLine("No matches."); return; }

    if (matches.Count == 1)
    {
        var s = matches[0].Student;
        Console.Write($"Remove {s.FirstName} {s.LastName}? (y/N): ");
        if ((Console.ReadLine() ?? "").Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            students.Remove(s);
            SaveData(students);
            Console.WriteLine("Student removed.");
        }
        return;
    }

    Console.WriteLine("Multiple matches:");

    foreach (var m in matches)
        Console.WriteLine($"{m.Index + 1}) {m.Student.FirstName} {m.Student.LastName}");

    Console.Write("Choose number to remove: ");

    if (int.TryParse(Console.ReadLine(), out int sel) && sel >= 1 && sel <= students.Count)
    {
        var chosen = students[sel - 1];

        Console.Write($"Remove {chosen.FirstName} {chosen.LastName}? (y/N): ");
        if ((Console.ReadLine() ?? "").Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            students.Remove(chosen);

            SaveData(students);

            Console.WriteLine("Student removed.");
        }
    }
    else
    {
        Console.WriteLine("Invalid selection.");
    }
}


void SearchStudent()
{
    Console.Write("Search by name");
    var term = (Console.ReadLine() ?? "").Trim();
    if (string.IsNullOrEmpty(term))
    {
        Console.WriteLine("Empty search.");
        return;
    }

    var matches = students
       .Select((st, idx) => new { Student = st, Index = idx })
       .Where(x =>
           x.Student.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
           x.Student.LastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
           $"{x.Student.FirstName} {x.Student.LastName}".Contains(term, StringComparison.OrdinalIgnoreCase))
       .ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine("No matches.");
        return;
    }
    Console.WriteLine("\n--- Results ---");
    foreach (var m in matches)
        Console.WriteLine($"{m.Index + 1}) {m.Student.FirstName} {m.Student.LastName}");

    Console.Write("Press (1) to view details or (0) to cancel: ");

    if (int.TryParse(Console.ReadLine(), out int sel) && sel >= 1 && sel <= students.Count)
    {
        var chosen = matches.FirstOrDefault(m => m.Index == sel - 1);

        if (chosen != null)
        {
            PrintInfo(chosen.Student);
        }
        else
        {
            Console.WriteLine("Selected student is not in the search results.");
        }
    }
    else
    {
        Console.WriteLine("Cancelled or invalid selection.");
    }
}


void StudentDetails()
{
    if (students.Count == 0)
    {
        Console.WriteLine("No students available.");
        return;
    }

    Console.Write("Enter student first name, last name or full name: ");

    var input = (Console.ReadLine() ?? "").Trim();

    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Empty input.");
        return;
    }

    var matches = students.Where(s =>
            string.Equals($"{s.FirstName} {s.LastName}", input, StringComparison.OrdinalIgnoreCase)
            || s.FirstName.Equals(input, StringComparison.OrdinalIgnoreCase)
            || s.LastName.Equals(input, StringComparison.OrdinalIgnoreCase)
            || s.FirstName.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0
            || s.LastName.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
        .ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine("No student found");
        return;
    }

    Student s;

    if (matches.Count == 1)
    {
        s = matches[0];
    }
    else
    {
        Console.WriteLine("Multiple matches:");
        for (int i = 0; i < matches.Count; i++)
            Console.WriteLine($"{i + 1}) {matches[i].FirstName} {matches[i].LastName}");

        Console.Write("Choose match number: ");
        if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 1 || sel > matches.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        s = matches[sel - 1];
    }

    bool TryChooseSubject(out Subject chosen)
    {
        chosen = default;
        var names = Enum.GetNames(typeof(Subject)).ToList();
        Console.WriteLine("Choose subject (number or name) or 0 for All:");
        Console.WriteLine("0) All subjects");
        for (int i = 0; i < names.Count; i++)
            Console.WriteLine($"{i + 1}) {names[i]}");

        var raw = (Console.ReadLine() ?? "").Trim();
        if (raw == "0") return false;

        if (int.TryParse(raw, out int idx) && idx >= 1 && idx <= names.Count)
        {
            chosen = Enum.Parse<Subject>(names[idx - 1]);
            return true;
        }

        if (Enum.TryParse<Subject>(raw, true, out var parsed))
        {
            chosen = parsed;
            return true;
        }

        Console.WriteLine("Invalid subject selection.");
        return TryChooseSubject(out chosen);
    }

    while (true)
    {
        Console.WriteLine($"\n===== {s.FirstName} {s.LastName} =====");
        Console.WriteLine("1) View info");
        Console.WriteLine("2) Add Grade");
        Console.WriteLine("3) Add Praise");
        Console.WriteLine("4) Add Remark");
        Console.WriteLine("5) Remove Grade");
        Console.WriteLine("6) Remove Praise");
        Console.WriteLine("7) Remove Remark");
        Console.WriteLine("8) Back");
        Console.Write("Choose: ");

        var choice = Console.ReadLine();
        if (choice == null) { Console.WriteLine("Invalid option."); continue; }

        switch (choice)
        {
            case "1":
                PrintInfo(s);
                break;
            case "2":
                AddGrade(s);
                break;
            case "3":
                AddPraise(s);
                break;
            case "4":
                AddRemark(s);
                break;
            case "5":
                {

                    if (s.Grades == null || s.Grades.Count == 0) { Console.WriteLine("No grades."); break; }
                    if (TryChooseSubject(out var subj))
                    {
                        var filtered = s.Grades.Where(g => g.Subject == subj).ToList();
                        if (filtered.Count == 0) { Console.WriteLine("No grades for that subject."); break; }
                        for (int i = 0; i < filtered.Count; i++) Console.WriteLine($"{i + 1}) {filtered[i]}");
                        Console.Write("Enter grade number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int gidx) && gidx >= 1 && gidx <= filtered.Count)
                        {
                            var toRemove = filtered[gidx - 1];
                            s.Grades.Remove(toRemove);
                            Console.WriteLine("Grade removed.");
                        }
                        else Console.WriteLine("Invalid selection.");
                    }
                    else
                    {

                        for (int i = 0; i < s.Grades.Count; i++) Console.WriteLine($"{i + 1}) {s.Grades[i]}");
                        Console.Write("Enter grade number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int gidx) && gidx >= 1 && gidx <= s.Grades.Count)
                        {
                            s.Grades.RemoveAt(gidx - 1);
                            Console.WriteLine("Grade removed.");
                        }
                        else Console.WriteLine("Invalid selection.");
                    }
                }
                break;
            case "6":
                {
                    if (s.Praises == null || s.Praises.Count == 0) { Console.WriteLine("No praises."); break; }
                    if (TryChooseSubject(out var subj))
                    {
                        var filtered = s.Praises.Where(p => p.Subject == subj).ToList();
                        if (filtered.Count == 0) { Console.WriteLine("No praises for that subject."); break; }
                        for (int i = 0; i < filtered.Count; i++) Console.WriteLine($"{i + 1}) {filtered[i]}");
                        Console.Write("Enter praise number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int pidx) && pidx >= 1 && pidx <= filtered.Count)
                        {
                            var toRemove = filtered[pidx - 1];
                            s.Praises.Remove(toRemove);
                            Console.WriteLine("Praise removed.");
                        }
                        else Console.WriteLine("Invalid selection.");
                    }
                    else
                    {
                        for (int i = 0; i < s.Praises.Count; i++) Console.WriteLine($"{i + 1}) {s.Praises[i]}");
                        Console.Write("Enter praise number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int pidx) && pidx >= 1 && pidx <= s.Praises.Count)
                        {
                            s.Praises.RemoveAt(pidx - 1);
                            Console.WriteLine("Praise removed.");
                        }
                        else Console.WriteLine("Invalid selection.");
                    }
                }
                break;
            case "7":
                {
                    if (s.Remarks == null || s.Remarks.Count == 0) { Console.WriteLine("No remarks."); break; }
                    if (TryChooseSubject(out var subj))
                    {
                        var filtered = s.Remarks.Where(r => r.Subject == subj).ToList();
                        if (filtered.Count == 0) { Console.WriteLine("No remarks for that subject."); break; }
                        for (int i = 0; i < filtered.Count; i++) Console.WriteLine($"{i + 1}) {filtered[i]}");
                        Console.Write("Enter remark number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int ridx) && ridx >= 1 && ridx <= filtered.Count)
                        {
                            var toRemove = filtered[ridx - 1];
                            s.Remarks.Remove(toRemove);
                            Console.WriteLine("Remark removed.");
                        }
                        else Console.WriteLine("Invalid selection.");
                    }
                    else
                    {
                        for (int i = 0; i < s.Remarks.Count; i++) Console.WriteLine($"{i + 1}) {s.Remarks[i]}");
                        Console.Write("Enter remark number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int ridx) && ridx >= 1 && ridx <= s.Remarks.Count)
                        {
                            s.Remarks.RemoveAt(ridx - 1);
                            Console.WriteLine("Remark removed.");
                        }
                        else Console.WriteLine("Invalid selection.");
                    }
                }
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
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
