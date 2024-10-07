using System;
using System.Collections.Generic;
using System.Drawing;

class Program
{

    static void Main()
    {

        Console.CursorVisible = false;
        char currentChar = '█';
        ConsoleColor currentColor = ConsoleColor.White;

        int startX = 20, startY = 10;
        int x = startX, y = startY;

        int borderX1 = 10;
        int borderY1 = 5;
        int borderX2 = 110;
        int borderY2 = 25;

        BorderLine(borderX1, borderY1, borderX2, borderY2, '█', ConsoleColor.Red);

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
                    if (y < borderY2 - 1) y++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > borderX1 + 1) x--;
                    break;
                case ConsoleKey.RightArrow:
                    if (x < borderX2 - 1) x++;
                    break;

                case ConsoleKey.D1:
                    currentColor = ConsoleColor.White;
                    break;
                case ConsoleKey.D2:
                    currentColor = ConsoleColor.Black;
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
                    currentColor = ConsoleColor.White;
                    BorderLine(borderX1, borderY1, borderX2, borderY2, '█', ConsoleColor.Red);

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
                        BorderLine(borderX1, borderY1, borderX2, borderY2, '█', ConsoleColor.Red);



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
    static void BorderLine(int x1, int y1, int x2, int y2, char borderChar, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        for (int x = x1; x <= x2; x++)
        {
            Console.SetCursorPosition(x, y1);
            Console.Write(borderChar);
            Console.SetCursorPosition(x, y2);
            Console.Write(borderChar);
        }

        for (int y = y1; y <= y2; y++)
        {
            Console.SetCursorPosition(x1, y);
            Console.Write(borderChar);
            Console.SetCursorPosition(x2, y);
            Console.Write(borderChar);
        }


    }
}
