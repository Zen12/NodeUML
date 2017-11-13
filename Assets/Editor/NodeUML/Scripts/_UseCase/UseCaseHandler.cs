using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    public class UseCaseHandler:IHandler
    {
        private EditorWindow win;

        public UseCaseHandler(EditorWindow win)
        {
            this.win = win;
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
        }

        public void Update()
        {
            win.BeginWindows();
            Buttons();
            win.EndWindows();
        }

        private void Buttons()
        {
            
        }
    }
}
