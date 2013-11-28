#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data.SqlClient;

using CommandLine;
using CommandLine.Text;
#endregion

namespace ConsoleApplication1
{
    class Program
    {
        static IEnumerable<string> Fields(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; ++i)
            {
                yield return reader[i].ToString();
            }
        }
        
        static void Main(string[] args)
        {
            var options = new Options();
            StreamWriter sw = null;
            SqlDataReader reader = null;
            
            // parse ommand line arguments
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            // reading script text
            String script;
            if (File.Exists(options.Command)) // trying to open file first
            {
                script = File.ReadAllText(options.Command);
            } else { // on fail just use command as script text
                script = options.Command;
            }

            // build connection string
            var sb = new SqlConnectionStringBuilder();
            if (options.Password!="") sb.Password = options.Password;
            if (options.User != "") sb.UserID = options.User;
            sb.DataSource = options.Source;
            sb.InitialCatalog = options.Database;

            // connection to database
            if (options.Verbose) Console.WriteLine("# Trying to connect to DataBase");
            SqlConnection connection = new SqlConnection(sb.ToString());
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return; // exit on fail
            }

            // creating command script
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = script;

            // trying to run command
            if (options.Verbose) Console.WriteLine("# Trying to run command");
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return; // exit on fail
            }

            // trying to open output stream if any
            try
            {
                if (options.OutputFilePath != null)
                {
                    sw = new StreamWriter(options.OutputFilePath, false,
                        Encoding.GetEncoding(options.Encoding));
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return; // exit on fail
            }

            // trying to itterate ove result
            try
            {
                if (options.Verbose) Console.WriteLine("# Itterate over result");
                while (reader.Read())
                {
                    String row = String.Join(options.Delimiter, Fields(reader));
                    // write to file either to console
                    if (sw!=null) sw.WriteLine(row);
                    else Console.WriteLine(row);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            finally
            {
                // cleaning
                if (sw != null) sw.Close();

                if (options.Verbose) Console.WriteLine("# Closing reader");
                reader.Close();
            }

            // closing connection
            if (options.Verbose) Console.WriteLine("# Closing connection");
            connection.Close();
            
            if (options.Verbose) Console.WriteLine("# Done");
        }
    }
}
