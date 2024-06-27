﻿using SQLite;

namespace PhoneBook.Models;

public class Item
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string Phone { get; set; }

    public string Description { get; set; }
}