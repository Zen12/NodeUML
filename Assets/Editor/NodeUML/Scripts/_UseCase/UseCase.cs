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
        private string useCaseName = "UseCaseName";
        private System.Action<int> OnFinishRelation;

        public UseCase(IdHandler idHander, System.Action<int> OnFinishRelation)
        {
            ID = idHander.GetUseCaseID();
            transform = new Rect(20, 20, 100, 120);
            this.OnFinishRelation = OnFinishRelation;
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
            useCaseName = GUILayout.TextArea(useCaseName, GUILayout.Width(transform.width - 15), GUILayout.Height(transform.height - 40));
            GUI.DragWindow();
        }
    }
}
