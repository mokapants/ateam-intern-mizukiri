using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpCommunication : MonoBehaviour
{
	Transform scrollContent;
	// 接続するURL
	private const string URL = "http://tk2-213-16074.vs.sakura.ne.jp/spRush/Skin/";

	// 用意してある石のprefab
	private GameObject[] stones;

	void Start()
	{
		scrollContent = GameObject.Find("Content").transform;

		stones = Resources.LoadAll<GameObject>("prefabs/skins");

		// コルーチンを呼び出す
		StartCoroutine("OnSend", URL);
	}

	// コルーチン
	IEnumerator OnSend(string url)
	{
		// 6個の画像をダウンロード
		// ここの実装無理やりすぎて泣きたい
		for (int i = 0; i < 6; i++)
		{
			//URLをGETで用意
			UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url + i.ToString() + ".png");
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
				Texture texture = ((DownloadHandlerTexture) webRequest.downloadHandler).texture;
				stones[i].GetComponent<RawImage>().texture = texture;
			}

			Instantiate(stones[i], scrollContent);
		}
	}
}