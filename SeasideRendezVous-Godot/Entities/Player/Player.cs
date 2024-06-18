using Godot;
namespace SeasideRendezvous.Entities.Player;

public partial class Player : CharacterBody3D
{
	[Export] private Node3D _head;
	[Export] private float _mouseSensitivity = 0.005f;
	private const float Speed = 6.0f;
	private const float JumpVelocity = 8.5f;
	private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Vector3 _position;
	

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		_position = GlobalTransform.Origin;
		
		if (!IsOnFloor())
			Velocity = new Vector3(Velocity.X, Velocity.Y - _gravity * (float)delta, Velocity.Z);

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			Velocity = new Vector3(Velocity.X, JumpVelocity, Velocity.Z);

		Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		var horizontalVelocity = new Vector3(Velocity.X, 0, Velocity.Z);
		var target = direction * Speed;
		horizontalVelocity = horizontalVelocity.Lerp(target, 10f * (float)delta);

		Velocity = new Vector3(horizontalVelocity.X, Velocity.Y, horizontalVelocity.Z);
		MoveAndSlide();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseInput)
		{
			RotateY(-mouseInput.Relative.X * _mouseSensitivity);
			_head.RotateX(-mouseInput.Relative.Y * _mouseSensitivity);

			var currentRotation = _head.RotationDegrees;
			currentRotation.X = Mathf.Clamp(currentRotation.X, -90.0f, 90.0f);
			
			_head.RotationDegrees = currentRotation;
		}
	}
	
}