using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullSliderValue : MonoBehaviour {
    private int m_TextInt;
    private Text UI_Text;
    private Slider UI_Slider;

    void Start()
    {
        UI_Slider = GameObject.FindGameObjectWithTag("UI_Slider").GetComponent<Slider>();
        UI_Text = GetComponent<Text>();
        //Player starts with 60 boost
        m_TextInt = 60;
    }

    void Update()
    {
        int temp = GetSliderValue();
        if (temp != m_TextInt)
        {
            m_TextInt = temp;
            UI_Text.text = m_TextInt.ToString();
        }
    }

    public int GetSliderValue()
    {
        return (int) UI_Slider.value;
    }

}
