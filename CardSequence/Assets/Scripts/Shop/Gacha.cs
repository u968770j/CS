using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    public int[] normalProbability = new int[maxIndex];
    public int[] rareProbability = new int[maxIndex];
    public int[,] enemyProbability = new int[10, maxIndex]            
            {
                {90,10,0,0},
                {80,20,0,0},
                {70,30,0,0},
                {60,39,1,0},
                {50,45,5,0},
                {46,43,10,1},
                {42,40,15,3},
                {38,37,20,5},
                {34,34,25,7},
                {30,30,30,10}
            };
    public int normalGachaCost;
    public int rareGachaCost;

    const int maxIndex = 4; // レアリティの個数（Common, Rare, Epic, Legendary）
    const int countPerPack = 1; // 1回に引ける個数

    private GachaTable[] gachatable = new GachaTable[maxIndex];
    private System.Random random;

    public enum GachaType
    {
        Normal,
        Rare,
        Enemy,
        Shop,
    }

    void Start()
    {
        normalGachaCost = 100;
        rareGachaCost = 200;
        // 抽選リストをロード
        for (int i = 0; i < maxIndex; i++)
        {
            gachatable[i] = Resources.Load<GachaTable>("GachaTable/" + (i + 1).ToString());
        }
        random = new System.Random((int)DateTime.Now.Ticks);
    }

    public List<string> PackOpen(GachaType type)
    {
        List<string> resultList = new List<string>();
        int totalProbability = 0;
        int cost = 0;

        if (type == GachaType.Normal)
        {
            cost = normalGachaCost;

            for (int i = 0; i < maxIndex; i++)
            {
                gachatable[i].probability = normalProbability[i];
            }
        }
        else if (type == GachaType.Rare)
        {
            cost = rareGachaCost;
            for (int i = 0; i < maxIndex; i++)
            {
                gachatable[i].probability = rareProbability[i];
            }
        }
        else if (type == GachaType.Shop)
        {
            cost = 0;

            for (int i = 0; i < maxIndex; i++)
            {
                gachatable[i].probability = normalProbability[i];
            }
        }
        else
        {
            cost = 0;
            if (TitleManager.round < 10)
            {
                for (int i = 0; i < maxIndex; i++)
                {
                    gachatable[i].probability = enemyProbability[TitleManager.round,i];
                }
            }
            else
            {
                for (int i = 0; i < maxIndex; i++)
                {
                    gachatable[i].probability = enemyProbability[9,i];
                }
            }

        }
        
        if (TitleManager.money < cost)
        {
            // お金が足りなかったらnullを返却
            return null;
        }
        else
        {
            TitleManager.money -= cost;
        }


        for (int i = 0; i < maxIndex; i++)
        {
            // レアリティの確率を足し合わせる
            totalProbability += gachatable[i].probability;
        }

        resultList = new List<string>(); // 抽選結果格納用変数
        for (int i = 0; i < countPerPack; i++)
        {
            // 抽選を行う
            string card = GetNormalCard(totalProbability);
            resultList.Add(card);
        }

        return resultList;
    }



 

    private string GetNormalCard(int _allProbability)
    {
        // ガチャ全体の確率を足し合わせた数値から乱数を作成 (レアリティの抽選)
        int randomValue = GetRamdom(_allProbability);
        int totalProbability = 0;

        for (int i = 0; i < maxIndex; i++)
        {
            totalProbability += gachatable[i].probability;
            if (totalProbability > randomValue)
            {
                // そのレアに含まれるキャラ数から乱数を作成 (キャラの抽選)
                string id = GetRamdom(gachatable[i].cards);
                return id;
            }
        }
        return null;
    }

 



    private int GetRamdom(int _max)
    {
        return random.Next(0, _max);
    }

    private string GetRamdom(List<string> _list)
    {
        return _list[random.Next(0, _list.Count)];
    }
}