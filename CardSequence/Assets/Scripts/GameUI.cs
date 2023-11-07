using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameUI : MonoBehaviour
{
    [SerializeField] Text enemyTurnResultText;
    [SerializeField] Text playerTurnResultText;
    [SerializeField] TextMeshProUGUI playerHPText;
    [SerializeField] TextMeshProUGUI enemyHPText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject gameResultPanel;
    [SerializeField] Slider playerHPBar;
    [SerializeField] Slider enemyHPBar;
    [SerializeField] Text gameResultText;
    [SerializeField] Text gameEndText;
    [SerializeField] TextMeshProUGUI reward;
    [SerializeField] TextMeshProUGUI win;
    [SerializeField] TextMeshProUGUI life;
    [SerializeField] TextMeshProUGUI endWin;
    [SerializeField] TextMeshProUGUI endLife;
    [SerializeField] TextMeshProUGUI autoBattleText;
    [SerializeField] TextMeshProUGUI startBattleText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] GameObject playerSpellImage;
    [SerializeField] GameObject enemySpellImage;
    [SerializeField] TextMeshProUGUI playerSpellText;
    [SerializeField] TextMeshProUGUI enemySpellText;
    [SerializeField] Text money;

    public void Init()
    {
        enemyTurnResultText.gameObject.SetActive(false);
        playerTurnResultText.gameObject.SetActive(false);
        playerSpellImage.gameObject.SetActive(false);
        enemySpellImage.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        gameResultPanel.SetActive(false);
    }

    public void ShowHP(int playerHP, int enemyHP)
    {
        playerHPText.text = $"{playerHP}";
        enemyHPText.text = $"{enemyHP}";
        playerHPBar.value = playerHP;
        enemyHPBar.value = enemyHP;

    }

    public void ShowStartBattleText(bool first)
    {
        if (first)
        {
            startBattleText.text = "���̃^�[��";
        }
        else
        {
            startBattleText.text = "�o�g���J�n";
        }
    }

    public void ShowAutoBattleText(bool auto)
    {
        if (auto)
        {
            autoBattleText.text = "�����퓬\nON";
            autoBattleText.color = Color.white;
        }
        else
        {
            autoBattleText.text = "�����퓬\nOFF";
            autoBattleText.color = Color.gray;
        }
    }
    public void ShowMoney()
    {
        money.text = TitleManager.money.ToString();
    }

    public void ShowWins()
    {
        win.text = $"������: {TitleManager.wins} / 15";
        endWin.text = $"������: {TitleManager.wins} / 15";
    }

    public void ShowLife()
    {
        life.text = $"�c�@: {TitleManager.life} / 3";
        endLife.text = $"�c�@: {TitleManager.life} / 3";
    }

    //���s�\��

    public void ShowTurnResult(int[] battleResult)
    {
        if (battleResult[0] > 0)
        {
            enemyTurnResultText.text = $"{battleResult[0]} DMG!";
        }
        else
        {
            enemyTurnResultText.text = $"Blocked!";
        }


        if (battleResult[1] > 0)
        {
            playerTurnResultText.text = $"{battleResult[1]} DMG!";
        }
        else
        {
            playerTurnResultText.text = $"Blocked!";
        }
        enemyTurnResultText.gameObject.SetActive(true);
        playerTurnResultText.gameObject.SetActive(true);
    }


    public void ShowGameResult(string result)
    {
        ShowWins();
        ShowLife();
        gameResultText.text = result;
        switch (result)
        {
            case "WIN":
                resultPanel.SetActive(true);
                reward.text = "��V: 150 G";
                TitleManager.money += 150;
                break;
            case "LOSE":
            case "DRAW":
                resultPanel.SetActive(true);
                reward.text = "��V: 100 G";
                TitleManager.money += 100;
                break;
            case "Clear":
                gameResultPanel.SetActive(true);
                gameEndText.text = "Game Clear!";
                break;
            case "GameOver":
                gameResultPanel.SetActive(true);
                gameEndText.text = "Game Over";
                break;
            default:
                reward.text = "��V: 100 G";
                TitleManager.money += 100;
                break;
        }
        
    }
    public void SetupNextTurn()
    {
        enemyTurnResultText.gameObject.SetActive(false);
        playerTurnResultText.gameObject.SetActive(false);
    }

    public void ShowSpeedText(int speed)
    {
        speedText.text = $"�퓬���x\nx {speed}";
    }

    public void ShowTurnText(int turn)
    {
        turnText.text = $"�^�[�����F{turn+1}/30";
    }

    public void ShowSpellCount(int playerSpell, int enemySpell)
    {
        playerSpellText.text = $"{playerSpell}";
        if (playerSpell > 0)
        {
            playerSpellImage.gameObject.SetActive(true);
        }
        else
        {
            playerSpellImage.gameObject.SetActive(false);
        }

        enemySpellText.text = $"{enemySpell}";
        if (enemySpell > 0)
        {
            enemySpellImage.gameObject.SetActive(true);
        }
        else
        {
            enemySpellImage.gameObject.SetActive(false);
        }
    }
}
