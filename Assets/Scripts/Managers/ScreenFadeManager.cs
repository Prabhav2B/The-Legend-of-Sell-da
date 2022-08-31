using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFadeManager : MonoBehaviour
{
    [SerializeField] private GameSequenceManager _gameSequenceManager;
    
    [SerializeField] private float _screenFadeDuration = 2f;
    private Image _fadeImage;

    void Awake()
    {
        if (_gameSequenceManager == null)
            throw new Exception("Game Sequence Reference Missing in Screen Fade Manager!"); 
        
        _fadeImage = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        _fadeImage.DOFade(0f, _screenFadeDuration).OnComplete(_gameSequenceManager.StartFirstDialogue);
    }

    public void ScreenFadeIn()
    {
        _fadeImage.DOFade(0f, _screenFadeDuration);
    }
    
    public void ScreenFadeOut()
    {
        _fadeImage.DOFade(1f, _screenFadeDuration);
    }
}