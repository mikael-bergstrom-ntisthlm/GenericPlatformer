using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
  [SerializeField]
  float bottomDeathzoneY = -5;

  Canvas theCanvas;

  Vector3 respawnPosition;

  private void Awake()
  {
    respawnPosition = transform.position;
    theCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.y < bottomDeathzoneY)
    {
      transform.position = respawnPosition;
      GetComponent<DeathAnimController>().Reset();
      if (theCanvas)
      {
        theCanvas.gameObject.SetActive(true);
        Animator deathAnimator = theCanvas.GetComponent<Animator>();
        deathAnimator.Play("Death", -1, 0f);
        print("hey");
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
