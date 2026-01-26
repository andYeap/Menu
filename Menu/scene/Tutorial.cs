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

		// Optimasi tombol: Menghapus fokus visual dan garis samar
		foreach (var btn in new[] { _kembaliBtn, _selanjutnyaBtn })
		{
			if (btn == null) continue;
			btn.FocusMode = FocusModeEnum.None;
			btn.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
		}

		_kembaliBtn.Pressed += () => TransitionToScene("res://Menu/scene/main_menu.tscn");
		_selanjutnyaBtn.Pressed += () => TransitionToScene("res://Menu/scene/level.tscn");

		// Animasi Fade In saat scene dibuka
		this.Modulate = new Color(1, 1, 1, 0);
		Tween fadeIn = CreateTween();
		fadeIn.TweenProperty(this, "modulate", new Color(1, 1, 1, 1), 0.5f)
			  .SetTrans(Tween.TransitionType.Quart)
			  .SetEase(Tween.EaseType.Out);
	}

	private void TransitionToScene(string scenePath)
	{
		if (!FileAccess.FileExists(scenePath)) return;

		_kembaliBtn.Disabled = true;
		_selanjutnyaBtn.Disabled = true;

		// Memanggil Loading Screen kapur sebagai transisi
		var loadingScene = GD.Load<PackedScene>("res://Menu/scene/LoadingScreen.tscn");
		if (loadingScene != null)
		{
			var loadingInstance = loadingScene.Instantiate<CanvasLayer>();
			GetTree().Root.AddChild(loadingInstance);
		}

		// Pindah scene
		GetTree().ChangeSceneToFile(scenePath);
	}
}
