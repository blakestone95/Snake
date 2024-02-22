using Godot;
using System;
using System.Collections.Generic;
using Snake;

public partial class Player : Area2D
{
	public const float Speed = 32.0f;
	// Starting direction is left
	// Need to track next direction and flush when we do a move, otherwise potentially the player can do a 180 trun
	private Vector2 direction = Vector2.Left;
	private Vector2 nextDirection = Vector2.Left;
	private int length = 4;
	private List<StaticBody2D> tail = new();
	[Export]
	public PackedScene TailScene { get; set; }
	[Signal]
	public delegate void ScoreEventHandler();

	// Note to self: this is not an actual function on the parent class,
	// it's just a way to stay organized
	private void GetInput()
	{
		bool left = Input.IsActionPressed("ui_left");
		bool right = Input.IsActionPressed("ui_right");
		bool up = Input.IsActionPressed("ui_up");
		bool down = Input.IsActionPressed("ui_down");

		switch((direction.X, direction.Y)) {
			case (> 0, 0):
			case (< 0, 0):
				if (up) nextDirection = Vector2.Up;
				if (down) nextDirection = Vector2.Down;
				break;
			case (0, > 0):
			case (0, < 0):
				if (left) nextDirection = Vector2.Left;
				if (right) nextDirection = Vector2.Right;
				break;
		}
	}

	public override void _Process(double delta)
	{
		GetInput(); // You've got to call the function, stupid!
	}

	public void OnBodyEntered(Node2D node)
	{
		// Probably fragile to check based on name... Can we check what collision layer?
		if (node.Name == "Boundary" || node.Name.ToString().StartsWith("Tail"))
		{
			GetTree().ChangeSceneToFile("res://src/Scenes/GameOver.tscn");
		}
		else if (node.Name == "Food")
		{
			Globals.Score += 1;
			length += 1;
			EmitSignal(SignalName.Score);
		}
	}

	public void Move()
	{
		// Store the last position before we move
		Vector2 lastPosition = GlobalPosition;

		// Move the head
		Vector2 moveVector = new Vector2(nextDirection.X * Speed, nextDirection.Y * Speed);
		Position += moveVector;
		direction = nextDirection; // Update the current direction

		// Move the tail segments
		Vector2 nextSegmentPosition = lastPosition;
		foreach(StaticBody2D segment in tail) {
			// Apply next position and store current position for next child
			Vector2 tmp = segment.GlobalPosition;
			segment.GlobalPosition = nextSegmentPosition;
			nextSegmentPosition = tmp;
        }

		// Add tail segment if not long enough
		if (tail.Count < length) {
			StaticBody2D newTail = TailScene.Instantiate<StaticBody2D>();
			newTail.GlobalPosition = nextSegmentPosition;
			newTail.Name = new StringName("Tail" + tail.Count);
			// If I add it as a child of the head, the position calculation gets all weird even if I use GlobalPosition...
			GetParent().AddChild(newTail);
			tail.Add(newTail);
		}
    }
}
