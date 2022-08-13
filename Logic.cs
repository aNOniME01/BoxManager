using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxManager
{
    internal class Logic
    {
        public static int[,] storage;

        private static int storageHeight;
        private static int storageWidth;

        public static int usedBoxes = 0;

        private static  List<BoxInfo> boxes;

        private static Random rnd = new Random(); 


        public static void GenerateBoxes(int num)
        {
            storageHeight = Program.storageHeight;
            storageWidth = Program.storageWidth;
            boxes = Program.boxes;

            boxes.Clear();
            for (int i = 0; i <= num; i++)
            {
                boxes.Add(new BoxInfo(rnd.Next(1, (storageHeight+1)/2), rnd.Next(1, (storageWidth+1)/2),i));
            }
        }
        public static void ArrangeBoxes()
        {
            storageHeight = Program.storageHeight;
            storageWidth = Program.storageWidth;
            boxes = Program.boxes;

            WipeStorage();

            SortBoxesByDescending(boxes);
            SortBoxesByDescending(Program.boxes2);

            for (int y = 0; y < storageHeight; y++)
            {
                for (int x = 0; x < storageWidth; x++)
                {
                    if (storage[y, x] == -1)
                    {
                        BoxInfo searchedBox = SearchBoxBySide(GetGapSize(x,y),y);
                        if (searchedBox != null)
                        {
                            PutBoxTo(x, y, searchedBox);
                            x += searchedBox.Width-1;
                        }
                    }
                }
            }
        }
        public static ConsoleColor SearchBoxColorByNum(int num)
        {
            foreach (BoxInfo box in Program.boxes2)
            {
                if (box.Num == num) return box.Color;
            }
            return ConsoleColor.Black;
        }
        public static int CountEmptySpace()
        {
            int count = 0;
            for (int y = 0; y < storageHeight; y++)
            {
                for (int x = 0; x < storageWidth; x++)
                {
                    if (storage[y, x] == -1) count++;
                }
            }
            return count;
        }


        private static void SortBoxesByDescending(List<BoxInfo> boxInfos) => boxInfos.Sort((x, y) => y.Area.CompareTo(x.Area));//Sort the boxes by size (descending)
        private static void WipeStorage()
        {
            storage = new int[storageHeight, storageWidth];

            for (int y = 0; y < storageHeight; y++)
            {
                for (int x = 0; x < storageWidth; x++)
                {
                    storage[y, x] = -1;
                }
            }
        }
        private static BoxInfo SearchBoxBySide(int size, int y)
        {
            foreach (BoxInfo box in boxes)
            {
                if (box.Width > box.Height)
                {
                    if (box.Width <= size && y + box.Height <= storageHeight)
                    {
                        boxes.Remove(box);
                        return box;
                    }
                    else if (box.Height <= size && y + box.Width <= storageHeight)
                    {
                        boxes.Remove(box);
                        box.Rotate();
                        return box;
                    }
                }
                else
                {
                    if (box.Height <= size && y + box.Width <= storageHeight)
                    {
                        boxes.Remove(box);
                        box.Rotate();
                        return box;
                    }
                    else if (box.Width <= size && y + box.Height <= storageHeight)
                    {
                        boxes.Remove(box);
                        return box;
                    }
                }
            }
            return null;
        }
        private static int GetGapSize(int x,int y)
        {
            int count = 0;
            while (x < storageWidth && storage[y, x] < 0)
            {
                count++;
                x++;
            }
            return count;
        }
        private static void PutBoxTo(int boxX,int boxY,BoxInfo box)
        {
            for (int y = boxY; y < boxY+box.Height; y++)
            {
                for (int x = boxX; x < boxX+box.Width; x++)
                {
                    storage[y,x] = box.Num;
                }
            }
            usedBoxes++;
            Renderer.Render();
        }

    }
}
