using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManu : MonoBehaviour
{
    #region Public Property
    public static MainManu Instance { get; private set; }
    public Button btnHome;
    public Button btnVideo;
    public Button btnPersonal;
    public Button btnLiveClass;
    public Button btnReport;

    public GameObject pageMain;
    public GameObject pageProfile;
    #endregion



    #region Private Property
    #endregion


    
    #region Monobehavior Callback    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnClickHome();       
    }


    private void Update()
    {
        
    }
    #endregion



    #region Public Method
    public void BaseOnClickMenu()
    {
        btnHome.interactable        = true;
        btnVideo.interactable       = true;
        btnPersonal.interactable    = true;
        btnLiveClass.interactable   = true;
        btnReport.interactable      = true;

        btnHome.GetComponentInChildren<Text>().color        = Color.white;
        btnVideo.GetComponentInChildren<Text>().color       = Color.white;
        btnPersonal.GetComponentInChildren<Text>().color    = Color.white;
        btnLiveClass.GetComponentInChildren<Text>().color   = Color.white;
        btnReport.GetComponentInChildren<Text>().color      = Color.white;
    }


    public void OnClickVideoOnDemand()
    {
        BaseOnClickMenu();
        btnVideo.interactable = false;
        btnVideo.GetComponentInChildren<Text>().color = new Color(0.85f, 0.11f, 0.24f);

        pageProfile.SetActive(false);
        pageMain.SetActive(true);
        VideoOnDemandManager.Instance.OpenTabMain();
    }

    public void OnClickHome()
    {
        BaseOnClickMenu();
        btnHome.interactable = false;
        btnHome.GetComponentInChildren<Text>().color = new Color(0.85f, 0.11f, 0.24f);

        pageProfile.SetActive(false);
        pageMain.SetActive(true);
        VideoOnDemandManager.Instance.OpenTabHome();
    }


    public void OnClickProfile()
    {
        BaseOnClickMenu();
        pageProfile.SetActive(true);
        pageMain.SetActive(false);
        NavBarSlider.Instance.Close();

        ProfileManager.Instance.Refresh();
    }
    #endregion



    #region Private Method
    #endregion
}
