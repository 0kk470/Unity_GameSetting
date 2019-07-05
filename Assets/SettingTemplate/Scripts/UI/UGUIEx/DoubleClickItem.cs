using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Networking;

public class DoubleClickItem : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onLeftDoubleClick = new UnityEvent();
    public UnityEvent onMiddleDoubleClick = new UnityEvent();
    public UnityEvent onRightDoubleClick = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        bool bDouble = true;
#if UNITY_STANDALONE || UNITY_EDITOR
        bDouble = eventData.clickCount == 2;
#endif
        if (bDouble)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (onLeftDoubleClick != null)
                    onLeftDoubleClick.Invoke();
            }
            else if(eventData.button == PointerEventData.InputButton.Middle)
            {
                if (onMiddleDoubleClick != null)
                    onMiddleDoubleClick.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (onRightDoubleClick != null)
                    onRightDoubleClick.Invoke();
            }
        }
    }
}
