using Godot;
using System;

public partial class Executable : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	//Checks if the player is clicking on the sprite and acts accordingly
    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseButton eventMouseButton && 
			Input.IsActionJustPressed("Click") && GetRect().HasPoint(ToLocal(eventMouseButton.Position))){
				GD.Print("You clicked on the executable");//For debugging
				
				//For now we just randomly change the colour of the sprite instead of loading a new one.
				//Green == cabbage.
				Random random = new();
				int index = random.Next(2);
				if(index == 0){
					Modulate = new Color(0, 1, 0);
				}
			}
    }
}
