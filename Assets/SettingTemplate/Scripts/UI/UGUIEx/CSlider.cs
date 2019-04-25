using UnityEngine.UI;
using UnityEngine;

class CSlider:MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;
    [SerializeField]
    private Text   m_SliderValue;

    private void Awake()
    {
        if(m_Slider != null)
        {
            m_Slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void Start()
    {
        if (m_Slider != null && m_SliderValue != null)
        {
            m_SliderValue.text = Mathf.FloorToInt(m_Slider.value).ToString();
        }
    }

    private void OnDestroy()
    {
        if (m_Slider != null)
        {
            m_Slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float fValue)
    {
        if(m_SliderValue != null)
        {
            m_SliderValue.text = Mathf.FloorToInt(fValue).ToString();
        }
    }
}
