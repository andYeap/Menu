using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _mulaiBtn;
	private Button _keluarBtn;

	public override void _Ready()
	{
		_mulaiBtn = GetNode<Button>("Mulai");
		_mulaiBtn.Pressed += OnMulaiPressed;
		_keluarBtn = GetNode<Button>("Keluar");
		_keluarBtn.Pressed += () => GetTree().Quit();
		
		Visible = true;
	}

	private void OnMulaiPressed()
	{
		Visible = false;
		_mulaiBtn.Disabled = true;
		
		var prefab = GD.Load<PackedScene>("res://Menu/scene/transisi.tscn");
		var instance = prefab.Instantiate<Transisi>();
		instance.sceneTujuan = "res://Menu/scene/tutorial.tscn";
		GetTree().Root.AddChild(instance);
	}
}
