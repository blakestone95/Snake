using Godot;
using System;

public partial class GameStart : Control
{
	public void OnStartButtonPressed() {
		GetTree().ChangeSceneToFile("res://src/Scenes/GameBoard.tscn");
	}
}
