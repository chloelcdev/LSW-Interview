using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ParchmentController : MonoBehaviour
{
    static ParchmentController _mainInstance;
    public static bool isOpen = false;

    AudioSource _typingSFX;

    CanvasGroup _canvasGroup;
    [SerializeField] float fadeTime = 0.4f;

    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _message;

    [SerializeField] Button _button;

    bool skipTypeWriter = false;
    [SerializeField] Vector2 characterTypeDelayMinMax = new Vector2(0.1f, 0.2f);

    private void Awake()
    {
        _mainInstance = this;

        _canvasGroup = GetComponent<CanvasGroup>();

        _message.text = "";

        _typingSFX = GetComponent<AudioSource>();
    }

    void CloseParchment()
    {
        isOpen = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, fadeTime);
        SFXPlayer.PlayPopSound();
    }

    public static void OpenParchment(string title, string message, System.Action onContinue = null)
    {
        _mainInstance.DoOpenParchment(title, message, onContinue);
    }

    public void DoOpenParchment(string title, string message, System.Action onContinue)
    {
        if (isOpen)
        {
            Debug.LogWarning("Trying to open parchment when it's already open!");
            return;
        }

        _canvasGroup.blocksRaycasts = true;

        isOpen = true;

        _title.text = title;
        _message.text = "";
        
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, fadeTime);
        SFXPlayer.PlayPopSound();

        StartCoroutine(DoTypewriterEffect(message, onContinue));

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => {
            skipTypeWriter = true;
        });
    }

    IEnumerator DoTypewriterEffect(string message, System.Action onContinue)
    {
        skipTypeWriter = false;

        _typingSFX.Play();

        // run the typewriter effect
        int characterIndex = 0;
        while (characterIndex < message.Length - 1)
        {
            if (skipTypeWriter)
                break;

            if (message[characterIndex] == '<')
                while (message[characterIndex] != '>')
                    characterIndex++;

            characterIndex++;

            // use alpha placement to avoid word wrapping weirdness
            _message.text = message.Insert(characterIndex, "<alpha=#00>");

            float minDelay = characterTypeDelayMinMax.x;
            float maxDelay = characterTypeDelayMinMax.y;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }

        _typingSFX.Stop();

        _message.text = message;

        skipTypeWriter = false;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => {
            CloseParchment();
            onContinue();
        });
    }
}
