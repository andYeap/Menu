using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _mulaiBtn;
	private Button _keluarBtn;

	public override void _Ready()
	{
		// 1. Ambil referensi node berdasarkan hierarki di gambar kamu
		_mulaiBtn = GetNode<Button>("CenterContainer/VBoxContainer/Mulai");
		_keluarBtn = GetNode<Button>("CenterContainer/VBoxContainer/Keluar");

		// 2. Cegah tombol "ter-klik" sendiri saat baru mulai (Focus Bug)
		_mulaiBtn.ReleaseFocus();
		_keluarBtn.ReleaseFocus();
		_mulaiBtn.FocusMode = FocusModeEnum.None;
		_keluarBtn.FocusMode = FocusModeEnum.None;

		// 3. Hubungkan signal klik ke fungsi
		_mulaiBtn.Pressed += OnMulaiPressed;
		_keluarBtn.Pressed += OnKeluarPressed;
		
		// Opsional: Pastikan menu terlihat saat start
		this.Modulate = new Color(1, 1, 1, 1);
	}

	private void OnMulaiPressed()
	{
		// Matikan input agar tidak terjadi klik ganda selama animasi
		SetProcessInput(false);
		_mulaiBtn.Disabled = true;

		GD.Print("Tombol Mulai Ditekan - Memulai Animasi Fade Out...");

		// Membuat animasi Fade Out (menghilang perlahan)
		Tween fadeTween = CreateTween();
		
		// Animasikan transparansi (modulate) ke 0 dalam 0.6 detik
		fadeTween.TweenProperty(this, "modulate", new Color(0, 0, 0, 0), 0.6f)
				 .SetTrans(Tween.TransitionType.Quart)
				 .SetEase(Tween.EaseType.Out);

		// Setelah animasi selesai, pindah scene
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
