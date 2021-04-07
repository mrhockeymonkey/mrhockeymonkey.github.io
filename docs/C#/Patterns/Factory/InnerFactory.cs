using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    /// <summary>
    /// When providing a factory it is likely you want to stop client code from instantiating objects directly
    /// If you mark the object ctor as private then a seperate factory cannot instantiate it...
    /// If you mark the object ctor as internal it is protected from client code but not from within the same project...
    /// The only other way for a factory to access a private ctor is to nest it within the class itself. 
    /// </summary>
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"Point({x}, {y})";

        public static class PointFactory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    
    class Program1
    {
        public static void Main()
        {
            var myPoint = Point.PointFactory.NewCartesianPoint(1, 2);
            Console.WriteLine(myPoint);
        }
    }
}
