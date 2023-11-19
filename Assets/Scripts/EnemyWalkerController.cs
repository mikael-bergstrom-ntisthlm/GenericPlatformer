using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyWalkerController : MonoBehaviour
{
  [SerializeField]
  float walkSpeed = 1;

  [SerializeField]
  bool autoTurn = false;

  public float XMove { get; set; }

  Rigidbody2D rBody;
  GroundCheckController groundChecker;

  private void Awake()
  {
    rBody = GetComponent<Rigidbody2D>();
    groundChecker = GetComponent<GroundCheckController>();
    XMove = -1;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    // Walking
    rBody.velocity = new Vector2(
      groundChecker.IsGrounded ? XMove * walkSpeed : 0,
      rBody.velocity.y
    );

    // Turn checking
    if (groundChecker.IsGrounded && autoTurn && !Physics2D.OverlapCircle(BottomTurnCheckerPoint, 0.1f))
    {
      XMove = -XMove;
    }
  }

  Vector2 BottomTurnCheckerPoint => new Vector2(
    XMove > 0 ? 
      GetComponent<Collider2D>().bounds.max.x + 0.1f : 
      GetComponent<Collider2D>().bounds.min.x - 0.1f,
    GetComponent<Collider2D>().bounds.min.y - 0.1f
  );

  // Visualizations
  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(BottomTurnCheckerPoint, 0.2f);
  }
}
