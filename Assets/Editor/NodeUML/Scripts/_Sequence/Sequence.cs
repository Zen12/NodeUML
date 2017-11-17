using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [System.Serializable]
    public class Sequence
    {

        private int ID;
        private string sequanceName;
        private Sequence.SequanceType type;
        private Node classNode;
        private int useCaseID;

        public Sequence(int ID, string name, Sequence.SequanceType type)
        {
            this.ID = ID;
            sequanceName = name;
            this.type = type;
        }

        public enum SequanceType
        {
            actor,
            object_
        }

        public void Draw()
        {
            GUILayout.BeginVertical(GUILayout.Width(100));
            GUILayout.Space(30);
            DrawImage();
            GUILayout.BeginHorizontal();
            GUILayout.Space(35);
            GUILayout.Box(EditorGUIUtility.whiteTexture, GUILayout.Height(300), GUILayout.Width(10));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void DrawImage()
        {
            switch (type)
            {
                case SequanceType.actor:
                    GUILayout.Box(ResourcesConfig.GetInstance().actor, GUILayout.Height(70), GUILayout.Width(70));
                    break;
                case SequanceType.object_:
                    GUILayout.Box("Class", GUILayout.Height(70), GUILayout.Width(70));
                    break;
            }
        }
    }


}
