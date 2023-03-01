using GroceryInventory.Pages.DB;
using Reed_Lab1.Pages.DataClasses;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace Reed_Lab1.Pages.DB
{
    public class DBClass
    {
        // SQL Connection Object added to the Class level
        public static SqlConnection Lab3DBConnection = new SqlConnection();

        // Connection Strings

        private static readonly String AuthConnString = "Server=Localhost;Database=AUTH;Trusted_Connection=True";
        private static readonly String Lab3DBConnString = "Server=Localhost;Database=Lab3;Trusted_Connection=True";


        // Database Interaction Method(s)
        public static SqlDataReader InstructorReader()
        {
            SqlCommand cmdInstructorRead = new SqlCommand();
            cmdInstructorRead.Connection = Lab3DBConnection;
            cmdInstructorRead.Connection.ConnectionString = Lab3DBConnString;
            cmdInstructorRead.CommandText = "SELECT * FROM Instructor";

            cmdInstructorRead.Connection.Open();

            SqlDataReader tempReader = cmdInstructorRead.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader OfficeReader()
        {
            SqlCommand cmdOfficeRead = new SqlCommand();
            cmdOfficeRead.Connection = Lab3DBConnection;
            cmdOfficeRead.Connection.ConnectionString = Lab3DBConnString;
            cmdOfficeRead.CommandText = "SELECT * FROM OfficeAppointment";

            cmdOfficeRead.Connection.Open();

            SqlDataReader tempReader = cmdOfficeRead.ExecuteReader();

            return tempReader;
        }



        public static SqlDataReader SingleOffice(int InstructorID) 
        {
            String sqlQuery = "SELECT * FROM OfficeAppointment WHERE InstructorID = " + InstructorID;
            
            SqlCommand cmdOfficeRead = new SqlCommand();
            cmdOfficeRead.Connection = Lab3DBConnection;
            cmdOfficeRead.Connection.ConnectionString = Lab3DBConnString;
            cmdOfficeRead.CommandText = sqlQuery;

            cmdOfficeRead.Connection.Open();

            SqlDataReader tempReader = cmdOfficeRead.ExecuteReader();

            return tempReader;


        }

        public static SqlDataReader SingleMeeting(int InstructorID)
        {
            String sqlQuery = "SELECT * FROM Meeting WHERE InstructorID = " + InstructorID;

            SqlCommand cmdMeetingRead = new SqlCommand();
            cmdMeetingRead.Connection = Lab3DBConnection;
            cmdMeetingRead.Connection.ConnectionString = Lab3DBConnString;
            cmdMeetingRead.CommandText = sqlQuery;

            cmdMeetingRead.Connection.Open();

            SqlDataReader tempReader = cmdMeetingRead.ExecuteReader();

            return tempReader;


        }

        public static SqlDataReader GeneralReaderQuery(string sqlQuery)
        {

            SqlCommand cmdInstructorRead = new SqlCommand();
            cmdInstructorRead.Connection = new SqlConnection();
            cmdInstructorRead.Connection.ConnectionString = Lab3DBConnString;
            cmdInstructorRead.CommandText = sqlQuery;
            cmdInstructorRead.Connection.Open();
            SqlDataReader tempReader = cmdInstructorRead.ExecuteReader();

            return tempReader;

        }

        public static void SelectQuery(string sqlQuery) 
        {
            SqlCommand cmdStudentRead = new SqlCommand();
            cmdStudentRead.Connection = new SqlConnection();
            cmdStudentRead.Connection.ConnectionString = Lab3DBConnString;
            cmdStudentRead.CommandText = sqlQuery;
            cmdStudentRead.Connection.Open();
            cmdStudentRead.ExecuteNonQuery();
        }

        public static void InsertMeeting(Meeting m)
        {
            String sqlQuery = "INSERT INTO Meeting (MeetingPurpose, InstructorID) VALUES (";
            sqlQuery += "'" + m.MeetingPurpose + "',";
            sqlQuery += m.InstructorID + ",";

            SqlCommand cmdMeetingRead = new SqlCommand();
            cmdMeetingRead.Connection = Lab3DBConnection;
            cmdMeetingRead.Connection.ConnectionString = Lab3DBConnString;
            cmdMeetingRead.CommandText = sqlQuery;

            cmdMeetingRead.Connection.Open();

            cmdMeetingRead.ExecuteNonQuery();
        }

        public static int LoginQuery(string loginQuery)
        {
            // This method expects to receive an SQL SELECT
            // query that uses the COUNT command.

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();

            return rowCount;
        }

        public static SqlDataReader InstructorUser()
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = Lab3DBConnection;
            cmdUserRead.Connection.ConnectionString = Lab3DBConnString;
            cmdUserRead.CommandText = "SELECT Instructor.User1, Credentials.Username FROM Instructor, Credentials Where Instructor.User1 = Credentials.Username";

            cmdUserRead.Connection.Open();

            SqlDataReader tempReader = cmdUserRead.ExecuteReader();

            return tempReader;
        }

        public static bool HashedParameterLogin(string Username, string Password, string SelectedUser)
        { 
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandType = System.Data.CommandType.StoredProcedure;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@SelectedUser", SelectedUser);
            cmdLogin.CommandText = "sp_Lab3Login";

            cmdLogin.Connection.Open();


            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["Password"].ToString();

                if (PasswordHash.ValidatePassword(Password, correctHash))
                {
                    return true;
                }
            }

            return false;
        }

        public static void CreateHashedUser(string Username, string Password, string Fullname, string Email, string SelectedUser)
        {
            string loginQuery =
                "INSERT INTO HashedCredentials (Username,Password, Fullname, Email, UserType) values (@Username, @Password";
            loginQuery += ",'" + Fullname + "'";
            loginQuery += ",'" + Email + "'";
            loginQuery += ",'" + SelectedUser + "')";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(Password));

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();

        }

        public static void CreateUser(string FullName, string Telephone, string Email, string Username)
        {
            string loginQuery =
                "INSERT INTO Student (StudentName, PhoneNumber, Email, User1) values (@FullName";
            loginQuery += ",'" + Telephone + "'";
            loginQuery += ",'" + Email + "'";
            loginQuery += ",'" + Username + "')";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@FullName", FullName);

            cmdLogin.Connection.Open();

            cmdLogin.ExecuteNonQuery();

        }
    }
}
