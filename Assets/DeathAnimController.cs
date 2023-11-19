using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DeathAnimController : MonoBehaviour
{
  Rigidbody2D rBody;
  SpriteRenderer spriteRenderer;

  [SerializeField]
  float deathJumpForce = 20;

  [SerializeField]
  LayerMask layersToIgnore;

  LayerMask excludedLayersDefault;

  public bool IsDead { get; private set; }

  private void Update()
  {
    if (rBody.velocity.y < 0 && IsDead)
    {
      spriteRenderer.flipY = true;
    }
  }

  private void Awake()
  {
    rBody = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void Death()
  {
    IsDead = true;
    excludedLayersDefault = rBody.excludeLayers;
    rBody.excludeLayers = layersToIgnore;
    rBody.AddForce(Vector2.up * deathJumpForce, ForceMode2D.Impulse);
  }

  public void Reset()
  {
    rBody.excludeLayers = excludedLayersDefault;
    IsDead = false;
    spriteRenderer.flipY = false;
  }
}
