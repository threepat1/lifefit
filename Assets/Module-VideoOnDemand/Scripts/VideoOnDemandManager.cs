using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOnDemandManager : MonoBehaviour
{
    #region Public Property
    public static VideoOnDemandManager Instance { get; private set; }
    #endregion



    #region Private Property
    [Header("Component")]
    public RectTransform scrollExecise;
    public RectTransform scrollProfile;
    public GameObject mainMenu;
    public GameObject tabVideo;
    public GameObject tabHome;
    public GameObject panelMain;
    public GameObject panelSession;
    public GameObject panelWeigthTraning;
    public GameObject panelWorkshop;

    [Header("Session")]
    public Text txtName;
    public Text txtProgram;
    public Text txtLevel;
    public Text txtDuration;
    public Text txtTrainer;
    public Text txtDescription;
    #endregion


    
    #region Monobehavior Callback    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GridLayoutGroup layout = scrollExecise.GetComponent<GridLayoutGroup>();
        float x = scrollExecise.rect.width / 2f - 16 - 8f;
        float y = x * 1.2f;
        layout.cellSize = new Vector2(x, y);

        GridLayoutGroup layoutProfile = scrollProfile.GetComponent<GridLayoutGroup>();
        layoutProfile.cellSize = layout.cellSize;
    }


    private void Update()
    {
        
    }
    #endregion



    #region Public Method
    public void OpenTabMain()
    {
        tabVideo.SetActive(true);
        tabHome.SetActive(false);
        mainMenu.SetActive(true);
        panelMain.SetActive(true);
        panelSession.SetActive(false);
        panelWeigthTraning.SetActive(false);
        panelWorkshop.SetActive(false);
    }


    public void OpenTabHome()
    {
        mainMenu.SetActive(true);
        tabHome.SetActive(true);
        tabVideo.SetActive(false);
        panelMain.SetActive(false);
        panelSession.SetActive(false);
        panelWeigthTraning.SetActive(false);
        panelWorkshop.SetActive(false);
    }


    public void OpenWeightTraining()
    {
        tabVideo.SetActive(true);
        mainMenu.SetActive(false);
        panelWeigthTraning.SetActive(true);
        panelSession.SetActive(false);
        panelMain.SetActive(false);
        panelWorkshop.SetActive(false);
    }


    public void OpenSession(TrainingData data)
    {
        tabVideo.SetActive(true);
        mainMenu.SetActive(false);
        panelSession.SetActive(true);
        panelWeigthTraning.SetActive(false);
        panelMain.SetActive(false);
        panelWorkshop.SetActive(false);

        txtName.text = data.name;
        txtProgram.text = data.name;
        txtLevel.text = data.level;
        txtTrainer.text = data.trainer;
        txtDuration.text = data.duration;
        txtDescription.text = data.description;
    }


    public void OpenWorkshop()
    {
        tabVideo.SetActive(true);
        mainMenu.SetActive(false);
        panelWorkshop.SetActive(true);
        panelSession.SetActive(false);
        panelWeigthTraning.SetActive(false);
        panelMain.SetActive(false);
        MediaPlayerManager.Instance.Restart();
    }
    #endregion



    #region Private Method
    #endregion
}
