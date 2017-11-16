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
        [SerializeField]
        public Rect transform;
        [SerializeField]
        public string useCaseName = "UseCaseName";
        private System.Action<int> OnFinishRelation;
        private System.Action<int> OnDeleteUseCase;

        public UseCase(IdHandler idHander, System.Action<int> OnFinishRelation, System.Action<int> OnDeleteUseCase)
        {
            ID = idHander.GetUseCaseID();
            transform = new Rect(20, 20, 150, 80);
            this.OnFinishRelation = OnFinishRelation;
            this.OnDeleteUseCase = OnDeleteUseCase;
        }

        public void UpdateDeppendecy(System.Action<int> OnFinishRelation, System.Action<int> OnDeleteUseCase)
        {
            this.OnFinishRelation = OnFinishRelation;
            this.OnDeleteUseCase = OnDeleteUseCase;
        }

        public void Draw()
        {
            GUI.color = Color.white;
            transform = GUI.Window(ID, transform, UpdateDraw, "Use Case"); 
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            if (transform.Contains(Event.current.mousePosition))
            {
                OnFinishRelation(ID);
            }
        }

        public void UpdateDraw(int id)
        {
            useCaseName = GUILayout.TextArea(useCaseName, GUILayout.Width(transform.width - 15), GUILayout.Height(transform.height - 50));
            if (GUILayout.Button("Delete"))
            {
                OnDeleteUseCase(ID);
            }
            GUI.DragWindow();
        }
    }
}
