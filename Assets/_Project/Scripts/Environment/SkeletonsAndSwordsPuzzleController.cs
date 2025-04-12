using System;
using AE.Environment.Interactable;
using UnityEngine;

namespace AE.Environment
{
    public class SkeletonsAndSwordsPuzzleController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CandleStand candleStand;
        [SerializeField] private GoldenSkullController goldenSkullController;

        private int _skeletonsToInteractWith = 0;
        private int _alreadyInteractedSkeletons = 0;

        private void Awake()
        {
            _skeletonsToInteractWith = GetComponentsInChildren<PutSwordInSkeletonInteraction>()?.Length ?? 0;
        }

        public void MarkPuzzleProgress()
        {
            _alreadyInteractedSkeletons++;
            HandleProgressEffects();
        }

        private void HandleProgressEffects()
        {
            candleStand.SetCurrentCandlesAmount(_alreadyInteractedSkeletons);
            goldenSkullController.SetCurrentPuzzleProgress(_alreadyInteractedSkeletons);
            if (_alreadyInteractedSkeletons == _skeletonsToInteractWith)
            {
                goldenSkullController.MarkAsReady();
            }
        }

        public void FinishLevel()
        {
            
        }
    }
}