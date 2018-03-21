using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class TestPlayfab : MonoBehaviour {

    public string m_TitleID;
    public string CATALOG_VERIONS;

    [Header("LoginWithCustomID")]
    public string m_CustomId;

	public void Start()
	{
        TestConnect();

	}
	
    public void TestConnect(){
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
        {
            CustomId = m_CustomId,
            TitleId = m_TitleID,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, ResultcallBack, ErroCallBAck);
    }
    public void ResultcallBack(LoginResult _LoginResult){
        Debug.Log("Login success");
        GetUserInventory();
        //GetCatalog();
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
            Debug.Log(PrintCatalogItem(_result.Catalog[i]));
            
        }
        if(_result.Catalog.Count>0){
            PruchaseItem(_result.Catalog[0]);
        }

       
    }
    public string PrintCatalogItem(CatalogItem item){
        return string.Format("item id: {0} - DisplayName: {1} - {2}", item.ItemId,item.DisplayName, GetVirtualCurrency(item.VirtualCurrencyPrices));
    }
    public string PrintItemInstance(ItemInstance item)
    {
        return string.Format("item id: {0} - DisplayName: {1} - PurchaseDate: {2} - UnitCurrency: {3} - UnitPrice: {4} ", item.ItemId, item.DisplayName, item.PurchaseDate,item.UnitCurrency,item.UnitPrice);
    }

    private string GetVirtualCurrency<T>(Dictionary<string, T> virtualCurrencyPrices)
    {
        var sb = new StringBuilder(virtualCurrencyPrices.Count);
        foreach (KeyValuePair<string, T> entry in virtualCurrencyPrices)
        {
            // do something with entry.Value or entry.Key
            sb.Append(string.Format("[currency: {0} - price: {1}]",entry.Key,entry.Value));
        }
        return sb.ToString();
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
        Debug.Log(string.Format("PruchaseItem {0}",PrintCatalogItem(item)));
    }

    private void PurchaseResultCallBack(PurchaseItemResult _result)
    {
        for (int i = 0; i < _result.Items.Count; i++)
        {
            Debug.Log(string.Format("PruchaseItem success {0}", PrintItemInstance(_result.Items[i])));
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
            Debug.Log(string.Format("Item id: {0} - ItemInstanceId: {1} - PurchaseDate: {2} - CatalogVersion: {3} - DisplayName: {4} - UnitCurrency: {5} - UnitPrice: {6} -",_result.Inventory[i].ItemId,_result.Inventory[i].ItemInstanceId,_result.Inventory[i].PurchaseDate,_result.Inventory[i].CatalogVersion,_result.Inventory[i].DisplayName,_result.Inventory[i].UnitCurrency,_result.Inventory[i].UnitPrice));
        }
        Debug.Log(string.Format("Virtual currency: {0}", GetVirtualCurrency(_result.VirtualCurrency)));
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