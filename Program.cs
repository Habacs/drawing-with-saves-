using System;
using System.Collections.Generic;
using System.Drawing;

class Program
{
    static void Main()
    {
        int topx = 10;
        int topy = 5;

        int buttomx = 10;
        int buttomy = 24;

        int leftSidex = 10;
        int leftSidey = 5;

        int rightSidex = 110;
        int rightSidey = 5;
        char borderchar = '█';
        ConsoleColor color = ConsoleColor.Red;

        while (topx != 110 && buttomx != 110)
        {

            Console.SetCursorPosition(topx, topy);

            Console.ForegroundColor = color;
            Console.Write(borderchar);
            topx++;

            Console.SetCursorPosition(buttomx, buttomy);
            Console.ForegroundColor = color;
            Console.Write(borderchar);
            buttomx++;
        }

        while (leftSidey != 25 && rightSidey != 25)
        {
            Console.SetCursorPosition(leftSidex, leftSidey);
            Console.ForegroundColor = color;
            Console.Write(borderchar);
            leftSidey++;


            Console.SetCursorPosition(rightSidex, rightSidey);
            Console.ForegroundColor = color;
            Console.Write(borderchar);
            rightSidey++;

        }




        Console.CursorVisible = false;
        char currentChar = '█';
        ConsoleColor currentColor = ConsoleColor.White;

        int startX = 20, startY = 10;
        int x = startX, y = startY;

        int borderX1 = 10;
        int borderY1 = 5;
        int borderX2 = 110;
        int borderY2 = 25;

        List<List<(int x, int y, char c, ConsoleColor color)>> savedDrawings = new List<List<(int, int, char, ConsoleColor)>>();
        List<(int x, int y, char c, ConsoleColor color)> currentDrawing = new List<(int, int, char, ConsoleColor)>();

        while (true)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = currentColor;
            Console.Write(currentChar);
            Console.ResetColor();

            currentDrawing.Add((x, y, currentChar, currentColor));

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (y > borderY1 + 1) y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (y < borderY2 - 2) y++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > borderX1 + 1) x--;
                    break;
                case ConsoleKey.RightArrow:
                    if (x < borderX2 - 1) x++;
                    break;

                case ConsoleKey.D1:
                    currentColor = ConsoleColor.Red;
                    break;
                case ConsoleKey.D2:
                    currentColor = ConsoleColor.Green;
                    break;
                case ConsoleKey.D3:
                    currentColor = ConsoleColor.Blue;
                    break;
                case ConsoleKey.D4:
                    currentColor = ConsoleColor.Yellow;
                    break;
                case ConsoleKey.D5:
                    currentColor = ConsoleColor.Magenta;
                    break;
                case ConsoleKey.D6:
                    currentColor = ConsoleColor.Cyan;
                    break;

                case ConsoleKey.C:
                    Console.Clear();
                    savedDrawings.Add(new List<(int, int, char, ConsoleColor)>(currentDrawing));  
                    currentDrawing.Clear();
                    x = startX;  
                    y = startY;  
                    break;

                case ConsoleKey.R:
                    if (savedDrawings.Count > 0)
                    {
                        int selectedDrawing = SelectDrawing(savedDrawings);
                        Console.Clear();
                        foreach (var pixel in savedDrawings[selectedDrawing])
                        {
                            Console.SetCursorPosition(pixel.x, pixel.y);
                            Console.ForegroundColor = pixel.color;
                            Console.Write(pixel.c);
                        }
                        currentDrawing = new List<(int, int, char, ConsoleColor)>(savedDrawings[selectedDrawing]); 
                    }
                    break;

                default:
                    break;
            }
        }
    }

    static int SelectDrawing(List<List<(int x, int y, char c, ConsoleColor color)>> savedDrawings)
    {
        int selectedIndex = 0;
        bool choosing = true;

        while (choosing)
        {
            Console.Clear();

            for (int i = 0; i < savedDrawings.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;  
                    Console.WriteLine($"rajz {i + 1}");
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine($"rajz {i + 1}");
                }
            }

            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0) selectedIndex--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedIndex < savedDrawings.Count - 1) selectedIndex++;
                    break;
                case ConsoleKey.Enter:
                    choosing = false;  
                    break;
            }
        }

        return selectedIndex;
    }
}