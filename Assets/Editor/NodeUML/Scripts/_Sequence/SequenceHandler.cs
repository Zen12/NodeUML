using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace NodeUML
{
    [System.Serializable]
    public class SequenceHandler:IHandler
    {
        [System.NonSerialized]
        private EditorWindow win;
        [System.NonSerialized]
        private List<Node> classList;
        [System.NonSerialized]
        private List<Actor> actors;
        [System.NonSerialized]
        private List<UseCase> useCases;
        [System.Serializable]
        private List<Sequence> sequances;

        private IdHandler idHandler;

        public SequenceHandler(EditorWindow win, IdHandler idHandler, List<Node> classList, List<Actor> actors, List<UseCase> useCases)
        {
            this.win = win;
            this.idHandler = idHandler;
            UpdateDependency(classList, actors, useCases);

            //temp:
            sequances = new List<Sequence>();
            Sequence s = new Sequence();
            sequances.Add(s);
        }

        public void UpdateDependency(List<Node> classList, List<Actor> actors, List<UseCase> useCases)
        {
            this.classList = classList;
            this.actors = actors;
            this.useCases = useCases;
        }

        public void Update()
        {
            GUILayout.BeginArea(new Rect(0, win.position.height - win.position.height / 4, win.position.width, win.position.height / 4));
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            DrawUseCases();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            DrawActors();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            DrawClasses();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            DrawTools();
            GUILayout.EndVertical();
            
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void DrawTools()
        {
            GUILayout.Label("Tools: ");
            if (GUILayout.Button("LOOP"))
            {

            }

            if (GUILayout.Button("ALT"))
            {

            }
        }

        private void DrawUseCases()
        {
            GUILayout.Label("UseCases: ");
            for (int i = 0; i < useCases.Count; i++)
            {
                if (GUILayout.Button(useCases[i].useCaseName))
                {
                    
                }
            }
        }

        private void DrawActors()
        {
            GUILayout.Label("Actors: ");
            for (int i = 0; i < useCases.Count; i++)
            {
                if (GUILayout.Button(useCases[i].useCaseName))
                {
                    
                }
            }
        }

        private void DrawClasses()
        {
            GUILayout.Label("Classes: ");
            for (int i = 0; i < classList.Count; i++)
            {
                if (GUILayout.Button(classList[i].nodeName))
                {
                    
                }
            }
        }
    }
}
