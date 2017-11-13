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

        private Vector2 snapScroll;
        private Vector2 classScroll;

        void Buttons()
        {
            if (GUILayout.Button("Save Data", GUILayout.Height(40), GUILayout.Width(100)))
            {
                controller.SaveData();
                Debug.Log("Data saved");
            }
                
            GUILayout.Label("Snap shots:");

            if (GUILayout.Button("Create new SnapShot"))
            {
                controller.CreateNewSnapeShot("New SnapShot");
            }
            GUILayout.Space(10);

            snapScroll = GUILayout.BeginScrollView(snapScroll, GUILayout.Height(100));

            for (int i = 0; i < controller.snapShots.Count; i++)
            {
                GUILayout.BeginHorizontal();

                var c = controller.GetCurrentSnapShot();
                if (c == controller.snapShots[i])
                {
                    GUI.backgroundColor = Color.green;
                }

                if (GUILayout.Button("<-", GUILayout.Width(30)))
                {
                    controller.SetActiveSnapeShot(controller.snapShots[i]);
                }
                GUI.backgroundColor = Color.white;

                GUILayout.TextField(controller.snapShots[i].snapShotName);

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            GUILayout.Label("Classes: ");

            if (GUILayout.Button("Create new Class"))
            {
                controller.CreateNewNode("ClassName");
            }

            GUILayout.Space(10);

            classScroll = GUILayout.BeginScrollView(classScroll);

            for (int i = 0; i < controller.listNodes.Count; i++)
            {
                if (controller.context.IsClassInCurrentContext(controller.listNodes[i].id))
                {
                    GUI.color = Color.green;
                }
                
                if (GUILayout.Button(controller.listNodes[i].nodeName))
                {
                    controller.GetCurrentSnapShot().AddClass(controller.listNodes[i].id);
                }

                GUI.color = Color.white;
            }

            GUILayout.EndScrollView();
        }

    }
}
