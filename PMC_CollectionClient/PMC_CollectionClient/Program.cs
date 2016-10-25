using System;
using PMC_Library;
using System.Threading;

namespace PMC_CollectionClient
{
    class Program
    {

        static Point<int>[] pointArray;
        static Position<int>[] positionArray;
        static Matrix<int>[] matrixArray;
        static Container<int>[] containerArray;
        static Containers<int> containersArray;

        static void Main(string[] args)
        {
            int ContainerCount = 3;
            int MatrixCount = 2;
            int PositionCount = 5;
            int PointCount = 5;

            #region *** Generate containers collections ***

            Random random = new Random();

            pointArray = new Point<int>[PointCount];
            positionArray = new Position<int>[PositionCount];
            matrixArray = new Matrix<int>[MatrixCount];
            containerArray = new Container<int>[ContainerCount];
            containersArray = new Containers<int>();


            #endregion


            Console.WriteLine("Hello, this is PMC Data Container.\n");
            Console.WriteLine("Containers is full, so you can test it.\n");

            Console.WriteLine("Here are all the commands I understand:\n\n settings - open containers settings\n find - find item by index\n show - display cuurent collections\n generate - generate new collection using settings param.\n exit - close Console Clien");



            do
            {
                Console.Write("\nwaiting for command: ");
                string command = Console.ReadLine();
                try
                {
                    switch (command)
                    {
                        case ("settings"):
                            {
                                Console.WriteLine("Put Container Count:");
                                ContainerCount = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Put Matrix Count:");
                                MatrixCount = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Put Position Count:");
                                PositionCount = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Put Point Count:");
                                PointCount = Convert.ToInt32(Console.ReadLine());
                                break;
                            }

                        case ("find"):
                            {
                                Console.WriteLine("Put Container Positon:");
                                ContainerCount = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Put Matrix Count:");
                                MatrixCount = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Put Position Count:");
                                PositionCount = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Put Point Count:");
                                PointCount = Convert.ToInt32(Console.ReadLine());

                                Display(containersArray[ContainerCount][MatrixCount][PositionCount][PointCount].ToString());
                                Console.ReadKey();
                                break;
                            }

                        case ("generate"):
                            {
                                Console.WriteLine("Done...");
                                break;
                            }

                        case ("show"):
                            {
                                DisplayAll();
                                break;
                            }

                        case ("exit"):
                            {
                                Console.WriteLine("\n* * * * * * * * *\nThank you for using. Bye!\n\n* * * * * * * * * *");
                                Thread.Sleep(2000);
                                Environment.Exit(0);
                                break;
                            }

                        default:
                            {
                                Console.WriteLine("wrong command");
                                break;
                            }

                    }

                    containersArray = new Containers<int>();
                    // Creating Point
                    for (int j = 0; j < PointCount; j++)
                    {
                        pointArray[j] = new Point<int>(random.Next(0, 10), random.Next(0, 10), random.Next(0, 10));
                    }

                    // Creating Position
                    for (int i = 0; i < PositionCount; i++)
                    {
                        positionArray[i] = new Position<int>();
                        for (int p = 0; p < PointCount; p++)
                        {
                            positionArray[i].Add(pointArray[p]);
                        }
                    }

                    // Creating Matrix
                    for (int m = 0; m < MatrixCount; m++)
                    {
                        matrixArray[m] = new Matrix<int>();
                        for (int i = 0; i < PositionCount; i++)
                        {
                            matrixArray[m].Add(positionArray[i]);
                        }
                    }

                    // Creating Container
                    for (int i = 0; i < ContainerCount; i++)
                    {
                        containerArray[i] = new Container<int>();
                        for (int j = 0; j < MatrixCount; j++)
                        {
                            containerArray[i].Add(matrixArray[j]);
                        }
                    }

                    // Containers pack
                    for (int i = 0; i < ContainerCount; i++)
                    {
                        containersArray.Add(containerArray[i]);
                    }

                }
                catch (ContainerRulesException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            } while (true);
        }

        public static void Display(string str)
        {
            Console.WriteLine(str);
        }

        public static void DisplayAll()
        {
            int a = 0;
            int b = 0;
            int c = 0;

            foreach (var containers in containersArray)
            {
                Console.WriteLine("Container #" + a);
                a++;
                b = 0;
                
                foreach (var container in containers)
                {
                    Console.WriteLine("Matrix #" + b);
                    b++;
                    c = 0;
                    foreach (var matrix in container)
                    {
                        Console.WriteLine("Position #" + c);
                        c++;
                        foreach (var position in matrix)
                        {
                            Console.WriteLine(position.ToString());
                        }
                    }
                }
            }
        }

    }
}
