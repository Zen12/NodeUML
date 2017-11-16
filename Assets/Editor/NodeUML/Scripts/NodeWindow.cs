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
        private UseCaseHandler useCaseHandler;
        private SequenceHandler sequanceHander;

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
            sequanceHander = new SequenceHandler(this, IdHandler.GetInstance(), controller.listNodes, useCaseHandler.actors, useCaseHandler.listOfUseCase);
        }

        void OnGUI()
        {
            BeginWindows();
            // classHandler.Update();
            // useCaseHandler.Update();
            sequanceHander.Update();
            EndWindows();
        }
    }
}