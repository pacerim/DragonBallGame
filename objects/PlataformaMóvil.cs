using Godot;
using System;

public partial class PlataformaMóvil : Path2D
{
	[Export]
	private bool Loop {get; set;} = true;

	[Export]
	private float Speed {get; set;} = 2.0f;
	
	[Export] 
	private float SpeedScale {get; set;} = 1.0f;
	
	private PathFollow2D _path;
	private AnimationPlayer _animation;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animation = GetNode<AnimationPlayer>("AnimationPlayer");
		_path = GetNode<PathFollow2D>("PathFollow2D");
		
		if(!Loop){
			_animation.Play("UpDown");
			_animation.SpeedScale = SpeedScale;
			SetProcess(false); 
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_path.Progress += Speed * (float)delta;
	}
}
