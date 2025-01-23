using Godot;
using System;

namespace WGJ25 
{
	public static class ObjectManager
	{
		public static Node2D SpawnObject(string scenePath, Vector2 pos, Node parent)
        {
            try
            {
                // Load and instantiate the item scene
                PackedScene scene = (PackedScene)GD.Load(scenePath);
                Node2D instance = scene.Instantiate<Node2D>();
                parent.CallDeferred("add_child", instance);

                // Set postion
                instance.Position = pos;

                return instance;
            }
            catch (Exception e)
            {
                GD.PrintErr(e);
                return null;
            }
        }
	}
}