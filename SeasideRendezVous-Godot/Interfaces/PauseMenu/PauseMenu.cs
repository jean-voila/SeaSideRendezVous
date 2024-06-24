using Godot;
namespace SeasideRendezvous.Interfaces.PauseMenu;

public partial class PauseMenu : Control
{
	public bool _canPause = true;
	[Signal] public delegate void CanPauseSignalEventHandler(bool canPause);
	private bool _paused;
	public override void _Ready()
	{
		Visible = false;
		CanPauseSignal+=CanPause;
	}
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("escape") && _canPause)
		{
			PauseToggle();
		}
	}

	private void PauseToggle(){
			_paused = !_paused;
			Input.MouseMode = _paused ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
			GetTree().Paused = _paused;
			Visible = _paused;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), _paused ? -20 : 0);
	}

	private void _QuitButtonPressedSignal(){
		GetTree().Quit();
	}

	private void _ResumeButtonPressedSignal(){
		PauseToggle();
	}

	private void CanPause(bool canPause){
		_canPause = canPause;
	}
}