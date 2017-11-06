using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeController
{
	private List<Node> listNodes;
	private NodeRelation nodeRelation;
	private Node currentSelected;

	public NodeController()
	{
		LoadData();
		nodeRelation = new NodeRelation();
	}

	private void SaveData()
	{
        

	}

	private void LoadData()
	{
		string json = string.Empty;
		var o = EditorGUIUtility.Load("NodeUML/Nodes.json");
		if (o != null)
		{
			json = ((TextAsset)o).text;
		}
		else
		{
			var n1 = new Node(new Rect(10, 10, 100, 200), "Win1", 1);
			var n2 = new Node(new Rect(210, 210, 100, 200), "Win2", 2);
			n1.AddRelationId(n2);
			listNodes = new List<Node>()
			{
				n1, n2
			};
		}
	}

	public void DrawNodes()
	{
		nodeRelation.DrawRelation(listNodes);
		for (int i = 0; i < listNodes.Count; i++)
		{
			listNodes[i].DrawNode();
		}
	}

}
