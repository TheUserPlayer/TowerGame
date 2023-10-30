using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(Attack))]
  public class CheckAttackRange : MonoBehaviour
  {
    public Attack _enemyMeleeAttack;
    public TriggerObserver TriggerObserver;

    private void Start()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerStay += TriggerStay;
      TriggerObserver.TriggerExit += TriggerExit;
      
      _enemyMeleeAttack.DisableAttack();
    }

    private void OnDestroy()
    {
      TriggerObserver.TriggerEnter -= TriggerEnter;
      TriggerObserver.TriggerStay -= TriggerStay;
      TriggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerStay(Collider obj)
    {
      _enemyMeleeAttack.EnableAttack();
    }

    private void TriggerExit(Collider obj)
    {
      _enemyMeleeAttack.DisableAttack();
    }

    private void TriggerEnter(Collider obj)
    {
      _enemyMeleeAttack.EnableAttack();
    }
  }
}