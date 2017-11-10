using System.Collections;
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

        public NodeRelation()
        {
            classRelations = new List<ClassRelation>();
        }

        public void DrawRelation(List<Node> nodes)
        {
            if (nodes == null)
            {
                Debug.Log("Re-open Uml editor");
                return;
            }

            for (int i = 0; i < nodes.Count; i++)//n1
            {
                for (int j = i; j < nodes.Count; j++)//n2
                {
                    for (int k = 0; k < classRelations.Count; k++)
                    {
                        for (int p1 = 0; p1 < nodes[i].listProperty.Count; p1++)
                        {
                            for (int p2 = 0; p2 < nodes[j].listProperty.Count; p2++)
                            {
                                if (classRelations[k].IsRelation(nodes[i].id, nodes[i].listProperty[p1].ID, nodes[j].id, nodes[j].listProperty[p2].ID))
                                {
                                    DrawNodeCurve(nodes[i].transform, nodes[j].transform, p1, p2);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddRelation(int nodeId1, ulong fieldId1, int nodeId2, ulong fieldId2)
        {
            var r = new ClassRelation(nodeId1, fieldId1, nodeId2, fieldId2);
            classRelations.Add(r);
        }

        private void DrawNodeCurve(Rect start, Rect end, int indexP1, int indexP2)
        {
            float direction = -1f;
            float offcetStart = 0f;
            float offcetEnd = 1f;

            float heightP1 = 30f + (23f * indexP1);
            float heightP2 = 30f + (23f * indexP2);

            if (start.x < end.x)
            {
                direction = 1f;
                offcetStart = 1f;
                offcetEnd = 0f;
            }

            Vector3 startPos = new Vector3(start.x + start.width * offcetStart, start.y + heightP1, 0);
            Vector3 endPos = new Vector3(end.x + end.width * offcetEnd, end.y + heightP2, 0);

            Vector3 startTan = startPos + Vector3.right * 50 * direction;
            Vector3 endTan = endPos + Vector3.left * 50 * direction;
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 2);
        }
    }

    [System.Serializable]
    public struct ClassRelation
    {
        public ClassData class1;
        public ClassData class2;

        public ClassRelation(int n1, ulong field1, int n2, ulong field2)
        {
            class1 = new ClassData(n1, field1);
            class2 = new ClassData(n2, field2);
        }

        public bool IsRelation(int n1, ulong field1, int n2, ulong field2)
        {
            return (class1.IsRelevantClass(n1, field1) && class2.IsRelevantClass(n2, field2))
            || (class2.IsRelevantClass(n1, field1) && class1.IsRelevantClass(n2, field2));
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
}
