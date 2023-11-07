using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Transform defaultParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultParent = transform.parent;
        if (defaultParent.name == "ShopPosition" && transform.GetComponent<Card>().Base.Price > TitleManager.money) return;
        Transform canvas = GameObject.Find("Canvas").transform;
        transform.SetParent(canvas, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (defaultParent.name == "ShopPosition" && transform.GetComponent<Card>().Base.Price > TitleManager.money) return;

        Vector2 v = new Vector2(Screen.width / 2, Screen.height / 2);
        transform.position = (eventData.position - v) * 10 / Screen.height;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (defaultParent.name == "ShopPosition" && transform.GetComponent<Card>().Base.Price > TitleManager.money) return;


        if (defaultParent.name == "Content")
        {
            defaultParent.transform.GetComponent<SortCardList>().Sort();
        }

        
        transform.SetParent(defaultParent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.localPosition = new Vector3(0, 0, 0);

    }
}
