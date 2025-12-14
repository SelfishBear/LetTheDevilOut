using CodeBase.Audio;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
    public class PlayerPrefab : MonoBehaviour
    {
        [field: SerializeField] public PlayerSprint PlayerSprint { get; set; }
        [field: SerializeField] public Interactor Interactor { get; set; }
        [field: SerializeField] public SFXAudioSources SfxAudioSources { get; set; }
        [field: SerializeField] public MusicAudioSource MusicAudioSource { get; set; }
        [field: SerializeField] public PlayerHealth PlayerHealth { get; set; }
        
    }
}