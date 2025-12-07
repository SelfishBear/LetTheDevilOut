using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.UI.HUD
{
	public class HUDPrefab : MonoBehaviour
	{
		[field: SerializeField] public SprintBarUI SprintBarUI { get; set; }
		[field: SerializeField] public InteractionUI InteractionUI { get; set; }
	}
}