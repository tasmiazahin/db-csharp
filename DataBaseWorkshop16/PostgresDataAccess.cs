using System;
using System.Configuration;
using System.Data;
using System.Xml.Linq;
using Dapper;
using Npgsql;

namespace DataBaseWorkshop16
{
    public class PostgresDataAccess
    {



        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }

        public static List<StudentModel> StudentsList()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();
                var output = cnn.Query<StudentModel>("SELECT * FROM lion_student2", new DynamicParameters());
                return output.ToList();

            }

        }

        public static List<CourseModel> CourseList()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();
                var output = cnn.Query<CourseModel>("SELECT * FROM lion_course2", new DynamicParameters());
                return output.OrderBy(o=>o.id).ToList();

            }
        }

        public static void createStudent()
        {
            Console.WriteLine("Enter your First Name:");
            string first_name = Console.ReadLine();
            Console.WriteLine("Enter your Last Name:");
            string last_name = Console.ReadLine().ToLower();
            Console.WriteLine("Enter your email");
            string email = Console.ReadLine().ToLower();
            Console.WriteLine("Enter your password");
            int password = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your age");
            int age = Convert.ToInt32(Console.ReadLine());

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();

                string sql = "INSERT INTO lion_student2 (first_name, last_name, email, password, age)" +
                                        "VALUES (@first_name, @last_name, @email, @password, @age)";
                cnn.Execute(sql, new { first_name, last_name, email, password, age });

                Console.WriteLine("New student created successfully!");

                cnn.Close();
            }



        }
        public static void createCourse()
        {
            Console.WriteLine("Enter your course Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter points:");
            int points = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter start date of course");
            DateTime start_date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter end date of course");
            DateTime end_date = Convert.ToDateTime(Console.ReadLine());


            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();

                string sql = "INSERT INTO lion_course2 (name, points, start_date, end_date)" +
                                        "VALUES (@name, @points, @start_date, @end_date)";
                cnn.Execute(sql, new { name, points, start_date, end_date });

                Console.WriteLine("New course added successfully!");

                cnn.Close();
            }
        }

        public static void changePassword()
        {
            Console.WriteLine("Enter your id");
            int id = Convert.ToInt32(Console.ReadLine());



            Console.WriteLine("Enter your current pssword");
            int currentPassword = Convert.ToInt32(Console.ReadLine());


            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();

                var output = cnn.Query<StudentModel>($"SELECT * FROM lion_student2 WHERE id = {id}", new DynamicParameters()).FirstOrDefault();


                if (currentPassword == output.password)
                {
                    Console.WriteLine("Enter your new pssword");
                    int newPassword = Convert.ToInt32(Console.ReadLine());


                    var query = "UPDATE lion_student2 SET password = @password WHERE @id = id";

                    using (var command = new NpgsqlCommand(query, (NpgsqlConnection?)cnn))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@password", newPassword);
                        command.ExecuteNonQuery();
                        Console.WriteLine("Password has changed successfully!");
                    }

                }
                else
                {
                    Console.WriteLine("Your password is incorrect");
                }

                cnn.Close();
            }
        }

        public static void editCourse()
        {

            Console.WriteLine("Enter your course id");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your subject name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter points:");
            int points = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter start date of course");
            DateTime start_date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter end date of course");
            DateTime end_date = Convert.ToDateTime(Console.ReadLine());

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();


                var query = "UPDATE lion_course2 SET name= @name, points = @points, start_date = @start_date, end_date= @end_date Where id=@id";

                using (var command = new NpgsqlCommand(query, (NpgsqlConnection?)cnn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@points", points);
                    command.Parameters.AddWithValue("@start_date", start_date);
                    command.Parameters.AddWithValue("@end_date", end_date);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Course updated!");
                }

                cnn.Close();
            }
        }
        public static void deleteCourse()
        {
            Console.WriteLine("Enter your course id that you want to delete");
            int id = Convert.ToInt32(Console.ReadLine());


            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))

            {
                cnn.Open();


                var query = "DELETE FROM lion_course2 Where id=@id";

                using (var command = new NpgsqlCommand(query, (NpgsqlConnection?)cnn))
                {
                    command.Parameters.AddWithValue("@id", id);


                    command.ExecuteNonQuery();
                    Console.WriteLine("Course has deleted!");
                }
                cnn.Close();


            }
        }
    }
}


