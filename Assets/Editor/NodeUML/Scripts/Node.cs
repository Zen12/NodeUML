using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class Node
    {
        public int id;

        public string nodeName;
        [SerializeField]
        public Rect transform;

        [SerializeField]
        public List<NodeInfoItem> listProperty;
        [SerializeField]
        public List<NodeInfoItem> listMethods;

        [System.NonSerialized]
        private NodeContext context;

        public Node(Rect position, string name, NodeContext context)
        {
            nodeName = name;
            transform = position;
            this.context = context;
            this.id = context.idHandler.GetClassID();
            listProperty = new List<NodeInfoItem>();
            listMethods = new List<NodeInfoItem>();
            context.currentSnapeShot.AddClass(id);
        }

        public void UpdateNodeDependesy(NodeContext context)
        {
            this.context = context;
            for (int i = 0; i < listProperty.Count; i++)
            {
                listProperty[i].UpdateNode(this, context);
            }

            for (int i = 0; i < listMethods.Count; i++)
            {
                listMethods[i].UpdateNode(this, context);
            }
        }

        private NodeInfoItem GetFiledById(ulong id)
        {
            for (int i = 0; i < listProperty.Count; i++)
            {
                if (listProperty[i].ID == id)
                {
                    return listProperty[i];
                }
            }
            return null;
        }

        public void DrawNode()
        {
            transform.height = 160 + 22 * (listMethods.Count + listProperty.Count); 

            #if !UML_NODE_DEBUB
            transform = GUI.Window(id, transform, UpdateNode, "Class"); 
            #else
            transform = GUI.Window(id, transform, UpdateNode, "Class: " + id); 
            #endif
        }

        public void UpdateEvents()
        {
            if (transform.Contains(Event.current.mousePosition))
            {
                context.OnMakeRelationToClass(id);
            }
        }

        private void UpdateNode(int id)
        {
            GUILayout.BeginVertical(GUILayout.Width(transform.width - 15));
            nodeName = GUILayout.TextField(nodeName, GUILayout.Width(transform.width - 15));
            GUILayout.Label("Property", GUILayout.Width(transform.width - 15));
            for (int i = 0; i < listProperty.Count; i++)
            {
                listProperty[i].Draw();
            }

            if (GUILayout.Button("New Properties", GUILayout.Width(transform.width - 15)))
            {
                listProperty.Add(new NodeInfoItem("", this, context, false));
            }
            GUILayout.Space(10);
            GUILayout.Label("Methods", GUILayout.Width(transform.width - 15));
            for (int i = 0; i < listMethods.Count; i++)
            {
                listMethods[i].Draw();
            }

            if (GUILayout.Button("New Method", GUILayout.Width(transform.width - 15)))
            {
                listMethods.Add(new NodeInfoItem("", this, context, true));
            }

            if (GUILayout.Button("Delete Class", GUILayout.Width(transform.width - 15)))
            {
                DeleteClass();
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        public void DeleteNodeInfo(NodeInfoItem info)
        {
            listProperty.Remove(info);
            listMethods.Remove(info);
        }

        public void DeleteClass()
        {
            for (int i = 0; i < listProperty.Count; i++)
            {
                context.OnDeleteField(id, listProperty[i].ID);
            }

            for (int i = 0; i < listMethods.Count; i++)
            {
                context.OnDeleteField(id, listMethods[i].ID);
            }

            listProperty.Clear();
            listMethods.Clear();
            context.OnDeleteClass(id);
        }

        public void AddField(NodeInfoItem n)
        {
            listProperty.Add(n);
        }

        public void AddMethod(NodeInfoItem n)
        {
            listMethods.Add(n);
        }

    }
}


