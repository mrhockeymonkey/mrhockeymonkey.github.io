// using Autofac;
// using static System.Console;
//
// namespace DesignPatterns.BridgePatterns
// {
//     /// <summary>
//     /// Bridge Pattern
//     /// 
//     /// 1: Use the Bridge pattern when you want to divide and organize a monolithic class that has several variants of some
//     /// functionality (for example, if the class can work with various database servers).
//     ///
//     /// 2: Use the pattern when you need to extend a class in several orthogonal (independent) dimensions.
//     ///
//     /// 3: Use the Bridge if you need to be able to switch implementations at runtime.
//     /// </summary>
//     public interface IRenderer
//     {
//         void RenderCircle(float radius);
//     }
//
//     public class VectorRenderer : IRenderer
//     {
//         public void RenderCircle(float radius)
//         {
//             WriteLine($"Drawing a circle of radius {radius}");
//         }
//     }
//
//     public class RasterRenderer : IRenderer
//     {
//         public void RenderCircle(float radius)
//         {
//             WriteLine($"Drawing pixels for circle of radius {radius}");
//         }
//     }
//
//     public abstract class Shape
//     {
//         protected IRenderer renderer;
//
//         // a bridge between the shape that's being drawn an
//         // the component which actually draws it
//         public Shape(IRenderer renderer)
//         {
//             this.renderer = renderer;
//         }
//
//         public abstract void Draw();
//         public abstract void Resize(float factor);
//     }
//
//     public class Circle : Shape
//     {
//         private float radius;
//
//         public Circle(IRenderer renderer, float radius) : base(renderer)
//         {
//             this.radius = radius;
//         }
//
//         public override void Draw()
//         {
//             renderer.RenderCircle(radius);
//         }
//
//         public override void Resize(float factor)
//         {
//             radius *= factor;
//         }
//     }
//
//     public class Demo
//     {
//         static void Main(string[] args)
//         {
//             var raster = new RasterRenderer();
//             var vector = new VectorRenderer();
//             
//             // client code dictates which renderer to use
//             var circle = new Circle(vector, 5, 5, 5);
//             circle.Draw();
//             circle.Resize(2);
//             circle.Draw();
//         }
//     }
// }