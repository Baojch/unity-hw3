using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocString : MonoBehaviour
{
    TextMeshProUGUI m_tmp; 

    [SerializeField]
    string m_key;

    void Awake()
    {
        m_tmp = GetComponent<TextMeshProUGUI>();    
    }

    void Start()
    {
        if(m_tmp && !string.IsNullOrEmpty(m_key))
        {
            m_tmp.text = LocalizationManager.Instance.GetLocString(m_key);
        }
    }
    public void SetKey()
    {
        m_tmp.text = LocalizationManager.Instance.GetLocString(m_key);
        SetFont();
    }
    public void SetFont()
    {
        if (m_tmp)
        {
            m_tmp.font =  LocalizationManager.Instance.GetFont();
        }
    }
}
