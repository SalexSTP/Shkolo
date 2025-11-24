using System.Text.Json;
using Shkolo.Models;
using Shkolo.Enums;

var options = new JsonSerializerOptions { WriteIndented = true };

List<Student> students = LoadData();

// helper = clear screen + title
void ClearAndHeader(string title)
{
    Console.Clear();
    Console.WriteLine($"===== {title} =====\n");
}

// helper = wait before clearing
void Pause()
{
    Console.WriteLine("\nPress ENTER to continue...");
    Console.ReadLine();
}


// MAIN MENU LOOP
while (true)
{
    ClearAndHeader("SHKOLO MENU");
    Console.WriteLine("1) Add Student");
    Console.WriteLine("2) List Students");
    Console.WriteLine("3) Student Details");
    Console.WriteLine("4) Search Student");
    Console.WriteLine("5) Delete Student");
    Console.WriteLine("6) Save & Exit");
    Console.Write("\nChoose: ");

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
            Pause();
            break;
    }
}


// PRINT STUDENT INFO
void PrintInfo(Student s)
{
    ClearAndHeader($"INFO FOR {s.FirstName} {s.LastName}");

    Console.WriteLine("--- Grades ---");
    if (s.Grades?.Count > 0)
        for (int i = 0; i < s.Grades.Count; i++)
            Console.WriteLine($"{i + 1}) {s.Grades[i]}");
    else
        Console.WriteLine("No grades.");

    Console.WriteLine("\n--- Praises ---");
    if (s.Praises?.Count > 0)
        for (int i = 0; i < s.Praises.Count; i++)
            Console.WriteLine($"{i + 1}) {s.Praises[i]}");
    else
        Console.WriteLine("No praises.");

    Console.WriteLine("\n--- Remarks ---");
    if (s.Remarks?.Count > 0)
        for (int i = 0; i < s.Remarks.Count; i++)
            Console.WriteLine($"{i + 1}) {s.Remarks[i]}");
    else
        Console.WriteLine("No remarks.");

    Console.WriteLine("\n--- Average Grade ---");
    if (s.Grades?.Count > 0)
        Console.WriteLine($"Average Grade: {s.CalculateAverageGrade():F2}");
    else
        Console.WriteLine("No grades to calculate average.");

    Pause();
}


// ADD GRADE
void AddGrade(Student s)
{
    ClearAndHeader("ADD GRADE");

    Console.Write("Enter grade value (2-6): ");
    if (!double.TryParse(Console.ReadLine(), out double value) || value < 2 || value > 6)
    {
        Console.WriteLine("Invalid grade");
        Pause();
        return;
    }

    Console.WriteLine("\nChoose Grade Reason:");
    var reasons = Enum.GetNames(typeof(GradeReason)).ToList();
    for (int i = 0; i < reasons.Count; i++) Console.WriteLine($"{i + 1}) {reasons[i]}");

    Console.Write("\nChoose reason (number or name): ");
    var reasonInput = Console.ReadLine()?.Trim() ?? "";
    GradeReason reason;

    if (int.TryParse(reasonInput, out int rIdx) && rIdx >= 1 && rIdx <= reasons.Count)
        reason = Enum.Parse<GradeReason>(reasons[rIdx - 1]);
    else if (!Enum.TryParse(reasonInput, true, out reason))
    {
        Console.WriteLine("Invalid option");
        Pause();
        return;
    }

    Console.WriteLine("\nChoose Subject:");
    var subjects = Enum.GetNames(typeof(Subject)).ToList();
    for (int i = 0; i < subjects.Count; i++) Console.WriteLine($"{i + 1}) {subjects[i]}");

    Console.Write("\nChoose subject: ");
    var subjectInput = Console.ReadLine()?.Trim() ?? "";
    Subject subject;

    if (int.TryParse(subjectInput, out int sIdx) && sIdx >= 1 && sIdx <= subjects.Count)
        subject = Enum.Parse<Subject>(subjects[sIdx - 1]);
    else if (!Enum.TryParse(subjectInput, true, out subject))
    {
        Console.WriteLine("Invalid option");
        Pause();
        return;
    }

    Console.Write("\nTeacher first name: ");
    string tFirst = Console.ReadLine() ?? "";

    Console.Write("Teacher last name: ");
    string tLast = Console.ReadLine() ?? "";

    var grade = new Grade(subject, value, reason, new Teacher(tFirst, tLast));
    s.AddGrade(grade);

    SaveData(students);
    Console.WriteLine("\nGrade added and data saved.");
    Pause();
}


