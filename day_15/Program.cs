﻿using Dijkstra;

using static Dijkstra.Dijkstra;

var input  = File.ReadAllLines(@"C:\Users\mark.parker\source\repos\aoc-2021\day_15\input.txt");
var graph  = input.SelectMany((line, y) => line.Select((c, x) => new Node(x, y, int.Parse(c.ToString())))).ToDictionary(n => (n.X, n.Y));
var height = input.Length;

Console.WriteLine($"part 1: {RunDijkstra(graph)}");

graph = graph.Values.SelectMany(n => Scale(n, 5, height)).ToDictionary(n => (n.X, n.Y));

Console.WriteLine($"part 2: {RunDijkstra(graph)}");

static IEnumerable<Node> Scale(Node node, int factor, int width)
{
	for (var x = 0; x < factor; x++) {
		for (var y = 0; y < factor; y++) {
			var nrisk = node.Risk + x + y;

			while (nrisk > 9) {
				nrisk -= 9;
			}

			yield return new Node(node.Y + (width * y), node.X + (width * x), nrisk);
		}
	}
}