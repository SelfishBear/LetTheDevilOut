using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class StepClimber : MonoBehaviour
    {
        private void OnCollisionStay(Collision other)
        {
            print("Collision detected with " + other.gameObject.name);
        }
    }
}