using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
  Canvas theCanvas;

  Transform target;

  private void Awake()
  {
    target = transform.GetChild(0);
    theCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawLine(transform.position, transform.GetChild(0).position);
    Gizmos.DrawWireSphere(transform.GetChild(0).position, 0.5f);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      other.transform.position = target.position;
      theCanvas.GetComponent<Animator>().Play("Teleport", -1, 0f);
    }
  }

}
