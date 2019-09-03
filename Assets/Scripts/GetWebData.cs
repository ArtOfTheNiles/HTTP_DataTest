using System.Collections;
using System.Net.Http;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GetWebData : MonoBehaviour
{
    public List<string> urlStrings = new List<string>
    {
        "https://www.nasa.gov/sites/default/files/thumbnails/image/potw1934a.jpg",
        "https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/1-pia16604-1041.jpg",
        "https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/potw1933a_0.jpg",
        "https://www.nasa.gov/sites/default/files/thumbnails/image/stsci-h-p1936a-m-1999x2000.png",
        "https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/as15-88-11901orig.jpg",
        "https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/maf_20190626_p_lox_sta_roll_out-316.jpg",
        "https://i.chzbgr.com/full/9327040000/h3F95D9F6/",
        "https://i.chzbgr.com/full/8979792896/h7ECA74F1/",
        "https://i.chzbgr.com/full/8965164544/h43B279EB/",
        "https://i.chzbgr.com/full/8821933312/h12F8C51C/",
        "https://i.chzbgr.com/full/9178678784/h0609E2AD/",
        "https://i.chzbgr.com/full/8819198464/hAE19817E/",
        "https://i.chzbgr.com/full/8965259008/h8A17B3B6/",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/46af2927b899d4979ea863b5f43988f2/5E0AA8CE/t51.2885-15/sh0.08/e35/s640x640/66313614_971031603241884_5885353626125408273_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/a40cec0c1748b7a2b36cc9813e0365c4/5DFD4D2E/t51.2885-15/sh0.08/e35/c0.78.770.770/s640x640/67875682_411759082782469_6851761287881993224_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/86b9d592ede1fd0c003c1f3d025408e2/5DEF9870/t51.2885-15/e15/s640x640/61698216_153979719009681_8891315229853522764_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/fe95f225dd5c693f968b752d1cd1cb95/5E01493E/t51.2885-15/sh0.08/e35/s640x640/70393934_709147536197053_7206713841999784605_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/00f56d5d96b5d6fb5c4616c304da7f1e/5E113E40/t51.2885-15/sh0.08/e35/c0.180.1440.1440/s640x640/68900820_141471640408499_5909136258292599348_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/209d5241e4c1d9430951eee879254b2f/5E0B1BCC/t51.2885-15/sh0.08/e35/c0.129.1034.1034a/s640x640/67543547_485019202280245_399267023991977389_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/58b128b1e0beb385bd187b13711e7df4/5DFBE9F3/t51.2885-15/sh0.08/e35/c0.39.1080.1080a/s640x640/67637354_414152862554748_6644809960581315193_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/a9d7093f4849bfcdf19db8c9f559aede/5E10B1EC/t51.2885-15/sh0.08/e35/c0.171.1440.1440a/s640x640/69230287_126285712005938_4515778671989930399_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/da16e43312e38ab52e7f24a15b656bf1/5E0952FF/t51.2885-15/sh0.08/e35/c0.28.1440.1440/s640x640/68976388_661210684392336_2605290772633589556_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net",
        "https://instagram.fdtw1-1.fna.fbcdn.net/vp/7cb7c18ee5c247f03ec528d12259e42a/5E0BF2A1/t51.2885-15/sh0.08/e35/c0.120.960.960a/s640x640/69053731_934179126936132_3871226860683499965_n.jpg?_nc_ht=instagram.fdtw1-1.fna.fbcdn.net"
    };

    public TextMeshProUGUI urlDisplayText;

    private List<bool> urlViewed = new List<bool>();
    private int numEntries;
    private Image image;
    private Texture2D displayTexture;
    private RectTransform myRectTransform;
    private ContentSizeFitter myContentSizeFitter;

    void Awake()
    {
        numEntries = urlStrings.Count;
        image = GetComponent<Image>();
        myRectTransform = GetComponent<RectTransform>();
        myContentSizeFitter = GetComponent<ContentSizeFitter>();
        InitializeBools();
    }

    private void InitializeBools()
    {
        for (int i = 0; i < numEntries; i++)
        {
            urlViewed.Add(false);
        }
    }

    private void CheckBools()
    {
        int boolsCountedTrue = 0;
        int totalBools = numEntries;

        for (int i = 0; i < totalBools; i++)
        {
            if (urlViewed[i] == true)
            {
                boolsCountedTrue++;
            }
        }

        if (boolsCountedTrue >= totalBools)
        {
            for (int i = 0; i < totalBools; i++)
            {
                urlViewed[i] = false;
            }
        }


    }

    public void RandomImage()
    {
        int randomIndex = GetRandomNumber();
        if (urlViewed[randomIndex] == true)
        {
            CheckBools();
            RandomImage();
        }
        else
        {
            urlViewed[randomIndex] = true;
            if (urlDisplayText != null)
            {
                urlDisplayText.text = urlStrings[randomIndex];
            }
            StartCoroutine(GetTexture(urlStrings[randomIndex]));
        }

    }

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            float displayRatio = 1f;
            displayTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            if (displayTexture.height > 700f)
            {
                displayRatio = 700f / displayTexture.height;
            }
            Sprite temp = Sprite.Create(displayTexture, new Rect(0f,0f,displayTexture.width,displayTexture.height ), new Vector2(displayTexture.width/2, displayTexture.height/2));
            image.overrideSprite = temp;

//            image.SetNativeSize();
            myRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, displayTexture.width * displayRatio);
            myRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, displayTexture.height * displayRatio);
            myRectTransform.ForceUpdateRectTransforms();
        }
    }

    public void CloseProgram()
    {
        Application.Quit();
    }

    private int GetRandomNumber()
    {
        int thisRandom = Mathf.RoundToInt(Random.Range(0, numEntries));
        return thisRandom;
    }
}
