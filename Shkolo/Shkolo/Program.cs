using System.Text.Json;
using Shkolo.Models;

var options = new JsonSerializerOptions
{
    WriteIndented = true
};

List<Student> students = LoadData();

// simple menu loop
while (true)
{
    Console.WriteLine("\n1) Add student");
    Console.WriteLine("2) List students");
    Console.WriteLine("3) Save & Exit");
    Console.Write("Choose: ");
    var choice = Console.ReadLine();

    if (choice == "1")
    {
        AddStudent(students);
    }
    else if (choice == "2")
    {
        ListStudents(students);
    }
    else if (choice == "3")
    {
        SaveData(students);
        Console.WriteLine("Saved. Bye!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
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
    foreach (var s in list)
        Console.WriteLine($"{s.FirstName} {s.LastName}");
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