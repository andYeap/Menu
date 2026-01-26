using Godot;
using System;

public partial class Resep : Control
{
	private Control _detailBahanPopup;
	private Button _kembaliBtn;
	private Button _closeBtn; // Referensi tombol tutup baru
	private Area2D _telurArea;

	public override void _Ready()
	{
		// 1. Perbaikan Otomatis: Agar node root tidak menghalangi klik ke bawah
		this.MouseFilter = MouseFilterEnum.Ignore; 

		// 2. Referensi Node sesuai hierarki kamu
		_detailBahanPopup = GetNode<Control>("DetailBahan");
		_kembaliBtn = GetNode<Button>("Kembali");
		_telurArea = GetNode<Area2D>("Telur/Area2D");
		
		// Mengambil tombol Close yang berada di dalam DetailBahan
		_closeBtn = _detailBahanPopup.GetNode<Button>("Close");

		// 3. Konfigurasi Awal
		_detailBahanPopup.Visible = false;
		_detailBahanPopup.Scale = Vector2.Zero;
		_detailBahanPopup.PivotOffset = _detailBahanPopup.Size / 2;

		// 4. Hubungkan Sinyal-Sinyal
		
		// Klik Telur
		if (_telurArea != null)
		{
			_telurArea.InputPickable = true; 
			_telurArea.InputEvent += OnTelurInput;
		}

		// Tombol Kembali ke Level
		_kembaliBtn.Pressed += () => GetTree().ChangeSceneToFile("res://Menu/scene/level.tscn");

		// Tombol Tutup DetailBahan
		_closeBtn.Pressed += OnCloseBtnPressed;
	}

	private void OnTelurInput(Node viewport, InputEvent @event, long shapeIdx)
	{
		// Deteksi klik kiri mouse via Input Map
		if (@event.IsActionPressed("click"))
		{
			GD.Print("Membuka DetailBahan...");
			ShowPopup(true);
		}
	}

	// Method khusus untuk menutup popup
	private void OnCloseBtnPressed()
	{
		GD.Print("Menutup DetailBahan...");
		ShowPopup(false);
	}

	private void ShowPopup(bool show)
	{
		if (show) _detailBahanPopup.Visible = true;

		Tween tween = CreateTween();
		Vector2 targetScale = show ? Vector2.One : Vector2.Zero;

		tween.TweenProperty(_detailBahanPopup, "scale", targetScale, 0.3f)
			 .SetTrans(Tween.TransitionType.Back)
			 .SetEase(show ? Tween.EaseType.Out : Tween.EaseType.In);

		if (!show)
		{
			tween.Finished += () => _detailBahanPopup.Visible = false;
		}
	}
}
