using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    #region Public Property
    public GameObject prefab;
    public Transform container;
    public static ProfileManager Instance { get; set; }
    #endregion



    #region Private Property
    private List<GameObject> allProfile = new List<GameObject>();
    #endregion


    
    #region Monobehavior Callback    
    private void Awake()
    {
        Instance = this;
    }
    #endregion



    #region Public Method
    public void Refresh()
    {
        if(PlayerData.loginData.data == null)
        {
            return;
        }

        var profile = PlayerData.loginData.data.profile;

        allProfile.ForEach(o => Destroy(o));
        allProfile.Clear();

        for(int i = 0; i < profile.Length; i++)
        {
            var obj = Instantiate(prefab, container);
            obj.GetComponent<ProfileItem>().Init(profile[i]);
            allProfile.Add(obj);
        }
    }
    #endregion



    #region Private Method
    #endregion
}
