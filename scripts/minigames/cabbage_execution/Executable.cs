using Godot;
using System;

public partial class Executable : RigidBody2D
{
	public bool CanBeClicked {get {return canBeClicked;} set {canBeClicked = value;}}
	public bool IsCabbage {get {return isCabbage;}}
	public bool Executed {get {return executed;} set {executed = value;}}
	private bool canBeClicked = true;
	private bool isCabbage = false;
    private bool executed = false;

    public override void _Process(double delta)
    {
        if(executed){
			//this.GravityScale = 1f;
			//this.ApplyImpulse(new Vector2(0, -20));
		}
    }

    //Checks if the player is clicking on the sprite and acts accordingly

    public override void _Input(InputEvent @event)
    {
		//Got rid of clicking on the executable to change it. Its quicker and more fun just to press the
		//movement buttons
        if(canBeClicked && !executed && (Input.IsActionJustPressed("move_left") || Input.IsActionJustPressed("move_right"))){
				GD.Print("You clicked on the executable");//For debugging
				
				GetNextExecutable();
			}
	}

	public void GetNextExecutable(){
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
