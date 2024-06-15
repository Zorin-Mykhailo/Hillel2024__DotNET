using HW22_Drugs.Application;
using Microsoft.Win32;
using System.Windows;

namespace HW22_Drugs;

public class DefaultDialogService : IDialogService
{
    public string FilePath { get; set; }

    public bool OpenFileDialog()
    {
        OpenFileDialog openFileDialog = new ();
        if(openFileDialog.ShowDialog() != true) return false;

        FilePath = openFileDialog.FileName;
        return true;
    }

    public bool SaveFileDialog()
    {
        SaveFileDialog saveFileDialog = new ();
        if(saveFileDialog.ShowDialog() != true) return false;

        FilePath = saveFileDialog.FileName;
        return true;
    }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }
}