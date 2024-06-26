﻿namespace HW22_Drugs.Contracts;

public interface IDialogService
{
    void ShowMessage(string message);   // показ сообщения
    string FilePath { get; set; }   // путь к выбранному файлу
    bool OpenFileDialog();  // открытие файла
    bool SaveFileDialog();  // сохранение файла
}