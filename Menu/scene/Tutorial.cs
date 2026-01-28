using Godot;
using System;

public partial class Tutorial : Control
{
	private Button _kembaliBtn;
	private Button _selanjutnyaBtn;

	public override void _Ready()
	{
		_kembaliBtn = GetNode<Button>("Kembali");
		_kembaliBtn.Pressed += OnKembaliPressed;
		_selanjutnyaBtn = GetNode<Button>("Selanjutnya");
		_selanjutnyaBtn.Pressed += OnSelanjutnyaPressed;
	}

	private void OnKembaliPressed()
	{
		var prefab = GD.Load<PackedScene>("res://Menu/scene/transisi.tscn");
		var instance = prefab.Instantiate<Transisi>();
		instance.sceneTujuan = "res://Menu/scene/main_menu.tscn";
		GetTree().Root.AddChild(instance);
	}
	
	private void OnSelanjutnyaPressed()
	{
		var prefab = GD.Load<PackedScene>("res://Menu/scene/transisi.tscn");
		var instance = prefab.Instantiate<Transisi>();
		instance.sceneTujuan = "res://Menu/scene/main_menu[].tscn";
		GetTree().Root.AddChild(instance);
	}
}
