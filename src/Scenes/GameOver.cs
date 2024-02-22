using Godot;
using System;
using Snake;

public partial class GameOver : Control
{
	public override void _Ready()
	{
		// Update Score
		var score = GetNode<Label>("Background/Menu/GameOverText/ScoreLabel");
        score.Text = "Score: " + Globals.Score;
	}
	
	public void OnRestartButtonPressed() {
		// Reset score on restart
		Globals.Score = 0;
		GetTree().ChangeSceneToFile("res://src/Scenes/GameBoard.tscn");
	}
}
