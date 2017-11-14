using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    [System.Serializable]
    public class IdHandler
    {
        public ulong nodeInfoID = 1;
        public int snapShotID = 1;

        public int windowID = 0;

        private static IdHandler instance;

        private IdHandler()
        {
            
        }

        public static IdHandler GetInstance()
        {
            if (instance == null)
            {
                Load();
            }
            return instance;
        }

        public void OnSaveFile()
        {
            string json = JsonUtility.ToJson(this);
            SaveUtility.SaveInProject(json, NodeConsts.FullResourcesFolder + "/" + NodeConsts.ID_HANDLER);
        }

        private static void Load()
        {
            string json = string.Empty;
            var o = EditorGUIUtility.Load(NodeConsts.ResourcesFolder + "/" + NodeConsts.ID_HANDLER);
            if (o != null)
            {
                instance = new IdHandler();
                json = ((TextAsset)o).text;
                JsonUtility.FromJsonOverwrite(json, instance);
            }
            else
            {
                instance = new IdHandler();
            }
        }

        public int GetClassID()
        {
            return windowID++;
        }

        public ulong GetNodeInfoItemID()
        {
            return nodeInfoID++;
        }

        public int GetSnapShotID()
        {
            return snapShotID++;
        }

        public int GetUseCaseID()
        {
            return windowID++;
        }

        public int GetActorID()
        {
            return windowID++;
        }
    }
}
