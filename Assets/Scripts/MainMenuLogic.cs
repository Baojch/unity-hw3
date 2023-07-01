using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField]
    GameObject m_mainMenuCanvas;

    [SerializeField]
    GameObject m_languageCanvas;
    void Start()
    {
        m_mainMenuCanvas.SetActive(true);
        m_languageCanvas.SetActive(false);
    }
    public void OnStartClicked()
    {
        Debug.Log("Start Clicked");

        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit Clicked");

        Quit();
    }
    public void OnLanguageClicked(){
        Debug.Log("Language Clicked");
        m_mainMenuCanvas.SetActive(false);
        m_languageCanvas.SetActive(true);
    }
    public void OnEnglishClicked(){
        Debug.Log("English Clicked");
        m_mainMenuCanvas.SetActive(true);
        m_languageCanvas.SetActive(false);
        LocalizationManager.Instance.SetLanguage(Language.English);
    }
    public void OnChineseClicked(){
        Debug.Log("Chinese Clicked");
        m_mainMenuCanvas.SetActive(true);
        m_languageCanvas.SetActive(false);
        LocalizationManager.Instance.SetLanguage(Language.Chinese);
    }
    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
