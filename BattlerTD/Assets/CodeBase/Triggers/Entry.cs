using System;
using UnityEngine;

namespace CodeBase.Triggers
{
    public class Entry : MonoBehaviour
    {
        public event Action EnterEvent;
        private void OnTriggerEnter(Collider other)
        {
            EnterEvent?.Invoke();
        }
    }
}

