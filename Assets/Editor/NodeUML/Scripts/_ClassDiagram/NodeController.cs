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
        public List<Node> listNodes;

        [SerializeField]
        private NodeRelation nodeRelation;
        [SerializeField]
        public List<SnapShot> snapShots;


        [System.NonSerialized]
        public NodeContext context;
        [System.NonSerialized]
        private IdHandler idHandler;

        public NodeController()
        {
            idHandler = IdHandler.GetInstance();
            nodeRelation = new NodeRelation();
            context = new NodeContext(idHandler, nodeRelation.OnMakeRelation, nodeRelation.OnClickOnClass, nodeRelation.OnDeleteField, OnDeleteClass);
            LoadData();
        }

        public void SetActiveSnapeShot(SnapShot s)
        {
            context.currentSnapeShot = s;
        }

        public void OnDeleteClass(int id)
        {
            nodeRelation.DeleteClass(id);
            listNodes.RemoveAll(((Node obj) => obj.id == id));
        }

        public SnapShot GetCurrentSnapShot()
        {
            return context.currentSnapeShot;
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(this);
            SaveUtility.SaveInProject(json, NodeConsts.FullResourcesFolder + "/" + NodeConsts.CLASS_DATA_FILE);
            idHandler.OnSaveFile();
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
            var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.CLASS_DATA_FILE);
            if (o != null)
            {
                json = ((TextAsset)o).text;
                JsonUtility.FromJsonOverwrite(json, this);
                for (int i = 0; i < listNodes.Count; i++)
                {
                    listNodes[i].UpdateNodeDependesy(context);
                }
            }

            if (listNodes == null)
            {
                listNodes = new List<Node>();
            }

            if (snapShots == null)
            {
                snapShots = new List<SnapShot>();
            }

            if (snapShots.Count == 0)
            {
                CreateNewSnapeShot("DEFAULT");
            }

            if (context.currentSnapeShot == null)
            {
                context.currentSnapeShot = snapShots[0];
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

        }

        public void CreateNewSnapeShot(string name)
        {
            snapShots.Add(new SnapShot(context, name));
        }

        public void DrawNodes()
        {
            nodeRelation.DrawRelation(listNodes, context);
            for (int i = 0; i < listNodes.Count; i++)
            {
                if (context.IsClassInCurrentContext(listNodes[i].id))
                {
                    listNodes[i].DrawNode();
                }
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
