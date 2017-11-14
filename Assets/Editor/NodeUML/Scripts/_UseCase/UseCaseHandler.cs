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

        public UseCaseHandler(EditorWindow win)
        {
            this.win = win;
            listOfUseCase = new List<UseCase>();
            actors = new List<Actor>();
            useCaseRelation = new UseCaseRelation();
            LoadData();
        }

        private void LoadData()
        {
            string json = string.Empty;
            var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.SEQUENCE_DATA_FILE);
            if (o != null)
            {
                json = ((TextAsset)o).text;
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
            {
                UseCase c = new UseCase(IdHandler.GetInstance(), useCaseRelation.OnSelectUseCase);
                listOfUseCase.Add(c);
                Actor a = new Actor("Actor name", IdHandler.GetInstance(), useCaseRelation.OnMakeStartRelation);
                actors.Add(a);
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
        }
    }
}
