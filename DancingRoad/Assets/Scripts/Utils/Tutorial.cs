using UnityEngine;
using DG.Tweening;

namespace Utils
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private GameObject leftArrow;
        [SerializeField] private GameObject rightArrow;
        [SerializeField] private GameObject handGameObjects;

        private void Start()
        {
            LoopArrow(leftArrow.transform, Vector3.left * 20f);
            LoopArrow(rightArrow.transform, Vector3.right * 20f);
            LoopHand(handGameObjects.transform);
        }

        private void LoopArrow(Transform arrow, Vector3 moveOffset)
        {
            arrow.DOLocalMove(arrow.localPosition + moveOffset, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void LoopHand(Transform hand)
        {
            hand.DOScale(1.1f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    
    }
}

