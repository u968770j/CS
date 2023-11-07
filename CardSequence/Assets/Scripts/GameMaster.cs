using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject submitButton;
    [SerializeField] GameUI gameUI;
    [SerializeField] BGMManager bgm;
    [SerializeField] SEManager se;
    RuleBook ruleBook;



    private int turn;
    private bool auto;
    private bool firstPush;
    private int speedButton;
    private int speed;

    private void Awake()
    {
        ruleBook = GetComponent<RuleBook>();
    }
    private void Start()
    {
        Setup();
    }


    //カードを生成して配る

    void Setup()
    {
        turn = 0;
        auto = true;
        firstPush = false;
        speedButton = 1;
        speed = 2;
        gameUI.Init();
        player.Life = 20;
        enemy.Life = 20;
        player.IsPlayer = true;
        enemy.IsPlayer = false;
        gameUI.ShowHP(player.Life, enemy.Life);
        gameUI.ShowMoney();
        gameUI.ShowStartBattleText(firstPush);
        gameUI.ShowSpeedText(speed);
        gameUI.ShowTurnText(turn);
        gameUI.ShowSpellCount(0, 0);
        player.OnSubmitAction = ReceiveAction;
        enemy.OnSubmitAction = ReceiveAction;
        CardSetup(player);
        CardSetup(enemy);

    }

    public void ReceiveAction()
    {
        submitButton.SetActive(false);
        firstPush = true;
        gameUI.ShowStartBattleText(firstPush);
        player.OrderedSubmit();
        enemy.OrderedSubmit();
        //勝利判定
        StartCoroutine(CardBattle());
    }

    void CardSetup(Battler battler)
    {
        for (int i = 0; i < TitleManager.deckLength; i++)
        {
            Card card = cardGenerator.Spawn(i, battler);
            battler.SetCardToHand(card);
        }
        battler.Hand.ResetPosition();
    }

    //カード判定
    //表示を遅らせる：コルーチン
    IEnumerator CardBattle()
    {
        gameUI.ShowTurnText(turn);
        int[] battleResult = ruleBook.GetBattleResult(player, enemy, turn);
        gameUI.ShowSpellCount(ruleBook.playerSpellCount, ruleBook.enemySpellCount);
        yield return new WaitForSeconds(1f / speed);
        se.PlaySE(player.SubmitCard.Base.Clip);
        se.PlaySE(enemy.SubmitCard.Base.Clip);
        enemy.Life -= battleResult[0];
        player.Life -= battleResult[1];
        gameUI.ShowTurnResult(battleResult);
        gameUI.ShowHP(player.Life, enemy.Life);
        gameUI.ShowMoney();
        turn += 1;
        yield return new WaitForSeconds(1f / speed);

        if (player.Life <= 0 || enemy.Life <= 0 || turn >= 30)
        {
            //ゲーム終了
            TitleManager.round += 1;
            ShowGameResult();
        }
        else if (player.Hand.IsEmpty)
        {
            CardSetup(player);
            CardSetup(enemy);
            SetupNextTurn();
            if (auto)
            {
                ReceiveAction();
            }
            
        }
        else
        {
            SetupNextTurn();
            if (auto)
            {
                ReceiveAction();
            }

        }

    }

    void ShowGameResult()
    {
        //勝敗パネルを表示
        if (player.Life <= 0 && enemy.Life <= 0)
        {
            gameUI.ShowGameResult("DRAW");
            TitleManager.battleEnd = true;

        }
        else if (player.Life <= 0 || (turn >= 30 && player.Life < enemy.Life))
        {
            bgm.PlayDefeat();
            TitleManager.life -= 1;
            if (TitleManager.life > 0)
            {
                gameUI.ShowGameResult("LOSE");
                TitleManager.battleEnd = true;
            }
            else
            {
                gameUI.ShowGameResult("GameOver");
                TitleManager.gameEnd = true;
            }
            
            
        }
        else if (enemy.Life <= 0 || (turn >= 30 && player.Life > enemy.Life))
        {
            bgm.PlayVictory();
            TitleManager.wins += 1;
            if (TitleManager.wins < 15)
            {
                gameUI.ShowGameResult("WIN");
                TitleManager.battleEnd = true;
            }
            else
            {
                gameUI.ShowGameResult("Clear");
                TitleManager.gameEnd = true;
            }
            
            
        }
        else 
        {
            gameUI.ShowGameResult("DRAW");
            TitleManager.battleEnd = true;
        }

    }


    //次ターン移行（場のカードを捨てる）
    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        gameUI.SetupNextTurn();
        submitButton.SetActive(true);
    }

    public void OnRetryButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    public void OnTitleButton()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnBattleButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnDeckButton()
    {
        Data.Instance.referer = "Game";
        SceneManager.LoadScene("Deck");
    }

    public void OnAutoButton()
    {
        if (auto)
        {
            auto = false;
        }
        else
        {
            auto = true;
            ReceiveAction();
        }
        gameUI.ShowAutoBattleText(auto);
    }

    public void OnSpeedButton()
    {
        speedButton += 1;
        switch (speedButton%3)
        {
            case 0:
                speed = 1;
                break;
            case 1:
                speed = 2;
                break;
            case 2:
                speed = 5;
                break;
            default:
                speed = 1;
                break;
        }
        gameUI.ShowSpeedText(speed);
    }

}
