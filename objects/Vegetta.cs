using Godot;
using System;


public partial class Vegetta : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private ProgressBar _healthBar;
	private Button _buttonUp1HP;
	private Button _buttonDown1HP;
	private CollisionShape2D _collisionShape2D;
	
	private CollidersScriptV _collidersScript ;
	
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public Vector2 ScreenSize; //Size of the game window.
	public int MaxHealth = 100;
	public int CurrentHealth;
	public bool isAlive;
	public Label deadMessage;
	
	public string _currentState = "idle";
	public bool _isAttacking = false;
	
	// Se llama cuando el nodo se crea por primera vez. Usado por ejemplo, para inicializar variables
	public override void _Ready(){
		
		_collidersScript = GetNode<CollidersScriptV>("Colliders");
		GD.Print(_collidersScript != null ? "Correcto" : "No correcto");
		
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_healthBar = GetNode<ProgressBar>("HealthBar");
		_buttonUp1HP = GetNode<Button>("Button");
		_buttonDown1HP = GetNode<Button>("Button2");
		_collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
		deadMessage = GetNode<Label>("DeadVegetta");
		GD.Print(deadMessage != null ? "siii" : "Noooo");
		// Inicializar la salud
		CurrentHealth = MaxHealth;
		_healthBar.MaxValue = MaxHealth;
		_healthBar.Value = CurrentHealth;
		
		// Conectar las señales de los botones a los métodos correspondientes
		_buttonUp1HP.Connect("pressed", new Callable(this, nameof(OnButtonUp1HPPressed)));
		_buttonDown1HP.Connect("pressed", new Callable(this, nameof(OnButtonDown1HPPressed)));
		
		isAlive = true;
		deadMessage.Visible = false;
		
		
	
		// Conectar la señal de colisión
		_collisionShape2D.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		
		
		// ----- COLLIDER MANAGING ---------
		_collidersScript.HandleAllColliderDisabling(true);
		 
		
	}

	// Proceso de físicas con cada tecla de movimiento pulsada 
	public override void _PhysicsProcess(double delta)
	{
	
		if(_isAttacking){
			// Si está atacando no permitir otros movimientos
			return;
		}
		Vector2 velocity = Velocity;

		
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			if(velocity.Y > 0){
				_currentState = "falling";
				_animatedSprite.Play("falling");
			} else if(Velocity.Y < 0){
				_currentState = "jumping";
				_animatedSprite.Play("jumping");
			}
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("move_upV") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			
		}
		
		
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_leftV", "move_rightV", "move_upV", "move_downV");
		
		
		
		// Player is pressing any movement input. If it was ==, it indicates player is idle
		if (direction != Vector2.Zero)
		{
			
			velocity.X = direction.X * Speed;
			_currentState = "move";
			if(IsOnFloor()){
				_animatedSprite.Play("move_right");
				
			}
			_animatedSprite.FlipH = velocity.X < 0;
		}
		else
		{
			
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if(IsOnFloor()){
				_animatedSprite.Play("idle");
			}
			
		}

		
		Velocity = velocity;
		MoveAndSlide();
		
		if (Input.IsActionJustPressed("punchV"))
		{
			PerformAttack("punch");
		}
		if (Input.IsActionJustPressed("kickV"))
		{
			PerformAttack("kick");
		}
		if (Input.IsActionJustPressed("blast"))
		{
			PerformAttack("blast");
		}
	}
	
	private async void PerformAttack(string attackType)
	{
		_isAttacking = true; // Bloquear otras acciones
		_animatedSprite.Play(attackType);
		GD.Print($"Realizando ataque: {attackType}");

 		bool isFacingLeft = _animatedSprite.FlipH;
		switch (attackType)
		{
			case "punch":
				_collidersScript.HandleSpecificColliderDisabling(false, "LeftHandCollider");
				_collidersScript.HandleSpecificColliderDisabling(false, "RightHandCollider");
				
				if (isFacingLeft)
				{
					_collidersScript.UpdateColliderPosition("LeftHandCollider", new Vector2(-10, 0));
					_collidersScript.UpdateColliderPosition("RightHandCollider", new Vector2(-10, 0)); 
				}else{
					_collidersScript.UpdateColliderPosition("LeftHandCollider", new Vector2(10, 0));
					_collidersScript.UpdateColliderPosition("RightHandCollider", new Vector2(10, 0)); 
				}
				break;
			case "kick":
				_collidersScript.HandleSpecificColliderDisabling(false, "LeftFootCollider");
				_collidersScript.HandleSpecificColliderDisabling(false, "RightFootCollider");
				if(isFacingLeft){
					_collidersScript.UpdateColliderPosition("LeftFootCollider", new Vector2(-13, 0)); 
					_collidersScript.UpdateColliderPosition("RightFootCollider", new Vector2(-10, 0)); 
				}else{
					 _collidersScript.UpdateColliderPosition("LeftFootCollider", new Vector2(13, 0)); 
					_collidersScript.UpdateColliderPosition("RightFootCollider", new Vector2(10, 0));
				}
				 
				break;
			case "blast":
				_collidersScript.HandleSpecificColliderDisabling(false, "LeftHandCollider");
				_collidersScript.HandleSpecificColliderDisabling(false, "RightHandCollider");
				_collidersScript.HandleSpecificColliderDisabling(false, "LeftFootCollider");
				_collidersScript.HandleSpecificColliderDisabling(false, "RightFootCollider");
				
					_collidersScript.UpdateColliderPosition("LeftHandCollider", new Vector2(-60, 10));
					_collidersScript.UpdateColliderPosition("RightHandCollider", new Vector2(60, 10)); 
					_collidersScript.UpdateColliderPosition("LeftFootCollider", new Vector2(-60, -60)); 
					_collidersScript.UpdateColliderPosition("RightFootCollider", new Vector2(60, -60));
				
				break;
		}
		
		// Esperar a que la animación termine
		await ToSignal(_animatedSprite, "animation_finished");
		
		// Restaurar las posiciones originales de los colliders
		_collidersScript.ResetColliderPosition("LeftHandCollider");
		_collidersScript.ResetColliderPosition("RightHandCollider");
		_collidersScript.ResetColliderPosition("LeftFootCollider");
		_collidersScript.ResetColliderPosition("RightFootCollider");
		
		_collidersScript.HandleAllColliderDisabling(true);
		
		_isAttacking = false; // Desbloquear movimientos
		_currentState = "idle"; // Volver al estado base
	}
	
	
	// ------------------------ DAMAGE AND COLLIDERS ------------------------------
	
	// Métodos para manejar los botones
	private void OnButtonUp1HPPressed()
	{
		CurrentHealth += 1;
		if (CurrentHealth > MaxHealth)
		{
			CurrentHealth = MaxHealth;
		}
		_healthBar.Value = CurrentHealth;
	}

	private void OnButtonDown1HPPressed()
	{
		CurrentHealth -= 1;
		if (CurrentHealth < 0)
		{
			CurrentHealth = 0;
		}
		_healthBar.Value = CurrentHealth;

		if (CurrentHealth == 0)
		{
			Dead();
		}
	}
	public void TakeDamage(int damage){
		CurrentHealth -= damage;
		if(CurrentHealth < 0){
			CurrentHealth = 0;
		}
		_healthBar.Value = CurrentHealth;
		
		if(CurrentHealth == 0){
			// Manejar la muerte del personaje
			Dead();
		}
		GD.Print("Vegetta ha recibido daño");
	}
	
	
	public void Dead(){
		GD.Print("Vegetta ha muerto");
		isAlive = false;
		deadMessage.Visible = true;
	}
	
	public void resetHealth(){
		CurrentHealth = MaxHealth;
		isAlive = true;
		_healthBar.Value = MaxHealth;
	}
	
	public void OnBodyEntered(Node body){
		// When one player collides with the other, coming from an attack it takes health from the attacked
		GD.Print("Vegetta entra en contacto con Goku");
		if (body is Goku)
		{
			TakeDamage(10); // Ajusta el valor del daño según sea necesario
		}
	}
	
	
	
	
}
