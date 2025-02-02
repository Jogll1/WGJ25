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
	private Sprite2D sprite;
	private Texture2D cabbage;
	private Texture2D noble;
    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
		cabbage = GD.Load<Texture2D>("res://art/minigames/cabbage/Cabbage.png");
		noble = GD.Load<Texture2D>("res://art/minigames/cabbage/noble.png");
		ProcessMode = Node.ProcessModeEnum.Pausable;
		GetNextExecutable();
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
			sprite.Texture = cabbage;
			isCabbage = true;
		}
		else {
			sprite.Texture = noble;
			isCabbage = false;
		}
	}
}
