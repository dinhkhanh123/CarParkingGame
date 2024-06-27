using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] LinesDrawer linesDrawer;

    [SerializeField] private CanvasGroup availableLineCavasGroup;
    [SerializeField] private GameObject availableLineHolder;
    [SerializeField] private Image availableLineFill;

    private bool availableLineUIActive = false;

    [Space]
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    private Route activeRoute;

    private void Start()
    {
        fadePanel.DOFade(0f,fadeDuration).From(1f);

        availableLineCavasGroup.alpha = 0f;

        linesDrawer.OnBeginDraw += OnBeginDrawHandler;
        linesDrawer.OnDraw += OnDrawHandler;
        linesDrawer.OnEndDraw += OnEndDrawHandler;

    }
    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;

        availableLineFill.color = activeRoute.carColor;
        availableLineFill.fillAmount = 1f;
        availableLineCavasGroup.DOFade(1f,.3f).From(0f);

        availableLineUIActive = true;
    }
  
    private void OnDrawHandler()
    {
        if (availableLineUIActive)
        {
            float maxLineLenght = activeRoute.maxLineLenght;
            float lineLenght = activeRoute.line.lenght;

            availableLineFill.fillAmount = 1 - (lineLenght / maxLineLenght);
        }
    }

    private void OnEndDrawHandler()
    {
        if (availableLineUIActive)
        {
            availableLineUIActive = false;  
            activeRoute = null;

            availableLineCavasGroup.DOFade(0f, .3f).From(0f);

        }
    }
}
