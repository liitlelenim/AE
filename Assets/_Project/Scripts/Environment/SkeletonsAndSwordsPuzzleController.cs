using UnityEngine;

namespace AE.Environment
{
    public class SkeletonsAndSwordsPuzzleController : MonoBehaviour
    {
        [SerializeField] private CandleStand candleStand;
        private int _alreadyInteractedSkeletons = 0;

        public void MarkPuzzleProgress()
        {
            _alreadyInteractedSkeletons++;
            HandleProgressEffects();
        }

        private void HandleProgressEffects()
        {
            candleStand.SetCurrentCandlesAmount(_alreadyInteractedSkeletons);
        }

        public void FinishLevel()
        {
            
        }
    }
}