﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [System.Serializable]
    public class NodeRelation
    {
        [SerializeField]
        private List<ClassRelation> classRelations;
        private RelationState relationState = new RelationState();


        public void OnMakeRelation(int idClass, ulong idField)
        {
            relationState.isMakingRelationState = true;
            relationState.selectedClassID = idClass;
            relationState.selectedFieldID = idField;
        }

        public void OnClickOnClass(int idClass)
        {
            if (relationState.isMakingRelationState)
            {
                if (idClass != relationState.selectedClassID)
                {
                    AddRelation(relationState.selectedClassID, relationState.selectedFieldID, idClass);
                    relationState.isMakingRelationState = false;
                }
            }

        }

        public void OnDeleteField(int idClass, ulong idFiled)
        {
            RemoveRelation(idClass, idFiled);
        }

        public void DeleteClass(int id)
        {
            classRelations.RemoveAll(((ClassRelation obj) => obj.idClass == id && obj.class1.classID == id));
        }

        public NodeRelation()
        {
            classRelations = new List<ClassRelation>();
        }

        public void DrawRelation(List<Node> nodes, NodeContext context)
        {
            if (nodes == null)
            {
                Debug.Log("Re-open Uml editor");
                return;
            }

            for (int i = 0; i < nodes.Count; i++)//n1
            {
                for (int j = 0; j < nodes.Count; j++)//n2
                {
                    for (int k = 0; k < classRelations.Count; k++)
                    {
                        for (int p = 0; p < nodes[i].listProperty.Count; p++)
                        {
                            if (context.IsClassInCurrentContext(nodes[i].id) && context.IsClassInCurrentContext(nodes[j].id))
                            {
                                if (classRelations[k].IsRelevantRelation(nodes[i].id, nodes[i].listProperty[p].ID, nodes[j].id))
                                {
                                
                                    DrawNodeCurve(nodes[i].transform, nodes[j].transform, p);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddRelation(int nodeId1, ulong fieldId1, int nodeId2)
        {
            var r = new ClassRelation(nodeId1, fieldId1, nodeId2);
            classRelations.Add(r);
        }

        private void RemoveRelation(int classID, ulong fieldID)
        {
            classRelations.RemoveAll((ClassRelation o) => o.class1.classID == classID && o.class1.fieldID == fieldID);
        }

        private void DrawNodeCurve(Rect start, Rect end, int indexP1)
        {
            float direction = -1f;
            float offcetStart = 0f;
            float offcetEnd = 1f;

            float heightP1 = 70f + (22f * indexP1);

            if (start.x < end.x)
            {
                direction = 1f;
                offcetStart = 1f;
                offcetEnd = 0f;
            }

            Vector3 startPos = new Vector3(start.x + start.width * offcetStart, start.y + heightP1, 0);
            Vector3 endPos = new Vector3(end.x + end.width * offcetEnd, end.y + 10f, 0);

            Vector3 startTan = startPos + Vector3.right * 50 * direction;
            Vector3 endTan = endPos + Vector3.left * 50 * direction;
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 2);
        }
    }

    [System.Serializable]
    public struct ClassRelation
    {
        public ClassData class1;
        public int idClass;

        public ClassRelation(int n1, ulong field1, int n2)
        {
            class1 = new ClassData(n1, field1);
            idClass = n2;
        }

        public bool IsRelevantRelation(int n1, ulong field1, int n2)
        {
            return (class1.IsRelevantClass(n1, field1) && idClass == n2);
        }
                       
    }

    [System.Serializable]
    public struct ClassData
    {
        public int classID;
        public ulong fieldID;

        public ClassData(int classId, ulong fieldId)
        {
            classID = classId;
            fieldID = fieldId;
        }

        public bool IsRelevantClass(int c, ulong f)
        {
            return c == classID && f == fieldID;
        }
    }

    internal class RelationState
    {
        public int selectedClassID;
        public ulong selectedFieldID;

        public bool isMakingRelationState = false;
        
    }
}
