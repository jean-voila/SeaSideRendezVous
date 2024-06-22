using Godot;
namespace SeasideRendezvous.Interfaces.PauseMenu;

public partial class PauseMenu : Control
{
	private bool _paused;
	public override void _Ready()
	{
		Visible = false;
	}
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("escape"))
		{
			PauseToggle();
		}
	}

	private void PauseToggle(){
			_paused = !_paused;
			Input.MouseMode = _paused ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
			GetTree().Paused = _paused;
			Visible = _paused;
	}

	private void _QuitButtonPressedSignal(){
		GetTree().Quit();
	}

	private void _ResumeButtonPressedSignal(){
		PauseToggle();
	}
}