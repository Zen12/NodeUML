using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    public class UseCaseHandler:IHandler
    {
        private EditorWindow win;
        public List<UseCase> listOfUseCase;
        public List<Actor> actors;

        public UseCaseHandler(EditorWindow win)
        {
            this.win = win;
            listOfUseCase = new List<UseCase>();
            actors = new List<Actor>();
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
                UseCase c = new UseCase();
                listOfUseCase.Add(c);

                Actor a = new Actor("Actor name");
                actors.Add(a);
            }
        }

        public void Update()
        {
            DrawElements();
            Buttons();
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
        }

        private void Buttons()
        {
            
        }
    }
}
