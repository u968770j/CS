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
        //���łɃZ�b�g���Ă���΁C��D�ɖ߂�
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
        //��D���烉���_���ŃJ�[�h�𔲂��Ƃ�
        Card card = hand.RandomRemove();
        //��o�p��set
        submitPosition.Set(card);
        //��oGameMaster�ɒʒm����
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
        hand.ResetPosition();
    }

    public void OrderedSubmit()
    {
        //��D���烉���_���ŃJ�[�h�𔲂��Ƃ�
        Card card = hand.OrderedRemove();
        //��o�p��set
        submitPosition.Set(card);
        //��oGameMaster�ɒʒm����
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
