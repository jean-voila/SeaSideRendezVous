using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Fade_In_Animation : Control
{
	[Export] private AnimationPlayer _introAnimation;
	private bool hasStarted = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_introAnimation.Play("fade_in");
		hasStarted = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!_introAnimation.IsPlaying() && hasStarted)
			QueueFree();
	}
}
