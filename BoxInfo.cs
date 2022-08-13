using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxManager
{
    internal class BoxInfo
    {
        private static Random rnd = new Random();
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int Num { get; private set; }
        public int Area { get; private set; }
        public ConsoleColor Color { get; private set; }
        public bool isTurneable { get; private set; }
        

        public BoxInfo( string line)
        {
            string[] data = line.Trim().Split(',');
            
            Height = Convert.ToInt32(data[0]);
            Width = Convert.ToInt32(data[1]);
            Num = Convert.ToInt32(data[2]);
            Area = Height * Width;
            Color = RandomConsoleColor();

            if(Program.storageHeight > Width && Program.storageWidth > Height) isTurneable = true;
            else isTurneable = false;
        }

        public BoxInfo( int height, int width,int boxNum)
        {            
            Height = height;
            Width = width;
            Num = boxNum;
            Area = Height * Width;
            Color = RandomConsoleColor();

            if (Program.storageHeight > Width && Program.storageWidth > Height) isTurneable = true;
            else isTurneable = false;
        }
        private static ConsoleColor RandomConsoleColor()
        {
            return Console.ForegroundColor = (ConsoleColor)rnd.Next(0, 16);
        }
        public void Rotate()
        {
            int hlpr = Height;
            Height = Width;
            Width = hlpr;
        }
    }
}
