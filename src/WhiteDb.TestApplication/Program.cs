namespace WhiteDb.TestApplication
{
    using System;
    using System.Linq;

    using WhiteDb.Data;

    public class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }

    public static class Program
    {
        private static void Main()
        {
            using (var dataContext = new DataContext<Person>("1"))
            {
                dataContext.Create(new Person { Age = 1, Name = "Kati" });
                dataContext.Create(new Person { Age = 2, Name = "Mati" });

                var query = dataContext.Query().Where(a => a.Age == 1);
                foreach (var person in query)
                {
                    Console.WriteLine("Name: {0}, Age: {1}", person.Name, person.Age);
                }
            }
        }
    }
}