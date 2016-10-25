using NUnit.Framework;
using PMC_Library;

namespace PMC_CollectionClient
{
    [TestFixture]
    public class ContainersTest
    {

        [Test]
        public void Point_Creation()
        {
            Point<int> point3D = new Point<int>(3, 3, 3);
            Point<int> point2D = new Point<int>(2, 2);

            Assert.NotNull(point3D);
            Assert.NotNull(point2D);

            Assert.True(point3D.X == 3 && point3D.Y == 3 && point3D.Z == 3 && point3D.Dimension == 3);
            Assert.True(point2D.X == 2 && point2D.Y == 2 && point2D.Dimension == 2);
        }

        [Test]
        public void Position_Creation()
        {
            Point<int> point3D_1 = new Point<int>(3, 3, 3);
            Point<int> point3D_2 = new Point<int>(3, 3, 3);
            Point<int> point2D = new Point<int>(2, 2);

            Position<int> position3D = new Position<int>();
            Position<int> position2D = new Position<int>();

            position3D.Add(point3D_1);
            position3D.Add(point3D_2);
            position2D.Add(point2D);

            Assert.IsNotEmpty(position3D);
            Assert.IsNotEmpty(position2D);

            Assert.True(position3D[0].X == 3 && position3D[0].Y == 3 && position3D[0].Z == 3 && position3D[0].Dimension == 3);
            Assert.True(position3D[1].X == 3 && position3D[1].Y == 3 && position3D[1].Z == 3 && position3D[1].Dimension == 3);
            Assert.True(position2D[0].X == 2 && position2D[0].Y == 2 && position2D[0].Dimension == 2);

            Assert.Throws<ContainerRulesException>(() => position3D.Add(point2D));
        }

        [Test]
        public void Matrix_Creation()
        {
            Point<int> point3D = new Point<int>(3, 3, 3);
            Point<int> point2D = new Point<int>(2, 2);

            Position<int> position3D_1 = new Position<int>();
            Position<int> position3D_2 = new Position<int>();
            Position<int> position2D = new Position<int>();

            Matrix<int> matrix = new Matrix<int>();

            position3D_1.Add(point3D);
            position3D_1.Add(point3D);
            position3D_2.Add(point3D);

            matrix.Add(position3D_1);
            matrix.Add(position3D_2);

            Assert.IsNotEmpty(matrix);
            Assert.True(matrix.Dimension == position3D_1.Dimension);
            Assert.Throws<ContainerRulesException>(() => matrix.Add(position2D));
        }

        [Test]
        public void Container_Creation()
        {
            Point<int> point3D = new Point<int>(3, 3, 3);
            Point<int> point2D = new Point<int>(2, 2);

            Position<int> position3D = new Position<int>();
            Position<int> position2D = new Position<int>();

            Matrix<int> matrix_1 = new Matrix<int>();
            Matrix<int> matrix_2 = new Matrix<int>();

            Container<int> container = new Container<int>();

            position3D.Add(point3D);
            position3D.Add(point3D);
            position3D.Add(point3D);

            matrix_1.Add(position3D);
            matrix_1.Add(position3D);

            container.Add(matrix_1);
            container.Add(matrix_1);

            Assert.IsNotEmpty(container);
            Assert.True(container.MatrixSize == matrix_1.Count);
            //Assert.Throws<ContainerRulesException>(() => container.Add(matrix_2));
        }

        [Test]
        public void Cointeiners_Creating()
        {
            Point<int> point3D = new Point<int>(3, 3, 3);
            Point<int> point2D = new Point<int>(2, 2);

            Position<int> position3D = new Position<int>();
            Position<int> position2D = new Position<int>();

            Matrix<int> matrix_1 = new Matrix<int>();
            Matrix<int> matrix_2 = new Matrix<int>();

            Container<int> container_1 = new Container<int>();
            Container<int> container_2 = new Container<int>();

            Containers<int> containers = new Containers<int>();

            position3D.Add(point3D);
            position3D.Add(point3D);
            position3D.Add(point3D);

            matrix_1.Add(position3D);
            matrix_1.Add(position3D);

            container_1.Add(matrix_1);
            container_1.Add(matrix_1);
            container_2.Add(matrix_1);

            containers.Add(container_1);
            containers.Add(container_1);

            Assert.IsNotEmpty(containers);
            Assert.True(containers.ContainerSize == containers.Count);
            Assert.Throws<ContainerRulesException>(() => containers.Add(container_2));
        }

        [Test]
        public void CollectionRule() //The number of data points at each position for XY may vary between positions and matrices
        {
            Point<int> point2D = new Point<int>(2, 2);

            Position<int> position2D_1 = new Position<int>();
            Position<int> position2D_2 = new Position<int>();

            Matrix<int> matrix = new Matrix<int>();

            position2D_1.Add(point2D);
            position2D_1.Add(point2D);

            position2D_2.Add(point2D);

            matrix.Add(position2D_1);
            matrix.Add(position2D_2);

            Assert.True(matrix[0].Count != matrix[1].Count);
        }

        [Test]
        public void CollectionRule2() //The number of data points at each XYZ position will be the same for all positions on a matrix and across equivalent matrix indexes across containers.
        {
            Point<int> point3D = new Point<int>(3, 3, 3);
            Point<int> point2D = new Point<int>(2, 2);

            Position<int> position3D_1 = new Position<int>();
            Position<int> position3D_2 = new Position<int>();

            Position<int> position2D_1 = new Position<int>();
            Position<int> position2D_2 = new Position<int>();


            Matrix<int> matrix_1 = new Matrix<int>();
            Matrix<int> matrix_2 = new Matrix<int>();

            Container<int> container_1 = new Container<int>();
            Container<int> container_2 = new Container<int>();

            Containers<int> containers = new Containers<int>();

            position3D_1.Add(point3D);

            position3D_2.Add(point3D);
            position3D_2.Add(point3D);

            position2D_1.Add(point2D);
            position2D_1.Add(point2D);

            position2D_2.Add(point2D);

            matrix_1.Add(position3D_1);
            matrix_1.Add(position3D_2);

            matrix_2.Add(position2D_1);
            matrix_2.Add(position2D_2);

            container_1.Add(matrix_1);
            container_1.Add(matrix_2);
            container_1.Add(matrix_1);

            container_2.Add(matrix_1);
            container_2.Add(matrix_2);
            container_2.Add(matrix_1);


            containers.Add(container_1);
            containers.Add(container_2);
            containers.Add(container_1);           
        }
    }
}

