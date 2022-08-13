using System;
using System.IO;

namespace BoxManager
{
    internal class Program
    {
        public static int storageHeight = 10;
        public static int storageWidth = 10;
        private static int boxNum = 10;

        public static List<BoxInfo> boxes = new List<BoxInfo>();
        public static List<BoxInfo> boxes2 = new List<BoxInfo>();
        static void Main(string[] args)
        {
            GetValues();

            Logic.GenerateBoxes(boxNum-1);
            //StreamReader sr = File.OpenText("boxPrefab.txt");
            //while (!sr.EndOfStream)
            //{
            //    boxes.Add(new BoxInfo(sr.ReadLine()));
            //}
            foreach (BoxInfo box in boxes)
            {
                boxes2.Add(box);
            }
            Logic.ArrangeBoxes();

            Renderer.Render();
        }
        private static void GetValues()
        {
            try
            {
                Console.Write("Enter storage height: ");
                storageHeight = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter storage width: ");
                storageWidth = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the number of boxes: ");
                boxNum = Convert.ToInt32(Console.ReadLine());

            }
            catch (Exception)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Input error: The simulation will use some default values!!!");
                Console.WriteLine("-----------------------press ENTER-------------------------");
                Console.ResetColor();
                Console.ReadLine();
            }

            Console.Clear();

        }
    }
}