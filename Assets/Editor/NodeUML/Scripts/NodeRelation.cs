using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeRelation
{

    public void DrawRelation(List<Node> nodes)
    {
        if (nodes == null)
        {
            Debug.Log("Re-open Uml editor");
            return;
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < nodes[i].RelationsToID.Count; j++)
            {
                DrawRelation(nodes[i], nodes[i].RelationsToID[j], nodes);
            }
        }
    }

    public void DrawRelation(Node n1, Node n2)
    {
        DrawNodeCurve(n1.transform, n2.transform);
    }

    public void DrawRelation(Node n1, int n2, List<Node> nodes)
    {
        Node node2 = null;

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].id == n2)
            {
                node2 = nodes[i];
                break;
            }
        }
        if (node2 == null)
        {
            return;
        }
        DrawNodeCurve(n1.transform, node2.transform);
    }


    private void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 2);
    }
}
