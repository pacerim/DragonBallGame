using Godot;
using System;
using System.Collections.Generic;

public partial class CollidersScriptG : Node2D
{
	
	public Dictionary<String, CollisionShape2D> GokuColliders = new Dictionary<String, CollisionShape2D>();
	private Dictionary<string, Vector2> originalPositions = new Dictionary<string, Vector2>();
	
	public bool disableColliders = true;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (CollisionShape2D node in GetTree().GetNodesInGroup("Goku Hand Feet Colliders"))
		{
			node.SetDisabled(true);
			GokuColliders[node.Name] =node;
			originalPositions[node.Name] = node.Position;
		}
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnLeftHandBodyEntered(Node2D body){
		
		if(body is Vegetta vegetta){
			GD.Print("Left hand collider entered body: " + body.Name);
			vegetta.TakeDamage(10);
		}
	}
	
	public void OnRightHandBodyEntered(Node2D body){
		if(body is Vegetta vegetta){
			GD.Print("Right hand collider entered body: " + body.Name);
			vegetta.TakeDamage(10);
		}
	}
	private void OnLeftFootBodyEntered(Node2D body)
	{
		if(body is Vegetta vegetta){
			GD.Print("Left foot collider entered body: " + body.Name);

			vegetta.TakeDamage(10);
		}
	}

	private void OnRightFootBodyEntered(Node2D body)
	{
		if(body is Vegetta vegetta){
			GD.Print("Right foot collider entered body: " + body.Name);

			vegetta.TakeDamage(10);
		}
	}
	
	public void HandleAllColliderDisabling(bool isDisabled){
		
		foreach (var collider in GokuColliders.Values)
		{
			
			collider.SetDisabled(isDisabled);
			GD.Print(collider.Name + (collider.Disabled ? " is disabled" : " is NOT disabled"));
			
		}
	}
	
	public void HandleSpecificColliderDisabling(bool isDisabled, string pickedColl){
		if( GokuColliders.ContainsKey(pickedColl)){
			 GokuColliders[pickedColl].SetDisabled(isDisabled);
			GD.Print( GokuColliders[pickedColl].Name + ( GokuColliders[pickedColl].Disabled ? " has been DISabled" : " has been enabled"));
		}else{
			GD.Print("Collider " + pickedColl + " not found.");
		}		
		
	}
	
	public void ResetColliderPosition(string colliderName)
	{
		if (GokuColliders.ContainsKey(colliderName) && originalPositions.ContainsKey(colliderName))
		{
			GokuColliders[colliderName].Position = originalPositions[colliderName];
			GD.Print("Collider " + colliderName + " position reset to " + originalPositions[colliderName]);
		}
		else
		{
			GD.Print("Collider " + colliderName + " not found or original position not stored.");
		}
	}
	
	public void UpdateColliderPosition(string colliderName, Vector2 newPosition)
	{
		if ( GokuColliders.ContainsKey(colliderName))
		{
			 GokuColliders[colliderName].Position = newPosition;
			GD.Print("Collider " + colliderName + " position updated to " + newPosition);
		}
		else
		{
			GD.Print("Collider " + colliderName + " not found.");
		}
	}
}
