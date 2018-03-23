using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class TestCloudScript : MonoBehaviour {

    public string m_FunctionName;
    public bool m_GeneratePlayStreamEvent;
    public TestLogin m_TestLogin;

	// Use this for initialization
	void Start () {
        m_TestLogin.onLoginResultAction += CloudScript;
	}
	
	
    void CloudScript(){
        Debug.Log("CloudScript");
        PlayFab.ClientModels.ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest();
        request.FunctionName = m_FunctionName;
        request.GeneratePlayStreamEvent = m_GeneratePlayStreamEvent;
        PlayFabClientAPI.ExecuteCloudScript(request,ExecuteCloudScriptResult,ErroCallBAck);
    }

    private void ExecuteCloudScriptResult(ExecuteCloudScriptResult obj)
    {
        Debug.Log(TestUtilities.PrintCloudScriptResult(obj));
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