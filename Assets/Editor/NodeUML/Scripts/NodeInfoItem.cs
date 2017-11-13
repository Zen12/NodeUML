using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class NodeInfoItem
    {
        public ulong ID;
        public string text;
        public bool isMethod;

        [System.NonSerialized]
        private Node node;

        [System.NonSerialized]
        private NodeContext context;

        public NodeInfoItem(string text, Node node, NodeContext context, bool isMethod)
        {
            this.text = text;
            this.node = node;
            this.context = context;
            this.isMethod = isMethod;
            ID = context.idHandler.GetNodeInfoItemID();
        }

        public void UpdateNode(Node node, NodeContext context)
        {
            this.node = node;
            this.context = context;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal(GUILayout.Width(node.transform.width - 15));

            if (!isMethod)
            {
                if (GUILayout.Button("<", GUILayout.Width(20)))
                {
                    context.OnMakeRelation(node.id, ID);
                }
                text = GUILayout.TextField(text, GUILayout.Width(node.transform.width - (15 + 20 + 13)));
                if (GUILayout.Button("X", GUILayout.Width(15)))
                {
                    node.DeleteNodeInfo(this);
                    context.OnDeleteField(node.id, ID);
                }
            }
            else
            {
                text = GUILayout.TextField(text, GUILayout.Width(node.transform.width - (25)));
                if (GUILayout.Button("X", GUILayout.Width(15)))
                {
                    node.DeleteNodeInfo(this);
                    context.OnDeleteField(node.id, ID);
                }
            }


            GUILayout.EndHorizontal();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
