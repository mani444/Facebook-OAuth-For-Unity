//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using System;
using com.shephertz.app42.paas.sdk.csharp.game;

public class SaveScoreCallBack : App42CallBack {
	
	public static string fbUserName = "";
	public static string fbUserProfilePic = "";
	public static string fbUserId = "";
	public static bool isConnected = false;
	
	public static bool fromSaveScore = false;
	public static bool fromLeaderBoard = false;
	
	
	public static Dictionary<string,object> metaInfo = new Dictionary<string,object>();
	public static Dictionary<string,object> nameAndPP = new Dictionary<string,object>();
	
	
	public static IList<object> fList = new List<object> ();
	
	
	
	AppController fbUserCon = new AppController ();
	
	public static string fbAccessToken = "";
	
	public void OnSuccess (object response)
	{

		if (response is Game){
			Game gameObj = (Game)response;
			Debug.Log("Game ::: "+gameObj.ToString());
			if (fromLeaderBoard){
				if(gameObj.GetScoreList().Count > 0){
					for (int i=0; i<gameObj.GetScoreList().Count; i++){
						string name = gameObj.GetScoreList()[i].GetFacebookProfile().GetName();
						string score = gameObj.GetScoreList()[i].GetValue().ToString();
						string jsonDoc = gameObj.GetScoreList()[i].GetJsonDocList()[0].GetJsonDoc();
						var parser = SimpleJSON.JObject.Parse(jsonDoc);
						string profilePic = parser["profilePic"];
						string userId = parser["userId"];
						Texture.GetInstance().ExecuteShow(name, profilePic);
						
						Debug.Log("userId ::: " + userId);
						
						IList<string> slist1 = new List<string>();
					//	slist1.Add(userId);
						slist1.Add(name);
						slist1.Add(profilePic);
						slist1.Add(score);
						fList.Add(slist1);
					}
				}
				fromLeaderBoard = false;
				Application.LoadLevel("LeaderBoardScene");
			}
			if (fromSaveScore){
				LoadingMessage.SetMessage("Score SuccessFully Saved.");
				//fromSaveScore = false;
			}
			
		}

	}
	
	
	public void OnException (Exception e)
	{
		Debug.Log("Exception :: "+e.ToString());
	}
	
	
	public static IList<object> GetList(){
		return fList;
	}
	

	
}


