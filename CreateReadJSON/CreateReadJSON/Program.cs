using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    static void Main(string[] args)
    {
        //Movie m1 = new Movie();
        //m1.Title = "Indiana Jones";
        //m1.Year = "1981";
        //m1.WonAward = true;
        //m1.Budget = 3500000;
        //m1.MusicBy = "John Williams";
        //m1.Cast = new string[]
        //{
        //    "Harry",
        //    "Colin",
        //    "Cosun"
        //};

        //Movie m2 = new Movie();
        //m2.Title = "Mission Im";
        //m2.Year = "1996";
        //m2.WonAward= true;
        //m2.Budget = 600000;
        //m2.MusicBy = "enio";
        //m2.Cast = new string[]
        //{
        //    "Cosun",
        //    "Cobin"
        //};

        //Movies movies = new Movies();
        //movies.Films = new Movie[] { m1, m2 };

        //JsonSerializerOptions options = new JsonSerializerOptions();
        //options.WriteIndented = true;

        //string movieJson = JsonSerializer.Serialize(movies, options);

        //Console.WriteLine(movieJson);  

        string jsonString = System.IO.File.ReadAllText("json1.txt");

        Movie movie = JsonSerializer.Deserialize<Movie>(jsonString);
        Console.WriteLine("Title: {0}, WonAwards: {1}\n", movie.Title, movie.WonAward);
    }
}