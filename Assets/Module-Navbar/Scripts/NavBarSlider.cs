using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarSlider : MonoBehaviour
{
    #region Public Property
    public static NavBarSlider Instance { get; private set; }

    [Header("Componenet")]
    public GameObject background;


    public bool open { get; set; }
    #endregion



    #region Private Property
    private RectTransform rect;
    private float targetPosition;
    #endregion


    
    #region Monobehavior Callback    
    private void Awake()
    {
        Instance = this;

        rect = this.GetComponent<RectTransform>();
        Close();
    }


    private void Update()
    {
        AnimateNavBar();
    }
    #endregion



    #region Public Method
    public void OpenAndClose()
    {
        if(open)
        {
            Close();
        }
        else
        {
            Open();
        }
    }


    public void Open()
    {
        targetPosition = rect.sizeDelta.x / 2f;
        open = true;
        background.SetActive(true);
    }


    public void Close()
    {
        targetPosition = -rect.sizeDelta.x / 2f;
        open = false;
        background.SetActive(false);
    }
    #endregion



    #region Private Method

    
    private void AnimateNavBar()
    {
        if(rect.anchoredPosition.x != targetPosition)
        {
            var pos = rect.anchoredPosition;
            pos.x = Mathf.MoveTowards(pos.x, targetPosition, 2500 * Time.deltaTime);
            rect.anchoredPosition = pos;
        }
    }
    #endregion
}
