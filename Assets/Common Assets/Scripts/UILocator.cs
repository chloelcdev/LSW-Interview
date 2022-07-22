using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocator : MonoBehaviour
{
    public static UILocator _instance;

    public List<RectTransformStringPair> UIElements;

    public Dictionary<string, RectTransform> UIDictionary = new Dictionary<string, RectTransform>();

    private void Awake()
    {
        _instance = this;

        UpdateUIDictionary();
    }

    void UpdateUIDictionary()
    {
        UIDictionary.Clear();

        foreach (var pair in UIElements)
        {
            UIDictionary.Add(pair.name, pair.rectTranform);
        }
    }

    public static RectTransform LocateUI(string name)
    {
        if (_instance.UIDictionary.ContainsKey(name))
            return _instance.UIDictionary[name];
        else
            return null;
    }
}

[System.Serializable]
public struct RectTransformStringPair
{
    public string name;
    public RectTransform rectTranform;
}

