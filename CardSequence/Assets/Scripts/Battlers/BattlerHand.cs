using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    public List<Card> list = new List<Card>();

    public bool IsEmpty { get => list.Count == 0;  }

    //listに追加して自分の子要素にする
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(transform);
    }

    public void Remove(Card card)
    {
        list.Remove(card);
    }

    //手札を整頓する
    public void ResetPosition()
    {
        //ソートする
        //list.Sort((card0, card1) => card0.Base.CardID - card1.Base.CardID);
        for (int i = 0; i < list.Count; i++)
        {
            float posX = (i - list.Count/2) * 0.7f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }

    public Card RandomRemove()
    {
        int r = Random.Range(0, list.Count);
        Card card = list[r];
        Remove(card);
        return card;
    }

    public Card OrderedRemove()
    {
        Card card = list[0];
        Remove(card);
        return card;
    }
}
