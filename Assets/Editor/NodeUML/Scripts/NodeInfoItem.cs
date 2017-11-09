using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeInfoItem
{

    public string Text{ private set; get; }

    [System.NonSerialized]
    private Node node;

    public NodeInfoItem(string text, Node node)
    {
        Text = text;
        this.node = node;
    }

    public NodeInfoItem(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public void Draw()
    {
        GUILayout.BeginHorizontal(GUILayout.Width(node.transform.width - 15));
        Text = GUILayout.TextField(Text, GUILayout.Width(node.transform.width - 25));
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
