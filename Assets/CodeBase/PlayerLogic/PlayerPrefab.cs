using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
    public class PlayerPrefab : MonoBehaviour
    {
        [field: SerializeField] public PlayerSprint PlayerSprint { get; set; }
        [field: SerializeField] public Interactor Interactor { get; set; }
    }
}