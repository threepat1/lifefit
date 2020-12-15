using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForTerms : MonoBehaviour
{
    #region Public Property
    public Toggle terms;
    public Toggle policy;
    public Button btnAccept;
    #endregion



    #region Private Property
    #endregion


    
    #region Monobehavior Callback    
    private void Start()
    {
        
    }


    private void Update()
    {
        
    }
    #endregion



    #region Public Method
    public void Toggle(bool check)
    {
        btnAccept.interactable = terms.isOn && policy.isOn;
    }
    #endregion




    #region Private Method
    #endregion
}
