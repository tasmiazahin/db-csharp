using Dapper;
using Npgsql;
using System.Data;

namespace DataBaseWorkshop16;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Välkommen till databasen! Vänligen välj ett av nedanstående alternativ:");
        Console.WriteLine("1. Lista studenter");
        Console.WriteLine("2. Lista kurser");
        Console.WriteLine("3. Skapa student");
        Console.WriteLine("4. Skapa kurs");
        Console.WriteLine("5. Byt lösenord");
        Console.WriteLine("6. Redigera kurs");
        Console.WriteLine("7. Radera kurs");
        Console.WriteLine("A. Avsluta");


        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                List<StudentModel> students = PostgresDataAccess.StudentsList();

                foreach (var item in students)
                {
                    Console.WriteLine($"Student id is : {item.id}, Name : {item.first_name} {item.last_name}, Email : {item.email}, Password : {item.password}, Age : {item.age}");
                    
                }
                break;

            case "2":
                List<CourseModel> courses = PostgresDataAccess.CourseList();

                foreach (var item in courses)
                {

                    Console.WriteLine($"Student id is : {item.id}, Name : {item.name}, Points : {item.points}, Start date : {item.start_date.ToShortDateString()}, End date {item.end_date.ToShortDateString()}");

                }
                break;


            case "3":
                PostgresDataAccess.createStudent();    
                break;
          
            case "4":
                PostgresDataAccess.createCourse();
                break;

            case "5":
                PostgresDataAccess.changePassword();
                break;

            case "6":
                PostgresDataAccess.editCourse();
                break;

            case "7":
                PostgresDataAccess.deleteCourse();
                break;

            case "A":
                Console.WriteLine("Tack för att du använde databasen och välkommen åter.");
                break;

            default:
                break;


        }
        Console.ReadLine();
    }

}


