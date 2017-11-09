using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodeController
{
    [SerializeField]
    private List<Node> listNodes;
    [System.NonSerialized]
    private NodeRelation nodeRelation;
    private Node currentSelected;

    public NodeController()
    {
        LoadData();
        nodeRelation = new NodeRelation();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        SaveUtility.SaveInProject(json, NodeConsts.FullResourcesFolder + "/" + NodeConsts.NodeDataFile);
        AssetDatabase.Refresh();
    }

    private void LoadData()
    {
        string json = string.Empty;
        var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.NodeDataFile);
        // var o = EditorGUIUtility.Load("NodeUML/Nodes.json");
        if (o != null)
        {
            json = ((TextAsset)o).text;
            JsonUtility.FromJsonOverwrite(json, this);
            for (int i = 0; i < listNodes.Count; i++)
            {
                listNodes[i].UpdateNodeDependesy();
            }
        }
        //fill with some data
        if (listNodes == null || listNodes.Count == 0)
        {
            var n1 = new Node(new Rect(10, 10, 100, 200), "Win1", 1);
            var n2 = new Node(new Rect(210, 210, 100, 200), "Win2", 2);
            var n3 = new Node(new Rect(410, 310, 100, 200), "Win3", 3);

            n1.AddRelationId(n2);
            n1.AddRelationId(n3);
            listNodes = new List<Node>()
            {
                n1, n2, n3
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
