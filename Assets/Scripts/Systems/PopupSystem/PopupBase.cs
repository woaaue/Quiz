using UnityEngine;
using JetBrains.Annotations;

public class PopupBase : MonoBehaviour
{
    [field: SerializeField] public PopupAnimator Animator { get; private set; }

    [UsedImplicitly]
    public void ClosePopup()
    {

    }

    private void Destroy()
    {
        EventSystem.Invoke(new HidePopupEvent());
        Destroy(gameObject);
    }
}
