using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text textSuccessful, textFailed, textExample, textMark;
    public Text[] textAnswers;
    public Button[] buttonAnswers;
    public GameObject panel;
    private int amountSuccessful, amountFailed, num1, num2, result, mark, trueAnswer;
    private string currentSign;
    private string[] sign = new string[] { "+", "-", "*", "/" };
    void Start()
    {
        textSuccessful.text = $"Верных: {amountSuccessful}";
        textFailed.text = $"Не верных: {amountFailed}";
        if (StaticManager.s_CurrentLvl[0])
        {
            currentSign = sign[0];
            num1 = RandomNumber(true);
            num2 = RandomNumber(true);
            result = num1 + num2;
        }
        else if (StaticManager.s_CurrentLvl[1])
        {
            currentSign = sign[1];
            num1 = RandomNumber(true);
            num2 = RandomNumber(true);
            result = num1 - num2;
        }
        else if (StaticManager.s_CurrentLvl[2])
        {
            currentSign = sign[2];
            num1 = RandomNumber(false);
            num2 = RandomNumber(false);
            result = num1 * num2;
        }
        else if (StaticManager.s_CurrentLvl[3])
        {
            currentSign = sign[3];
            num1 = RandomNumber(false);
            num2 = RandomNumber(false);
            result = num1 / num2;
        }
        textExample.text = $"{num1} {currentSign} {num2} = ?";
        CalculateTextAnswers();
    }
    private int RandomNumber(bool mode)
    {
        int tempInt;
        switch (mode)
        {
            case true:
                if (StaticManager.s_Negative)
                {
                    tempInt = Random.Range(-StaticManager.s_MaxTerm, StaticManager.s_MaxTerm + 1);
                }
                else
                {
                    tempInt = Random.Range(2, StaticManager.s_MaxTerm + 1);
                }
                break;
            case false:
                if (StaticManager.s_Negative)
                {
                    tempInt = Random.Range(-StaticManager.s_MaxFactor, StaticManager.s_MaxFactor + 1);
                }
                else
                {
                    tempInt = Random.Range(2, StaticManager.s_MaxFactor + 1);
                }
                break;
        }
        return tempInt;
    }
    private void CalculateTextAnswers()
    {
        trueAnswer = Random.Range(0, textAnswers.Length);
        for (int i = 0; i < textAnswers.Length; i++)
        {
            if (i != trueAnswer)
            {
                int tempInt = result + Random.Range(-10, 11);
                textAnswers[i].text = $"{tempInt}";
                buttonAnswers[i].onClick.RemoveAllListeners();
                buttonAnswers[i].onClick.AddListener(() => OnMouseDown(tempInt));
                for (int j = 0; j < i; j++)
                {
                    if (textAnswers[j].text == textAnswers[i].text)
                    {
                        tempInt = result + Random.Range(-10, 11);
                        textAnswers[i].text = $"{tempInt}";
                    }
                }
            }
            else
            {
                textAnswers[i].text = $"{result}";
                buttonAnswers[i].onClick.RemoveAllListeners();
                buttonAnswers[i].onClick.AddListener(() => OnMouseDown(result));
            }
        }
    }
    private IEnumerator Waiting()
    {
        for (int i = 0; i < buttonAnswers.Length; i++)
        {
            buttonAnswers[i].interactable = false;
        }
        buttonAnswers[trueAnswer].image.color = new Color(0, 200, 0);
        for (int i = 0; i < buttonAnswers.Length; i++)
        {
            if (i != trueAnswer)
            {
                buttonAnswers[i].image.color = new Color(200, 0, 0);
            }
        }
        yield return new WaitForSeconds(3.0f);
        if (amountFailed + amountSuccessful < 20)
        {
            for (int i = 0; i < buttonAnswers.Length; i++)
            {
                buttonAnswers[i].interactable = true;
                buttonAnswers[i].image.color = new Color(255, 255, 255);
            }
            Start();
        }
        else
        {
            panel.SetActive(true);
            CountMark();
        }
    }
    public void OnMouseDown(int choosedResult)
    {
        if (choosedResult == result)
        {
            textExample.text = $"Все верно!\n{num1} {currentSign} {num2} = {result}";
            amountSuccessful++;
        }
        else 
        {
            textExample.text = $"Не верно!\n{num1} {currentSign} {num2} = {result}";
            amountFailed++;
        }
        StartCoroutine(Waiting());
    }
    private void CountMark()
    {
        if (amountFailed < 3)
        {
            mark = 5;
            PlayerPrefs.SetInt("Amount5", PlayerPrefs.GetInt("Amount5") + 1);
        }
        else if (amountFailed < 5)
        {
            mark = 4;
            PlayerPrefs.SetInt("Amount4", PlayerPrefs.GetInt("Amount4") + 1);
        }
        else if (amountFailed < 8)
        {
            mark = 3;
            PlayerPrefs.SetInt("Amount3", PlayerPrefs.GetInt("Amount3") + 1);
        }
        else
        {
            mark = 2;
            PlayerPrefs.SetInt("Amount2", PlayerPrefs.GetInt("Amount2") + 1);
        }
        textMark.text = $"Твоя оценка: {mark}";
    }
    public void Again()
    {
        amountSuccessful = 0;
        amountFailed = 0;
        Start();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
