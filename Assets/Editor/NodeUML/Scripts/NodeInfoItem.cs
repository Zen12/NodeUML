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

        [System.NonSerialized]
        private Node node;

        public NodeInfoItem(string text, Node node)
        {
            this.text = text;
            this.node = node;
            ID = node.idHandler.GetNodeInfoItemID();
        }

        public void UpdateNode(Node node)
        {
            this.node = node;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal(GUILayout.Width(node.transform.width - 15));

            if (GUILayout.Button("<", GUILayout.Width(20)))
            {
                Debug.Log("Relation");
            }

            text = GUILayout.TextField(text, GUILayout.Width(node.transform.width - (15 + 20 + 13)));
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
}
