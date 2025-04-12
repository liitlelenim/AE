using System;
using AE.Manager.Locator;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AE.Manager
{
    public class FaderManager : MonoBehaviour, IManager
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


        public void FadeIn(Action onFinished = null, float duration = 1f)
        {
            DOVirtual.Color(fadedColor, transparentColor, duration, color => { faderImage.color = color; })
                .SetAutoKill()
                .OnComplete(() => { onFinished?.Invoke(); })
                .Play();
        }

        public void FadeOut(Action onFinished = null, float duration = 1f)
        {
            DOVirtual.Color(transparentColor, fadedColor, duration, color => { faderImage.color = color; })
                .SetAutoKill()
                .OnComplete(() => { onFinished?.Invoke(); })
                .Play();
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