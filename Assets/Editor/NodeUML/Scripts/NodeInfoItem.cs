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

        private event System.Action<int, ulong> OnMakeRealation;

        public NodeInfoItem(string text, Node node, System.Action<int, ulong> onMakeRelation)
        {
            this.text = text;
            this.node = node;
            ID = node.idHandler.GetNodeInfoItemID();
            OnMakeRealation += onMakeRelation;

        }

        public void UpdateNode(Node node, System.Action<int, ulong> onMakeRelation)
        {
            this.node = node;
            OnMakeRealation += onMakeRelation;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal(GUILayout.Width(node.transform.width - 15));

            if (GUILayout.Button("<", GUILayout.Width(20)))
            {
                OnMakeRealation(node.id, ID);
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
