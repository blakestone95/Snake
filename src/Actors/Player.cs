using Godot;
using System;

public partial class Player : Area2D
{
	public const float Speed = 100.0f;
	// Starting direction is left
	private Vector2 direction = Vector2.Left;

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
				if (up) direction = Vector2.Up;
				if (down) direction = Vector2.Down;
				break;
			case (0, > 0):
			case (0, < 0):
				if (left) direction = Vector2.Left;
				if (right) direction = Vector2.Right;
				break;
		}
	}

	public override void _Process(double delta)
	{
		GetInput(); // You've got to call the function, stupid!
		Vector2 velocity = new Vector2(direction.X * Speed, direction.Y * Speed);
		Position += velocity * (float)delta;
	}
}
