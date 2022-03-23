# Univeristy Management System Console App

University Project is a project that allows users to control the progress on college.
It is written as a Console Application using C# and PostgreSQL as DataBase.
This is my personal project and it is not created for business purposes.
I'm trying to learn C# by making simple projects to improve my C# and SQL knowledge.
For this project, I created over 50 tests. There is ASP.NET Core 6 version of this application on my [GitHub](https://github.com/Przemoosz/UniversityASP.NET)


## Installation

Download latest version of this project (take version from main branch)

Install npgsql (recomended version is 6.0.2) 
```bash
dotnet add package Npgsql
```
In PostgreSQL shell type:
```bash
CREATE DATABASE university;
```
In University/Utils.cs change value in methods DefaultUsername and DefaultPassword to your username and password to PostgreSQL admin.

Type run method to run application

```bash
dotnet run
```
