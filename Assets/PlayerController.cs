using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  float walkSpeed = 1;

  [SerializeField]
  LayerMask groundLayer;

  Rigidbody2D rBody;
  float xSpeed = 0;

  bool isGrounded = false;

  private void Awake()
  {
    rBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    rBody.velocity = new Vector2(
      xSpeed * walkSpeed,
      rBody.velocity.y
    );
  }

  void FixedUpdate()
  {
    // Ground checking
    isGrounded = Physics2D.OverlapBox(BottomBoxPosition, BottomBoxSize, 0, groundLayer);
  }

  void OnMove(InputValue value)
  {
    xSpeed = value.Get<Vector2>().x;
  }

  void OnJump()
  {
    print("yes");
  }

  Vector2 BottomBoxPosition => new Vector2(
      GetComponent<Collider2D>().bounds.center.x,
      GetComponent<Collider2D>().bounds.min.y
    );

  Vector2 BottomBoxSize => new Vector2(
    GetComponent<Collider2D>().bounds.size.x * 0.95f,
    0.1f
  );

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireCube(BottomBoxPosition, BottomBoxSize);
  }
}
