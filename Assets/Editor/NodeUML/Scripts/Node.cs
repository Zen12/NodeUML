using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Rect Transform { private set; get; }

    public string NodeName { private set; get; }

    public int id;

    [SerializeField]
    private List<NodeInfoItem> listProperty;
    [SerializeField]
    private List<NodeInfoItem> listMethods;

    public List<Node> RelationsToID;

    public Node(Rect position, string name, int id)
    {
        NodeName = name;
        Transform = position;
        this.id = id;
        listProperty = new List<NodeInfoItem>();
        listMethods = new List<NodeInfoItem>();
        RelationsToID = new List<Node>();
    }

    public void AddRelationId(Node node)
    {
        RelationsToID.Add(node);
    }

    public void DrawNode()
    {
        Transform = GUI.Window(id, Transform, UpdateNode, NodeName); 
    }

    private void UpdateNode(int id)
    {
        GUILayout.BeginVertical(GUILayout.Width(Transform.width - 15));
        for (int i = 0; i < listProperty.Count; i++)
        {
            listProperty[i].Draw();
        }

        if (GUILayout.Button("New Property", GUILayout.Width(Transform.width - 15)))
        {
            listProperty.Add(new NodeInfoItem("", this));
        }
        GUILayout.Space(10);
        for (int i = 0; i < listMethods.Count; i++)
        {
            listMethods[i].Draw();
        }

        if (GUILayout.Button("New Method", GUILayout.Width(Transform.width - 15)))
        {
            listMethods.Add(new NodeInfoItem("", this));
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }

    public void DeleteNodeInfo(NodeInfoItem info)
    {
        listProperty.Remove(info);
        listMethods.Remove(info);
    }
}


