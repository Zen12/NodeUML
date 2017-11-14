using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [System.Serializable]
    public class UseCase
    {

        public int ID;
        private Rect transform;
        private string useCaseName = "UseCaseName";

        public UseCase()
        {
            ID = 21;
            transform = new Rect(20, 20, 100, 120);
        }

        public void Draw()
        {
            GUI.color = Color.white;
            transform = GUI.Window(ID, transform, UpdateDraw, "Use Case"); 
        }

        public void UpdateDraw(int id)
        {
            useCaseName = GUILayout.TextArea(useCaseName, GUILayout.Width(transform.width - 15), GUILayout.Height(transform.height - 40));
            GUI.DragWindow();
        }
    }
}
