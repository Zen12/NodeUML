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
        private NodeContext context;

        public NodeController()
        {
            idHandler = new IdHandler();
            nodeRelation = new NodeRelation();
            context = new NodeContext(idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass, nodeRelation.OnDeleteField);
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
            var node = new Node(NodeConsts.NodeTransform, name, context);
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
                    listNodes[i].UpdateNodeDependesy(context);
                }
            }
            #if UML_NODE_DEBUB
            //fill with some data
            if (listNodes == null || listNodes.Count == 0)
            {
                Debug.Log("Add some data");
                var n1 = new Node(new Rect(10, 10, NodeConsts.NodeWith, NodeConsts.NodeHeight), "Win1", context);
                var n2 = new Node(new Rect(210, 210, NodeConsts.NodeWith, NodeConsts.NodeHeight), "Win2", context);
                var n3 = new Node(new Rect(410, 310, NodeConsts.NodeWith, NodeConsts.NodeHeight), "Win3", context);

                var f1 = new NodeInfoItem("1", n1, context);
                var f2 = new NodeInfoItem("2", n2, context);
                var f3 = new NodeInfoItem("3", n3, context);
                var f4 = new NodeInfoItem("4", n3, context);
                var f5 = new NodeInfoItem("5", n3, context);
                var f6 = new NodeInfoItem("6", n3, context);
                var f7 = new NodeInfoItem("7", n3, context);

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
            }
            #endif
            if (listNodes == null)
            {
                listNodes = new List<Node>();
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

        public void UpdateEvent()
        {
            for (int i = 0; i < listNodes.Count; i++)
            {
                listNodes[i].UpdateEvents();
            }
        }

    }
}
