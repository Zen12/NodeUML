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
        public IdHandler idHandler;

        private System.Action<int, ulong> OnMakeRelation;

        private event System.Action<int> OnMakeRelationToClass;

        public Node(Rect position, string name, IdHandler idHandler, System.Action<int, ulong> onMakeRelation, System.Action<int> onMakeRelationToClass)
        {
            nodeName = name;
            transform = position;
            OnMakeRelation = onMakeRelation;
            OnMakeRelationToClass += onMakeRelationToClass;

            listProperty = new List<NodeInfoItem>();
            listMethods = new List<NodeInfoItem>();
            this.idHandler = idHandler;
            this.id = idHandler.GetClassID();
        }

        public void UpdateNodeDependesy(IdHandler idHandler, System.Action<int, ulong> onMakeRelation, System.Action<int> onMakeRelationToClass)
        {
            OnMakeRelationToClass += onMakeRelationToClass;
            this.OnMakeRelation = onMakeRelation;
            this.idHandler = idHandler;
            for (int i = 0; i < listProperty.Count; i++)
            {
                listProperty[i].UpdateNode(this, onMakeRelation);
            }

            for (int i = 0; i < listMethods.Count; i++)
            {
                listMethods[i].UpdateNode(this, onMakeRelation);
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
//            if (Event.current.type == EventType.MouseDown && PointIsWithinRect(Event.current.MousePosition, myDraggableObject.rect))
//            {
//                currentlyDragged = myDraggableObject;
//            }
//            else if (Event.current.type == EventType.MouseDrag && currentlyDragged != null)
//            {
//                currentlyDragged.rect = new Rect(currentlyDragged.rect.x + Event.current.mousePosition.x, currentlyDragged.rect.y + Event.current.mousePosition.y, currentlyDragged.rect.width, currentlyDragged.rect.height);
//            }
//            else if (Event.current.type == EventType.MouseUp)
//            {
//                currentlyDragged = null;
//            }

            if (Event.current.button == 0)
            {
                if (transform.Contains(Event.current.mousePosition))
                {
                    OnMakeRelationToClass(id);
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
                listProperty.Add(new NodeInfoItem("", this, OnMakeRelation));
            }
            GUILayout.Space(10);
            for (int i = 0; i < listMethods.Count; i++)
            {
                listMethods[i].Draw();
            }

            if (GUILayout.Button("New Method", GUILayout.Width(transform.width - 15)))
            {
                listMethods.Add(new NodeInfoItem("", this, OnMakeRelation));
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


