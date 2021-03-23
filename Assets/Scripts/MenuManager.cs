using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour
{
    public InputField inputTerm, inputFactor;
    public Text[] textResults;
    public Toggle toggle;
    private void Start()
    {
        StaticManager.s_Negative = Convert.ToBoolean(PlayerPrefs.GetInt("Negative"));
        StaticManager.s_MaxTerm = PlayerPrefs.GetInt("MaxTerm");
        StaticManager.s_MaxFactor = PlayerPrefs.GetInt("MaxFactor");
        toggle.isOn = StaticManager.s_Negative;
        inputTerm.text = StaticManager.s_MaxTerm.ToString();
        inputFactor.text = StaticManager.s_MaxFactor.ToString();
    }
    public void OnMouseDown(string sign)
    {
        switch (sign)
        {
            case "+":
                for (int i = 0; i < StaticManager.s_CurrentLvl.Length; i++)
                {
                    if (i == 0) StaticManager.s_CurrentLvl[0] = true;
                    else StaticManager.s_CurrentLvl[i] = false;
                }
                break;
            case "-":
                for (int i = 0; i < StaticManager.s_CurrentLvl.Length; i++)
                {
                    if (i == 1) StaticManager.s_CurrentLvl[1] = true;
                    else StaticManager.s_CurrentLvl[i] = false;
                }
                break;
            case "*":
                for (int i = 0; i < StaticManager.s_CurrentLvl.Length; i++)
                {
                    if (i == 2) StaticManager.s_CurrentLvl[2] = true;
                    else StaticManager.s_CurrentLvl[i] = false;
                }
                break;
            case "/":
                for (int i = 0; i < StaticManager.s_CurrentLvl.Length; i++)
                {
                    if (i == 3) StaticManager.s_CurrentLvl[3] = true;
                    else StaticManager.s_CurrentLvl[i] = false;
                }
                break;
        }
        SceneManager.LoadScene(1);
    }
    public void ChangeValueNegative()
    {
        StaticManager.s_Negative = !StaticManager.s_Negative;
        PlayerPrefs.SetInt("Negative", Convert.ToInt32(StaticManager.s_Negative));
    }
    public void ChangeValueTerm()
    {
        StaticManager.s_MaxTerm = Convert.ToInt32(inputTerm.text);
        PlayerPrefs.SetInt("MaxTerm", StaticManager.s_MaxTerm);
    }
    public void ChangeValueFactor()
    {
        StaticManager.s_MaxFactor = Convert.ToInt32(inputFactor.text);
        PlayerPrefs.SetInt("MaxFactor", StaticManager.s_MaxFactor);
    }
    public void ShowResults()
    {
        for (int i = 0; i < textResults.Length; i++)
        {
            textResults[i].text = $"Кол-во оценок {5-i}: {PlayerPrefs.GetInt($"Amount{5-i}")}";
            textResults[i].fontStyle = FontStyle.Bold;
        }
    }
}
