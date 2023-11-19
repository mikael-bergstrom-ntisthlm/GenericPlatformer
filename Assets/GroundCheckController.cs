using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundCheckController : MonoBehaviour
{
  [SerializeField]
  LayerMask groundLayer;

  public bool IsGrounded { get; set; }

  // Update is called once per frame
  void FixedUpdate()
  {
    // Ground checking
    IsGrounded = Physics2D.OverlapBox(BottomBoxPosition, BottomBoxSize, 0, groundLayer);
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
