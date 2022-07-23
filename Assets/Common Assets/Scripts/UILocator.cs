using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocator : MonoBehaviour
{
    public static UILocator _instance;

    public List<UIPieceInfo> UIElements;

    public Dictionary<string, UIPieceInfo> UIDictionary = new Dictionary<string, UIPieceInfo>();

    private void Awake()
    {
        _instance = this;

        UpdateUIDictionary();
    }

    void UpdateUIDictionary()
    {
        UIDictionary.Clear();

        foreach (var UIPiece in UIElements)
        {
            UIDictionary.Add(UIPiece.name, UIPiece);
        }
    }

    public static bool UIIsOpen()
    {
        foreach (var UIPiece in _instance.UIDictionary.Values)
        {
            if (!UIPiece.blocksInput) continue;

            var canvasGroup = UIPiece.rectTranform.GetComponent<CanvasGroup>();
            if (canvasGroup != null && canvasGroup.alpha > 0)
                return true;
        }

        return false;
    }

    public static RectTransform LocateUI(string name)
    {
        if (_instance.UIDictionary.ContainsKey(name))
            return _instance.UIDictionary[name].rectTranform;
        else
            return null;
    }
}

[System.Serializable]
public struct UIPieceInfo
{
    public string name;
    public RectTransform rectTranform;
    public bool blocksInput;
}

