using System.Runtime.CompilerServices;
using Godot;
namespace SeasideRendezvous.Maps.MapDebug;

public partial class MapDebug : Node3D
{
	[Export] private CharacterBody3D _player;
	[Export] private CsgBox3D _corridor;
	[Export] private AudioStreamPlayer3D _sunlightSonata;
	private bool _corridorEntered = false;
	[Export] private Node3D _titleText;
	[Export] private Timer _corridorTimer;
	[Export] private AnimationPlayer _textAnimation;
	[Export] private CsgBox3D _cantGoBackWall;
	[Export] private RigidBody3D _bernie;
	
	
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
		}
	}

	private void _CorridorTimerTimeout(){
		_titleText.Show();
		_textAnimation.Play("fade_in");
		_cantGoBackWall.Show();
		_cantGoBackWall.UseCollision = true;
		_bernie.Show();
	}
	

}