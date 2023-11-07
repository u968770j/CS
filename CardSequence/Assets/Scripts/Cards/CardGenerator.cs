using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Card enemyCardPrefab;


    //ÉJÅ[ÉhÇÃê∂ê¨
    public Card Spawn(int number, Battler battler)
    {
        Card card = Instantiate(cardPrefab);
        if (battler.IsPlayer)
        {
            card.Set(Resources.Load<CardBase>($"Data/{TitleManager.playerDeck[number]}"));
        }
        else
        {
            card.Set(Resources.Load<CardBase>($"Data/{TitleManager.enemyDeck[number]}"));
        }
        ChangeCardColor(card);
        return card;
    }

    public Card[] SpawnAllCards()
    {
        CardBase[] cardBases = Resources.LoadAll<CardBase>($"Data/");
        cardBases = cardBases.OrderBy(cardBase => cardBase.CardID).ToArray();
        Card[] card = new Card[cardBases.Length];
        for (int i = 0; i < cardBases.Length; i++)
        {
            card[i] = Instantiate(cardPrefab);
            card[i].Set(cardBases[i]);
            ChangeCardColor(card[i]);
        }
        return card;
    }

    public Card SpawnEnemyCard(int number)
    {
        Card card = Instantiate(enemyCardPrefab);
        card.Set(Resources.Load<CardBase>($"Data/{TitleManager.enemyDeck[number]}"));
        ChangeCardColor(card);
        return card;
    }

    public Card SpawnDeck(int number)
    {
        Card card = Instantiate(cardPrefab);
        card.Set(Resources.Load<CardBase>($"Data/{TitleManager.playerDeck[number]}"));
        ChangeCardColor(card);
        return card;
    }

    public Card SpawnList(int number)
    {
        Card card = Instantiate(cardPrefab);
        card.Set(Resources.Load<CardBase>($"Data/{TitleManager.cardList[number].Base.CardID}"));
        ChangeCardColor(card);
        return card;
    }

    public Card SpawnShop(int number)
    {
        Card card = Instantiate(cardPrefab);
        card.Set(Resources.Load<CardBase>($"Data/{number}"));
        ChangeCardColor(card);
        return card;
    }

    public void ChangeCardColor(Card card)
    {
        Transform frame = card.transform.Find("Canvas/Frame");
        Transform name = frame.transform.Find("Name");
        Transform description = frame.transform.Find("Description");
        switch (card.Base.Rarity)
        {
            case Rarity.Common:
                frame.GetComponent<Image>().color = Color.white;
                name.GetComponent<Image>().color = Color.white;
                description.GetComponent<Image>().color = Color.white;
                break;
            case Rarity.Rare:
                frame.GetComponent<Image>().color = Color.blue;
                name.GetComponent<Image>().color = Color.blue;
                description.GetComponent<Image>().color = Color.blue;
                break;
            case Rarity.Epic:
                frame.GetComponent<Image>().color = Color.magenta;
                name.GetComponent<Image>().color = Color.magenta;
                description.GetComponent<Image>().color = Color.magenta;
                break;
            case Rarity.Legendary:
                frame.GetComponent<Image>().color = Color.yellow;
                name.GetComponent<Image>().color = Color.yellow;
                description.GetComponent<Image>().color = Color.yellow;
                break;
            default:
                frame.GetComponent<Image>().color = Color.white;
                name.GetComponent<Image>().color = Color.white;
                description.GetComponent<Image>().color = Color.white;
                break;
        }
    }

}
