using DG.Tweening;
using UnityEngine;

namespace AE.Environment
{
    [RequireComponent(typeof(Light))]
    public class FlickeringPointLight : MonoBehaviour
    {
        [Header("Flickering Point Light Settings")]
        [SerializeField] private float flickerTime = 0.5f;

        [SerializeField] private float flickerMinimumFactor = 0.5f;

        private Light _pointLight;
        private float _initialLightIntensity;
        private float _initialLightRange;

        private void Awake()
        {
            _pointLight = GetComponent<Light>();

            _initialLightIntensity = _pointLight.intensity;
            _initialLightRange = _pointLight.range;

            DOVirtual.Float(1, flickerMinimumFactor, flickerTime, currentFlickerValue =>
            {
                _pointLight.intensity = _initialLightIntensity * currentFlickerValue;
                _pointLight.range = _initialLightRange * currentFlickerValue;
            })
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
        }
    }
}