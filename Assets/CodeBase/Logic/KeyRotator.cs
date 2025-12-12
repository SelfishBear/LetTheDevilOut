using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class KeyRotator : MonoBehaviour
    {
        [SerializeField] private Key _key;

        private void Update()
        {
            RotateKey();
        }

        private void RotateKey()
        {
            Quaternion rotation = transform.rotation;
            rotation *= Quaternion.Euler(0, 90 * Time.deltaTime, 0);
            transform.rotation = rotation;
        }
    }
}