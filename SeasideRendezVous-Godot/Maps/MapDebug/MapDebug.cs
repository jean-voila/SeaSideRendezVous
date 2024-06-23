using System.Runtime.CompilerServices;
using Godot;
using SeasideRendezvous.Entities.Player;
namespace SeasideRendezvous.Maps.MapDebug;

public partial class MapDebug : Node3D
{
	[Export] private CharacterBody3D _player;
	[Export] private CsgBox3D _corridor;
	[Export] private AudioStreamPlayer _sunlightSonata;
	private bool _corridorEntered = false;
	[Export] private Node3D _titleText;
	[Export] private Timer _corridorTimer;
	[Export] private AnimationPlayer _textAnimation;
	[Export] private CsgBox3D _cantGoBackWall;
	[Export] private RigidBody3D _bernie;

	[Export] private Node3D _terrain;
	[Export] private Timer _lsdModeTimer;
	[Export] private AnimationPlayer _lsdModeAnimation;

	[Export] private Timer _endOfGameTimer;

	private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
		
	public override void _Ready()
	{
		_corridor.Hide();
		_titleText.Hide();
		_cantGoBackWall.Hide();
		_cantGoBackWall.UseCollision = false;
		_bernie.Hide();
	}

	public override void _Process(double delta)
	{

	}

	private void _CenterOfMapEnteredSignal(Node3D body){
		_corridor.Show();
	}

	private void _CorridorEnteredSignal(Node3D body){
		if (!_corridorEntered){
			_corridorEntered = true;
			_sunlightSonata.Play();
			_corridorTimer.Start();
			_cantGoBackWall.Show();
			_cantGoBackWall.UseCollision = true;
			_lsdModeTimer.Start();
			_endOfGameTimer.Start();
		}
	}

	private void _CorridorTimerTimeout(){
		_titleText.Show();
		_textAnimation.Play("fade_in");
		_bernie.Show();
		
	}

	private void _LsdModeTimeoutSignal(){
		_lsdModeAnimation.Play("LSDMode");
		_titleText.Hide();
		_player.EmitSignal("LowGravitySignal", true);
	}

	private void _EndOfGameTimeout(){
		_player.EmitSignal("EndOfGameSignal");
	}

	

}