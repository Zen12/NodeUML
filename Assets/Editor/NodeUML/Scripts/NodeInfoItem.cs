using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeInfoItem
{
    public string text;

    [System.NonSerialized]
    private Node node;

    public NodeInfoItem(string text, Node node)
    {
        this.text = text;
        this.node = node;
    }

    public void UpdateNode(Node node)
    {
        this.node = node;
    }

    public void Draw()
    {
        GUILayout.BeginHorizontal(GUILayout.Width(node.transform.width - 15));
        text = GUILayout.TextField(text, GUILayout.Width(node.transform.width - 25));
        if (GUILayout.Button("X", GUILayout.Width(15)))
        {
            node.DeleteNodeInfo(this);
        }
        GUILayout.EndHorizontal();
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
