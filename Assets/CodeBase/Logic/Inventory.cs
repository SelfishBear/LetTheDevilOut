using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Inventory : MonoBehaviour
    {
        public Dictionary<int, KeyType> CollectedKeys = new();

        public void AddKey(KeyType keyType)
        {
            int keyId = keyType.GetHashCode();
            if (CollectedKeys.TryAdd(keyId, keyType))
            {
                Debug.Log($"Key of type {keyType} added to inventory.");
            }
        }

        private void Update()
        {
            foreach (KeyValuePair<int, KeyType> collectedKey in CollectedKeys)
            {
                print("Key in inventory: " + collectedKey.Value);
            }
        }
    }
}