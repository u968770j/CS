using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class Card : MonoBehaviour
{
    //�J�[�hUI
    //�Q�[��������
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI defText;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image type;
    [SerializeField] TextMeshProUGUI typeText;


    public CardBase Base { get; private set; }

    //�֐���o�^�ł���
    public UnityAction<Card> OnClickCard;

    public void Set(CardBase cardBase)
    {
        Base = cardBase;
        nameText.text = cardBase.Name;
        numberText.text = cardBase.CardID.ToString();
        atkText.text = cardBase.Atk.ToString();
        defText.text = cardBase.Def.ToString();
        icon.sprite = cardBase.Icon;
        descriptionText.text = cardBase.Description;
        if (cardBase.Type == Type.Army)
        {
            typeText.text = "�R��";
        }
        else if (cardBase.Type == Type.Royalty)
        {
            typeText.text = "����";
        }
        else if (cardBase.Type == Type.Monster)
        {
            typeText.text = "����";
        }
        else
        {
            typeText.text = "";
            type.enabled = false;
        }
    }

    public void BuffATK(int buffedATK)
    {
        atkText.text = buffedATK.ToString();
        atkText.color = Color.green;
    }

    public void BuffDEF(int buffedDEF)
    {
        defText.text = buffedDEF.ToString();
        defText.color = Color.green;
    }

    public void PointerEnter()
    {

        GetComponentInChildren<Canvas>().sortingLayerName = "Card";
    }

    public void PointerExit()
    {

        GetComponentInChildren<Canvas>().sortingLayerName = "Default";

    }

}
