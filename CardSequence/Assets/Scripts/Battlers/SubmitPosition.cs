using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmitPosition : MonoBehaviour
{
    //�I�����ꂽ�J�[�h���Ǘ�����
    Card submitCard;

    public Card SubmitCard { get => submitCard; }

    //�����̎q�v�f�ɂ���C�ʒu�������̏ꏊ�ɂ���
    public void Set(Card card)
    {
        submitCard = card;
        card.transform.SetParent(transform);
        card.transform.DOMove(transform.position, 0.1f);
        //card.transform.position = transform.position;
    }

    public void DeleteCard()
    {
        Destroy(submitCard.gameObject);
        submitCard = null;

    }
}
