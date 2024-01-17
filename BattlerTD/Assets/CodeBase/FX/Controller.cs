using CodeBase.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.FX
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Entry entry;
        [SerializeField] private System system;

        private void Start()
        {
            entry.EnterEvent += system.Stop;
        }
        private void OnDestroy()
        {
            entry.EnterEvent -= system.Stop;
        }
    }
}
