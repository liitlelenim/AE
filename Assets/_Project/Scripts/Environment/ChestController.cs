using DG.Tweening;
using UnityEngine;

namespace AE.Environment
{
    public class ChestController : MonoBehaviour
    {
        [Header("Opening Sequence Settings")] 
        [SerializeField] private Transform keySocketTransform;
        [SerializeField] private float keyMovementDuration = 0.5f; 
       
        [Space]
        
        [SerializeField] private Transform chestLidTransform;
        [SerializeField] private float lidOpeningAngle = 45f;
        [SerializeField] private float lidOpeningDuration = 1f;
        
        [Header("References")]
        [SerializeField] private SkeletonsAndSwordsPuzzleController skeletonsAndSwordsPuzzleController;
        public void OpenChest(GameObject keyToAnimate)
        {
            Sequence openingChestSequence = DOTween.Sequence();
            keyToAnimate.transform.SetParent(keySocketTransform,true);
            openingChestSequence.Append(keyToAnimate.transform.DOLocalMove(Vector3.zero,keyMovementDuration));
            openingChestSequence.Join(keyToAnimate.transform.DOLocalRotate(Vector3.zero, keyMovementDuration));
            
            openingChestSequence.Append(chestLidTransform.transform.DOLocalRotate(Vector3.right * -lidOpeningAngle,lidOpeningDuration));
            openingChestSequence.OnComplete(OnChestOpeningComplete);
            openingChestSequence.Play();
        }

        private void OnChestOpeningComplete()
        {
            skeletonsAndSwordsPuzzleController.FinishLevel();
        }
        
    }
}