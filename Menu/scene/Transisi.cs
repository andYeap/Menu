using Godot;
using System;

public partial class Transisi : CanvasLayer
{
	[Export] public string sceneTujuan;
	private AnimationPlayer _anim;

	public override void _Ready()
	{
		_anim = GetNode<AnimationPlayer>("AnimationPlayer");
		_anim.AnimationFinished += OnAnimationFinished;

		if (!string.IsNullOrEmpty(sceneTujuan))
		{
			_anim.Play("tirai");
		}
		else
		{
			_anim.Play("tiraiBuka");
		}
	}

	private void OnAnimationFinished(StringName animName)
	{
		if (animName == "tirai")
		{
			GetTree().ChangeSceneToFile(sceneTujuan);
		}
		else
		{
			QueueFree();
		}
	}
}
