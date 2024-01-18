using UnityEngine;

namespace CodeBase.FX
{
    public class System : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] smokes;

        private void Start()
        {
            Play();
        }

        public void Play()
        {
            foreach (var smoke in smokes)
            {
                smoke.Play();
            }
        }

        public void Stop()
        {
            foreach (var smoke in smokes)
            {
                smoke.Stop();
            }
        }
    }
}

