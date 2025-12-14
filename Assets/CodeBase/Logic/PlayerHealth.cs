using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float CurrentHealth { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; }

        public event Action OnHealthChanged;
        public event Action OnDeath;
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            OnHealthChanged?.Invoke();
            if (CurrentHealth <= 0)
            {
                print("Player is dead");
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }
        }
    }
}