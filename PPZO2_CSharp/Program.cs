using System.Linq;
using System.Collections.Generic;
using System;

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

    public Member(MemberName name, int id)
    {
        Name = name;
        Id = id;
        BorrowList = new List<Book>();
    }
}

