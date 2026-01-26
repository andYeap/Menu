using Godot;
using System;

public partial class Shop : Control
{
	private Label _coinLabel;
	private Area2D _telurArea;
	private Button _kembaliBtn;

	// Tentukan harga telur dan saldo awal koin
	private int _currentCoins = 100; 
	private int _itemPrice = 25;

	public override void _Ready()
	{
		// 1. Inisialisasi referensi node
		_coinLabel = GetNode<Label>("Coin");
		_kembaliBtn = GetNode<Button>("Kembali");
		
		// 2. Akses Area2D di dalam instansi Telur
		_telurArea = GetNode<Area2D>("Telur/Area2D");

		// Update tampilan koin awal
		UpdateCoinUI();

		// 3. Hubungkan sinyal input pada Area2D
		if (_telurArea != null)
		{
			_telurArea.InputPickable = true;
			_telurArea.InputEvent += OnTelurInputEvent;
		}

		// Hubungkan tombol kembali
		_kembaliBtn.Pressed += () => GetTree().ChangeSceneToFile("res://Menu/scene/level.tscn");
	}

	private void OnTelurInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		// Deteksi klik kiri mouse
		if (@event.IsActionPressed("click"))
		{
			ProcessPurchase();
		}
	}

	private void ProcessPurchase()
	{
		// Cek apakah koin cukup
		if (_currentCoins >= _itemPrice)
		{
			GD.Print("Pembelian Berhasil!");
			
			// Kurangi koin
			_currentCoins -= _itemPrice;
			UpdateCoinUI();

			// Efek kartu menghilang (Hapus node Telur dari scene)
			Node telurNode = GetNode("Telur");
			if (telurNode != null)
			{
				telurNode.QueueFree(); 
			}
		}
		else
		{
			GD.Print("Koin tidak cukup!");
			// Anda bisa menambahkan animasi merah pada label koin di sini
		}
	}

	private void UpdateCoinUI()
	{
		// Perbarui teks pada label koin
		_coinLabel.Text = "Koin : " + _currentCoins.ToString();
	}
}