// ADD PRAISE
void AddPraise(Student s)
{
    ClearAndHeader("ADD PRAISE");

    var subjects = Enum.GetNames(typeof(Subject)).ToList();
    for (int i = 0; i < subjects.Count; i++) Console.WriteLine($"{i + 1}) {subjects[i]}");
    Console.Write("\nChoose subject: ");

    var subjectInput = Console.ReadLine()?.Trim() ?? "";
    Subject subject;

    if (int.TryParse(subjectInput, out int sIdx) && sIdx >= 1 && sIdx <= subjects.Count)
        subject = Enum.Parse<Subject>(subjects[sIdx - 1]);
    else if (!Enum.TryParse(subjectInput, true, out subject))
    {
        Console.WriteLine("Invalid option");
        Pause();
        return;
    }

    Console.WriteLine("\nChoose Praise Type:");
    var types = Enum.GetNames(typeof(PraiseType)).ToList();
    for (int i = 0; i < types.Count; i++) Console.WriteLine($"{i + 1}) {types[i]}");

    Console.Write("\nChoose type: ");
    var typeInput = Console.ReadLine()?.Trim() ?? "";
    PraiseType ptype;

    if (int.TryParse(typeInput, out int pIdx) && pIdx >= 1 && pIdx <= types.Count)
        ptype = Enum.Parse<PraiseType>(types[pIdx - 1]);
    else if (!Enum.TryParse(typeInput, true, out ptype))
    {
        Console.WriteLine("Invalid option");
        Pause();
        return;
    }

    s.AddPraise(new Praise(subject, ptype));
    SaveData(students);

    Console.WriteLine("\nPraise added and data saved.");
    Pause();
}


// ADD REMARK
void AddRemark(Student s)
{
    ClearAndHeader("ADD REMARK");

    var subjects = Enum.GetNames(typeof(Subject)).ToList();
    for (int i = 0; i < subjects.Count; i++) Console.WriteLine($"{i + 1}) {subjects[i]}");
    Console.Write("\nChoose subject: ");

    var subjectInput = Console.ReadLine()?.Trim() ?? "";
    Subject subject;

    if (int.TryParse(subjectInput, out int sIdx) && sIdx >= 1 && sIdx <= subjects.Count)
        subject = Enum.Parse<Subject>(subjects[sIdx - 1]);
    else if (!Enum.TryParse(subjectInput, true, out subject))
    {
        Console.WriteLine("Invalid option");
        Pause();
        return;
    }

    Console.WriteLine("\nChoose Remark Type:");
    var types = Enum.GetNames(typeof(RemarkType)).ToList();
    for (int i = 0; i < types.Count; i++) Console.WriteLine($"{i + 1}) {types[i]}");

    Console.Write("\nChoose type: ");
    var typeInput = Console.ReadLine()?.Trim() ?? "";
    RemarkType rtype;

    if (int.TryParse(typeInput, out int rIdx) && rIdx >= 1 && rIdx <= types.Count)
        rtype = Enum.Parse<RemarkType>(types[rIdx - 1]);
    else if (!Enum.TryParse(typeInput, true, out rtype))
    {
        Console.WriteLine("Invalid option");
        Pause();
        return;
    }

    s.AddRemark(new Remark(subject, rtype));
    SaveData(students);

    Console.WriteLine("\nRemark added and data saved.");
    Pause();
}


