using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class DeckManager : MonoBehaviour
{
    [SerializeField] Transform cardListTransform;
    [SerializeField] Transform[] dropPlaceTransform;
    [SerializeField] Transform[] shopTransform;
    [SerializeField] Transform normalGachaTransform;
    [SerializeField] Transform rareGachaTransform;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject battleButton;
    [SerializeField] Gacha gacha;
    [SerializeField] DeckUI deckUI;
    [SerializeField] SEManager seManager;
    [SerializeField] AudioClip buttonSE;
    [SerializeField] AudioClip commonSE;
    [SerializeField] AudioClip rareSE;
    [SerializeField] AudioClip epicSE;
    [SerializeField] AudioClip legendarySE;
    [SerializeField] AudioClip moneySE;
    [SerializeField] Transform[] enemyDeckTransform;
    [SerializeField] Transform openEnemyDeckButton;
    [SerializeField] GameObject enemyDeckPanel;


    int openedEnemyCardNumber;
    int openFee;

    // Start is called before the first frame update
    void Start()
    {
        enemyDeckPanel.SetActive(false);
        DeckSetup();
        CardListSetup();
        openedEnemyCardNumber = 10;
        openFee = 5;
        if (TitleManager.battleEnd)
        {
            TitleManager.battleEnd = false;
            TitleManager.openedEnemyCardPlace =  new bool[] { false, false, false, false, false, false, false, false, false, false };
            UpgradeEnemyDeck();
        }
        else
        {
            RestoreEnemyDeck();
        }
        deckUI.ShowOpenFee(openFee);
        deckUI.ShowTotalOpenFee(openFee * openedEnemyCardNumber);
        Data.Instance.referer = "Deck";
        for (int i = 0; i < 4; i++)
        {
            ShopSetup(i);
        }
        deckUI.ShowMoney();
        deckUI.ShowRound();
        deckUI.ShowWins();
        deckUI.ShowLife();
        deckUI.ShowGachaCost(gacha.normalGachaCost, gacha.rareGachaCost);
    }

    void ShopSetup(int i)
    {
        List<string> gachaResult = gacha.PackOpen(Gacha.GachaType.Shop);
        if (gachaResult != null)
        {
            Card card = cardGenerator.SpawnShop(int.Parse(gachaResult[0]));
            card.transform.SetParent(shopTransform[i]);
            card.transform.localPosition = new Vector3(0, 0, 0);
            card.transform.localScale = Vector3.one;
            deckUI.ShowShopPrice(i, card.Base.Price);
        }
    }

    void RestoreEnemyDeck()
    {
        for (int i = 0; i < TitleManager.deckLength; i++)
        {
            if (TitleManager.openedEnemyCardPlace[i])
            {
                CreateEnemyCard(i);
                enemyDeckTransform[i].GetComponent<Button>().interactable = false;
                openedEnemyCardNumber -= 1;
                deckUI.ShowTotalOpenFee(openFee * openedEnemyCardNumber);
            } 
        }
        
    }


    void UpgradeEnemyDeck()
    {
        for (int i = 0; i < TitleManager.deckLength; i++)
        {
            List<String> card = gacha.PackOpen(Gacha.GachaType.Enemy);
            TitleManager.enemyDeck[i] = int.Parse(card[0]);
        }
        
    }

    
    void DeckSetup()
    {
        for (int i = 0; i < TitleManager.deckLength; i++)
        {
            if (TitleManager.playerDeck[i] >= 0)
            {
                Card card = cardGenerator.SpawnDeck(i);
                card.transform.SetParent(dropPlaceTransform[i]);
                card.transform.localPosition = new Vector3(0, 0, 0);
                card.transform.localScale = Vector3.one;
            }

        }
    }
    

    void CardListSetup()
    {
        if (TitleManager.cardList.Count > 0)
        {
            for (int i = 0; i < TitleManager.cardList.Count; i++)
            {
                Card card = cardGenerator.SpawnList(i);
                card.transform.SetParent(cardListTransform);
                card.transform.localScale = Vector3.one;
                //card.transform.localPosition = new Vector3(0, 0, 0);
            }
        }

    }

    void CheckDeck()
    {
        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < 10; i++)
        {
            Transform deck = canvas.transform.Find($"{i+1}");
            Transform card = deck.transform.Find("DeckCard Variant(Clone)");
            if (card == null)
            {
                TitleManager.playerDeck[i] = -1;
            }
            else
            {
                TitleManager.playerDeck[i] = card.GetComponent<Card>().Base.CardID;
            }
        }
    }

    void CheckList()
    {
        if (cardListTransform != null)
        {
            Card[] children = new Card[cardListTransform.transform.childCount];
            TitleManager.cardList.Clear();
            for (int i = 0; i < cardListTransform.transform.childCount; i++)
            {
                children[i] = cardListTransform.transform.GetChild(i).GetComponent<Card>();
                TitleManager.cardList.Add(children[i]);
            }
        }
    }

    void NormalCardCheck()
    {       
        if (normalGachaTransform.childCount != 0)
        {
            normalGachaTransform.GetChild(0).SetParent(cardListTransform);
        }
    }

    void RareCardCheck()
    {
        if (rareGachaTransform.childCount != 0)
        {
            rareGachaTransform.GetChild(0).SetParent(cardListTransform);
        }
    }

    public void OnTitleButton()
    {
        NormalCardCheck();
        RareCardCheck();
        CheckDeck();
        CheckList();
        
        SceneManager.LoadScene("Title");

        
    }

    public void OnBattleButton()
    {
        NormalCardCheck();
        RareCardCheck();
        CheckList();
        CheckDeck();
        SceneManager.LoadScene("GameScene");
    }


    public void OnNormalGachaButton()
    {
        NormalCardCheck();
        List<string> gachaResult = gacha.PackOpen(Gacha.GachaType.Normal);
        if(gachaResult != null)
        {
            deckUI.ShowMoney();
            Card card = cardGenerator.SpawnShop(int.Parse(gachaResult[0]));
            card.transform.SetParent(normalGachaTransform);
            card.transform.localPosition = new Vector3(0, 0, 0);
            card.transform.localScale = Vector3.one;
            PlayGachaSE(card.Base.Rarity);
        }
    }

    public void OnRareGachaButton()
    {
        RareCardCheck();
        List<string> gachaResult = gacha.PackOpen(Gacha.GachaType.Rare);
        if (gachaResult != null)
        {
            deckUI.ShowMoney();
            Card card = cardGenerator.SpawnShop(int.Parse(gachaResult[0]));
            card.transform.SetParent(rareGachaTransform);
            card.transform.localPosition = new Vector3(0, 0, 0);
            card.transform.localScale = Vector3.one;
            PlayGachaSE(card.Base.Rarity);
        }
    }

    void PlayGachaSE(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                seManager.PlaySE(commonSE);
                break;
            case Rarity.Rare:
                seManager.PlaySE(rareSE);
                break;
            case Rarity.Epic:
                seManager.PlaySE(epicSE);
                break;
            case Rarity.Legendary:
                seManager.PlaySE(legendarySE);
                break;
            default:
                seManager.PlaySE(commonSE);
                break;
        }
    }

    public void OpenEnemyCard(int number)
    {
        if (TitleManager.money >= openFee)
        {
            TitleManager.money -= openFee;
            deckUI.ShowMoney();
            seManager.PlaySE(moneySE);
            CreateEnemyCard(number);
            enemyDeckTransform[number].GetComponent<Button>().interactable = false;
            TitleManager.openedEnemyCardPlace[number] = true;
            openedEnemyCardNumber -= 1;
            deckUI.ShowTotalOpenFee(openFee * openedEnemyCardNumber);
        }

    }

    public void OpenEnemyDeck()
    {
        if (TitleManager.money >= openFee * openedEnemyCardNumber)
        {
            TitleManager.money -= openFee * openedEnemyCardNumber;
            deckUI.ShowMoney();
            seManager.PlaySE(moneySE);
            for (int i = 0; i < TitleManager.deckLength; i++)
            {
                CreateEnemyCard(i);
                enemyDeckTransform[i].GetComponent<Button>().interactable = false;
                TitleManager.openedEnemyCardPlace[i] = true;
            }
            openEnemyDeckButton.GetComponent<Button>().interactable = false;
            openedEnemyCardNumber = 0;
            deckUI.ShowTotalOpenFee(0);
        }

    }

    void CreateEnemyCard(int number)
    {
        if (TitleManager.enemyDeck[number] >= 0)
        {
            Card card = cardGenerator.SpawnEnemyCard(number);
            card.transform.SetParent(enemyDeckTransform[number].GetChild(1));
            card.transform.localPosition = new Vector3(0, 0, 0);
            card.transform.localScale = Vector3.one;
        }
        enemyDeckTransform[number].GetComponent<Button>().interactable = false;
    }

    public void OnCheckEnemyButton()
    {
        enemyDeckPanel.SetActive(true);
    }

    public void OnReturnButton()
    {
        enemyDeckPanel.SetActive(false);
    }

    public void ShowWindow(GameObject window)
    {
        window.SetActive(true);
    }

    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
}
