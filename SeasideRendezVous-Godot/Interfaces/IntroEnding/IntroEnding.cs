using Godot;
using System;

public partial class IntroEnding : Control
{
	private bool hasStarted = false;
	[Export] private AnimationPlayer _outroAnimation;
	[Export] private Timer _startNextLevel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Visible && !hasStarted){
			hasStarted = true;
			_outroAnimation.Play("fade_out");
			Input.MouseMode = Input.MouseModeEnum.Visible;
			_startNextLevel.Start();
		}
	}

	private void _StartNextLevelTimeout(){
		GetTree().ChangeSceneToFile("res://Maps/Level1/Level1.tscn");
	}
}
