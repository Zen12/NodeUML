using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeUML
{
    public class NodeUML : EditorWindow
    {
        private NodeController controller;

        private ClassHandler classHandler;

        [SerializeField]
        private UseCaseHandler useCaseHandler;

        [MenuItem("Window/UML Node")]
        private static void ShowView()
        {
            NodeUML editor = EditorWindow.GetWindow<NodeUML>();
            editor.Init();
        }

        public void Init()
        {
            controller = new NodeController();
            classHandler = new ClassHandler(this, controller);
            useCaseHandler = new UseCaseHandler(this);
        }

        void OnGUI()
        {
            BeginWindows();
            classHandler.Update();
            //useCaseHandler.Update();
            EndWindows();
        }
    }
}