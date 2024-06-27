﻿using PhoneBook.Views;

namespace PhoneBook;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
    }
}
