using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AE.Manager
{
    public class FaderManager : MonoBehaviour
    {
        [Header("Fader Settings")]
        [SerializeField] private bool shouldBeFadedAtStart = true;
        [SerializeField] private Color fadedColor = Color.black;
        [SerializeField] private Color transparentColor = Color.clear;
        [Header("References")]
        [SerializeField] private Image faderImage;

        private void Awake()
        {
            SetInitialFaderColor();
        }


        public void FadeIn(float duration = 1f, Action onFinished = null)
        {
            DOVirtual.Color(fadedColor, transparentColor, duration, color =>
            {
                faderImage.color = color;
            })
            .SetAutoKill()
            .OnComplete(() => {
                onFinished?.Invoke();
            }).Play();
        }
        public void FadeOut(float duration = 1f, Action onFinished = null)
        {
            DOVirtual.Color(fadedColor, transparentColor, duration, color =>
            {
                faderImage.color = color;
            })
            .SetAutoKill()
            .OnComplete(() => {
                onFinished?.Invoke();
            }).Play();
        }

        private void SetInitialFaderColor()
        {
            if (shouldBeFadedAtStart)
            {
                faderImage.color = fadedColor;
                FadeIn();
            }
            else
            {
                faderImage.color = transparentColor;
            }
        }
    }
}
