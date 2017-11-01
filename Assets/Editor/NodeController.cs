using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeController
{
	private List<Node> listNodes;
	private NodeRelation nodeRelation;
	private Node currentSelected;

	public NodeController ()
	{
		listNodes = new List<Node> () {
			new Node (new Rect (10, 10, 100, 200), "Win1", 1),
			new Node (new Rect (210, 210, 100, 200), "Win2", 2)
		};
		nodeRelation = new NodeRelation ();
	}


	public void DrawNodes ()
	{
		nodeRelation.DrawRelation (listNodes);
		for (int i = 0; i < listNodes.Count; i++) {
			listNodes [i].DrawNode ();
		}
	}

}
