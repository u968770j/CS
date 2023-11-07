using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;
    public bool IsSubmitted { get; private set; }
    public UnityAction OnSubmitAction;

    public BattlerHand Hand { get => hand; }
    public Card SubmitCard { get => submitPosition.SubmitCard; }
    public int Life { get; set; }

    public bool IsPlayer { get; set; }

    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        if (IsSubmitted)
        {
            return;
        }
        //すでにセットしていれば，手札に戻す
        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);
        submitPosition.Set(card);
        hand.ResetPosition();
    }



    public void RandomSubmit()
    {
        //手札からランダムでカードを抜きとる
        Card card = hand.RandomRemove();
        //提出用にset
        submitPosition.Set(card);
        //提出GameMasterに通知する
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
        hand.ResetPosition();
    }

    public void OrderedSubmit()
    {
        //手札からランダムでカードを抜きとる
        Card card = hand.OrderedRemove();
        //提出用にset
        submitPosition.Set(card);
        //提出GameMasterに通知する
        IsSubmitted = true;
        //OnSubmitAction?.Invoke();
        hand.ResetPosition();
    }

    public void SetupNextTurn()
    {
        IsSubmitted = false;
        submitPosition.DeleteCard();
    }

}
