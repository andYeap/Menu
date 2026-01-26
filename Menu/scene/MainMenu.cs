using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _mulaiBtn;
	private Button _keluarBtn;

	public override void _Ready()
	{
		_mulaiBtn = GetNode<Button>("CenterContainer/VBoxContainer/Mulai");
		_keluarBtn = GetNode<Button>("CenterContainer/VBoxContainer/Keluar");

		_mulaiBtn.ReleaseFocus();
		_keluarBtn.ReleaseFocus();
		_mulaiBtn.FocusMode = FocusModeEnum.None;
		_keluarBtn.FocusMode = FocusModeEnum.None;

		_mulaiBtn.Pressed += OnMulaiPressed;
		_keluarBtn.Pressed += OnKeluarPressed;
		
		this.Modulate = new Color(1, 1, 1, 1);
	}

	private void OnMulaiPressed()
	{
		SetProcessInput(false);
		_mulaiBtn.Disabled = true;

		GD.Print("Tombol Mulai Ditekan - Memulai Animasi Fade Out...");

		Tween fadeTween = CreateTween();
		
		fadeTween.TweenProperty(this, "modulate", new Color(0, 0, 0, 0), 0.6f)
				 .SetTrans(Tween.TransitionType.Quart)
				 .SetEase(Tween.EaseType.Out);

		fadeTween.Finished += () => 
		{
			GD.Print("Pindah Scene sekarang.");
			GetTree().ChangeSceneToFile("res://Menu/scene/tutorial.tscn");
		};
	}

	private void OnKeluarPressed()
	{
		GD.Print("Keluar dari Game.");
		GetTree().Quit();
	}
}
