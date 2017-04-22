/*
 Copyright Jonathan Porter 2017 All right reserved. Only may be distribuited as part of hiring process of Jonathan Porter.
 */
using System;


namespace CODETEST
{
    class JonathanPorterTest
    {
        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        //PART A of the Test

        public static Point[] ConvertToPoints(char row, int column)
        {
            int trueRow = ((Byte)row - (Byte)'A') + 1;//maybe have a sanity check on this becuase it could throw if row is greater then 256
            //Sanity checks
            if (1 > trueRow || trueRow > 13) { throw new ArgumentException("Invalid row"); }
            if (1 > column || column > 13) { throw new ArgumentException("Invalid column"); }

            //reduce Coordinates to be 0 based
            trueRow--;
            column--;
            //divide Column by 2 since 2 columns exist in one real column
            int trueColumn = column / 2;
            //find out which sub column it is in. 
            int subColumn = column % 2;

            //the first 2 points and just the max and min of the box
            Point[] result = new Point[3];
            result[0] = GetPoint(trueColumn, trueRow);
            result[1] = GetPoint(trueColumn + 1, trueRow + 1);
            //the last one has its x or y increase based on subColumn
            result[2] = GetPoint(trueColumn + 1 * subColumn, trueRow + 1 * (subColumn ^ 1));
            return result;
        }
        private static Point GetPoint(int x, int y)
        {
            return new Point(x * 10, y * 10);
        }

        //PART B of the Test
        public static void ConvertToCoordinates(Point v1, Point v2, Point v3, out char row, out int column)
        {
            //assume the points are not in any order
            //so get the top left cordinate
            int xMin = Min(v1.X, v2.X, v3.X);
            int yMin = Min(v1.Y, v2.Y, v3.Y);

            //checks to see if the inputs are sane.
            if (xMin > 50 || xMin < 0) { throw new ArgumentException("The x Coordinates are out of bounds"); }
            if (yMin > 50 || yMin < 0) { throw new ArgumentException("The y Coordinates are out of bounds"); }

            //then see if the triangle has it right angle on the left or the right. 
            int count = v1.X + v2.X + v3.X - xMin * 3;
            //if count is 10 then its in the left otherwise it will be 20 on the right.
            if (count != 10 && count != 20) { throw new ArgumentException("The Coordinates do not represent a proper triangle"); }

            //to get the row we divide by 10 and convert it to a char
            row = (Char)((byte)'A' + yMin / 10);
            //to get the column we divide by 5 since 2 columns inhabit the same 
            //real column and a one to make it 1 based then add another if the right angle is on the right
            column = xMin / 5 + 1 + ((count == 20) ? 1 : 0);
            return;
        }

        private static int Min(int x1, int x2, int x3)
        {
            //need a min that takes 3 parameters
            return (x1 < x2) ? Math.Min(x1, x3) : Math.Min(x2, x3);
        }


        static void Main(string[] args)
        {
            // PrintCoordinatesAndPoints('A', 1);
            // PrintCoordinatesAndPoints('A', 2);

            RunTest();
            Console.ReadLine();
        }
        static void PrintCoordinatesAndPoints(char row, int column)
        {
            Point[] Coordinates = ConvertToPoints(row, column);
            Console.Write("{0},{1}:", row, column);
            foreach (var cord in Coordinates)
            {
                Console.Write(" ({0},{1})", cord.X, cord.Y);
            }
            Console.WriteLine();
        }
        private static void RunTest()
        {
            for (int y = 0; y < 6; y++)
            {
                for (int x = 1; x <= 12; x++)
                {

                    char row = (Char)(y + (byte)'A');
                    Point[] Vectors = ConvertToPoints(row, x);
                    PrintCoordinatesAndPoints(row, x);
                    char rowResult;
                    int columnResult;
                    ConvertToCoordinates(Vectors[0], Vectors[2], Vectors[1], out rowResult, out columnResult);
                    string matchStr = (rowResult == row && columnResult == x) ? "==" : "!=";
                    Console.WriteLine("({0}, {1}) {4} ({2}, {3})", row, x, rowResult, columnResult, matchStr);
                }
            }
        }
    }
}
