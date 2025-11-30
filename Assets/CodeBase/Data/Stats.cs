using System;
using CodeBase.StaticData;

namespace CodeBase.Data
{
	[Serializable]
	public class Stats
	{
		private float _baseDamage;
		private float _baseHealthRegenRate;
		private float _baseJumpForce;
		private float _baseMaxHealth;
		private float _baseMaxStamina;
		private float _baseScytheRadius;
		private float _baseSprintSpeed;
		private float _baseStaminaRegenRate;
		private float _baseWalkSpeed;
		private float _baseWeightCapacity;
		private float _damage;
		private float _healthRegenRate;
		private float _jumpForce;
		private float _maxHealth;
		private float _maxStamina;
		private float _scytheRadius;
		private float _sprintSpeed;
		private float _staminaRegenRate;
		private float _walkSpeed;
		private float _weightCapacity;
		public Action OnDamageChanged;
		public Action<float> OnHealthRegenRateChanged;
		public Action<float> OnJumpForceChanged;
		public Action<float> OnMaxHealthChanged;
		public Action<float> OnMaxStaminaChanged;
		public Action<float> OnScytheRadiusChanged;
		public Action<float> OnSprintSpeedChanged;
		public Action<float> OnStaminaRegenRateChanged;
		public Action<float> OnWalkSpeedChanged;
		public Action<float> OnWeightCapacityChanged;

		public float WeightCapacity
		{
			get
			{
				return _weightCapacity;
			}
			set
			{
				if (value != _weightCapacity)
					OnWeightCapacityChanged?.Invoke(value);

				_weightCapacity = value;
			}
		}

		public float HealthRegenRate
		{
			get
			{
				return _healthRegenRate;
			}
			set
			{
				if (value != _healthRegenRate)
					OnHealthRegenRateChanged?.Invoke(value);

				_healthRegenRate = value;
			}
		}

		public float StaminaRegenRate
		{
			get
			{
				return _staminaRegenRate;
			}
			set
			{
				if (value != _staminaRegenRate)
					OnStaminaRegenRateChanged?.Invoke(value);

				_staminaRegenRate = value;
			}
		}

		public float MaxStamina
		{
			get
			{
				return _maxStamina;
			}
			set
			{
				if (value != _maxStamina)
					OnMaxStaminaChanged?.Invoke(value);

				_maxStamina = value;
			}
		}
		public float MaxHealth
		{
			get
			{
				return _maxHealth;
			}
			set
			{
				if (value != _maxHealth)
					OnMaxHealthChanged?.Invoke(value);

				_maxHealth = value;
			}
		}

		public float ScytheRadius
		{
			get
			{
				return _scytheRadius;
			}
			set
			{
				if (value != _scytheRadius)
					OnScytheRadiusChanged?.Invoke(value);

				_scytheRadius = value;
			}
		}
		public float WalkSpeed
		{
			get
			{
				return _walkSpeed;
			}
			set
			{
				if (value != _walkSpeed)
					OnWalkSpeedChanged?.Invoke(value);

				_walkSpeed = value;
			}
		}
		public float JumpForce
		{
			get
			{
				return _jumpForce;
			}
			set
			{
				if (value != _jumpForce)
					OnJumpForceChanged?.Invoke(value);

				_jumpForce = value;
			}
		}
		public float SprintSpeed
		{
			get
			{
				return _sprintSpeed;
			}
			set
			{
				if (value != _sprintSpeed)
					OnSprintSpeedChanged?.Invoke(value);

				_sprintSpeed = value;
			}
		}
		public float Damage
		{
			get
			{
				return _damage;
			}
			set
			{
				if (value != _damage)
					OnDamageChanged?.Invoke();

				_damage = value;
			}
		}

		public void Reset()
		{
			_maxHealth = _baseMaxHealth;
			_maxStamina = _baseMaxStamina;
			_scytheRadius = _baseScytheRadius;
			_walkSpeed = _baseWalkSpeed;
			_jumpForce = _baseJumpForce;
			_sprintSpeed = _baseSprintSpeed;
			_damage = _baseDamage;
			_weightCapacity = _baseWeightCapacity;
			_healthRegenRate = _baseHealthRegenRate;
			_staminaRegenRate = _baseStaminaRegenRate;
		}

		public void SetBaseStats(HeroStaticData heroStaticData)
		{
			_baseDamage = heroStaticData.MaxHealth;
			_baseJumpForce = heroStaticData.JumpForce;
			_baseMaxHealth = heroStaticData.MaxHealth;
			_baseMaxStamina = heroStaticData.MaxStamina;
			_baseScytheRadius = heroStaticData.ScytheRadius;
			_baseSprintSpeed = heroStaticData.SprintSpeed;
			_baseWalkSpeed = heroStaticData.WalkSpeed;
			_baseWeightCapacity = heroStaticData.WeightCapacity;
			_baseHealthRegenRate = heroStaticData.HealthRegenRate;
			_baseStaminaRegenRate = heroStaticData.StaminaRegenRate;
		}
	}
}