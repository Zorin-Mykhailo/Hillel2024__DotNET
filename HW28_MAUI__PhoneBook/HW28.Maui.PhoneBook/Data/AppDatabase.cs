﻿using HW28.Maui.PhoneBook.Models;
using SQLite;

namespace HW28.Maui.PhoneBook.Data;

public class AppDatabase
{
    SQLiteAsyncConnection Database;
    public AppDatabase()
    {
    }
    async Task Init()
    {
        if(Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Item>();
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<Item>().ToListAsync();
    }

    public async Task<List<Item>> GetItemsNotDoneAsync()
    {
        await Init();
        return await Database.Table<Item>().Where(t => t.Done).ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public async Task<Item> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Item item)
    {
        await Init();
        if(item.ID != 0)
        {
            return await Database.UpdateAsync(item);
        }
        else
        {
            return await Database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(Item item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}
