using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace AE.Environment
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GoldenSkullController : MonoBehaviour
    {
        public bool IsReady { get; private set; } = false;

        [Header("Skull Preparation Animation Settings")]
        [SerializeField] private int skullTwitchIntervalMilliseconds = 20;
        [SerializeField] private float initialPositionSetUpDuration = 0.5f;
        [SerializeField] private float initialMaxTwitchingOffset = 0.0025f;
        [Header("Skull Transformation Animation Settings")]
        [SerializeField] private Color skullAfterTransformationColor = Color.yellow;
        [SerializeField] private float colorTransformationDuration = 1f;
        [SerializeField] private float floatingYOffset = 0.5f;
        [SerializeField] private float floatingInterval = 2f;
        
        private MeshRenderer _meshRenderer;
        private Tween _skullFloatingTween;
        private int _currentPuzzleProgress;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = new Material(_meshRenderer.material);
            PerformTwitching().Forget();
        }

        private async UniTask PerformTwitching()
        {
            await UniTask.WaitUntil(() => _currentPuzzleProgress > 0);

            SetSkullInitialTwitchingPosition();
            
            while (!IsReady)
            {
                await UniTask.Delay(skullTwitchIntervalMilliseconds / _currentPuzzleProgress);
                transform.localPosition = 
                    new Vector3(Random.Range(-initialMaxTwitchingOffset, initialMaxTwitchingOffset) * _currentPuzzleProgress,
                                Random.Range(-initialMaxTwitchingOffset, initialMaxTwitchingOffset) * _currentPuzzleProgress,
                                Random.Range(-initialMaxTwitchingOffset, initialMaxTwitchingOffset) * _currentPuzzleProgress);
            }
        }
        public void SetSkullInitialTwitchingPosition()
        {
            transform.DOLocalMove(Vector3.zero, initialPositionSetUpDuration)
                .SetAutoKill(true)
                .Play();
        }
        public void SetCurrentPuzzleProgress(int currentPuzzleProgress)
        {
            _currentPuzzleProgress = currentPuzzleProgress;
        }
        public void MarkAsReady()
        {
            IsReady = true;
            transform.localPosition = Vector3.zero;
            _skullFloatingTween = transform.DOLocalMoveY(floatingYOffset,floatingInterval)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .Play();

            _meshRenderer.material.DOColor(skullAfterTransformationColor, colorTransformationDuration)
                .SetAutoKill()
                .Play();
        }

        public void MarkAsTaken()
        {
            _skullFloatingTween?.Kill();
        }
    }
}
