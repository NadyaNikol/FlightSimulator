using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PlaneFlight
{
    public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);


    partial class Plane
    {
        List<Dispatcher> ListDisp;
        int speed;
        int height;


        public Plane() { ListDisp = new List<Dispatcher>(); }

        int countD;
        void AddDisp()
        {
            string NameD;

            do
            {
                Write("Введите количество диспетчеров -> ");
                countD = int.Parse(ReadLine());
                if (ListDisp.Count + countD < 2) WriteLine("Диспетчеров не может быть меньше 2ух\n\n");

            } while (ListDisp.Count + countD < 2);


            for (int i = 0; i < countD; i++)
            {
                Write($"Введите имя {i + 1}-го диспетчера -> ");
                NameD = ReadLine();
                Dispatcher D = new Dispatcher(NameD);

                ListDisp.Add(D);

            }


            AddProperty(countD); // привязка к событиям
        }


        void DelDisp()
        {
            WriteLine("Введите имя диспетчера, которого хотите удалить");
            string name = ReadLine();
            int pos = 0;

            if (ListDisp.Count - 1 < 2) WriteLine("Диспетчеров не может быть меньше 2ух");

            else
            {
                for (int i = 0; i < ListDisp.Count; i++)
                {
                    if (ListDisp[i].Name == name)
                    {
                        pos = i;
                        ListDisp.Remove(ListDisp[i]);
                    }

                }
            }

            DelProperty(pos); // отвязка от событий
        }



        void AddProperty(int countD)
        {
            for (int i = 0; i < countD; i++) // привязка к событиям
            {
                ListDisp[i].PropertyMorePenalty += ShowMessage;
                ListDisp[i].PropertyMoreHeight += ShowMessage;
                ListDisp[i].PropertyMoreSpeed += ShowMessage;
                ListDisp[i].PropertyZeroHeightOrSpeed += ShowMessage;
            }

        }


        void DelProperty(int poz)
        {
            for (int i = 0; i < ListDisp.Count; i++) // отвязка от событий
            {
                if (i == poz)
                {
                    ListDisp[i].PropertyMorePenalty -= ShowMessage;
                    ListDisp[i].PropertyMoreHeight -= ShowMessage;
                    ListDisp[i].PropertyMoreSpeed -= ShowMessage;
                    ListDisp[i].PropertyZeroHeightOrSpeed -= ShowMessage;
                    break;
                }
            }

        }

        int n;
        void MenuDisp()// удаление или добавление диспетчеров
        {
            WriteLine("Выберите пункт меню добавить или удалить диспетчеров передвигаясь" +
                " стрелками влево - вправо\n" +
                "Чтобы вернуться в предыдущее меню нажмите ESC");
            WriteLine("\n\n\nДобавить\t\tУдалить");
            ConsoleKeyInfo k;
            do
            {

                k = ReadKey();
                Clear();
                if (k.Key == ConsoleKey.LeftArrow)
                {
                    WriteLine("Добавить ");
                    WriteLine("  *\t\t\tУдалить");
                    n = 0;
                }

                else if (k.Key == ConsoleKey.RightArrow)
                {
                    WriteLine("\t\t\tУдалить ");
                    WriteLine("Добавить  \t\t*\t\t");
                    n = 1;
                }

                else if (k.Key == ConsoleKey.Enter)
                {
                    if (n == 0) AddDisp();
                    else DelDisp();
                    break;
                }

            } while (k.Key != ConsoleKey.Escape);
        }

        void ControlPlane(ConsoleKeyInfo key)
        {

            // СКОРОСТЬ
            if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.RightArrow)
                SpeedShiftUp();
            else if (key.Key == ConsoleKey.RightArrow) { SpeedUp(); }
            else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.LeftArrow) SpeedShiftDown();
            else if (key.Key == ConsoleKey.LeftArrow) { SpeedDown(); }


            // ВЫСОТА
            else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.UpArrow) HeightShiftUp();
            else if (key.Key == ConsoleKey.UpArrow) { HeightUp(); }
            else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.DownArrow) HeightShiftDown();
            else if (key.Key == ConsoleKey.DownArrow) { HeightDown(); }

        }


        // скорость
        int SpeedUp()
        {
            return speed += 50;
        }

        int SpeedShiftUp()
        {
            return speed += 150;
        }

        int SpeedDown()
        {
            return speed -= 50;
        }

        int SpeedShiftDown()
        {
            return speed -= 150;
        }



        // высота
        int HeightUp()
        {
            return height += 250;
        }

        int HeightShiftUp()
        {
            return height += 500;
        }

        int HeightDown()
        {
            return height -= 250;
        }

        int HeightShiftDown()
        {
            return height -= 500;
        }


        string Rules()
        {
            return $"Управление самолетом \n" +
                   $"Стрелка ВПРАВО --- увеличение скорости на 50 км/ч\n" +
                   $"SHIFT + стрелка ВПРАВО --- увеличение скорости на 150 км/ч\n" +
                   $"Стрелка  ВЛЕВО --- уменьшение скорости на 50 км/ч\n" +
                   $"SHIFT + стрелка ВЛЕВО ---  уменьшение скорости на 50 км/ч\n" +

                   $"\n\nСтрелка ВВЕРХ ---увеличение высоты на 250 м\n" +
                   $"SHIFT + стрелка ВВЕРХ --- увеличение высоты на 500 м\n" +
                   $"Стрелка  ВНИЗ --- уменьшение высоты на 250 м\n" +
                   $"SHIFT + стрелка ВНИЗ ---  уменьшение высоты на 500 м\n" +

                   $"\n\nЧтобы снова просмотреть правила управления самолетом нажмите TAB во время игры\n" +
                   $"Чтобы добавить или удалить диспетчеров нажмите SPACE во время игры\n\n";
        }

        private static void ShowMessage(object sender, PropertyEventArgs e)
        {
            WriteLine(e.mess);
        }

    }
}