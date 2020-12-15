using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileItem : MonoBehaviour
{
    #region Public Property
    public Text txtName;
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
    public void Init(LoginProfile data)
    {
        txtName.text = data.name;
    }
    #endregion



    #region Private Method
    #endregion
}
