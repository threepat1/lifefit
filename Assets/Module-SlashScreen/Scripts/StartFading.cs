using iBoost;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFading : MonoBehaviour
{
    #region Public Property
    public Image logo;
    #endregion



    #region Private Property
    #endregion


    
    #region Monobehavior Callback    
    IEnumerator Start()
    {
        yield return logo.FadeAlpha(1, 0.025f, FadeMode.LERP);
        yield return new WaitForSeconds(1f);
        yield return logo.FadeAlpha(0, 0.025f, FadeMode.LERP);

        SceneManager.LoadScene("LanguageSetting");
    }


    #endregion



    #region Private Method
    private IEnumerator FadeIn()
    {
        while(logo.color.a < 1)
        {
            Color a = logo.color;
            a.a = Mathf.Lerp(a.a, 1, 0.05f);
            logo.color = a;
            yield return null;
        }
    }
    #endregion

}
