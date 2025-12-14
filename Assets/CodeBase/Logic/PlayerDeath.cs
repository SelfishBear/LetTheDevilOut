using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerDeath : MonoBehaviour
    {
        private const string InitialPointTag = "InitialPoint";

        [SerializeField] private Camera _playerCamera;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private CharacterController _characterController;
        
        private GameObject _playerInitialPoint;

        private void Awake()
        {
            _playerInitialPoint = GameObject.FindWithTag(InitialPointTag);
        }

        private void Start()
        {
            _playerHealth.OnDeath += OnDeath;
        }

        private void OnDestroy()
        {
            _playerHealth.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            StartCoroutine(DeathCoroutine());
        }

        public IEnumerator DeathCoroutine()
        {
            _playerCamera.cullingMask = 0;
            _characterController.enabled = false;
            transform.position = _playerInitialPoint.transform.position;
            _characterController.enabled = true;
            
            yield return new WaitForSeconds(2f);
            _playerCamera.cullingMask = -1;
            _playerHealth.CurrentHealth = 100f;
        }
    }
}