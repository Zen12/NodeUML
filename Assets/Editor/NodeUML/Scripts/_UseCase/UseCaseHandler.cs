using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [System.Serializable]
    public class UseCaseHandler:IHandler
    {
        [System.NonSerialized]
        private EditorWindow win;
        [SerializeField]
        public List<UseCase> listOfUseCase;
        [SerializeField]
        public List<Actor> actors;
        [SerializeField]
        public UseCaseRelation useCaseRelation;
        [SerializeField]
        public List<SnapShotUseCase> snapShotUseCase;

        private SnapShotUseCase currentHandler;

        public UseCaseHandler(EditorWindow win)
        {
            this.win = win;
            listOfUseCase = new List<UseCase>();
            actors = new List<Actor>();
            useCaseRelation = new UseCaseRelation();
            snapShotUseCase = new List<SnapShotUseCase>();
            LoadData();
            if (snapShotUseCase.Count == 0)
            {
                SnapShotUseCase s = new SnapShotUseCase("DEFAULT");
                snapShotUseCase.Add(s);
            }
            currentHandler = snapShotUseCase[0];
        }

        private void LoadData()
        {
            string json = string.Empty;
            var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.SEQUENCE_DATA_FILE);
            if (o != null)
            {
                json = ((TextAsset)o).text;
                JsonUtility.FromJsonOverwrite(json, this);
                UpdateDeppendecy();
            }
            else
            {
                CreateUseCase("UseCase");
                CreateActor("Actor name");
            }
        }

        private void UpdateDeppendecy()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].UpdateDeppendecy(useCaseRelation.OnMakeStartRelation);
            }


            for (int i = 0; i < listOfUseCase.Count; i++)
            {
                listOfUseCase[i].UpdateDeppendecy(useCaseRelation.OnSelectUseCase);
            }

        }

        public void Save()
        {
            var json = JsonUtility.ToJson(this);
            SaveUtility.SaveInProject(json, NodeConsts.FullResourcesFolder + "/" + NodeConsts.USECASE_DATA_FILE);
            IdHandler.GetInstance().OnSaveFile();
            AssetDatabase.Refresh();
        }

        public void Update()
        {
            DrawElements();

            float devider = 5f;
            GUILayout.BeginArea(new Rect(win.position.width - win.position.width / devider, 0, win.position.width / devider, win.position.height));
            Buttons();
            GUILayout.EndArea();
        }

        private void DrawElements()
        {
            for (int i = 0; i < listOfUseCase.Count; i++)
            {
                listOfUseCase[i].Draw();
            }

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].Draw();
            }

            useCaseRelation.Draw(actors, listOfUseCase);
        }

        private void Buttons()
        {
            if (GUILayout.Button("Save Data", GUILayout.Height(40), GUILayout.Width(100)))
            {
                Save();
                Debug.Log("Data saved");
            }
            if (GUILayout.Button("Create actor", GUILayout.Height(40), GUILayout.Width(100)))
            {
                CreateActor("Actor Name");
            }

            if (GUILayout.Button("Create use-case", GUILayout.Height(40), GUILayout.Width(100)))
            {
                CreateUseCase("Use case name");
            }

            GUILayout.Label("List of SnapShots");

            for (int i = 0; i < snapShotUseCase.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("<-", GUILayout.Width(30)))
                {
                    Debug.Log("a");
                }

                snapShotUseCase[i].snapShotName = GUILayout.TextField(snapShotUseCase[i].snapShotName);

                if (GUILayout.Button("X"))
                {
                    Debug.Log("a");
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.Label("List of Actors");

            for (int i = 0; i < actors.Count; i++)
            {
                if (GUILayout.Button(actors[i].actorName))
                {
                    Debug.Log("a");
                }
            }

            GUILayout.Label("List of Actors");

            for (int i = 0; i < listOfUseCase.Count; i++)
            {
                if (GUILayout.Button(listOfUseCase[i].useCaseName))
                {
                    Debug.Log("a");
                }
            }

        }

        private void CreateActor(string name)
        {
            Actor a = new Actor(name, IdHandler.GetInstance(), useCaseRelation.OnMakeStartRelation);
            actors.Add(a);
        }

        private void CreateUseCase(string name)
        {
            UseCase c = new UseCase(IdHandler.GetInstance(), useCaseRelation.OnSelectUseCase);
            listOfUseCase.Add(c);
        }
    }
}
