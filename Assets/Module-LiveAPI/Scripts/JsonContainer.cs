using UnityEngine;


[System.Serializable]
public class BaseContainer
{
    public string status;
    public string message;
}


[System.Serializable]
public class LoginProfile
{
    public string id;
    public string name;
    public string sex;
    public string weigth;
    public string height;
}

[System.Serializable]
public class BaseToken
{
    public string token;
    public long expiration;
}

[System.Serializable]
public class LoginToken
{
    public BaseToken access;
    public BaseToken refresh;
}

[System.Serializable]
public class LoginData
{
    public LoginProfile[] profile;
    public LoginToken token;
}
// EXCERCISE

[System.Serializable]
public class ExerciseSession
{
}

[System.Serializable]
public class ExerciseSessionData
{
}





//=======================

[System.Serializable]
public class LoginContainer : BaseContainer
{
    public LoginData data;
}



public class JsonContainer { }