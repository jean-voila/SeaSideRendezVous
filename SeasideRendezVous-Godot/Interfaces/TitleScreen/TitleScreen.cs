using Godot;
using System;

public partial class TitleScreen : Control
{
	private bool hasStarted = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Visible && !hasStarted){
			hasStarted = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
			GetTree().Paused = true;
		}
	}
}
