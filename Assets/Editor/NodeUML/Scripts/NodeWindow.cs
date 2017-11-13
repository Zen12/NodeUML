using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    public class NodeUML : EditorWindow
    {

        NodeController controller;


        [MenuItem("Window/UML Node")]
        private static void ShowView()
        {
            NodeUML editor = EditorWindow.GetWindow<NodeUML>();
            editor.Init();
        }

        public void Init()
        {
            controller = new NodeController();
        }

        void OnGUI()
        {
            BeginWindows();
            UpdateGUI();
            UpdateInput();
            EndWindows();
        }

        private void UpdateGUI()
        {
            float devider = 5f;
            GUILayout.BeginArea(new Rect(position.width - position.width / devider, 0, position.width / devider, position.height));
            Buttons();
            GUILayout.EndArea();
            //Draw all the Nodes
            controller.DrawNodes();
        }

        private void UpdateInput()
        {
            if (Event.current.clickCount > 0)
            {
                //left click
                if (Event.current.button == 0)
                {
                    controller.UpdateEvent();  
                }
            }
        }

        void Buttons()
        {
            if (GUILayout.Button("Save Data", GUILayout.Height(40), GUILayout.Width(100)))
            {
                controller.SaveData();
                Debug.Log("Data saved");
            }

            if (GUILayout.Button("New Node", GUILayout.Height(40), GUILayout.Width(100)))
            {
                controller.CreateNewNode("ClassName");
            }
        }

    }
}
