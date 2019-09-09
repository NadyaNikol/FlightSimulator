using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PlaneFlight
{
    partial class Plane
    {
        public void Flight()
        {
            ConsoleKeyInfo key;

            WriteLine(Rules());

            AddDisp(); // добавление диспетчера
            WriteLine("\n");

            int RecomHeight;
            int AllPenalty = 0;

            int point = 0; // для прорисовки

            bool IsPlaneFly = false;

            do
            {
                ForegroundColor = ConsoleColor.Gray;
                if (speed > 50) // диспетчера управляют самолетом со скоростью больше 50км/ч
                {

                    WriteLine("\n\n___Рекомендуемая высота___ ");

                    for (int i = 0; i < ListDisp.Count; i++)
                    {
                        if (IsPlaneFly) // исключитальная ситуация (диспетчер)
                        {
                            ListDisp[i].ControlSpeed = speed;
                            ListDisp[i].ControlHeight = height;
                        }

                        RecomHeight = ListDisp[i].RecommendedHeight(speed);
                        WriteLine($"Диспетчера {ListDisp[i].Name} -> {RecomHeight}");

                        if (height > RecomHeight)
                        { height += RecomHeight; RecomHeight = height - RecomHeight; height -= RecomHeight; } // меняем местами

                        if (RecomHeight - height >= 300) // штрафы
                            ListDisp[i].Penalty += ListDisp[i].PenaltyPointsHeight(RecomHeight - height);

                        if (speed > 1000) ListDisp[i].Penalty += ListDisp[i].PenaltyPointsSpeed();

                        AllPenalty += ListDisp[i].Penalty;

                    }
                }

                for (int i = 0; i < ListDisp.Count; i++)
                    WriteLine($"Штрафные очки диспетчера {ListDisp[i].Name} -> {ListDisp[i].Penalty}");

                WriteLine("Текущая скорость -> " + speed);
                WriteLine("Текущая высота -> " + height);


                Show(point);
                key = ReadKey();
                Clear();

                // прорисовка ======================
                if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.UpArrow) point += 2;
                else if (key.Key == ConsoleKey.UpArrow) point++;
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.DownArrow) point -= 2;
                else if (key.Key == ConsoleKey.DownArrow) point--;
                //=====================================


                ControlPlane(key); // управление самолетом
                if (speed > 0 && height > 0) IsPlaneFly = true; // если самолет полетел

                if (key.Key == ConsoleKey.Tab) { WriteLine(Rules()); ReadKey(); }// вывод правил
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    MenuDisp();
                }


            } while (speed != 0);

        }

        void Show(int point)
        {
            ForegroundColor = ConsoleColor.Cyan;
            int a = 30, b = 60;
            char[,] arr = new char[a, b];


            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {

                    if (i == a - (point + 3) && j == b / 2) arr[i, j] = '+';
                    else if (i == a - (point + 2) && j >= b / 2 - 3 && j <= b / 2 + 3) arr[i, j] = 'X';
                    else if (i == a - 1) arr[i, j] = '*';
                    else arr[i, j] = ' ';
                }
            }



            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    Write(arr[i, j]);
                }
                WriteLine();
            }

        }
    }
}