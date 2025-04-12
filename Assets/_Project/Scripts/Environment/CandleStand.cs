using UnityEngine;

namespace AE.Environment
{
    public class CandleStand : MonoBehaviour
    {
        [Header("Effects References")]
        [SerializeField] private GameObject[] candlesEffects;

        public void SetCurrentCandlesAmount(int amount)
        {
            for (int i = 0; i < candlesEffects.Length; i++)
            {
                candlesEffects[i].SetActive(i < amount);
            }
        }
    }
}
