using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScreenFadeManager : MonoBehaviour
{
    [SerializeField] private GameSequenceManager _gameSequenceManager;
    
    [SerializeField] private float _screenFadeDuration = 2f;
    private Image _fadeImage;
    private TextMeshProUGUI text;

    private Color originalTextColor;
    private Color fadeoutTextColor;

    void Awake()
    {
        if (_gameSequenceManager == null)
            throw new Exception("Game Sequence Reference Missing in Screen Fade Manager!"); 
        
        _fadeImage = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        originalTextColor = text.color;
        fadeoutTextColor = new Color(originalTextColor.a, originalTextColor.g, originalTextColor.b, 0);
    }

    public void ScreenFadeOutWorldEvent(string dayStartText)
    {
        text.text = dayStartText;
        text.DOColor(fadeoutTextColor, _screenFadeDuration).SetEase(Ease.InQuad);
        _fadeImage.DOFade(0f, _screenFadeDuration).SetEase(Ease.InQuad).OnComplete(_gameSequenceManager.ExecuteNextWorldEvent);
    }
    
    public void ScreenFadeInWorldEvent()
    {
        _fadeImage.DOFade(1f, _screenFadeDuration);
    }
}
