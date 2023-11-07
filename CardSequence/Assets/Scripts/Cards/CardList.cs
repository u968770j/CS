using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CardList : MonoBehaviour
{
    int[] deck = TitleManager.playerDeck;
    List<Card> list = TitleManager.cardList;
    //public bool IsEmpty { get => GameMaster.playerDeck.Length == 0; }

    //list‚É’Ç‰Á‚µ‚ÄŽ©•ª‚ÌŽq—v‘f‚É‚·‚é
    public void AddDeck(int cardID, int order)
    {
        deck[order-1] = cardID;
    }

    public void RemoveDeck(int order)
    {
        deck[order - 1] = -1;
    }

    public bool IsDeckFull()
    {
        return deck.Any(i => i == -1);
    }

    public void AddList(Card card)
    {
        list.Add(card);
    }

    public void RemoveList(Card card)
    {
        list.Remove(card);
    }


    public void SortList()
    {
        list.Sort((card0, card1) => card0.Base.CardID - card1.Base.CardID);
    }

}
