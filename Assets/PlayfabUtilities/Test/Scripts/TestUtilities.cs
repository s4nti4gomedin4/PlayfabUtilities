using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public class TestUtilities  {

    public static string PrintCatalogItem(CatalogItem item)
    {
        return string.Format("item id: {0} - DisplayName: {1} - {2}", item.ItemId, item.DisplayName, GetVirtualCurrency(item.VirtualCurrencyPrices));
    }
    public static string PrintItemInstance(ItemInstance item)
    {
        return string.Format("item id: {0} - DisplayName: {1} - PurchaseDate: {2} - UnitCurrency: {3} - UnitPrice: {4} ", item.ItemId, item.DisplayName, item.PurchaseDate, item.UnitCurrency, item.UnitPrice);
    }
    public static string PrintItemInstance2(ItemInstance item)
    {
        return string.Format("Item id: {0} - ItemInstanceId: {1} - PurchaseDate: {2} - CatalogVersion: {3} - DisplayName: {4} - UnitCurrency: {5} - UnitPrice: {6} -", item.ItemId,item.ItemInstanceId,item.PurchaseDate, item.CatalogVersion, item.DisplayName,item.UnitCurrency,item.UnitPrice);
    }

	internal static object PrintCloudScriptResult(ExecuteCloudScriptResult obj)
	{
        return string.Format("Function Name: {0}, Revision: {1}, Logs: {2}, ExecutionTimeSeconds: {3}, MemoryConsumeBytes: {4}, APIRequestsIssued: {5}, HttpRequestsIssued: {6} ,FunctionResult: {7},Error: {8}",obj.FunctionName,obj.Revision,PrintLog(obj.Logs),obj.ExecutionTimeSeconds,obj.MemoryConsumedBytes,obj.APIRequestsIssued,obj.HttpRequestsIssued,obj.FunctionResult,PrintError(obj.Error));
	}

	private static object PrintError(ScriptExecutionError error)
    {
        
        if(error!=null){
            return string.Format("Error: {0},Error Message: {1}",error.Error,error.Message);
        }else{
            return "No error";
        }
    }

    private static string PrintLog(List<LogStatement> logs)
    {
        var sb = new System.Text.StringBuilder(logs.Count);
        for (int i = 0; i < logs.Count; i++)
        {
        // do something with entry.Value or entry.Key
            sb.Append(string.Format("[Level: {0} - Message: {1}, Data:{2}]", logs[i].Level, logs[i].Message,logs[i].Data));
        }
        return sb.ToString();
    }

    public  static string PrintStoreItem(StoreItem storeItem)
    {
        return string.Format("ItemId: {0},{1} ", storeItem.ItemId, TestUtilities.GetVirtualCurrency(storeItem.VirtualCurrencyPrices));
    }
    public static string GetVirtualCurrency<T>(Dictionary<string, T> virtualCurrencyPrices)
    {
        var sb = new System.Text.StringBuilder(virtualCurrencyPrices.Count);
        foreach (KeyValuePair<string, T> entry in virtualCurrencyPrices)
        {
            // do something with entry.Value or entry.Key
            sb.Append(string.Format("[currency: {0} - price: {1}]", entry.Key, entry.Value));
        }
        return sb.ToString();
    }
}
