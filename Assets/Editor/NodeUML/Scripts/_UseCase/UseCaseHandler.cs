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

        private SnapShotUseCase currentSnapShopt;

        public UseCaseHandler(EditorWindow win)
        {
            snapShotUseCase = new List<SnapShotUseCase>();
            SetSnapShot();
            listOfUseCase = new List<UseCase>();
            actors = new List<Actor>();
            useCaseRelation = new UseCaseRelation();

            this.win = win;
            LoadData();
        }

        private void SetSnapShot()
        {
            if (snapShotUseCase.Count == 0)
            {
                SnapShotUseCase s = new SnapShotUseCase("DEFAULT");
                snapShotUseCase.Add(s);
            }
            currentSnapShopt = snapShotUseCase[0];
        }

        private void LoadData()
        {
            string json = string.Empty;
            var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.USECASE_DATA_FILE);
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
            SetSnapShot();
        }

        private void UpdateDeppendecy()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].UpdateDeppendecy(useCaseRelation.OnMakeStartRelation, DeleteActor);
            }

            for (int i = 0; i < listOfUseCase.Count; i++)
            {
                listOfUseCase[i].UpdateDeppendecy(useCaseRelation.OnSelectUseCase, DeleteUseCase);
            }

        }

        public void DeleteActor(int id)
        {
            actors.RemoveAll((Actor obj) => obj.ID == id);
            useCaseRelation.OnDeleteActor(id);
        }

        public void DeleteUseCase(int id)
        {
            listOfUseCase.RemoveAll((UseCase obj) => obj.ID == id);
            useCaseRelation.OnDeleteUseCase(id);
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
                if (currentSnapShopt.useCaseID.Contains(listOfUseCase[i].ID))
                {
                    listOfUseCase[i].Draw();
                }
            }

            for (int i = 0; i < actors.Count; i++)
            {
                if (currentSnapShopt.actorID.Contains(actors[i].ID))
                {
                    actors[i].Draw();
                }
            }

            useCaseRelation.Draw(actors, listOfUseCase, currentSnapShopt);
        }

        private void Buttons()
        {
            if (GUILayout.Button("Save Data", GUILayout.Height(40), GUILayout.Width(100)))
            {
                Save();
                Debug.Log("Data saved");
            }




            GUILayout.Label("List of SnapShots");

            if (GUILayout.Button("Add new SnapShop"))
            {
                SnapShotUseCase s = new SnapShotUseCase("New snap shot");
                currentSnapShopt = s;
                snapShotUseCase.Add(s);
            }

            for (int i = 0; i < snapShotUseCase.Count; i++)
            {
                GUILayout.BeginHorizontal();

                if (currentSnapShopt == snapShotUseCase[i])
                {
                    GUI.color = Color.green;
                }

                if (GUILayout.Button("<-", GUILayout.Width(30)))
                {
                    currentSnapShopt = snapShotUseCase[i];
                }

                GUI.color = Color.white;

                snapShotUseCase[i].snapShotName = GUILayout.TextField(snapShotUseCase[i].snapShotName);

                if (GUILayout.Button("X"))
                {
                    snapShotUseCase.RemoveAll((SnapShotUseCase obj) => obj.ID == snapShotUseCase[i].ID);
                    if (snapShotUseCase.Count > 0)
                    {
                        currentSnapShopt = snapShotUseCase[0];
                    }
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.Label("List of Actors");

            if (GUILayout.Button("Create actor"))
            {
                CreateActor("Actor Name");
            }

            GUILayout.Space(10);

            for (int i = 0; i < actors.Count; i++)
            {
                if (currentSnapShopt.actorID.Contains(actors[i].ID))
                {
                    GUI.color = Color.green;
                }

                if (GUILayout.Button(actors[i].actorName))
                {
                    if (!currentSnapShopt.actorID.Contains(actors[i].ID))
                    {
                        currentSnapShopt.AddActor(actors[i].ID);
                    }
                    else
                    {
                        currentSnapShopt.RemoveActor(actors[i].ID);
                    }
                }
                GUI.color = Color.white;
            }

            GUILayout.Label("List of Actors");

            if (GUILayout.Button("Create use-case"))
            {
                CreateUseCase("Use case name");
            }

            GUILayout.Space(10);

            for (int i = 0; i < listOfUseCase.Count; i++)
            {
                if (currentSnapShopt.useCaseID.Contains(listOfUseCase[i].ID))
                {
                    GUI.color = Color.green;
                }
                if (GUILayout.Button(listOfUseCase[i].useCaseName))
                {
                    if (!currentSnapShopt.useCaseID.Contains(listOfUseCase[i].ID))
                    {
                        currentSnapShopt.AddUseCase(listOfUseCase[i].ID);
                    }
                    else
                    {
                        currentSnapShopt.RemoveUseCase(listOfUseCase[i].ID);
                    }
                }

                GUI.color = Color.white;
            }

        }

        private void CreateActor(string name)
        {
            Actor a = new Actor(name, IdHandler.GetInstance(), useCaseRelation.OnMakeStartRelation, DeleteActor);
            actors.Add(a);
            currentSnapShopt.AddActor(a.ID);
        }

        private void CreateUseCase(string name)
        {
            UseCase c = new UseCase(IdHandler.GetInstance(), useCaseRelation.OnSelectUseCase, DeleteUseCase);
            listOfUseCase.Add(c);
            currentSnapShopt.AddUseCase(c.ID);
        }
    }
}
