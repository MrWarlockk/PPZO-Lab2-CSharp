using System.Linq;
using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

// Imie nazwisko
public class MemberName
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = "";

    public MemberName() {}
    public MemberName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}


// Ksiazka
public class Book
{
    public int Id { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; } = -1;

    public Book() {}
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
    public MemberName Name { get; set; } = new MemberName();
    public int Id { get; set; } = -1;
    public List<Book> BorrowList = new List<Book>();

    public Member() {}
    public Member(MemberName name, int id, List<Book> borrowList)
    {
        Name = name;
        Id = id;
        BorrowList = borrowList;
    }
}

// Dodawanie osoby do listy w bibliotece
public class AddPersonClass
{
    public static void AddPerson(List<Member> personList, ref int id, out bool error)
    {
        error = false;
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

// Wyszukiwanie osoby
public class SearchPersonClass
{
    public static Member? SearchPerson(List<Member> personList, out bool error, int id = -1, string firstName = "", string lastName = "") {
        error = false;
        List<Book> borrowList = new List<Book>();
        Member? memberTemp = null;

        firstName = firstName.ToLower();
        lastName = lastName.ToLower();

        // Wyszukiwanie za pomoca id
        if (id != -1)
        {
            foreach (var person in personList)
            {
                if (person.Id == id)
                {
                    memberTemp = person;
                    break;
                }
            }

            // Brak znalezionej osoby
            if (memberTemp == null)
                error = true;
        }
        // Wyszukiwanie za pomoca imienia + nazwiska
        else
        {
            foreach (var person in personList)
            {
                if (person.Name.FirstName == firstName && person.Name.LastName == lastName)
                {
                    memberTemp = person;
                    break;
                }
            }

            // Brak znalezionej osoby
            if (memberTemp == null)
                error = true;
        }
        return memberTemp;
    }
}

// Szukanie ksiazki za pomoca id
public class SearchBookClass
{
    public static Book SearchBook(List<Book> bookList, int bookId, out bool error)
    {
        error = false;
        Book bookTemp = new Book( -1, "temp", "temp", -1 );

        // Wyszukiwanie za pomoca id
        foreach (var book in bookList)
        {
            if (book.Id == bookId)
            {
                bookTemp = book;
            }
        }

        // Brak znalezionej ksiazki
        if (bookTemp.Year == -1)
            error = true;

        return bookTemp;
    }
}


public static class AddBookClass
{
    public static void AddBook(Book bookToAdd, Member personBorrowing, out bool error)
    {
        error = false;
        personBorrowing.BorrowList.Add(bookToAdd);
    }
}

public static class RemoveBookClass
{
    public static void RemoveBook(Book bookToRemove, Member personBorrowing, out bool error)
    {
        error = false;
        personBorrowing.BorrowList.Remove(bookToRemove);
    }
}

public class MainClass()
{
    public static void Main() {
        bool error = false;
        bool hasBooks = true;

        // Glowne listy osob/ksiazek, glowne id
        List<Member> memberList = new List<Member>();
        List<Book> bookList = new List<Book>();
        int id = 0;

        Member personTemp = new Member();
        Book bookTemp = new Book();
        //person* personPointer;

        // Wybory w konsoli
        int input = -1;
        int input2 = -1;
        int input3 = -1;
        int input4 = -1;


        string firstNameSearch;
        string lastNameSearch;
        int personIdSearch;
        int bookIdSearch;

        // Przykladowe ksiazki dodane do listy
        bookList.Add(new Book (0, "the vanishing half", "brit bennett", 2020));
        bookList.Add(new Book(1, "the september house", "carissa orlando", 2023));
        bookList.Add(new Book(2, "harry potter and the philosopher's stone", "j.k. rowling", 1997));
    }
}