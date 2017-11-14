using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    public class Actor
    {
        public int ID;
        public string actorName;
        public List<int> useCasesID;
        private Rect transform;
        private Texture img;

        public Actor(string actorName)
        {
            this.actorName = actorName;
            ID = 20;
            transform = new Rect(20, 20, 100, 120);
            img = ResourcesConfig.GetInstance().actor;
        }

        public void Draw()
        {
            GUI.color = Color.white;
            transform = GUI.Window(ID, transform, UpdateDraw, "Actor"); 
        }

        public void UpdateDraw(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box(img, GUILayout.Width(transform.width - 30), GUILayout.Height(transform.height - 50));
            if (GUILayout.Button(">"))
            {

            }
            GUILayout.EndVertical();
            actorName = GUILayout.TextField(actorName, GUILayout.Width(transform.width - 15));
            GUI.DragWindow();
        }
    }
}
