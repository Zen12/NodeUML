using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    public class ClassHandler:IHandler
    {
        private EditorWindow win;
        private NodeController controller;

        private Vector2 snapScroll;
        private Vector2 classScroll;

        public ClassHandler(EditorWindow win, NodeController controller)
        {
            this.controller = controller;
            this.win = win;
        }

        public void Update()
        {
            UpdateGUI();
            UpdateInput();
        }

        private void UpdateGUI()
        {
            float devider = 5f;
            GUILayout.BeginArea(new Rect(win.position.width - win.position.width / devider, 0, win.position.width / devider, win.position.height));
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

            GUILayout.Label("Snap shots:");

            if (GUILayout.Button("Create new SnapShot"))
            {
                controller.CreateNewSnapeShot("New SnapShot");
            }
            GUILayout.Space(10);

            snapScroll = GUILayout.BeginScrollView(snapScroll, GUILayout.Height(100));

            SnapShot removeS = null;

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

                controller.snapShots[i].snapShotName = GUILayout.TextField(controller.snapShots[i].snapShotName);

                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    removeS = controller.snapShots[i];
                }

                GUILayout.EndHorizontal();
            }

            controller.snapShots.Remove(removeS);

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
                    if (!controller.context.IsClassInCurrentContext(controller.listNodes[i].id))
                    {
                        controller.GetCurrentSnapShot().AddClass(controller.listNodes[i].id);
                    }
                    else
                    {
                        controller.GetCurrentSnapShot().RemoveClass(controller.listNodes[i].id);
                    }
                }

                GUI.color = Color.white;
            }

            GUILayout.EndScrollView();
        }

    }
}
