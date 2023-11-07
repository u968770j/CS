using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DeckUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentMoney;
    [SerializeField] TextMeshProUGUI currentMoneyOnPanel;
    [SerializeField] TextMeshProUGUI totalCardOpenFee;
    [SerializeField] TextMeshProUGUI currentRound;
    [SerializeField] TextMeshProUGUI currentWins;
    [SerializeField] TextMeshProUGUI currentLife;
    [SerializeField] TextMeshProUGUI cardOpenFee;
    [SerializeField] TextMeshProUGUI normalGachaCost;
    [SerializeField] TextMeshProUGUI rareGachaCost;
    [SerializeField] TextMeshProUGUI[] shopPrice;

    public void ShowShopPrice(int i, int price)
    {
        shopPrice[i].text = $"{price} G";
    }

    public void ShowGachaCost(int normal,int rare)
    {
        normalGachaCost.text = $"�K�`��\n1�� {normal} G";
        rareGachaCost.text = $"���A�K�`��\n1�� {rare} G";
    }

    public void ShowMoney()
    {
        currentMoney.text = $"{TitleManager.money} G";
        currentMoneyOnPanel.text = $"{TitleManager.money} G";
        for (int i = 0; i < 4; i++)
        {
            int price = int.Parse(shopPrice[i].text.Split(' ')[0]);
            if (TitleManager.money < price)
            {
                shopPrice[i].color = Color.red;
            }
            else
            {
                shopPrice[i].color = Color.white;
            }
        }
    }

    public void ShowTotalOpenFee(int fee)
    {
        totalCardOpenFee.text = $"���ׂĊJ����\n{fee} G";
    }

    public void ShowOpenFee(int fee)
    {
        cardOpenFee.text = $"�G�̃f�b�L��\n���邱�Ƃ��ł���\n1�� {fee} G";
    }

    public void ShowRound()
    {
        currentRound.text = $"���E���h: {TitleManager.round}";
    }
    public void ShowWins()
    {
        currentWins.text = $"������: {TitleManager.wins}/15";
    }
    public void ShowLife()
    {
        currentLife.text = $"�c�@: {TitleManager.life}/3";
    }
}
