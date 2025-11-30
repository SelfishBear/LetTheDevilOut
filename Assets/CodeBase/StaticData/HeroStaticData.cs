using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Player", menuName = "Static Data/Player")]
    public class HeroStaticData : ScriptableObject
    {
        [Range(10, 100)] public float WeightCapacity;
        [Range(0f, 2f)] public float StaminaRegenRate;
        [Range(0f, 2f)] public float HealthRegenRate;
        [Range(1, 10)] public float ScytheRadius;
        [Range(1, 10)] public float WalkSpeed;
        [Range(1, 20)] public float SprintSpeed;
        [Range(1, 20)] public float JumpForce;
        [Range(25, 200)] public float MaxHealth;
        [Range(1f, 20f)] public float MaxStamina;

        private readonly float _baseHealthRegenRate = 0.2f;
        private readonly float _baseJumpForce = 5f;
        private readonly float _baseMaxHealth = 100f;
        private readonly float _baseMaxStamina = 3f;

        private readonly float _baseScytheRadius = 3f;
        private readonly float _baseSprintSpeed = 1f;
        private readonly float _baseStaminaRegenRate = 0.2f;
        private readonly float _baseWalkSpeed = 4f;
        private readonly float _baseWeightCapacity = 20f;

        public void Reset()
        {
            ScytheRadius = _baseScytheRadius;
            WalkSpeed = _baseWalkSpeed;
            SprintSpeed = _baseSprintSpeed;
            JumpForce = _baseJumpForce;
            MaxHealth = _baseMaxHealth;
            MaxStamina = _baseMaxStamina;
            WeightCapacity = _baseWeightCapacity;
            StaminaRegenRate = _baseStaminaRegenRate;
            HealthRegenRate = _baseHealthRegenRate;
        }
    }
}