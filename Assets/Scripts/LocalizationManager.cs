using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;

public enum Language
{
    English,
    Chinese
}

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField]
    Language m_currentLanguage;
    [SerializeField]
    TMP_FontAsset m_font_Chinese;
    [SerializeField]
    TMP_FontAsset m_font_English;
    Dictionary<Language, TextAsset> m_localizationFiles = new Dictionary<Language, TextAsset>();
    Dictionary<string, string> m_localizationData = new Dictionary<string, string>();

    void Awake()
    {
        // Singleton Creation
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Keep this object alive FOREVER
        DontDestroyOnLoad(gameObject);

        SetupLocalizationFiles();
        SetupLocalizationData();
    }

    void SetupLocalizationFiles()
    {
        foreach(Language language in Language.GetValues(typeof(Language)))
        {
            string textAssetPath = "Localization/" + language;
            TextAsset textAsset = (TextAsset)Resources.Load(textAssetPath);

            if(textAsset)
            {
                m_localizationFiles[language] = textAsset;
                Debug.Log("Localization File created for: " + language);
            }
            else
            {
                Debug.LogError("Text Asset Path not found for: " + textAssetPath);
            }
        }
    }

    void SetupLocalizationData()
    {
        TextAsset textAsset;

        if(m_localizationFiles.ContainsKey(m_currentLanguage))
        {
            Debug.Log("Selected language: " + m_currentLanguage);
            textAsset = m_localizationFiles[m_currentLanguage];
        }
        else
        {
            Debug.LogError("Couldn't find localization file for: " + m_currentLanguage);
            textAsset = m_localizationFiles[Language.English];
        }

        // Load XML Doc
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);

        XmlNodeList entryList = xmlDocument.GetElementsByTagName("Entry");

        foreach(XmlNode entry in entryList)
        {
            string key = entry.FirstChild.InnerText;
            string value = entry.LastChild.InnerText;

            if(!m_localizationData.ContainsKey(key))
            {
                Debug.Log("Added Key: " + key + " with Value: " + value);
                m_localizationData.Add(key, value);
            }
            else
            {
                Debug.LogError("Already contains entry for Key: " + key);
            }
        }
    }

    public string GetLocString(string key)
    {
        string locString = "";
        if(!m_localizationData.TryGetValue(key, out locString))
        {
            locString = "KEY: " + key + " NOT FOUND";
        }

        return locString;
    }
    public void SetLanguage(Language language){
        TextAsset textAsset;
        m_currentLanguage = language;
        textAsset = m_localizationFiles[m_currentLanguage];
        // Load XML Doc
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);

        XmlNodeList entryList = xmlDocument.GetElementsByTagName("Entry");

        foreach(XmlNode entry in entryList)
        {
            string key = entry.FirstChild.InnerText;
            string value = entry.LastChild.InnerText;

            if(!m_localizationData.ContainsKey(key))
            {
                Debug.Log("Added Key: " + key + " with Value: " + value);
                m_localizationData.Add(key, value);
            }
            else
            {
                // Debug.LogError("Already contains entry for Key: " + key);
                // Debug.Log("the value for "+key+" is " +m_localizationData[key]);
                // Debug.Log(value);
                m_localizationData[key] = value;
                LocString[] locStrings = FindObjectsOfType<LocString>();

                foreach (LocString locString in locStrings)
                {
                    locString.SetKey();
                    Debug.Log("set key");
                }
            }
        }
    }

    public TMP_FontAsset GetFont()
    {
        if(m_currentLanguage == Language.Chinese){
            return m_font_Chinese;
        }
        else{
            return m_font_English;
        }
    }

    public Language GetLanguage(){
        return m_currentLanguage;
    }
    
}
