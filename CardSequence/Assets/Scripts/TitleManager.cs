using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class TitleManager : MonoBehaviour
{
    [SerializeField] SEManager seManager;
    [SerializeField] AudioClip startButtonSE;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject panel;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] Transform cardListTransform;

    public static bool running;
    public static int round;//ラウンド数
    public static int wins;//勝利数
    public static int life;//残機
    public static int money;//所持金
    public static int deckLength;
    public static int[] playerDeck;
    public static int[] enemyDeck;
    public static List<Card> cardList;
    public static bool gameEnd;
    public static bool[] openedEnemyCardPlace;
    public static bool battleEnd;

    private bool buttonFlag;

    private void Start()
    {
        buttonFlag = true;
        if (!running || gameEnd)
        {
            continueButton.SetActive(false);
            startButton.transform.position = new Vector3(0, -2, 0);
            running = true;
            FirstSetup();
        }
        else
        {
            startButton.transform.position = new Vector3(-6, -2, 0);
        }
        CardListSetup();
    }

    void FirstSetup()
    {
        round = 1;
        wins = 0;
        life = 3;
        money = 200;
        deckLength = 10;
        playerDeck = new int[10] { 0, 0, 0, 0, 1, 1, 1, 1, 100, 100 };
        enemyDeck = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        cardList = new List<Card>();
        gameEnd = false;
        openedEnemyCardPlace = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        battleEnd = true;
        panel.SetActive(false);
    }

    public void CardListSetup()
    {
        Card[] cards = cardGenerator.SpawnAllCards();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.SetParent(cardListTransform);
            cards[i].transform.localScale = Vector3.one;
        }
    }

    public void OnStartButton()
    {
        if (buttonFlag)
        {
            FirstSetup();
            buttonFlag = false;
            //seManager.PlaySE(startButtonSE);
            SceneManager.LoadScene("Deck");
        }
    }

    public void OnContinueButton()
    {
        if (buttonFlag)
        {
            buttonFlag = false;
            //seManager.PlaySE(startButtonSE);
            SceneManager.LoadScene("Deck");
        }
    }

    public void OnAllCardListButtton()
    {
        panel.SetActive(true);
    }

    public void OnReturnButton()
    {
        panel.SetActive(false);
    }

    public void OnQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
