﻿using System.Linq;
using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Runtime.ExceptionServices;

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
        Console.WriteLine("\n\n\nDodawanie nowej osoby do biblioteki: ");
        Console.WriteLine("Imie: ");
        do
        {
            firstName = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(firstName))
                Console.WriteLine("Imie nie moze byc puste, podaj prawdziwa wartosc: ");
        } while (string.IsNullOrEmpty(firstName));
        Console.WriteLine("Nazwisko: ");
        do
        {
            lastName = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(lastName))
                Console.WriteLine("Nazwisko nie moze byc puste, podaj prawdziwa wartosc: ");
        } while (string.IsNullOrEmpty(lastName));

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
    public static void AddBook(Book bookToAdd, Member? personBorrowing, out bool error)
    {
        error = false;
        if(personBorrowing != null)
        personBorrowing.BorrowList.Add(bookToAdd);
        else
        {
            error = true;
        }
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
    public static void Main()
    {
        bool error = false;
        bool hasBooks = true;

        // Glowne listy osob/ksiazek, glowne id
        List<Member> memberList = new List<Member>();
        List<Book> bookList = new List<Book>();
        int id = 0;

        Member? personTemp = new Member();
        Book? bookTemp = new Book();
        //person* personPointer;

        // Wybory w konsoli
        int input = -1;
        int input2 = -1;
        int input3 = -1;
        int input4 = -1;


        string firstNameSearch;
        string lastNameSearch;
        int personIdSearch = -1;
        int bookIdSearch = -1;

        // Przykladowe ksiazki dodane do listy
        bookList.Add(new Book(0, "the vanishing half", "brit bennett", 2020));
        bookList.Add(new Book(1, "the september house", "carissa orlando", 2023));
        bookList.Add(new Book(2, "harry potter and the philosopher's stone", "j.k. rowling", 1997));

        // Menu
        while (input != 6)
        {
            Console.WriteLine("Co Chcesz zrobic? Wybierz odpowiedni numer:");
            Console.WriteLine("1: Dodaj osobe do biblioteki");
            Console.WriteLine("2: Wyszukaj osobe");
            Console.WriteLine("3: Dodaj ksiazke do osoby");
            Console.WriteLine("4: Usun ksiazke od osoby");
            Console.WriteLine("5: Wyswietl wszystkie osoby oraz wypozyczone ksiazki");
            Console.WriteLine("6: Wyjdz z programu\n");

            Console.WriteLine("Wybor: ");
            try
            {
                input = int.Parse(Console.ReadLine() ?? "-1");
            }
            catch
            {
                Console.WriteLine("Nieprawidlowy input\n\n");
            }


            switch (input)
            {
                // 1: Dodawanie osoby
                case 1:
                    AddPersonClass.AddPerson(memberList, ref id, out error);
                    Console.WriteLine($"Osoba dodana pomyslnie (Id: {id - 1})\n\n\n\n\n");
                    break;

                // 2: Szukanie osoby
                case 2:
                    Console.WriteLine("Wybierz odpowiedni numer wyszukiwania:");
                    Console.WriteLine("1: Imie i nazwisko");
                    Console.WriteLine("2: Id\n");
                    Console.WriteLine("Wybor: ");
                    try { input2 = int.Parse(Console.ReadLine() ?? "-1"); }
                    catch
                    {
                        Console.WriteLine("Nieprawidlowy input\n\n");
                    }

                    switch (input2)
                    {
                        //2: Szukanie osoby -> 1: Szukanie po imieniu/nazwisku
                        case 1:
                            Console.WriteLine("\n\n\nWyszukiwanie po imieniu i nazwisku: \n");
                            Console.WriteLine("Imie: ");
                            firstNameSearch = Console.ReadLine() ?? "-1";
                            Console.WriteLine("Nazwisko: ");
                            lastNameSearch = Console.ReadLine() ?? "-1";


                            personTemp = SearchPersonClass.SearchPerson(memberList, out error, -1, firstNameSearch, lastNameSearch);

                            if (!error && personTemp != null)
                            {

                                Console.WriteLine("Informacje osoby wyszukiwanej:\n");
                                Console.WriteLine($"Id: {personTemp.Id}    Imie: {personTemp.Name.FirstName}    Nazwisko: {personTemp.Name.LastName}\n");
                                hasBooks = false;
                                Console.WriteLine("Lista wypozyczonych ksiazek: \n");
                                foreach (var book in personTemp.BorrowList)
                                {
                                    Console.WriteLine($"Autor:  {book.Author}    Id:  << {book.Id} <<    Tytul:  << {book.Title} <<    Rok: << {book.Year}");
                                    hasBooks = true;
                                }

                                if (!hasBooks)
                                {
                                    Console.WriteLine("Brak wypozyczonych ksiazek");
                                }
                            }

                            else
                            {
                                Console.WriteLine("BLAD PODCZAS WYSZUKIWANIA");
                                error = false;
                            }

                            Console.WriteLine("\n\n\n");
                                
                            personTemp = new Member();

                            break;
                        //2: Szukanie osoby -> 2: Szukanie po id
                        case 2:
                            Console.WriteLine("\n\n\nWyszukiwanie po Id: \n");
                            Console.WriteLine("Id: ");
                            try { personIdSearch = int.Parse(Console.ReadLine() ?? "-1"); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n");
                            }

                            personTemp = SearchPersonClass.SearchPerson(memberList, out error, personIdSearch, "", "");

                            if (!error && personTemp != null)
                            {
                                Console.WriteLine("Informacje osoby wyszukiwanej:\n ");
                                Console.WriteLine($"Id:   {personTemp.Id}     Imie:   {personTemp.Name.FirstName}     Nazwisko:   {personTemp.Name.LastName}  \n");


                                hasBooks = false;
                                Console.WriteLine("Lista wypozyczonych ksiazek: \n");
                                foreach (var book in personTemp.BorrowList)
                                {
                                    Console.WriteLine($"Autor:   {book.Author} <<    Id:   {book.Id}     Tytul:   {book.Title}     Rok:   {book.Year}");
                                    hasBooks = true;
                                }

                                if (!hasBooks)
                                {
                                    Console.WriteLine("Brak wypozyczonych ksiazek");
                                }
                            }
                            else
                            {
                                Console.WriteLine("BLAD PODCZAS WYSZUKIWANIA");
                                error = false;
                            }

                            Console.WriteLine("\n\n\n");

                            personTemp = new Member();

                            break;

                        default:
                            break;
                    }
                    break;
                // 3: Dodanie ksiazki do osoby
                case 3:
                    Console.WriteLine("Wybierz odpowiedni sposob wyszukiwania wypozyczajacego:");
                    Console.WriteLine("1: Imie i nazwisko");
                    Console.WriteLine("2: Id\n");
                    Console.WriteLine("Wybor: ");

                    try { input3 = int.Parse(Console.ReadLine() ?? "-1"); }
                    catch
                    {
                        Console.WriteLine("Nieprawidlowy input\n");
                    }

                    switch (input3)
                    {
                        // 3: Dodanie ksiazki do osoby -> 1: Szukanie po imieniu/nazwisku
                        case 1:
                            Console.WriteLine("\n\n\nImie i nazwisko wypozycajacego: ");
                            Console.WriteLine("Imie: ");
                            firstNameSearch = Console.ReadLine() ?? "";
                            Console.WriteLine("Nazwisko: ");
                            lastNameSearch = Console.ReadLine() ?? "";
                            Console.WriteLine("Id ksiazki: ");
                            try { bookIdSearch = int.Parse(Console.ReadLine() ?? "-1"); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n\n");
                                break;
                            }

                            personTemp = SearchPersonClass.SearchPerson(memberList, out error, -1, firstNameSearch, lastNameSearch);
                            bookTemp = SearchBookClass.SearchBook(bookList, bookIdSearch, out error);

                            if (!error && bookTemp != null && personTemp != null)
                            {
                                AddBookClass.AddBook(bookTemp, personTemp, out error);
                                Console.WriteLine("Ksiazka dodana pomyslnie\n\n\n\n");
                            }

                            else
                            {
                                Console.WriteLine("BLAD PODCZAS WYSZUKIWANIA OSOBY LUB KSIAZKI\n\n\n\n");
                                error = false;
                            }

                            personTemp = new Member();
                            bookTemp = new Book();

                            break;

                        // 3: Dodanie ksiazki do osoby -> 2: Szukanie po id
                        case 2:
                            Console.WriteLine("\n\n\nId wypozyczajacego: \n");
                            try { personIdSearch = int.Parse(Console.ReadLine() ?? ""); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n\n");
                                break;
                            }
                            Console.WriteLine("\n\n\nId ksiazki: \n");
                            try { bookIdSearch = int.Parse(Console.ReadLine() ?? ""); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n\n");
                                break;
                            }

                            personTemp = SearchPersonClass.SearchPerson(memberList, out error, personIdSearch, "", "");
                            bookTemp = SearchBookClass.SearchBook(bookList, bookIdSearch, out error);

                            if (!error && bookTemp != null && personTemp != null)
                            {
                                AddBookClass.AddBook(bookTemp, personTemp, out error);
                                Console.WriteLine("Ksiazka dodana pomyslnie\n\n\n\n");
                            }

                            else
                            {
                                Console.WriteLine("BLAD PODCZAS WYSZUKIWANIA OSOBY LUB KSIAZKI\n\n\n\n");
                                error = false;
                            }

                            personTemp = new Member();
                            bookTemp = new Book();

                            break;

                        default:
                            break;
                    }
                    break;

                // 4: Usuniecie ksiazki od osoby
                case 4:
                    Console.WriteLine("Wybierz odpowiedni sposob wyszukiwania wypozyczajacego:");
                    Console.WriteLine("1: Imie i nazwisko");
                    Console.WriteLine("2: Id\n");
                    Console.WriteLine("Wybor: ");
                    try { input4 = int.Parse(Console.ReadLine() ?? "-1"); }
                    catch
                    {
                        Console.WriteLine("Nieprawidlowy input\n\n");
                        break;
                    }

                    switch (input4)
                    {
                        // 4: Usuniecie ksiazki od osoby -> 1: Szukanie po imieniu/nazwisku
                        case 1:
                            Console.WriteLine("\n\n\nImie i nazwisko wypozyczajacego: ");
                            Console.WriteLine("Imie: ");
                            firstNameSearch = Console.ReadLine() ?? "";
                            Console.WriteLine("Nazwisko: ");
                            lastNameSearch = Console.ReadLine() ?? "";
                            Console.WriteLine("Id ksiazki: ");
                            try { bookIdSearch = int.Parse(Console.ReadLine() ?? "-1"); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n\n");
                                break;
                            }

                            personTemp = SearchPersonClass.SearchPerson(memberList, out error, -1, firstNameSearch, lastNameSearch);
                            bookTemp = SearchBookClass.SearchBook(bookList, bookIdSearch, out error);

                            if (!error && personTemp != null && bookTemp != null)
                            {
                                RemoveBookClass.RemoveBook(bookTemp, personTemp, out error);
                                Console.WriteLine("Ksiazka usunieta pomyslnie\n\n\n\n");
                            }

                            else
                            {
                                Console.WriteLine("BLAD PODCZAS WYSZUKIWANIA OSOBY LUB KSIAZKI\n\n\n\n");
                                error = false;
                            }

                            personTemp = new Member();
                            bookTemp = new Book();

                            break;

                        // 4: Usuniecie ksiazki od osoby -> 2: Szukanie po id
                        case 2:

                            Console.WriteLine("\n\n\nId wypozyczajacego: \n");
                            try { personIdSearch = int.Parse(Console.ReadLine() ?? ""); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n\n");
                                break;
                            }
                            try { bookIdSearch = int.Parse(Console.ReadLine() ?? ""); }
                            catch
                            {
                                Console.WriteLine("Nieprawidlowy input\n\n");
                                break;
                            }

                            personTemp = SearchPersonClass.SearchPerson(memberList, out error, personIdSearch, "", "");
                            bookTemp = SearchBookClass.SearchBook(bookList, bookIdSearch, out error);

                            if (!error && bookTemp != null && personTemp != null)
                            {
                                RemoveBookClass.RemoveBook(bookTemp, personTemp, out error);
                                Console.WriteLine("Ksiazka usunieta pomyslnie\n\n\n\n");
                            }

                            else
                            {
                                Console.WriteLine("BLAD PODCZAS WYSZUKIWANIA OSOBY LUB KSIAZKI\n\n\n\n");
                                error = false;
                            }

                            personTemp = new Member();
                            bookTemp = new Book();

                            break;

                        default:
                            break;
                    }
                    break;
                // 5: Wypisywanie wszystkich osob i ich wypozyczonych ksiazek
                case 5:
                    // Petla dla osoby
                    foreach (var person in memberList)
                    {
                        Console.WriteLine("Informacje osoby wyszukiwanej:\n ");
                        Console.WriteLine($"id:   {person.Id}     Imie:   {person.Name.FirstName}     Nazwisko:   {person.Name.LastName}  \n");
                        Console.WriteLine("Wypozyczone ksiazki:\n\n ");

                        hasBooks = false;

                        foreach (var book in person.BorrowList)
                        {
                            Console.WriteLine($"Autor:   {book.Author}     id:   {book.Id}     Tytul:   {book.Title}     Rok:   {book.Year}");
                            hasBooks = true;
                        }

                        if (!hasBooks)
                        {
                            Console.WriteLine("Brak wypozyczonych ksiazek");
                        }
                    }

                    Console.WriteLine("\n\n\n");
                    break;
                default:

                    break;
            }

        }

    }
}