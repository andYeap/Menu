using Godot;
using System;

public partial class Level : Control
{
	private Button _level1Btn;
	private Button _kembaliBtn;
	private Button _shopBtn;
	private Button _resepBtn;
	private Button _closeBtn;
	private Button _mainBtn;
	private Control _choosedLevelPopup;

	public override void _Ready()
	{
		// 1. Inisialisasi referensi Node sesuai hierarki gambar kamu
		_level1Btn = GetNode<Button>("Level/Level1");
		_kembaliBtn = GetNode<Button>("Kembali");
		_shopBtn = GetNode<Button>("Shop");
		_resepBtn = GetNode<Button>("Resep");
		_choosedLevelPopup = GetNode<Control>("ChoosedLevel");
		
		// Tombol di dalam popup ChoosedLevel
		_closeBtn = GetNode<Button>("ChoosedLevel/Close");
		_mainBtn = GetNode<Button>("ChoosedLevel/Main"); 

		// 2. Pengaturan awal Popup (Sembunyi)
		_choosedLevelPopup.Visible = false;
		_choosedLevelPopup.Scale = Vector2.Zero;
		
		// Mengatur titik pusat animasi ke tengah popup
		_choosedLevelPopup.PivotOffset = _choosedLevelPopup.Size / 2;

		// 3. Menghubungkan semua Signal ke Method
		_level1Btn.Pressed += OnLevel1Pressed;
		_kembaliBtn.Pressed += OnKembaliPressed;
		_shopBtn.Pressed += OnShopPressed;
		_resepBtn.Pressed += OnResepPressed;
		_closeBtn.Pressed += OnClosePressed;
		_mainBtn.Pressed += OnMainPressed;
	}

	// --- LOGIKA TOMBOL UTAMA ---

	private void OnLevel1Pressed()
	{
		_choosedLevelPopup.Visible = true;
		AnimatePopup(true);
	}

	private void OnKembaliPressed()
	{
		// Berpindah ke scene tutorial
		GetTree().ChangeSceneToFile("res://Menu/scene/tutorial.tscn");
	}

	private void OnShopPressed()
	{
		// SESUAI GAMBAR: pastikan path file shop.tscn sudah benar
		GD.Print("Membuka Scene Shop...");
		GetTree().ChangeSceneToFile("res://Menu/scene/shop.tscn");
	}

	private void OnResepPressed()
	{
		// SESUAI GAMBAR: pastikan path file resep.tscn sudah benar
		GD.Print("Membuka Scene Resep...");
		GetTree().ChangeSceneToFile("res://Menu/scene/resep.tscn");
	}

	// --- LOGIKA POPUP CHOOSEDLEVEL ---

	private void OnClosePressed()
	{
		AnimatePopup(false);
	}

	private void OnMainPressed()
	{
		GD.Print("Memulai Level 1...");
		// Ganti dengan path scene gameplay kamu
		GetTree().ChangeSceneToFile("res://Gameplay.tscn");
	}

	// --- FUNGSI ANIMASI POPUP ---

	private void AnimatePopup(bool open)
	{
		Tween tween = CreateTween();
		Vector2 targetScale = open ? Vector2.One : Vector2.Zero;
		float duration = open ? 0.3f : 0.2f;

		tween.TweenProperty(_choosedLevelPopup, "scale", targetScale, duration)
			 .SetTrans(Tween.TransitionType.Back)
			 .SetEase(open ? Tween.EaseType.Out : Tween.EaseType.In);

		if (!open)
		{
			// Sembunyikan node hanya setelah animasi selesai
			tween.Finished += () => _choosedLevelPopup.Visible = false;
		}
	}
}
