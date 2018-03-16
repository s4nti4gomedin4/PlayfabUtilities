using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class TestPlayfab : MonoBehaviour {

    public string m_TitleID;

    [Header("LoginWithCustomID")]
    public string m_CustomId;

	public void Start()
	{
        Test1();
	}
	
    public void Test1(){
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
        {
            CustomId = m_CustomId,
            TitleId = m_TitleID,
            CreateAccount = true
        };

        print(request.TitleId);
        PlayFabClientAPI.LoginWithCustomID(request, ResultcallBack, ErroCallBAck);
    }
    public void ResultcallBack(LoginResult _LoginResult){
        Debug.Log(_LoginResult.ToString());
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
