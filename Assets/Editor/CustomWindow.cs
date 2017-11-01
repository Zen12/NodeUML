using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomWindow : EditorWindow
{

	NodeController controller;

	[MenuItem ("Window/UML View")]
	private static void ShowView ()
	{
		CustomWindow editor = EditorWindow.GetWindow<CustomWindow> ();
		editor.Init ();
	}

	public void Init ()
	{
		controller = new NodeController ();
	}

	void OnGUI ()
	{
		BeginWindows ();
		float devider = 5f;
		GUILayout.BeginArea (new Rect (position.width - position.width / devider, 0, position.width / devider, position.height));
		GUILayout.Box ("Info", GUILayout.Height (position.height), GUILayout.Width (position.width / devider));
		GUILayout.EndArea ();

		//Draw all the Nodes
		controller.DrawNodes ();
		EndWindows ();
	}

}
