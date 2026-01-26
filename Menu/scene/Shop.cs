using Godot;
using System;

public partial class Shop : Control
{
	private Label _coinLabel;
	private Area2D _telurArea;
	private Button _kembaliBtn;

	private int _currentCoins = 100; 
	private int _itemPrice = 25;

	public override void _Ready()
	{
		_coinLabel = GetNode<Label>("Coin");
		_kembaliBtn = GetNode<Button>("Kembali");
		
		_telurArea = GetNode<Area2D>("Telur/Area2D");

		UpdateCoinUI();

		if (_telurArea != null)
		{
			_telurArea.InputPickable = true;
			_telurArea.InputEvent += OnTelurInputEvent;
		}

		_kembaliBtn.Pressed += () => GetTree().ChangeSceneToFile("res://Menu/scene/level.tscn");
	}

	private void OnTelurInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event.IsActionPressed("click"))
		{
			ProcessPurchase();
		}
	}

	private void ProcessPurchase()
	{
		if (_currentCoins >= _itemPrice)
		{
			GD.Print("Pembelian Berhasil!");
			
			_currentCoins -= _itemPrice;
			UpdateCoinUI();

			Node telurNode = GetNode("Telur");
			if (telurNode != null)
			{
				telurNode.QueueFree(); 
			}
		}
		else
		{
			GD.Print("Koin tidak cukup!");
		}
	}

	private void UpdateCoinUI()
	{
		_coinLabel.Text = "Koin : " + _currentCoins.ToString();
	}
}
