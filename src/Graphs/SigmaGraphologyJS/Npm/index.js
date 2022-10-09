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


import Graph from "graphology";
import Sigma from "sigma";
import ForceSupervisor from "graphology-layout-force/worker";

// colors
const RED = "#FA4F40";
const BLUE = "#727EE0";
const GREEN = "#5DB346";

// State for drag/drop
let draggedNode = null;
let isDragging = false;

let forceLayout = null;
let fixedNode = null;

let graphData = {
    "nodes": [
        {"key": "site-1", "label": "site-1", "color": RED},
        {"key": "cluster-1", "label": "cluster-1", "color": BLUE},
        {"key": "cluster-2", "label": "cluster-2", "color": BLUE},
        {"key": "ns-1", "label": "ns-1", "color": GREEN},
        {"key": "ns-2", "label": "ns-2", "color": GREEN},
        {"key": "ns-3", "label": "ns-3", "color": GREEN},
    ],
    "edges": [
        {"from": "site-1", "to": "cluster-1", "rel": "Location"},
        {"from": "site-1", "to": "cluster-2", "rel": "Location"},
        {"from": "assets/compute/kubernetes-clusters/cluster-1", "to": "ns-1", "rel": "Host"},
        {"from": "assets/compute/kubernetes-clusters/cluster-1", "to": "ns-2", "rel": "Host"},
        {"from": "assets/compute/kubernetes-clusters/cluster-2", "to": "ns-3", "rel": "Host"},
    ]
}

window.stopForceLayout = function() {
    forceLayout.stop();
}

window.makeGraph = function (props) {
    const container = document.getElementById("sigma-container");
    const graph = new Graph();

    function randomInRange(min, max) {
        return Math.random() * (max-min) + min;
    }
    let min = 0;
    let max = 5;
    graphData.nodes.forEach((n) => {
        console.log(`adding node: ${n.key}`);
        graph.addNode(n.key, {x: randomInRange(min, max), y: randomInRange(min, max), size: 10, color: n.color, label: n.label});
    })

    graphData.edges.forEach((e) => {
        console.log(`adding edge: ${e.from} --> ${e.to}`);
        graph.addEdge(e.from, e.to, {label: e.rel});
    })

    forceLayout = new ForceSupervisor(graph, {
        settings: {
            attraction: 0.0005, //0.0005,
            repulsion: 0.001, //0.1,
            gravity: 0.0001, //0.0001,
            inertia: 0.9,
            maxMove: 200
        },
        maxIterations: 1,
        // isNodeFixed: (_, attr) => attr.highlighted,
        isNodeFixed: (key, attr) => key == fixedNode,
    },);
    forceLayout.start();

    console.log(forceLayout);

    const renderer = new Sigma(graph, container, {
        renderEdgeLabels: false,
        renderLabels: true,

    });

    // On mouse down on a node
    //  - we enable the drag mode
    //  - save in the dragged node in the state
    //  - highlight the node
    //  - disable the camera so its state is not updated
    renderer.on("downNode", (e) => {
        // enable dragging on selected node
        isDragging = true;
        draggedNode = e.node;
        forceLayout.stop();
        graph.setNodeAttribute(draggedNode, "highlighted", true);

        // set the last touched node to be fixed so that it doesnt spring back away from where the user placed it
        fixedNode = e.node;
    });

    // On mouse move, if the drag mode is enabled, we change the position of the draggedNode
    renderer.getMouseCaptor().on("mousemovebody", (e) => {
        if (!isDragging || !draggedNode) return;

        // Get new position of node
        const pos = renderer.viewportToGraph(e);

        graph.setNodeAttribute(draggedNode, "x", pos.x);
        graph.setNodeAttribute(draggedNode, "y", pos.y);

        // Prevent sigma to move camera:
        e.preventSigmaDefault();
        e.original.preventDefault();
        e.original.stopPropagation();
    });

    // On mouse up, we reset the autoscale and the dragging mode
    renderer.getMouseCaptor().on("mouseup", () => {
        if (draggedNode) {
            graph.removeNodeAttribute(draggedNode, "highlighted");
        }
        isDragging = false;
        draggedNode = null;
        forceLayout.start();
    });

    // Disable the autoscale at the first down interaction
    renderer.getMouseCaptor().on("mousedown", () => {
        if (!renderer.getCustomBBox()) renderer.setCustomBBox(renderer.getBBox());
    });

    
}
