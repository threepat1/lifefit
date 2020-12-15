using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    #region Public Property
    public InputField txtEmail;
    public InputField txtPassword;
    public Text txtError;


    public Text btnLoginText;
    public Button btnLogin;
    #endregion



    #region Private Property
    #endregion


    
    #region Monobehavior Callback    
    private void Start()
    {
        txtEmail.text = PlayerPrefs.GetString("email");
    }


    private void Update()
    {
        
    }
    #endregion



    #region Public Method
    public void Login()
    {
        string email = txtEmail.text;
        string password = txtPassword.text;

        if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            txtError.text = "*Email and Password can't be empty";
            return;
        }

        btnLogin.interactable = false;
        btnLoginText.text = "Please Wait...";

        LiveAPI.Instance.Query(QueryType.GET_LOGIN, $"email={email}&password={password}", (res, err) =>
        {
            btnLogin.interactable = true;
            btnLoginText.text = "Login";
            if(!string.IsNullOrEmpty(err))
            {
                Debug.Log(err);
                return;
            }

            LoginContainer login = JsonUtility.FromJson<LoginContainer>(res);
            if(login.status != "success")
            {
                Debug.Log(login.message);
                txtError.text = login.message;
                return;
            }

            PlayerData.loginData = login;
            PlayerPrefs.SetString("email", email);
            SceneManager.LoadScene("TermsConditions");
        }, null);
    }
    #endregion



    #region Private Method
    #endregion
}
