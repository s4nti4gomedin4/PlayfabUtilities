using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class TestLogin : MonoBehaviour
{

    public System.Action onLoginResultAction;

    [Header("Login values")]
    public string m_CustomId;
    public string m_TitleID;

    public void Start()
    {
        TestConnect();

    }

    public void TestConnect()
    {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
        {
            CustomId = m_CustomId,
            TitleId = m_TitleID,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, ResultcallBack, ErroCallBAck);
    }

    public void ResultcallBack(LoginResult _LoginResult)
    {
        Debug.Log("Login success");
        if(onLoginResultAction!=null){
            onLoginResultAction();
        }
    }

    public void ErroCallBAck(PlayFabError _errorCallback)
    {
        Debug.Log(_errorCallback.GenerateErrorReport());
        //AccountNotFound 1001
        //InvalidSignature    1273
        //InvalidTitleId  1004
        //PlayerSecretAlreadyConfigured   1294
        //PlayerSecretNotConfigured   1323
        //RequestViewConstraintParamsNotAllowed
    }

}
