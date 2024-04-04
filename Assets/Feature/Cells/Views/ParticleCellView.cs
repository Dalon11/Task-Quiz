using UnityEngine;

namespace Quiz.Cells.Views
{
    public class ParticleCellView
    {
        private readonly ParticleSystem _particle;

        public ParticleCellView(ParticleSystem particl) => _particle = particl;

        public void PlayParticle() => _particle.Play();
    }
}