// DELETE STUDENT
void DeleteStudent()
{
    ClearAndHeader("DELETE STUDENT");

    Console.Write("Search name: ");
    string term = Console.ReadLine()?.Trim() ?? "";

    if (term == "")
    {
        Console.WriteLine("Empty input.");
        Pause();
        return;
    }

    var matches = students
        .Select((st, idx) => new { Student = st, Index = idx })
        .Where(x =>
            x.Student.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            x.Student.LastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            $"{x.Student.FirstName} {x.Student.LastName}"
                .Contains(term, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine("No matches.");
        Pause();
        return;
    }

    if (matches.Count == 1)
    {
        var s = matches[0].Student;
        Console.Write($"Remove {s.FirstName} {s.LastName}? (y/N): ");

        if (Console.ReadLine()?.Trim().ToLower() == "y")
        {
            students.Remove(s);
            SaveData(students);
            Console.WriteLine("Student removed.");
        }
        Pause();
        return;
    }

    Console.WriteLine("Multiple matches:");
    foreach (var m in matches)
        Console.WriteLine($"{m.Index + 1}) {m.Student.FirstName} {m.Student.LastName}");

    Console.Write("\nChoose number: ");
    if (int.TryParse(Console.ReadLine(), out int sel) &&
        sel >= 1 && sel <= students.Count)
    {
        var chosen = students[sel - 1];

        Console.Write($"Remove {chosen.FirstName} {chosen.LastName}? (y/N): ");
        if (Console.ReadLine()?.Trim().ToLower() == "y")
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

    Pause();
}


// SEARCH STUDENT
void SearchStudent()
{
    ClearAndHeader("SEARCH STUDENT");

    Console.Write("Search by name: ");
    var term = Console.ReadLine()?.Trim() ?? "";

    if (term == "")
    {
        Console.WriteLine("Empty search.");
        Pause();
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
        Pause();
        return;
    }

    Console.WriteLine("\n--- Results ---");
    foreach (var m in matches)
        Console.WriteLine($"{m.Index + 1}) {m.Student.FirstName} {m.Student.LastName}");

    Console.Write("\nPress (1) to view details or (0) to cancel: ");

    if (int.TryParse(Console.ReadLine(), out int sel) && sel >= 1 && sel <= students.Count)
    {
        var chosen = matches.FirstOrDefault(m => m.Index == sel - 1);
        if (chosen != null)
            PrintInfo(chosen.Student);
        else
            Console.WriteLine("Invalid selection.");
    }
    else
    {
        Console.WriteLine("Cancelled.");
    }
}


// STUDENT DETAILS MENU
void StudentDetails()
{
    ClearAndHeader("STUDENT DETAILS");

    if (students.Count == 0)
    {
        Console.WriteLine("No students available.");
        Pause();
        return;
    }

    Console.Write("Enter student first/last/full name: ");
    var input = Console.ReadLine()?.Trim() ?? "";

    if (input == "")
    {
        Console.WriteLine("Empty input.");
        Pause();
        return;
    }

    var matches = students.Where(s =>
        $"{s.FirstName} {s.LastName}".Equals(input, StringComparison.OrdinalIgnoreCase) ||
        s.FirstName.Equals(input, StringComparison.OrdinalIgnoreCase) ||
        s.LastName.Equals(input, StringComparison.OrdinalIgnoreCase) ||
        s.FirstName.Contains(input, StringComparison.OrdinalIgnoreCase) ||
        s.LastName.Contains(input, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine("No student found.");
        Pause();
        return;
    }

    Student s;

    if (matches.Count == 1)
        s = matches[0];
    else
    {
        Console.WriteLine("\nMultiple matches:");
        for (int i = 0; i < matches.Count; i++)
            Console.WriteLine($"{i + 1}) {matches[i].FirstName} {matches[i].LastName}");

        Console.Write("\nChoose: ");
        if (!int.TryParse(Console.ReadLine(), out int sel) ||
            sel < 1 || sel > matches.Count)
        {
            Console.WriteLine("Invalid selection.");
            Pause();
            return;
        }

        s = matches[sel - 1];
    }

    // subject helper inside student menu
    bool TryChooseSubject(out Subject chosen)
    {
        ClearAndHeader("CHOOSE SUBJECT");

        chosen = default;
        var names = Enum.GetNames(typeof(Subject)).ToList();

        Console.WriteLine("0) All Subjects");
        for (int i = 0; i < names.Count; i++)
            Console.WriteLine($"{i + 1}) {names[i]}");

        Console.Write("\nChoose: ");
        var raw = Console.ReadLine()?.Trim() ?? "";
        if (raw == "0") return false;

        if (int.TryParse(raw, out int idx) &&
            idx >= 1 && idx <= names.Count)
        {
            chosen = Enum.Parse<Subject>(names[idx - 1]);
            return true;
        }

        if (Enum.TryParse(raw, true, out Subject parsed))
        {
            chosen = parsed;
            return true;
        }

        Console.WriteLine("Invalid subject. Try again.");
        Pause();
        return TryChooseSubject(out chosen);
    }

    // STUDENT MENU LOOP
    while (true)
    {
        ClearAndHeader($"{s.FirstName} {s.LastName}");

        Console.WriteLine("1) View info");
        Console.WriteLine("2) Add Grade");
        Console.WriteLine("3) Add Praise");
        Console.WriteLine("4) Add Remark");
        Console.WriteLine("5) Remove Grade");
        Console.WriteLine("6) Remove Praise");
        Console.WriteLine("7) Remove Remark");
        Console.WriteLine("8) Back");
        Console.Write("\nChoose: ");

        var choice = Console.ReadLine()?.Trim();

        switch (choice)
        {
            case "1": PrintInfo(s); break;
            case "2": AddGrade(s); break;
            case "3": AddPraise(s); break;
            case "4": AddRemark(s); break;

            // deleting items inside student
            case "5":
                {
                    if (s.Grades?.Count == 0) { Console.WriteLine("No grades."); Pause(); break; }

                    if (TryChooseSubject(out var subj))
                    {
                        var filtered = s.Grades.Where(g => g.Subject == subj).ToList();
                        if (filtered.Count == 0) { Console.WriteLine("No grades in this subject."); Pause(); break; }

                        ClearAndHeader("REMOVE GRADE");
                        for (int i = 0; i < filtered.Count; i++)
                            Console.WriteLine($"{i + 1}) {filtered[i]}");

                        Console.Write("\nChoose: ");
                        if (int.TryParse(Console.ReadLine(), out int gIdx) &&
                            gIdx >= 1 && gIdx <= filtered.Count)
                        {
                            s.Grades.Remove(filtered[gIdx - 1]);
                            Console.WriteLine("Grade removed.");
                        }
                        else Console.WriteLine("Invalid.");
                    }
                    else
                    {
                        ClearAndHeader("REMOVE GRADE");
                        for (int i = 0; i < s.Grades.Count; i++)
                            Console.WriteLine($"{i + 1}) {s.Grades[i]}");

                        Console.Write("\nChoose: ");
                        if (int.TryParse(Console.ReadLine(), out int gIdx) &&
                            gIdx >= 1 && gIdx <= s.Grades.Count)
                        {
                            s.Grades.RemoveAt(gIdx - 1);
                            Console.WriteLine("Grade removed.");
                        }
                        else Console.WriteLine("Invalid.");
                    }
                    Pause();
                    break;
                }

            case "6":
                {
                    if (s.Praises?.Count == 0) { Console.WriteLine("No praises."); Pause(); break; }

                    if (TryChooseSubject(out var subj))
                    {
                        var filtered = s.Praises.Where(p => p.Subject == subj).ToList();
                        if (filtered.Count == 0) { Console.WriteLine("None for this subject."); Pause(); break; }

                        ClearAndHeader("REMOVE PRAISE");
                        for (int i = 0; i < filtered.Count; i++)
                            Console.WriteLine($"{i + 1}) {filtered[i]}");

                        Console.Write("\nChoose: ");
                        if (int.TryParse(Console.ReadLine(), out int pIdx) &&
                            pIdx >= 1 && pIdx <= filtered.Count)
                        {
                            s.Praises.Remove(filtered[pIdx - 1]);
                            Console.WriteLine("Praise removed.");
                        }
                        else Console.WriteLine("Invalid.");
                    }
                    else
                    {
                        ClearAndHeader("REMOVE PRAISE");
                        for (int i = 0; i < s.Praises.Count; i++)
                            Console.WriteLine($"{i + 1}) {s.Praises[i]}");

                        Console.Write("\nChoose: ");
                        if (int.TryParse(Console.ReadLine(), out int pIdx) &&
                            pIdx >= 1 && pIdx <= s.Praises.Count)
                        {
                            s.Praises.RemoveAt(pIdx - 1);
                            Console.WriteLine("Praise removed.");
                        }
                        else Console.WriteLine("Invalid.");
                    }
                    Pause();
                    break;
                }

            case "7":
                {
                    if (s.Remarks?.Count == 0) { Console.WriteLine("No remarks."); Pause(); break; }

                    if (TryChooseSubject(out var subj))
                    {
                        var filtered = s.Remarks.Where(r => r.Subject == subj).ToList();
                        if (filtered.Count == 0) { Console.WriteLine("None for this subject."); Pause(); break; }

                        ClearAndHeader("REMOVE REMARK");
                        for (int i = 0; i < filtered.Count; i++)
                            Console.WriteLine($"{i + 1}) {filtered[i]}");

                        Console.Write("\nChoose: ");
                        if (int.TryParse(Console.ReadLine(), out int rIdx) &&
                            rIdx >= 1 && rIdx <= filtered.Count)
                        {
                            s.Remarks.Remove(filtered[rIdx - 1]);
                            Console.WriteLine("Remark removed.");
                        }
                        else Console.WriteLine("Invalid.");
                    }
                    else
                    {
                        ClearAndHeader("REMOVE REMARK");
                        for (int i = 0; i < s.Remarks.Count; i++)
                            Console.WriteLine($"{i + 1}) {s.Remarks[i]}");

                        Console.Write("\nChoose: ");
                        if (int.TryParse(Console.ReadLine(), out int rIdx) &&
                            rIdx >= 1 && rIdx <= s.Remarks.Count)
                        {
                            s.Remarks.RemoveAt(rIdx - 1);
                            Console.WriteLine("Remark removed.");
                        }
                        else Console.WriteLine("Invalid.");
                    }
                    Pause();
                    break;
                }

            case "8":
                return;

            default:
                Console.WriteLine("Invalid option.");
                Pause();
                break;
        }
    }
}


// ADD STUDENT
void AddStudent(List<Student> list)
{
    ClearAndHeader("ADD STUDENT");

    string first = ReadValidName("First name");
    string last = ReadValidName("Last name");

    list.Add(new Student(first, last));
    SaveData(list);

    Console.WriteLine("\nStudent added.");
    Pause();
}

string ReadValidName(string label)
{
    while (true)
    {
        Console.Write($"{label}: ");
        string input = Console.ReadLine()?.Trim() ?? "";

        // Empty check
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Name cannot be empty.");
            continue;
        }

        // Only letters check
        if (!input.All(char.IsLetter))
        {
            Console.WriteLine("Name must contain only letters (A–Z).");
            continue;
        }

        // Capitalize the first letter, lowercase the rest
        input = char.ToUpper(input[0]) + input.Substring(1).ToLower();

        return input;
    }
}

// LIST STUDENTS
void ListStudents(List<Student> list)
{
    ClearAndHeader("STUDENTS");

    if (list.Count == 0)
    {
        Console.WriteLine("No students.");
        Pause();
        return;
    }

    for (int i = 0; i < list.Count; i++)
        Console.WriteLine($"{i + 1}) {list[i].FirstName} {list[i].LastName}");

    Pause();
}


// DATA LOADING / SAVING
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
