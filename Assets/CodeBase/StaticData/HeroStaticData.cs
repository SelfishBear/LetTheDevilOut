using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Player", menuName = "Static Data/Player")]
    public class HeroStaticData : ScriptableObject
    {
        [Range(0, 10)] public float MoveSpeed;
        [Range(0, 100)] public float FOV;
        [Range(0, 10)] public float MouseSensitivity;
        [Range(0, 100)] public float MaxLookAngle;
        [Range(0, 100)] public float ZoomFOV;
        [Range(0, 10)] public float SprintSpeed;
        [Range(0, 10)] public float SprintDuration;
        [Range(0, 1)] public float SprintCooldown;
        [Range(0, 100)] public float SprintFOV;
        [Range(0, 50)] public float SprintFOVStepTime;
        
        [Header("Flashlight Settings")]
        [Range(0, 100)] public float BaseRange;
        [Range(0, 100)] public float BaseSpotAngle;
        [Range(0, 100)] public float BaseStepTime;
        
        private float _baseRange = 20f;
        private float _baseSpotAngle = 80f;
        private float _baseStepTime = 5f;


        private float _baseMoveSpeed = 5f;
        private float _baseFOV = 60f;
        private float _baseMouseSensitivity = 2f;
        private float _baseMaxLookAngle = 50f;
        private float _baseZoomFOV = 30f;
        private float _baseSprintSpeed = 7f;
        private float _baseSprintDuration = 5f;
        private float _baseSprintCooldown = 0.5f;
        private float _baseSprintFOV = 80f;
        private float _baseSprintFOVStepTime = 10f;


        public void Reset()
        {
            MoveSpeed = _baseMoveSpeed;
            FOV = _baseFOV;
            MouseSensitivity = _baseMouseSensitivity;
            MaxLookAngle = _baseMaxLookAngle;
            ZoomFOV = _baseZoomFOV;
            SprintSpeed = _baseSprintSpeed;
            SprintDuration = _baseSprintDuration;
            SprintCooldown = _baseSprintCooldown;
            SprintFOV = _baseSprintFOV;
            SprintFOVStepTime = _baseSprintFOVStepTime; 
            
            BaseRange = _baseRange;
            BaseSpotAngle = _baseSpotAngle;
        }
    }
}