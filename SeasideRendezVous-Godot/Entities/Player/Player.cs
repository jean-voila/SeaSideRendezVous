using Godot;
namespace SeasideRendezvous.Entities.Player;

public partial class Player : CharacterBody3D
{
	[Export] private Node3D _head;
	[Export] private float MouseSensitivity = 0.002f;
	[Export] private const float Speed = 5.4f;
	
	[Export] private float JumpVelocity = 4f;
	[Export] private float Acceleration = 10.0f;
	[Export] private AudioStreamPlayer3D _footstepsSFX;

	private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Vector3 _position;
	

	private float _currentSpeed = Speed;
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
		if (inputDir == Vector2.Zero || !IsOnFloor()) {
			_footstepsSFX.Stop();
		} else if (!_footstepsSFX.Playing) {
			_footstepsSFX.Play();
		}
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		var horizontalVelocity = new Vector3(Velocity.X, 0, Velocity.Z);
		var target = direction * _currentSpeed;
		horizontalVelocity = horizontalVelocity.Lerp(target, Acceleration * (float)delta);

		Velocity = new Vector3(horizontalVelocity.X, Velocity.Y, horizontalVelocity.Z);
		MoveAndSlide();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseInput)
		{
			RotateY(-mouseInput.Relative.X * MouseSensitivity);
			_head.RotateX(-mouseInput.Relative.Y * MouseSensitivity);

			var currentRotation = _head.RotationDegrees;
			currentRotation.X = Mathf.Clamp(currentRotation.X, -90.0f, 90.0f);
			
			_head.RotationDegrees = currentRotation;
		}

		if (@event.IsActionPressed("caps")){
			_currentSpeed = Speed * 1.5f;
		}
		if (@event.IsActionReleased("caps")){
			_currentSpeed = Speed;
		}
		if (@event.IsActionPressed("centerOfMap")){
			Teleport(new Vector3(0, 1, 0));
		}
		if (@event.IsActionPressed("corridor")){
			Teleport(new Vector3(30.179f, 0.929f, -43.619f));
		}
	}

	    private void Teleport(Vector3 newPosition)
    {
        Transform3D newTransform = Transform3D.Identity;
        newTransform.Origin = newPosition;
        GlobalTransform = newTransform;
    }
	
}