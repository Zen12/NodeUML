using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeRelation
{

	public void DrawRelation (List<Node> nodes)
	{
		for (int i = 1; i < nodes.Count; i += 2) {
			DrawRelation (nodes [i - 1], nodes [i]);
		}
	}

	public void DrawRelation (Node n1, Node n2)
	{
		DrawNodeCurve (n1.Transform, n2.Transform);
	}

	private void DrawNodeCurve (Rect start, Rect end)
	{
		Vector3 startPos = new Vector3 (start.x + start.width, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3 (end.x, end.y + end.height / 2, 0);
		Vector3 startTan = startPos + Vector3.right * 50;
		Vector3 endTan = endPos + Vector3.left * 50;
		Handles.DrawBezier (startPos, endPos, startTan, endTan, Color.black, null, 2);
	}
}
