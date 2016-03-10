#Shoe Stores

Shoe Stores is a site that allows the user to enter a the names of shoe stores, and the names of shoe brands, and specify which stores carry what brands, using a database. This was created for the C# Week 4 code review, and is primarily to implement a database with a many to many relationship into a normal app.

###Instructions

If you want to access the project folder, type "git clone https://github.com/trashmoldwilliams/shoe-store.git" into a terminal running Git Bash and Mono, then in the folder type "dnu restore" and "dnx kestrel" to run the site, going to the provided link for the homepage. This can only be done on a Windows machine.

To properly implement the database schema for this program, import the .sql files from the project folder and import them into Microsoft SQL Server Management Studio, or another database manager. Prepend the script with these lines before execution:

  * CREATE DATABASE shoe_stores;
  - GO

or if you want to set up the database manually, enter "sqlcmd -S "(localdb)\mssqllocaldb"" into Powershell and run these lines:

  * CREATE DATABASE shoe_stores;
  - GO
  - CREATE TABLE stores (id INT IDENTITY(1,1), name VARCHAR(255));
  - CREATE TABLE brands (id INT IDENTITY(1,1), name VARCHAR(255));
  - CREATE TABLE brands_stores (id INT IDENTITY(1,1), shoes_id INT, brands_id INT);
  - GO

Languages Used
* HTML
* C#
  * Nancy
  * Razor
* & coded in Atom, databases in SQL

(c) Will Johnson 2016
