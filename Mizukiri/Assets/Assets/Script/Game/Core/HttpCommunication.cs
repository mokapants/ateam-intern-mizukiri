using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpCommunication : MonoBehaviour
{
	Transform scrollContent;
	Stone stone;
	Image gauge;
	// 接続するURL
	private const string URL = "http://tk2-213-16074.vs.sakura.ne.jp/spRush/Skin/";

	const int length = 6;

	// 用意してある石のprefab
	public static GameObject[] stones;

	void Awake()
	{
		scrollContent = GameObject.Find("Content").transform;
		stones = Resources.LoadAll<GameObject>("prefabs/skins");
		gauge = GameObject.Find("Gauge").GetComponent<Image>();
		gauge.fillAmount = 0;

		if (!GameManager.isFirst)
		{
			for (int i = 0; i < length; i++)
			{
				Debug.Log(stones[i]);
				Instantiate(stones[i], scrollContent);
			}
			this.enabled = false;
			return;
		}
		GameManager.isFirst = false;

		stone = GameObject.Find("stone(Clone)").GetComponent<Stone>();

		// コルーチンを呼び出す
		StartCoroutine("OnSend", URL);
	}

	// コルーチン
	IEnumerator OnSend(string url)
	{
		// 6個の画像をダウンロード
		// ここの実装無理やりすぎて泣きたい
		int gaugeCounter = 0;
		for (int i = -1; i < length; i++)
		{
			Debug.Log("nowloading");
			//URLをGETで用意
			UnityWebRequest webRequest;
			if (i == -1)
			{
				webRequest = UnityWebRequest.Get(url + "unlock.json");
			}
			else
			{
				webRequest = UnityWebRequestTexture.GetTexture(url + i.ToString() + ".png");
			}
			//URLに接続して結果が戻ってくるまで待機
			yield return webRequest.SendWebRequest();

			//エラーが出ていないかチェック
			if (webRequest.isNetworkError || webRequest.isHttpError)
			{
				// 通信失敗
				// Debug.Log("error!" + webRequest.error);
				Debug.Log(url + i.ToString() + ".png");
			}
			else
			{
				// 通信成功
				Debug.Log(i.ToString() + "atta");
				if (i == -1)
				{
					Debug.Log(webRequest.downloadHandler.text);
					SelectStoneManager.unlockData = JsonUtility.FromJson<UnlockData>(webRequest.downloadHandler.text);
					Debug.Log(SelectStoneManager.unlockData.ChallengeCount[0]);
				}
				else
				{
					Texture texture = ((DownloadHandlerTexture) webRequest.downloadHandler).texture;
					stones[i].GetComponent<RawImage>().texture = texture;
				}
			}
			if (i != -1)
			{
				Instantiate(stones[i], scrollContent);
			}
			gaugeCounter++;
			gauge.fillAmount = (float) gaugeCounter / 7;
		}
		stone.ChangeSkin(HttpCommunication.stones);
		GameManager.isLoad = false;
	}
}