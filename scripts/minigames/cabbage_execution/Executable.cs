using Godot;
using System;

public partial class Executable : Sprite2D
{
	public bool CanBeClicked {get {return canBeClicked;} set {canBeClicked = value;}}
	public bool IsCabbage {get {return isCabbage;}}
	private bool canBeClicked = true;
	private bool isCabbage = false;

	//Checks if the player is clicking on the sprite and acts accordingly
    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseButton eventMouseButton && canBeClicked &&
			Input.IsActionJustPressed("Click") && GetRect().HasPoint(ToLocal(eventMouseButton.Position))){
				GD.Print("You clicked on the executable");//For debugging
				
				//For now we just randomly change the colour of the sprite instead of loading a new one.
				//Green == cabbage.
				Random random = new();
				int index = random.Next(2);
				if(index == 0){
					Modulate = new Color(0, 1, 0);
					isCabbage = true;
				}
				else {
					Modulate = new Color(1, 1, 1);
					isCabbage = false;
				}
			}
    }
}
