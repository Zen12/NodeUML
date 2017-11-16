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
        private System.Action<int> OnDeleteActor;

        public Actor(string actorName, IdHandler idhadler, System.Action<int> OnMakeRelation, System.Action<int> OnDeleteActor)
        {
            this.actorName = actorName;
            ID = idhadler.GetActorID();
            transform = new Rect(20, 20, 120, 140);
            img = ResourcesConfig.GetInstance().actor;
            this.OnMakeRelation = OnMakeRelation;
            this.OnDeleteActor = OnDeleteActor;
        }

        public void UpdateDeppendecy(System.Action<int> OnMakeRelation, System.Action<int> OnDeleteActor)
        {
            this.OnMakeRelation = OnMakeRelation;
            this.OnDeleteActor = OnDeleteActor;
            img = ResourcesConfig.GetInstance().actor;
        }

        public void Draw()
        {
            GUI.color = Color.white;
            transform = GUI.Window(ID, transform, UpdateDraw, "Actor"); 
        }

        public void UpdateDraw(int id)
        {
            GUILayout.Box(img, GUILayout.Width(transform.width - 30), GUILayout.Height(transform.height - 70));
            GUILayout.BeginHorizontal();
            actorName = GUILayout.TextField(actorName, GUILayout.Width(transform.width - 40));
            if (GUILayout.Button(">"))
            {
                OnMakeRelation(ID);
            }
            GUILayout.EndVertical();
            if (GUILayout.Button("Delete"))
            {
                OnDeleteActor(ID);
            }
            GUI.DragWindow();
        }
    }
}
