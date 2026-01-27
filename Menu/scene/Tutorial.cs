using Godot;
using System;

public partial class Tutorial : Control
{
	private Button _kembaliBtn;
	private Button _selanjutnyaBtn;

	public override void _Ready()
	{
		_kembaliBtn = GetNode<Button>("Kembali");
		_selanjutnyaBtn = GetNode<Button>("Selanjutnya");

		_kembaliBtn.ReleaseFocus();
		_selanjutnyaBtn.ReleaseFocus();

		_kembaliBtn.Pressed += OnKembaliPressed;
		_selanjutnyaBtn.Pressed += OnSelanjutnyaPressed;

		this.Modulate = new Color(1, 1, 1, 0);
		Tween fadeIn = CreateTween();
		fadeIn.TweenProperty(this, "modulate", new Color(1, 1, 1, 1), 0.5f);
	}

	private void OnKembaliPressed()
	{
		TransitionToScene("res://Menu/scene/main_menu.tscn");
	}

	private void OnSelanjutnyaPressed()
	{
		TransitionToScene("res://Menu/scene/level.tscn");
	}

	private void TransitionToScene(string scenePath)
	{
		_kembaliBtn.Disabled = true;
		_selanjutnyaBtn.Disabled = true;

		Tween fadeOut = CreateTween();
		fadeOut.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), 0.4f);
		fadeOut.Finished += () => GetTree().ChangeSceneToFile(scenePath);
	}
}
