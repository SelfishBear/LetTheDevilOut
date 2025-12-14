using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }
        public void TakeDamage(float damage);
        public event Action OnHealthChanged;
        public event Action OnDeath;
    }
}