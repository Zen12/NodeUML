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

        #if UML_NODE_DEBUB
        [SerializeField]
        public List<NodeInfoItem> listProperty;
        [SerializeField]
        public List<NodeInfoItem> listMethods;

        [SerializeField]
        #else
        [SerializeField]
        private List<NodeInfoItem> listProperty;
        [SerializeField]
        private List<NodeInfoItem> listMethods;
        [SerializeField]
        #endif
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
            #if !UML_NODE_DEBUB
            transform = GUI.Window(id, transform, UpdateNode, nodeName); 
            #else
            transform = GUI.Window(id, transform, UpdateNode, nodeName + " ID: " + id); 
            #endif
            UpdateEvents();
        }

        private void UpdateEvents()
        {
            if (Event.current.button == 0)
            {
                if (transform.Contains(Event.current.mousePosition))
                {
                    context.OnMakeRelationToClass(id);
                }
            }
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
                listProperty.Add(new NodeInfoItem("", this, context));
            }
            GUILayout.Space(10);
            for (int i = 0; i < listMethods.Count; i++)
            {
                listMethods[i].Draw();
            }

            if (GUILayout.Button("New Method", GUILayout.Width(transform.width - 15)))
            {
                listMethods.Add(new NodeInfoItem("", this, context));
            }
            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        public void DeleteNodeInfo(NodeInfoItem info)
        {
            listProperty.Remove(info);
            listMethods.Remove(info);
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


