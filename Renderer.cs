using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxManager
{
    internal class Renderer
    {
        private static int[,] storage;

        private static int storageHeight;
        private static int storageWidth;

        private static List<BoxInfo> boxes;
        public static void Render()
        {
            storageHeight = Program.storageHeight+1;
            storageWidth = Program.storageWidth+1;
            storage = Logic.storage;
            boxes = Program.boxes2;

            Console.Clear();

            RenderStorage();

            WriteStringAt(storageHeight + 2, storageWidth/2, ConsoleColor.Black, "Boxes: ");

            RenderBoxPreview();

            RenderPlacedBoxes();

            Console.ReadKey();
        }
        private static void RenderStorage()
        {
            for (int i = 0; i < storageHeight; i++) Console.WriteLine();// Makeing room for the storage render

            for (int i = 0; i <= storageHeight; i++)
            {
                for (int j = 0; j <= storageWidth; j++)
                {
                    if (i == 0 || i == storageHeight || j == 0 || j == storageWidth) WriteAt(i, j, ConsoleColor.Black, 'X');
                }
            }
            WriteStringAt(1, storageWidth + 2, ConsoleColor.Black, $"storageHeight: {storageHeight-1}");
            WriteStringAt(2, storageWidth + 2, ConsoleColor.Black, $"storageWidth: {storageWidth-1}");

            int emptySpace = Logic.CountEmptySpace();
            int usedBoxes = Logic.usedBoxes;

            WriteStringAt(4, storageWidth + 2, ConsoleColor.Black, $"emptySpace: {emptySpace}");
            WriteStringAt(5, storageWidth + 2, ConsoleColor.Black, $"coveredSpace: {(storageHeight-1) * (storageWidth-1) - emptySpace}");
            WriteStringAt(6, storageWidth + 2, ConsoleColor.Black, $"usedBoxes: {usedBoxes}");
            WriteStringAt(7, storageWidth + 2, ConsoleColor.Black, $"remainingBoxes: {boxes.Count - usedBoxes}");
        }
        private static void RenderBoxPreview()
        {
            int boxHSum = 0;
            foreach (BoxInfo box in boxes)
            {
                boxHSum++;
                WriteStringAt(storageHeight + 1 + box.Height + boxHSum, 0, ConsoleColor.Black, box.Num.ToString() + ":");
                for (int i = 0; i < box.Height; i++)
                {
                    for (int j = 0; j < box.Width; j++)
                    {
                        WriteAt(storageHeight + 2 + box.Height + boxHSum - i, 5 + j, box.Color, box.Num.ToString()[0]);
                    }
                }
                boxHSum += box.Height;
            }
        }
        private static void RenderPlacedBoxes()
        {
            for (int y = 0; y < storageHeight-1; y++)
            {
                for (int x = 0; x < storageWidth-1; x++)
                {
                    if(storage[y,x] != -1)
                    {
                        WriteAt(y + 1, x + 1, Logic.SearchBoxColorByNum(storage[y, x]), storage[y, x].ToString()[0]);
                    }
                    else
                    {
                        WriteAt(y+1, x+1, ConsoleColor.Black, ' ');
                    }
                }
            }
        }
        private static void WriteAt(int x, int y,ConsoleColor bColor, char text)
        {
            Console.SetCursorPosition(y, x);
            Console.BackgroundColor = bColor;
            Console.Write(text);
            Console.ResetColor();
        }
        private static void WriteStringAt(int x, int y,ConsoleColor bColor, string text)
        {
            Console.SetCursorPosition(y, x);
            Console.BackgroundColor = bColor;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
