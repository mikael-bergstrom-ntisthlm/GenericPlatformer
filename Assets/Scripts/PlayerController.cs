using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
  [SerializeField]
  float walkSpeed = 1;

  [SerializeField]
  float jumpForce = 20;

  Rigidbody2D rBody;
  GroundCheckController groundChecker;

  public float XMove { get; set; }

  private void Awake()
  {
    rBody = GetComponent<Rigidbody2D>();
    groundChecker = GetComponent<GroundCheckController>();
  }

  private void Update()
  {
    if (XMove > 0)
    {
      GetComponent<SpriteRenderer>().flipX = false;
    }
    else if (XMove < 0)
    {
      GetComponent<SpriteRenderer>().flipX = true;
    }
  }

  void FixedUpdate()
  {
    if (GetComponent<DeathAnimController>().IsDead) return;
    rBody.velocity = new Vector2(
      XMove * walkSpeed,
      rBody.velocity.y
    );
  }

  // Input checking
  void OnMove(InputValue value)
  {
    XMove = value.Get<Vector2>().x;
  }

  void OnJump()
  {
    if (groundChecker.IsGrounded)
    {
      rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      List<Collider2D> colliders = new();
      if (Physics2D.OverlapBoxAll(BottomBoxPosition, BottomBoxSize, 0).Contains(other.collider))
      {
        rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        other.gameObject.GetComponent<DeathAnimController>().Death();
      }
      else
      {
        GetComponent<DeathAnimController>().Death();
      }

    }
  }

  // Groundcheck box properties
  Vector2 BottomBoxPosition => new Vector2(
      GetComponent<Collider2D>().bounds.center.x,
      GetComponent<Collider2D>().bounds.min.y
    );

  Vector2 BottomBoxSize => new Vector2(
    GetComponent<Collider2D>().bounds.size.x * 0.95f,
    0.1f
  );

  // Visualizations
  private void OnDrawGizmos()
  {
    Gizmos.DrawWireCube(BottomBoxPosition, BottomBoxSize);
  }
}
