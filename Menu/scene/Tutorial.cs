using Godot;
using System;

public partial class Tutorial : Control
{
	private Button _kembaliBtn;
	private Button _selanjutnyaBtn;

	public override void _Ready()
	{
		// 1. Ambil referensi node berdasarkan hierarki gambar kamu
		// Karena 'Kembali' dan 'Selanjutnya' adalah anak langsung dari 'Tutorial'
		_kembaliBtn = GetNode<Button>("Kembali");
		_selanjutnyaBtn = GetNode<Button>("Selanjutnya");

		// 2. Cegah auto-focus agar tidak ter-klik sendiri saat scene terbuka
		_kembaliBtn.ReleaseFocus();
		_selanjutnyaBtn.ReleaseFocus();

		// 3. Hubungkan signal klik
		_kembaliBtn.Pressed += OnKembaliPressed;
		_selanjutnyaBtn.Pressed += OnSelanjutnyaPressed;

		// 4. Efek Fade In saat masuk ke scene Tutorial
		this.Modulate = new Color(1, 1, 1, 0);
		Tween fadeIn = CreateTween();
		fadeIn.TweenProperty(this, "modulate", new Color(1, 1, 1, 1), 0.5f);
	}

	private void OnKembaliPressed()
	{
		// Transisi balik ke Main Menu
		TransitionToScene("res://Menu/scene/main_menu.tscn");
	}

	private void OnSelanjutnyaPressed()
	{
		// Transisi maju ke Level
		TransitionToScene("res://Menu/scene/level.tscn");
	}

	// Fungsi pembantu agar tidak menulis kode Tween berulang kali
	private void TransitionToScene(string scenePath)
	{
		_kembaliBtn.Disabled = true;
		_selanjutnyaBtn.Disabled = true;

		Tween fadeOut = CreateTween();
		fadeOut.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), 0.4f);
		fadeOut.Finished += () => GetTree().ChangeSceneToFile(scenePath);
	}
}
