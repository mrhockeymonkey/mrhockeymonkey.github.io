// using System.Collections;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
//
// namespace DesignPatterns.CompositePatterns
// {
//     // this is a variation of composite when we cannot use a base class 
//     // you can instead treat the leaf object as a collections itself
//     // this allows the use of an extension method to act as the uniform way to interact 
//     
//     // effectively instead of treating a composite object as if it were single
//     // we now treat a single in the same way to do a composite. clever.
//
//     public static class ExtensionMethods
//     {
//         public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
//         {
//             if (ReferenceEquals(self, other)) return;
//
//             foreach (var from in self)
//             foreach (var to in other)
//             {
//                 from.Out.Add(to);
//                 to.In.Add(from);
//             }
//         }
//     }
//
//     public class Neuron : IEnumerable<Neuron>
//     {
//         public float Value;
//         public List<Neuron> In, Out;
//
//         public IEnumerator<Neuron> GetEnumerator()
//         {
//             yield return this;
//         }
//
//         IEnumerator IEnumerable.GetEnumerator()
//         {
//             yield return this;
//         }
//     }
//
//     public class NeuronLayer : Collection<Neuron>
//     {
//
//     }
//
//     public class Demo
//     {
//         static void Main(string[] args)
//         {
//             var neuron1 = new Neuron();
//             var neuron2 = new Neuron();
//             var layer1 = new NeuronLayer();
//             var layer2 = new NeuronLayer();
//
//             neuron1.ConnectTo(neuron2);
//             neuron1.ConnectTo(layer1);
//             layer1.ConnectTo(layer2);
//         }
//     }
// }