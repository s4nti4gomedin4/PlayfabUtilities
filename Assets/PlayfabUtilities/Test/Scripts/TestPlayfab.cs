using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class TestPlayfab : MonoBehaviour {


    public string CATALOG_VERIONS;
    public TestLogin m_TestLogin;

	private void Start()
	{
        if(m_TestLogin!=null){
            m_TestLogin.onLoginResultAction += GetUserInventory;
        }
	}

	public void GetCatalog()
    {

        GetCatalogItemsRequest request = new GetCatalogItemsRequest();
        request.CatalogVersion = CATALOG_VERIONS;
        PlayFabClientAPI.GetCatalogItems(request, GetCatalogItemsResult, ErroCallBAck);
        Debug.Log("GetCatalog");
    }

    public void GetCatalogItemsResult(GetCatalogItemsResult _result)
    {
        for (int i = 0; i < _result.Catalog.Count; i++)
        {
            Debug.Log(TestUtilities.PrintCatalogItem(_result.Catalog[i]));
            
        }
        if(_result.Catalog.Count>0){
            PruchaseItem(_result.Catalog[0]);
        }

       
    }
   




    private void PruchaseItem(CatalogItem item)
    {
        IEnumerator keysEnumerator = item.VirtualCurrencyPrices.Keys.GetEnumerator();
        keysEnumerator.MoveNext();
        string firstKey = keysEnumerator.Current.ToString();

        IEnumerator valuesEnumerator = item.VirtualCurrencyPrices.Values.GetEnumerator();
        valuesEnumerator.MoveNext();
        int firstValue = Convert.ToInt32(valuesEnumerator.Current);

        PurchaseItemRequest request = new PurchaseItemRequest();
        request.ItemId = item.ItemId;
        request.VirtualCurrency = firstKey;
        request.Price = firstValue;
        request.CatalogVersion = CATALOG_VERIONS;
        PlayFabClientAPI.PurchaseItem(request, PurchaseResultCallBack, ErroCallBAck);
        Debug.Log(string.Format("PruchaseItem {0}",TestUtilities.PrintCatalogItem(item)));
    }

    private void PurchaseResultCallBack(PurchaseItemResult _result)
    {
        for (int i = 0; i < _result.Items.Count; i++)
        {
            Debug.Log(string.Format("PruchaseItem success {0}", TestUtilities.PrintItemInstance(_result.Items[i])));
        }

    }
    private void GetUserInventory(){
        GetUserInventoryRequest request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, GetUserInvetoryResult,ErroCallBAck);
    }

    private void GetUserInvetoryResult(GetUserInventoryResult _result)
    {
        Debug.Log("GetUserInvetoryResult: ");
        for (int i = 0; i < _result.Inventory.Count; i++)
        {
            Debug.Log(TestUtilities.PrintItemInstance2(_result.Inventory[i]));
        }
        Debug.Log(string.Format("Virtual currency: {0}", TestUtilities.GetVirtualCurrency(_result.VirtualCurrency)));
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