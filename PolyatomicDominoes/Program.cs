using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace PolyatomicDominoes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Deminsions");
            int deminsion = 4;//int.Parse(Console.ReadLine());
            Square[,] squares = new Square[deminsion, deminsion];
            List<Square> squareList = new List<Square>(deminsion * deminsion);
            List<Square> squareBucket = new List<Square>(deminsion * deminsion);
            int elementNum = 0;
            string file = System.IO.File.ReadAllText(@"C:\Users\Jacob\Documents\squares.txt");
            List<string> seporateElements = new List<string>(64);
            /*squareList = new List<Square> { new Square("left","sdfgsdf","trhgbd","bottom","0"),
                                            new Square("egfdfsbsfd","left","fdbdfb","top","1"),
                                            new Square("rhrgfg","right","top","ergwrgr","2"),
                                            new Square("very bottom right","right","outer right bottom","bottom","3")};*/
            foreach (string singleString in file.Split(new char[4] { ' ', '\t', '\n', '\r' }))
            {
                if (singleString != "")
                {
                    seporateElements.Add(singleString);
                }
            }
            for (int X = 0; X < squares.GetLength(0); X++)
            {
                for (int Y = 0; Y < squares.GetLength(1); Y++)
                {
                    elementNum++;
                    if(seporateElements[elementNum].StartsWith("element")||
                       seporateElements[elementNum].StartsWith("#"))
                    {
                        elementNum++;
                    }
                    string top = seporateElements[elementNum];
                    elementNum++;
                    string bottom = seporateElements[elementNum];
                    elementNum++;
                    string left = seporateElements[elementNum];
                    elementNum++;
                    string right = seporateElements[elementNum];
                    elementNum++;
                    squareList.Add(new Square(top, bottom, left, right, (Y * 4 + X).ToString()));
                    /*Console.WriteLine("Element #" + elementNum.ToString());
                    Console.WriteLine("enter top");
                    string top = Console.ReadLine();
                    Console.WriteLine("enter bottom");
                    string bottom = Console.ReadLine();
                    Console.WriteLine("enter left");
                    string left = Console.ReadLine();
                    Console.WriteLine("enter right");
                    string right = Console.ReadLine();
                    Square newSquare = new Square(top, bottom, left, right, "sq:" + elementNum.ToString());*/
                    squares[X, Y] = new Square("", "", "", "", "");
                    //squareList.Add(newSquare);
                    //elementNum++;
                }
            }
            squareBucket = squareList;
            addSquare(0, 0, squareBucket, squares, -1);
            Console.WriteLine("Done");
            Console.Read();
        }
        static void addSquare(int X, int Y, List<Square> squareBucket, Square[,] squares, int level)
        {
            level++;
            string leftMatch = "";
            string rightMatch = "";
            string bottomMatch = "";
            string topMatch = "";
            if (X == 0)
            {
                //left side is nothing
                rightMatch = squares[X + 1, Y].left;
            }
            else if (X < squares.GetLength(0) + 1)
            {
                //right side is nothing
                leftMatch = squares[X - 1, Y].right;
            }
            else
            {
                //get left and right 
                leftMatch = squares[X - 1, Y].right;
                rightMatch = squares[X + 1, Y].left;
            }
            if (Y == 0)
            {
                //top side is nothing
                bottomMatch = squares[X, Y + 1].top;
            }
            else if (Y < squares.GetLength(1) + 1)
            {
                //bottom side is nothing
                topMatch = squares[X, Y - 1].bottom;
            }
            else
            {
                //top and bottom exist
                bottomMatch = squares[X, Y + 1].top;
                topMatch = squares[X, Y - 1].bottom;
            }
            List<Square> possibleMatches = new List<Square>();
            List<int> possibleRotNums = new List<int>();
            Square currentSquare;
            foreach (Square square in squareBucket)
            {
                currentSquare = square;
                int possibleRotNum = 0;
                for (int rot = 0; rot <= 3; rot++)
                {
                    currentSquare.rotate();
                    Square newSquare = new Square(currentSquare);
                    bool hit = true;
                    if (topMatch != "")
                    {
                        if (topMatch == newSquare.top)
                        {
                            hit = true;
                        }
                        else
                        {
                            hit = false;
                            continue;
                        }
                    }
                    if (bottomMatch != "")
                    {
                        if (bottomMatch == newSquare.bottom)
                        {
                            hit = true;
                        }
                        else
                        {
                            hit = false;
                            continue;
                        }
                    }
                    if (leftMatch != "")
                    {
                        if (leftMatch == newSquare.left)
                        {
                            hit = true;
                        }
                        else
                        {
                            hit = false;
                            continue;
                        }
                    }
                    if (rightMatch != "")
                    {
                        if (rightMatch == newSquare.right)
                        {
                            hit = true;
                        }
                        else
                        {
                            hit = false;
                            continue;
                        }
                    }
                    if (hit == true)
                    {
                        possibleMatches.Add(newSquare);
                        possibleRotNum++;
                        continue;
                    }
                }
                possibleRotNums.Add(possibleRotNum);
            }
            int oldX = X;
            int oldY = Y;
            foreach(Square possibleMatch in possibleMatches)
            {
                if (level > 13)
                {
                    Console.WriteLine("--------------------------\nAt level"+level.ToString());
                    printSquares(squares);
                }
                /*else if (level > 10)
                {
                    int i = 2;
                }*/
                X = oldX;
                Y = oldY;
                List<Square> tempSquareBucket = new List<Square>();
                foreach (Square square in squareBucket)
                {
                    if (square.name != possibleMatch.name)
                    {
                        tempSquareBucket.Add(square);//add only if the names aren't the same
                    }
                }/*
                for (int removeNum = 0; removeNum < possibleRotNum; removeNum++)
                {
                    tempSquareBucket.Remove(possibleMatch);
                }*/
                squares[X, Y] = possibleMatch;
                X++;
                if (X == squares.GetLength(0))
                {
                    X = 0;
                    Y++;
                }
                if (Y == squares.GetLength(1))
                {
                    Console.WriteLine("Found solution");
                    printSquares(squares);
                    throw new Exception("All done!");
                }
                addSquare(X, Y, tempSquareBucket, squares, level);
            }
            squares[oldX, oldY] = new Square("", "", "", "", "");
            //no match found
            //all done
        }
        static void printSquares(Square[,] squares)
        {
            for (int Yloc = 0; Yloc < squares.GetLength(0); Yloc++)
            {
                for (int Xloc = 0; Xloc < squares.GetLength(1); Xloc++)
                {
                    Console.Write(squares[Xloc, Yloc].name + "    ");
                }
                Console.Write("\n");
            }
        }
    }
}
