import Graph from "graphology";
import Sigma from "sigma";
import ForceSupervisor from "graphology-layout-force/worker";

window.createGraph = function () {
    const container = document.getElementById("graph-container");
    const graph = new Graph();
    
    graph.addNode("n1", { x: 0, y: 0, size: 10 });
    graph.addNode("n2", { x: -5, y: 5, size: 10 });
    graph.addNode("n3", { x: 5, y: 5, size: 10 });
    graph.addNode("n4", { x: 0, y: 10, size: 10 });
    graph.addEdge("n1", "n2");
    graph.addEdge("n2", "n4");
    graph.addEdge("n4", "n3");
    graph.addEdge("n3", "n1");

// Create the spring layout and start it
    const layout = new ForceSupervisor(graph, { isNodeFixed: (_, attr) => attr.highlighted });
    layout.start();

// Create the sigma
    const renderer = new Sigma(graph, container);
}
