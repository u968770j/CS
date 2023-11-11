using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardExpantion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform cardPanel; // カードのUI Panel

    private bool isCursorOverCard = false;
    private float cursorOverTime = 0f;
    private float requiredCursorTime = 1f; // カードを拡大するまでの必要なカーソル合わせ時間
    private int defaultSiblingIndex;
    private Transform defaultParent;

    // Update is called once per frame
    void Update()
    {
        // カード上にカーソルがあるかどうかの検出
        if (isCursorOverCard)
        {
            // カーソルがカード上にある時間の計測
            cursorOverTime += Time.deltaTime;

            // 一定時間以上カーソルが合わさっていた場合、カードを拡大
            if (cursorOverTime >= requiredCursorTime)
            {
                ExpandCard();
            }
        }
        else
        {
            // カーソルがカード上にない場合はリセット
            ResetCard();
        }
    }

    // マウスがUI Panelに入った時の処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        isCursorOverCard = true;
        defaultParent = transform.parent.transform;
        defaultSiblingIndex = defaultParent.GetSiblingIndex();
        defaultParent.SetAsLastSibling();
    }

    // マウスがUI Panelから出た時の処理
    public void OnPointerExit(PointerEventData eventData)
    {
        isCursorOverCard = false;
        defaultParent.SetSiblingIndex(defaultSiblingIndex);
    }

    // カードを拡大する処理
    private void ExpandCard()
    {
        // ここにカードを拡大する処理を追加
        transform.localScale = Vector3.one * 1.5f;
        GetComponentInChildren<Canvas>().sortingLayerName = "Card";
    }

    private void ResetCard()
    {
        cursorOverTime = 0f;
        transform.localScale = Vector3.one;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
    }
}
