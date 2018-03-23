using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class TestStore : MonoBehaviour {
    public TestLogin m_TestLogin;
    public string m_CatalogVersion;
    public string m_StoreId;
    public bool automaticButFirstItem;
	
	void Start () {
        m_TestLogin.onLoginResultAction += GetStore;
	}
	

	void GetStore () {
        Debug.Log("GetStore");
        PlayFab.ClientModels.GetStoreItemsRequest request =  new PlayFab.ClientModels.GetStoreItemsRequest();
        request.CatalogVersion = m_CatalogVersion;
        request.StoreId = m_StoreId;
        PlayFabClientAPI.GetStoreItems(request, resultCallback, ErroCallBAck);
	}
    void BuyStoreItem(StoreItem item){
        Debug.Log("BuyStoreItem "+item.ItemId);

        IEnumerator keysEnumerator = item.VirtualCurrencyPrices.Keys.GetEnumerator();
        keysEnumerator.MoveNext();
        string firstKey = keysEnumerator.Current.ToString();

        IEnumerator valuesEnumerator = item.VirtualCurrencyPrices.Values.GetEnumerator();
        valuesEnumerator.MoveNext();
        int firstValue = Convert.ToInt32(valuesEnumerator.Current);

        PurchaseItemRequest request = new PurchaseItemRequest();
        request.CatalogVersion = m_CatalogVersion;
        request.StoreId = m_StoreId;
        request.ItemId = item.ItemId;
        request.VirtualCurrency = firstKey;
        request.Price = firstValue;
        PlayFabClientAPI.PurchaseItem(request,pruchaseResult,ErroCallBAck);
    }

    private void pruchaseResult(PurchaseItemResult obj)
    {
        for (int i = 0; i < obj.Items.Count; i++)
        {
            Debug.Log(TestUtilities.PrintItemInstance(obj.Items[i]));
        }
    }

    private void resultCallback(GetStoreItemsResult obj)
    {
        for (int i = 0; i < obj.Store.Count; i++)
        {
            Debug.Log(TestUtilities.PrintStoreItem(obj.Store[i]));
        }
        if(automaticButFirstItem){
			if(obj.Store.Count>0){
				BuyStoreItem(obj.Store[0]);
			}
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