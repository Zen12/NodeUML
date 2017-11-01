using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Rect Transform { private set; get; }

	public string NodeName { private set; get; }

	private int id;

	private List<NodeInfoItem> listProperty;
	private List<NodeInfoItem> listMethods;

	public Node (Rect position, string name, int id)
	{
		NodeName = name;
		Transform = position;
		this.id = id;
		listProperty = new List<NodeInfoItem> {
			new NodeInfoItem ("1", this),
			new NodeInfoItem ("2", this),
		};
		listMethods = new List<NodeInfoItem> {
			new NodeInfoItem ("M1", this),
			new NodeInfoItem ("M2", this),
		};
	}

	public void DrawNode ()
	{
		Transform = GUI.Window (id, Transform, UpdateNode, NodeName); 
	}

	private void UpdateNode (int id)
	{
		GUILayout.BeginVertical (GUILayout.Width (Transform.width - 15));
		for (int i = 0; i < listProperty.Count; i++) {
			listProperty [i].Draw ();
		}

		if (GUILayout.Button ("New Property", GUILayout.Width (Transform.width - 15))) {
			listProperty.Add (new NodeInfoItem ("", this));
		}
		GUILayout.Space (10);
		for (int i = 0; i < listMethods.Count; i++) {
			listMethods [i].Draw ();
		}

		if (GUILayout.Button ("New Method", GUILayout.Width (Transform.width - 15))) {
			listMethods.Add (new NodeInfoItem ("", this));
		}
		GUILayout.EndVertical ();
		GUI.DragWindow ();
	}

	internal void DeleteNodeInfo (NodeInfoItem info)
	{
		listProperty.Remove (info);
		listMethods.Remove (info);
	}
}

internal class NodeInfoItem
{

	public string Text{ private set; get; }

	private Node node;

	public NodeInfoItem (string text, Node node)
	{
		Text = text;
		this.node = node;
	}

	public void Draw ()
	{
		GUILayout.BeginHorizontal (GUILayout.Width (node.Transform.width - 15));
		Text = GUILayout.TextField (Text, GUILayout.Width (node.Transform.width - 25));
		if (GUILayout.Button ("X", GUILayout.Width (15))) {
			node.DeleteNodeInfo (this);
		}
		GUILayout.EndHorizontal ();
	}
}
