using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class EndGameUI : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.transform.DOScale(1.2f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}