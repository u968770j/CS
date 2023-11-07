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
        normalGachaCost.text = $"ガチャ\n1回 {normal} G";
        rareGachaCost.text = $"レアガチャ\n1回 {rare} G";
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
        totalCardOpenFee.text = $"すべて開ける\n{fee} G";
    }

    public void ShowOpenFee(int fee)
    {
        cardOpenFee.text = $"敵のデッキを\n見ることができる\n1枚 {fee} G";
    }

    public void ShowRound()
    {
        currentRound.text = $"ラウンド: {TitleManager.round}";
    }
    public void ShowWins()
    {
        currentWins.text = $"勝利数: {TitleManager.wins}/15";
    }
    public void ShowLife()
    {
        currentLife.text = $"残機: {TitleManager.life}/3";
    }
}
