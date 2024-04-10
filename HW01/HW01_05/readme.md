﻿# Домашнє завдання 01.05

Розробити додаток, що має такий набір класів:

1) клас `Person` містить у собі такі члени:  
- [x] захищені поля:  
	- [x] `name` (ім'я),   
	- [x] `surname` (прізвище),  
	- [x] `age` (вік),  
	- [x] `phone` (телефон);  
- [x] властивості:  
	- [x] `Name`,  
	- [x] `Surname`,  
	- [x] `Age`,  
	- [x] `Phone`; 
- [x] конструктор з параметрами;  
- [x] метод `Print` для виведення інформації на екран.  

2) клас `Student`, похідний від класу `Person`, містить такі члени:  
- [x] захищені поля: 
	- [x] `average` (середній бал),  
	- [x] `number_of_group` (номер групи);  
- [x] властивості:  
	- [x] `Average`,  
	- [x] `Number_Of_Group`;  
- [x] конструктор із параметрами;  
- [x] метод Print для виведення інформації на екран;

3) клас `Academy_Group` містить такі члени:  
- [x] посилальну змінну, що вказує на масив студентів;  
- [x] лічильник `count` кількості студентів у групі;  
- [x] конструктор за замовчуванням;  
- [x] метод `Add` для додавання студентів до групи;  
- [x] метод `Remove` для видалення студента з групи (критерій видалення - прізвище);
- [x] метод `Edit` для редагування відомостей про студента (критерій - прізвище студента);
- [x] метод друку групи `Print`;
- [x] метод `Save` для збереження даних у файл;
- [x] метод `Load` для завантаження даних із файлу;
- [x] метод `Search` для пошуку студента за заданим критерієм.

4) клас `Main_Class`, що реалізує користувацький інтерфейс додатка і демонструє роботу з класом Academy_Group.

# Примітки

Відсутні