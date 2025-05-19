using System.Linq;
using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

// Imie nazwisko
public class MemberName
{
    public string FirstName{ get; set; }
    public string LastName { get; set; }

    public MemberName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}


// Ksiazka
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public Book(int id, string title, string author, int year)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
    }
}


// Osoba
public class Member
{
    public MemberName Name { get; set; }
    public int Id { get; set; }
    public List<Book> BorrowList;

    public Member(MemberName name, int id, List<Book> borrowList)
    {
        Name = name;
        Id = id;
        BorrowList = borrowList;
    }
}

// Dodawanie osoby do listy w bibliotece
public class addPersonClass
{
    public void addPerson(ref List<Member> personList, ref int id, ref bool error)
    {
        string firstName = "temp";
        string lastName = "temp";
        List<Book> borrowList = new List<Book>();
        Console.Write("\n\n\n\nDodawanie nowej osoby do biblioteki: ");
        Console.Write("\nImie: ");
        firstName = Console.ReadLine() ?? "";
        Console.Write("Nazwisko: ");
        lastName = Console.ReadLine() ?? "";

        // Zamiana danych wejsciowych na male litery
        firstName = firstName.ToLower();
        lastName = lastName.ToLower();

        MemberName nameTemp = new MemberName(firstName, lastName);
        Member personTemp = new Member(nameTemp, id, borrowList);

        personList.Add(personTemp);
        id++;
    }
}