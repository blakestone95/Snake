using Godot;
using System;

public partial class GameOver : Control
{
	public void OnRestartButtonPressed() {
		GetTree().ChangeSceneToFile("res://src/Scenes/GameBoard.tscn");
	}
}
