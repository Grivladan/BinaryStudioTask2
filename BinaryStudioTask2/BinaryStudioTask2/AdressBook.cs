using System;
using System.Collections.Generic;
using System.Reflection;

class User
{
    public User(string LastName, string FirstName, DateTime BirthDate, string City,
        string Adress, string PhoneNumber, string Gender, string Email)
    {
        this.LastName = LastName;
        this.FirstName = FirstName;
        this.BirthDate = BirthDate;
        this.City = City;
        this.Adress = Adress;
        this.PhoneNumber = PhoneNumber;
        this.Gender = Gender;
        this.Email = Email;
    }

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime TimeAdded { get; set; }
    public string City { get; set; }
    public string Adress { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return ToString("S");
    }

    public string ToString(string fmt)
    {
        if (String.IsNullOrEmpty(fmt))
            fmt = "S";
        switch (fmt.ToUpperInvariant())
        {
            case "S":
                return string.Format("[{0}, {1}, {2}, {3}]", LastName, FirstName, PhoneNumber, Adress);
            case "F":
                string str = "";
                PropertyInfo[] infos = this.GetType().GetProperties();
                foreach (PropertyInfo pi in infos)
                {
                    str += pi.Name + " " + pi.GetValue(this, null).ToString() + "\n";
                }
                return str;
            default:
                String msg = String.Format("'{0}' is an invalid format string", fmt);
                throw new ArgumentException(msg);
        }
    }
}

class AdressBook
{
    private List<User> users;
    public AdressBook()
    {
        users = new List<User>();
    }

    private bool Contain(User user)
    {
        foreach (var item in users)
            if (item.FirstName == user.FirstName && item.LastName == user.LastName && item.Adress == user.Adress)
                return true;
        return false;
    }

    private User Find(User user)
    {
        foreach (var item in users)
            if (item.FirstName == user.FirstName && item.LastName == user.LastName && item.Adress == user.Adress)
                return item;
        return null;
    }

    public void AddUser(User user)
    {
        try
        {
            user.TimeAdded = DateTime.Now;
            if (Contain(user))
                throw new Exception("Adress book already contains this user");
            users.Add(user);
            UserAdded("User " + user.ToString() + " was added");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void RemoveUser(User user)
    {
        try
        {
            if (!Contain(user))
                throw new Exception("Adress book doesn't contain this user");
            users.Remove(Find(user));
            UserRemoved("User " + user.ToString() + " was removed");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void PrintAdressBook()
    {
        foreach (var i in users)
            Console.WriteLine(i);
    }

    public delegate void SomeActionWithUser(string message);

    public event SomeActionWithUser UserAdded;
    public event SomeActionWithUser UserRemoved;
}

