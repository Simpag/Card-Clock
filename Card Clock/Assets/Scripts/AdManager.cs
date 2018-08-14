using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    [SerializeField]
    private string
        androidGameId = "2740072",
        iosGameId = "2740070";

    private GameManager GameManager;

    void Start()
    {
        string gameId = null;

        #if UNITY_ANDROID
        gameId = androidGameId;
        #elif UNITY_IOS
        gameId = iosGameId;
        #endif

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId);
        }

        GameManager = FindObjectOfType<GameManager>();
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions() { resultCallback = HandleAdResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleAdResult (ShowResult _result)
    {
        switch (_result)
        {
            case ShowResult.Finished:
                Debug.Log("finished");
                GameManager.ShowNextCard();
                break;
        }
    }
}
