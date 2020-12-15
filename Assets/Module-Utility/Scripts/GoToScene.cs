using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    #region Public Property
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
    public void GoScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    #endregion



    #region Private Method
    #endregion
}
