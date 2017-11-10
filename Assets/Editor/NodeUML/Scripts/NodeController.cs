using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [System.Serializable]
    public class NodeController
    {
        [SerializeField]
        private List<Node> listNodes;
        [SerializeField]
        private IdHandler idHandler;
        [SerializeField]
        private NodeRelation nodeRelation;
        [System.NonSerialized]
        private Node currentSelected;

        public NodeController()
        {
            idHandler = new IdHandler();
            nodeRelation = new NodeRelation();
            LoadData();
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(this);
            SaveUtility.SaveInProject(json, NodeConsts.FullResourcesFolder + "/" + NodeConsts.NodeDataFile);
            AssetDatabase.Refresh();
        }

        public void CreateNewNode(string name)
        {
            var node = new Node(NodeConsts.NodeTransform, name, idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass);
            listNodes.Add(node);
        }

        private void LoadData()
        {
            string json = string.Empty;
            var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.NodeDataFile);
            if (o != null)
            {
                json = ((TextAsset)o).text;
                JsonUtility.FromJsonOverwrite(json, this);
                for (int i = 0; i < listNodes.Count; i++)
                {
                    listNodes[i].UpdateNodeDependesy(idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass);
                }
            }
            #if UML_NODE_DEBUB
            //fill with some data
            if (listNodes == null || listNodes.Count == 0)
            {
                Debug.Log("Add some data");
                var n1 = new Node(new Rect(10, 10, NodeConsts.NodeWith, NodeConsts.NodeHeight), "Win1", idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass);
                var n2 = new Node(new Rect(210, 210, NodeConsts.NodeWith, NodeConsts.NodeHeight), "Win2", idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass);
                var n3 = new Node(new Rect(410, 310, NodeConsts.NodeWith, NodeConsts.NodeHeight), "Win3", idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass);

                var f1 = new NodeInfoItem("1", n1, nodeRelation.OnMakeRelation);
                var f2 = new NodeInfoItem("2", n2, nodeRelation.OnMakeRelation);
                var f3 = new NodeInfoItem("3", n3, nodeRelation.OnMakeRelation);
                var f4 = new NodeInfoItem("4", n3, nodeRelation.OnMakeRelation);
                var f5 = new NodeInfoItem("5", n3, nodeRelation.OnMakeRelation);
                var f6 = new NodeInfoItem("6", n3, nodeRelation.OnMakeRelation);
                var f7 = new NodeInfoItem("7", n3, nodeRelation.OnMakeRelation);

                n1.AddField(f1);
                n2.AddField(f2);
                n3.AddField(f3);
                n3.AddField(f4);
                n3.AddField(f5);
                n3.AddField(f6);
                n3.AddField(f7);

                nodeRelation.AddRelation(n1.id, f1.ID, n2.id);
                nodeRelation.AddRelation(n3.id, f7.ID, n1.id);

                listNodes = new List<Node>()
                {
                    n1, n2, n3
                };
//
//                nodeRelation.AddRelation(n1.id, n1.listProperty[0].ID, n2.id, n2.listProperty[0].ID);
//                nodeRelation.AddRelation(n1.id, n1.listProperty[0].ID, n3.id, n3.listProperty[0].ID);

            }
            #endif
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
}
