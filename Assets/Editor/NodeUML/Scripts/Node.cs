using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int id;
    public string nodeName;
    [SerializeField]
    public Rect transform;
    [SerializeField]
    private List<NodeInfoItem> listProperty;
    [SerializeField]
    private List<NodeInfoItem> listMethods;

    public List<int> RelationsToID;

    public Node(Rect position, string name, int id)
    {
        nodeName = name;
        transform = position;
        this.id = id;
        listProperty = new List<NodeInfoItem>();
        listMethods = new List<NodeInfoItem>();
        RelationsToID = new List<int>();
    }

    public void UpdateNodeDependesy()
    {
        for (int i = 0; i < listProperty.Count; i++)
        {
            listProperty[i].UpdateNode(this);
        }

        for (int i = 0; i < listMethods.Count; i++)
        {
            listMethods[i].UpdateNode(this);
        }
    }

    public void AddRelationId(Node node)
    {
        RelationsToID.Add(node.id);
    }

    public void DrawNode()
    {
        transform = GUI.Window(id, transform, UpdateNode, nodeName); 
    }

    private void UpdateNode(int id)
    {
        GUILayout.BeginVertical(GUILayout.Width(transform.width - 15));
        for (int i = 0; i < listProperty.Count; i++)
        {
            listProperty[i].Draw();
        }

        if (GUILayout.Button("New Property", GUILayout.Width(transform.width - 15)))
        {
            listProperty.Add(new NodeInfoItem("", this));
        }
        GUILayout.Space(10);
        for (int i = 0; i < listMethods.Count; i++)
        {
            listMethods[i].Draw();
        }

        if (GUILayout.Button("New Method", GUILayout.Width(transform.width - 15)))
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


