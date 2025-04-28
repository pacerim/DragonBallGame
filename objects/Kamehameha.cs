using Godot;
using System;

public partial class Kamehameha : Area2D
{
	
	[Export] public float Speed = 600.0f; // Velocidad del proyectil
	[Export] public int Damage = 25; // Daño que inflige el Kamehameha

	private Vector2 _direction = Vector2.Right; // Dirección del Kamehameha (por defecto hacia la derecha)
	// Called when the node enters the scene tree for the first time.
	public override void _PhysicsProcess(double delta)
	{
		// Mueve el Kamehameha en la dirección establecida
		Position += _direction * Speed * (float)delta;

		// Opcional: Eliminar si sale de los límites del mapa
		if (Position.X > 2000 || Position.X < -2000) // Ajusta el rango según el tamaño de tu mapa
		{
			
			QueueFree();
		}
	
	}
	
	 public void SetDirection(Vector2 direction)
	{
		_direction = direction.Normalized(); // Establece la dirección del proyectil
	}

	private void OnBodyEntered(Node body)
	{
		// Detectar colisión con otros objetos y aplicar daño
		if (body is Vegetta vegetta) // Cambia 'Vegetta' por la clase de tu objetivo
		{
			vegetta.TakeDamage(Damage);
			QueueFree(); // Destruye el Kamehameha después de colisionar
		}
		else
		{
			// Destruye el Kamehameha si choca con algo que no sea Vegetta
			QueueFree();
		}
	}
}
