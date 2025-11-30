using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class ActorUI : MonoBehaviour
	{
		// public HpBar HpBar;
		//
		// private IHealth _health;
		//
		// private void Start()
		// {
		// 	IHealth health = GetComponent<IHealth>();
		//
		// 	if (health != null)
		// 		Construct(health);
		// }
		//
		// private void OnDestroy()
		// {
		// 	if (_health != null)
		// 		_health.HealthChanged -= UpdateHpBar;
		// }
		//
		// public void Construct(IHealth health)
		// {
		// 	_health = health;
		// 	_health.HealthChanged += UpdateHpBar;
		// 	_health.MaxHealthChanged += UpdateMaxHpBar;
		// }
		//
		// public void Deconstruct()
		// {
		// 	if (_health != null)
		// 		_health.HealthChanged -= UpdateHpBar;
		// }
		//
		// public void UpdateHpBar()
		// {
		// 	HpBar.SetValue(_health.Current, _health.Max);
		// }
		//
		// private void UpdateMaxHpBar()
		// {
		// 	HpBar.SetMaxValue(_health.Max);
		// }
	}
}