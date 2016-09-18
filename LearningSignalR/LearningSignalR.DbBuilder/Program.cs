using System;
using LearningSignalR.Db;

namespace LearningSignalR.DbBuilder
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("START");

            using (var dbContext = new AppDbContext("DefaultConnection"))
            {
                StaticData.Seed(dbContext);
            }

            Console.WriteLine("END");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
