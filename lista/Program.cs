using System;
using System.IO;

class Grade
{
    public int Score { get; set; }
    public Grade Next { get; set; }

    public Grade(int score)
    {
        Score = score;
        Next = null;
    }
}

class Student
{
    public string Name { get; set; }
    public Student Next { get; set; }
    public Grade Grades { get; set; }

    public Student(string name)
    {
        Name = name;
        Next = null;
        Grades = null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Users\alexa\OneDrive\estudiantes.txt";
        Student head = null, current = null;

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    Student newStudent = new Student(data[0].Trim());

                    for (int i = 1; i < data.Length; i++)
                    {
                        int score = int.Parse(data[i].Trim());
                        Grade newGrade = new Grade(score);
                        AddGradeToStudent(newStudent, newGrade);
                    }

                    AddStudentToList(ref head, ref current, newStudent);
                }
            }

            PrintStudentList(head);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }
    }

    static void AddGradeToStudent(Student student, Grade newGrade)
    {
        if (student.Grades == null)
        {
            student.Grades = newGrade;
        }
        else
        {
            Grade temp = student.Grades;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = newGrade;
        }
    }

    static void AddStudentToList(ref Student head, ref Student current, Student newStudent)
    {
        if (head == null)
        {
            head = newStudent;
            current = head;
        }
        else
        {
            current.Next = newStudent;
            current = current.Next;
        }
    }

    static void PrintStudentList(Student head)
    {
        Student current = head;
        while (current != null)
        {
            Console.WriteLine($"Nombre del estudiante: {current.Name}");
            Console.Write("Calificaciones: ");
            Grade gradeCurrent = current.Grades;
            while (gradeCurrent != null)
            {
                Console.Write($"{gradeCurrent.Score} ");
                gradeCurrent = gradeCurrent.Next;
            }
            Console.WriteLine();
            current = current.Next;
        }
    }
}
