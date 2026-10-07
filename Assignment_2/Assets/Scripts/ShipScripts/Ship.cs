using UnityEngine;
using System.Collections;
public abstract class Ship : MonoBehaviour
{
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    public int health;
    [SerializeField] protected float moveSpeed, shootForce, reloadTime;
    [SerializeField] protected float friction;
    protected Vector2 moveDirection;
    protected bool canShoot = true;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        CustomStart();
    }
    abstract protected void CustomStart();
    abstract protected void Move();
    void FixedUpdate() { Move(); }
}
