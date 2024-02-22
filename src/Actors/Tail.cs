using Godot;
using System;

public partial class Tail : StaticBody2D
{
	readonly Node2D head; // For collision detection
	readonly Node2D parent; // For positioning
	// private Vector

	public Tail (Node2D _head, Node2D _parent) {
		head = _head;
		parent = _parent;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// TODO: Collision Detection
		// parent.Position
	}
}
