using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeUML
{
    [System.Serializable]
    public class Actor
    {
        public int ID;
        public string actorName;
        public List<int> useCasesID;
        [SerializeField]
        public Rect transform;
        private Texture img;
        private System.Action<int> OnMakeRelation;

        public Actor(string actorName, IdHandler idhadler, System.Action<int> OnMakeRelation)
        {
            this.actorName = actorName;
            ID = idhadler.GetActorID();
            transform = new Rect(20, 20, 100, 120);
            img = ResourcesConfig.GetInstance().actor;
            this.OnMakeRelation = OnMakeRelation;
        }

        public void Draw()
        {
            GUI.color = Color.white;
            transform = GUI.Window(ID, transform, UpdateDraw, "Actor"); 
        }

        public void UpdateDraw(int id)
        {
            if (img == null)
            {
                img = ResourcesConfig.GetInstance().actor;
            }
            GUILayout.BeginHorizontal();
            GUILayout.Box(img, GUILayout.Width(transform.width - 30), GUILayout.Height(transform.height - 50));
            if (GUILayout.Button(">"))
            {
                OnMakeRelation(ID);
            }
            GUILayout.EndVertical();
            actorName = GUILayout.TextField(actorName, GUILayout.Width(transform.width - 15));
            GUI.DragWindow();
        }
    }
}
