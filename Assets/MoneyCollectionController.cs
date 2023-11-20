using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class MoneyCollectionController : MonoBehaviour
{
  [SerializeField]
  int money = 0;

  [SerializeField]
  TMP_Text moneyTextBox;

  private void Awake()
  {
    UpdateMoneyBox();
  }

  void UpdateMoneyBox()
  {
    moneyTextBox.text = money.ToString();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.TryGetComponent<MoneyController>(out MoneyController moneyController))
    {
      money += moneyController.value;
      UpdateMoneyBox();
    }
  }
}
