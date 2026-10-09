using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // For Unity's new Input System
public class PlayerShip : Ship // This makes it so that PlayerShip inherits methods from the Ship class
{
    private float defaultHealth;
    public static PlayerShip Instance; // We declare a static instance for the player so we can easily find it in the game
    private PlayerInput input;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction shootAction;
    public Animator animator;
    void Awake() // Awake method runs before Start method
    {
        if (Instance == null)
        {
            Instance = this;
        }

        input = new PlayerInput(); // create the Input System instance
        moveAction = input.Player.Move; // we want to reference the Move action from the Player action map. The Move input comes from the Player action map in the Input Actions asset, and a Move InputAction(Vector2) is created that maps WASD or arrow keys into a single direction vector.
        lookAction = input.Player.Look;
        shootAction = input.Player.Attack;
    }
    void OnEnable()
    {
        input.Player.Enable(); // we enable input actions when the object is active
    }
    void OnDisable()
    {
        input.Player.Disable(); // we disable input actions when the object is inactive
    }

    protected override void CustomStart() // Abstract methods need to be overridden and implemented
    {
        defaultHealth = health;
    }
    protected override void Move() // Abstract methods need to be overridden and implemented
    {
        if (moveDirection.magnitude > 0)
        {
            rigidBody.linearVelocity = moveDirection * moveSpeed; // Here we move the object in the direction of motion
        }
        else
        {
            rigidBody.linearVelocity -= rigidBody.linearVelocity * friction; // Here we apply friction when it is not moving
        }
    }
    void Update()
    {
        moveDirection = moveAction.ReadValue<UnityEngine.Vector2>().normalized; // determine where we move based on player input (WASD or arrow keys using Unity's new Input System

        UnityEngine.Vector2 shootDirection = (GetMousePos() - new UnityEngine.Vector2(transform.position.x, transform.position.y)).normalized;
        transform.eulerAngles = new UnityEngine.Vector3(0, 0, -90 + Mathf.Atan2(shootDirection.y, shootDirection.x) * 180 / Mathf.PI);

        if (shootAction.IsPressed() && canShoot)
        {
            StartCoroutine(Shoot(shootDirection, shootForce));
        }

        animator.SetBool("isShooting", shootAction.IsPressed());
    }

    UnityEngine.Vector2 GetMousePos()
    {
        UnityEngine.Vector2 screenPos = lookAction.ReadValue<UnityEngine.Vector2>();
        UnityEngine.Vector3 mousePos = new UnityEngine.Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane);
        UnityEngine.Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return new UnityEngine.Vector2(worldPos.x, worldPos.y);
    }
}