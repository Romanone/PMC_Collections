using System;
using System.Collections;

namespace PMC_Library
{
    /// <summary>
    /// Conteiner collection
    /// </summary>
    /// <typeparam name="T">Any supported numerical C# type</typeparam>
    public class Containers<T> : PMCCollection<Container<T>> where T : struct
    {
        public int ContainerSize { get; set; }

        /// <summary>
        /// Add item to containers
        /// </summary>
        /// <param name="item">Container<T> type</param>
        public override void Add(Container<T> item)
        {
            if (ContainerSize == 0)
            {
                ContainerSize = item.Count;
            }

            if (item.Count == ContainerSize)
            {
                int tempK = 0;

                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].Dimension == 3)
                    {
                        tempK = i;
                        break;
                    }
                }

                if (item[tempK].Dimension == 3)
                {
                    if (collectionList.Count == 0)
                    {
                        collectionList.Add(item);
                    }

                    else
                    {
                        bool skip = false;
                        int tempJ = 0;
                        int tempI = 0;

                        for (int i = 0; i < collectionList.Count; i++)
                        {
                            var tempCycle = collectionList[i] as Container<T>;

                            for (int j = 0; j < tempCycle.Count; j++)
                            {
                                if (tempCycle[j].Dimension == 3)
                                {
                                    tempI = i;
                                    tempJ = j;
                                    skip = true;

                                    break;
                                }
                            }

                            if (skip)
                            {
                                break;
                            }
                        }

                        var temp = collectionList[tempI] as Container<T>;

                        for (int i = 0; i < temp[tempI].Count; i++)
                        {
                            if ((int)temp[tempJ].PositionSizeArray[i] == item[tempK][i].Count)
                            {
                                continue;
                            }

                            else
                            {
                                throw new ContainerRulesException
                                    ("RULE: The number of data points at each XYZ position will be the same for all positions on a matrix and across equivalent matrix indexes across containers.");
                            }
                        }
                        collectionList.Add(item);
                    }
                }

                else
                {
                    collectionList.Add(item);
                }

            }

            else
            {
                throw new ContainerRulesException
                    ("RULE: Each container in a containers collection contains the same number of matrices and the type of each indexed matrix will be the same across containers. ");
            }
        }
    }

    /// <summary>
    /// Matrix colection
    /// </summary>
    /// <typeparam name="T">Any supported numerical C# type</typeparam>
    public class Container<T> : PMCCollection<Matrix<T>> where T : struct
    {
        public int MatrixSize { get; set; }

        public ArrayList PositionSizeArray { get; set; }

        /// <summary>
        /// Add item to container
        /// </summary>
        /// <param name="item">Matrix<T> type</param>
        public override void Add(Matrix<T> item)
        {
            if (MatrixSize == 0)
            {
                MatrixSize = item.Count;
            }

            if (item.Count == MatrixSize)
            {
                if (item.Dimension == 3)
                {
                    if (collectionList.Count == 0)
                    {
                        collectionList.Add(item);
                    }

                    else
                    {
                        int tempI = 0;

                        for (int i = 0; i < collectionList.Count; i++)
                        {
                            var tempCycle = collectionList[i] as Matrix<T>;

                            if (tempCycle.Dimension == 3)
                            {
                                tempI = i;
                                break;
                            }
                        }

                        var temp = collectionList[tempI] as Matrix<T>;

                        if (temp.Dimension == 3)
                        {
                            for (int i = 0; i < temp.Count; i++)
                            {
                                if ((int)temp.PositionSizeArray[i] == item[i].Count)
                                {
                                    continue;
                                }

                                else
                                {
                                    throw new ContainerRulesException
                                        ("RULE: The number of data points at each XYZ position will be the same for all positions on a matrix and across equivalent matrix indexes across containers.");
                                }
                            }

                            collectionList.Add(item);
                        }

                        else
                        {
                            collectionList.Add(item);
                        }


                    }
                }

                else
                {
                    collectionList.Add(item);
                }
            }

            else
            {
                throw new ContainerRulesException
                    ("RULE: All matrices in all containers have the same number of positions.");
            }
        }
    }

    /// <summary>
    /// Position collection
    /// </summary>
    /// <typeparam name="T">Any supported numerical C# type</typeparam>
    public class Matrix<T> : PMCCollection<Position<T>> where T : struct
    {
        public int PositionSize { get; set; }
        public ArrayList PositionSizeArray { get; set; }
        public int Dimension { get; set; }

        public Matrix()
        {
            PositionSizeArray = new ArrayList();
        }

        /// <summary>
        /// Add item to matrix
        /// </summary>
        /// <param name="item">Position<T> type</param>
        public override void Add(Position<T> item)
        {
            if (Dimension == 0)
            {
                Dimension = item.Dimension;
            }

            if (item.Dimension == Dimension)
            {
                if (item.Dimension == 3)
                {
                    if ((int)PositionSizeArray.Count == collectionList.Count)
                    {
                        PositionSizeArray.Add(item.Count);
                    }

                    if (item.Count == (int)PositionSizeArray[collectionList.Count])
                    {
                        collectionList.Add(item);
                    }

                    else
                    {
                        throw new ContainerRulesException
                            ("RULE: The number of data points at each XYZ position will be the same for all positions on a matrix and across equivalent matrix indexes across containers.");
                    }
                }

                else
                {
                    collectionList.Add(item);
                }
            }

            else
            {
                throw new ContainerRulesException("Dimention of Position in the Matrix should be the same");
            }
        }
    }

    /// <summary>
    /// Point collection
    /// </summary>
    /// <typeparam name="T">Any supported numerical C# type</typeparam>
    public class Position<T> : PMCCollection<Point<T>> where T : struct
    {
        public int Dimension { get; set; }

        /// <summary>
        /// Add item to Position
        /// </summary>
        /// <param name="item">Point<T> type</param>
        public override void Add(Point<T> item)
        {
            if (collectionList.Count == 0)
            {
                Dimension = item.Dimension;
                collectionList.Add(item);
            }

            else if (item.Dimension == Dimension)
            {
                collectionList.Add(item);
            }

            else
            {
                throw new ContainerRulesException("Dimention of Points in the Position should be the same");
            }
        }
    }

    /// <summary>
    /// Simplest elemet of collection
    /// </summary>
    /// <typeparam name="T">Any supported numerical C# type</typeparam>
    public class Point<T>
    {
        private readonly T x;
        private readonly T y;
        private readonly T z;
        private readonly int dimension;

        public T X { get { return x; } }
        public T Y { get { return y; } }
        public T Z { get { return z; } }
        public int Dimension { get { return dimension; } }

        public Point(T x, T y)
        {
            this.x = x;
            this.y = y;
            this.z = default(T);

            dimension = 2;
        }

        public Point(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            dimension = 3;
        }

        public override String ToString()
        {
            return ("Point: X=" + X + " Y=" + Y + " Z=" + Z);
        }
    }

    /// <summary>
    /// Class with specific containers rules exception 
    /// </summary>
    public class ContainerRulesException : Exception
    {
        public ContainerRulesException(string message) : base(message)
        {

        }
    }
}
