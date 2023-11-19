using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
  [SerializeField]
  float bottomDeathzoneY = -5;

  [SerializeField]
  Canvas deathCanvas;

  Vector3 respawnPosition;

  private void Awake()
  {
    respawnPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.y < bottomDeathzoneY)
    {
      transform.position = respawnPosition;
      GetComponent<DeathAnimController>().Reset();
      if (deathCanvas)
      {
        deathCanvas.gameObject.SetActive(true);
        Animator deathAnimator = deathCanvas.GetComponent<Animator>();
        deathAnimator.Play("CanvasDeath", -1, 0f);
      }
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawLine(
      new Vector3(
        transform.position.x - 10,
        bottomDeathzoneY
      ),
      new Vector3(
        transform.position.x + 10,
        bottomDeathzoneY
      )
    );
  }

}
