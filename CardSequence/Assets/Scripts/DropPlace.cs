using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler
{
    [SerializeField] CardList cardList;
    [SerializeField] DeckUI deckUI;
    [SerializeField] SEManager seManager;
    [SerializeField] AudioClip moneySE;
    [SerializeField] AudioClip dropSE;

    public void OnDrop(PointerEventData eventData)
    {
        CardMovement cardMovement = eventData.pointerDrag.GetComponent<CardMovement>();

        if (cardMovement != null)
        {
            if (cardMovement.defaultParent.name == "ShopPosition")
            {
                if (eventData.pointerDrag.GetComponent<Card>().Base.Price > TitleManager.money)
                {
                    Debug.Log("End:Out of Money!");
                    return;
                }
                else
                {
                    TitleManager.money -= eventData.pointerDrag.GetComponent<Card>().Base.Price;
                    deckUI.ShowMoney();
                    seManager.PlaySE(moneySE);
                }

            }

            if (this.name == "Viewport")
            {
                cardMovement.defaultParent = this.transform.GetChild(0).transform;
            }
            else if (this.name == "Sell")
            {
                cardMovement.defaultParent = this.transform;
                Rarity rarity = eventData.pointerDrag.GetComponent<Card>().Base.Rarity;
                switch (rarity)
                {
                    case Rarity.Common:
                        TitleManager.money += 10;
                        break;
                    case Rarity.Rare:
                        TitleManager.money += 20;
                        break;
                    case Rarity.Epic:
                        TitleManager.money += 30;
                        break;
                    case Rarity.Legendary:
                        TitleManager.money += 50;
                        break;
                    default:
                        break;
                }
                deckUI.ShowMoney();
                Destroy(cardMovement.gameObject);
                seManager.PlaySE(moneySE);
            }
            else
            {
                if (this.transform.childCount > 1)
                {
                    if (cardMovement.defaultParent.name != "Viewport" && cardMovement.defaultParent.name != "NormalCardPosition" && cardMovement.defaultParent.name != "RareCardPosition" && cardMovement.defaultParent.name != "ShopPosition")
                    {
                        this.transform.GetChild(1).SetParent(cardMovement.defaultParent, false);
                    }
                    else
                    {
                        this.transform.GetChild(1).SetParent(cardList.transform, false);
                    }
                    
                }
                cardMovement.defaultParent = this.transform;
            }
            seManager.PlaySE(dropSE);
        }
        
    }
}
