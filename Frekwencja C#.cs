using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Imie;
    public List<double> Oceny = new List<double>();
    public int Obecnosci = 0;
    public int Zajecia = 0;
}

class Kurs
{
    public string Nazwa;
    public List<Student> Studenci = new List<Student>();
}

class Program
{
    static List<Kurs> kursy = new List<Kurs>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Dodaj kurs");
            Console.WriteLine("2. Dodaj studenta");
            Console.WriteLine("3. Dodaj ocenę");
            Console.WriteLine("4. Dodaj frekwencję");
            Console.WriteLine("5. Pokaż raport");
            Console.WriteLine("6. Koniec");
            Console.Write("Wybierz: ");

            string wybor = Console.ReadLine();
            switch (wybor)
            {
                case "1": DodajKurs(); break;
                case "2": DodajStudenta(); break;
                case "3": DodajOcene(); break;
                case "4": DodajFrekwencje(); break;
                case "5": PokazRaport(); break;
                case "6": return;
                default: Console.WriteLine("Nieznana opcja."); break;
            }
        }
    }

    static void DodajKurs()
    {
        Console.Write("Podaj nazwę kursu: ");
        string nazwa = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nazwa))
        {
            kursy.Add(new Kurs { Nazwa = nazwa });
            Console.WriteLine("Kurs dodany.");
        }
    }

    static void DodajStudenta()
    {
        Kurs kurs = WybierzKurs();
        if (kurs == null) return;

        Console.Write("Podaj imię i nazwisko studenta: ");
        string imie = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(imie))
        {
            kurs.Studenci.Add(new Student { Imie = imie });
            Console.WriteLine("Student dodany.");
        }
    }

    static void DodajOcene()
    {
        Kurs kurs = WybierzKurs();
        if (kurs == null || kurs.Studenci.Count == 0) return;

        Student student = WybierzStudenta(kurs);
        if (student == null) return;

        Console.Write("Podaj ocenę: ");
        if (double.TryParse(Console.ReadLine(), out double ocena))
        {
            student.Oceny.Add(ocena);
            Console.WriteLine("Ocena dodana.");
        }
        else Console.WriteLine("Błędna ocena.");
    }

    static void DodajFrekwencje()
    {
        Kurs kurs = WybierzKurs();
        if (kurs == null || kurs.Studenci.Count == 0) return;

        foreach (var student in kurs.Studenci)
        {
            Console.Write($"{student.Imie} - obecny? (t/n): ");
            string odp = Console.ReadLine()?.ToLower();
            student.Zajecia++;
            if (odp == "t") student.Obecnosci++;
        }
        Console.WriteLine("Frekwencja zapisana.");
    }

    static void PokazRaport()
    {
        Kurs kurs = WybierzKurs();
        if (kurs == null) return;

        Console.WriteLine($"\nRaport dla kursu: {kurs.Nazwa}");
        foreach (var student in kurs.Studenci)
        {
            string oceny = student.Oceny.Count > 0 ? string.Join(", ", student.Oceny) : "brak ocen";
            double srednia = student.Oceny.Count > 0 ? student.Oceny.Average() : 0;
            string frekwencja = student.Zajecia > 0 ?
                $"{student.Obecnosci}/{student.Zajecia} obecności" : "brak danych";

            Console.WriteLine($"\nStudent: {student.Imie}");
            Console.WriteLine($"Oceny: {oceny}");
            if (student.Oceny.Count > 0)
                Console.WriteLine($"Średnia: {srednia:F2}");
            Console.WriteLine($"Frekwencja: {frekwencja}");
        }
    }

    static Kurs WybierzKurs()
    {
        if (kursy.Count == 0)
        {
            Console.WriteLine("Brak kursów.");
            return null;
        }

        for (int i = 0; i < kursy.Count; i++)
            Console.WriteLine($"{i + 1}. {kursy[i].Nazwa}");

        Console.Write("Wybierz kurs: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= kursy.Count)
            return kursy[idx - 1];

        Console.WriteLine("Niepoprawny wybór.");
        return null;
    }

    static Student WybierzStudenta(Kurs kurs)
    {
        for (int i = 0; i < kurs.Studenci.Count; i++)
            Console.WriteLine($"{i + 1}. {kurs.Studenci[i].Imie}");

        Console.Write("Wybierz studenta: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= kurs.Studenci.Count)
            return kurs.Studenci[idx - 1];

        Console.WriteLine("Niepoprawny wybór.");
        return null;
    }
}