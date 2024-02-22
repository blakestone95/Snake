using Godot;
using System;
using Snake;

public partial class GameBoard : Node2D
{
	[Export]
	public PackedScene FoodScene { get; set; }
	const int gridSize = 32;
	const int boardWidth = 30;
	const int boardHeight = 15;
	const int boardLeftPadding = 3;
	const int boardTopPadding = 3;
	StaticBody2D food;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		food = FoodScene.Instantiate<StaticBody2D>();
		food.Position = GetNewFoodPosition();
		AddChild(food);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
    public void OnScore()
    {
		// Update Score
        GetNode<Label>("ScoreLabel").Text = "Score: " + Globals.Score;

		// Move food
		food.Position = GetNewFoodPosition();
    }

	public static Vector2 GetNewFoodPosition()
	{
		// TODO: Avoid placing on the player?

		RandomNumberGenerator rng = new();
		
		// Subtracting 1 from board dimensions to account for food's size
		int boardX = rng.RandiRange(0, boardWidth - 1);
		int boardY = rng.RandiRange(0, boardHeight - 1);

		int x = (boardLeftPadding + (boardX)) * gridSize;
		int y = (boardTopPadding + (boardY)) * gridSize;

		return new Vector2(x ,y);
	}
}
