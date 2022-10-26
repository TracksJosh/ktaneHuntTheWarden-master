using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class HuntTheWardenScript : MonoBehaviour {

    public Renderer backing;
    public KMAudio audio;
    public KMBombModule bombModule;
    public KMSelectable module;
    public KMSelectable testButton;
    public GameObject Status;
    public GameObject Darkness;
    public GameObject Tendril1;
    public GameObject Tendril2;
    public GameObject Tendril3;
    public GameObject Tendril4;
    public Sprite[] TendrilInactive;
    public Sprite[] TendrilActive;
    public AudioClip[] Active;
    public AudioClip[] Inactive;

    private bool stage2 = false;

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool _isSelected;
    private bool _isSolved;

    private float uvAnimationTileX = 20;
    private float uvAnimationTileY = 1;
    private float framesPerSecond = 30.0f;
    private int index;
    private bool anim;
    private bool anim2;
    private bool tend;

    // Use this for initialization
    void Start() {
        moduleId = moduleIdCounter++;
        Darkness.SetActive(false);
        Status.SetActive(false);
        module.OnFocus += delegate { _isSelected = true; };
        module.OnDefocus += delegate { _isSelected = false; };
        testButton.OnInteract += delegate { if (!tend) TestingButton(); return false; };
        index = Mathf.FloorToInt(Time.deltaTime * framesPerSecond);
        index = Mathf.FloorToInt(index % (uvAnimationTileX * uvAnimationTileY));
        index = 0;
        StartCoroutine(AnimateBack());
        StartCoroutine(AnimateTend());
    }

    // Update is called once per frame
    void Update() {

    }

    void TestingButton()
    {
        if (!tend)
        {
            tend = true;
        }
    }

    IEnumerator AnimateBack()
    {
        anim = true;
        if (anim)
        {
            
            if (index >= uvAnimationTileX * uvAnimationTileY)
            {
                index = 0;
            }
            Vector2 offset = new Vector2((index / uvAnimationTileX) / uvAnimationTileY, index / uvAnimationTileX - (index / uvAnimationTileX));
            index++;
            backing.sharedMaterial.SetTextureOffset("_MainTex", offset);
            yield return new WaitForSeconds(0.2f);
            anim = false;
            StartCoroutine(AnimateBack());
        }

    }

   IEnumerator AnimateTend()
    {
        anim2 = true;
        if (anim2)
        {
            if (!tend)
            {
                for(int i = 0; i < 16; i++)
                {
                    Tendril1.GetComponent<SpriteRenderer>().sprite = TendrilInactive[i];
                    Tendril2.GetComponent<SpriteRenderer>().sprite = TendrilInactive[i];
                    Tendril3.GetComponent<SpriteRenderer>().sprite = TendrilInactive[i];
                    Tendril4.GetComponent<SpriteRenderer>().sprite = TendrilInactive[i];
                    yield return new WaitForSeconds(0.1f);
                }
                
            }
            if (tend)
            {
                int b = Rnd.Range(0, 5);
                switch (b)
                {
                    case 0:
                        audio.PlaySoundAtTransform("active1", transform);
                        break;
                    case 1:
                        audio.PlaySoundAtTransform("active2", transform);
                        break;
                    case 2:
                        audio.PlaySoundAtTransform("active3", transform);
                        break;
                    case 3:
                        audio.PlaySoundAtTransform("active4", transform);
                        break;
                    case 4:
                        audio.PlaySoundAtTransform("active5", transform);
                        break;
                }
                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Tendril1.GetComponent<SpriteRenderer>().sprite = TendrilActive[i];
                        Tendril2.GetComponent<SpriteRenderer>().sprite = TendrilActive[i];
                        Tendril3.GetComponent<SpriteRenderer>().sprite = TendrilActive[i];
                        Tendril4.GetComponent<SpriteRenderer>().sprite = TendrilActive[i];
                        yield return new WaitForSeconds(0.05f);
                    }
                }
                b = Rnd.Range(0, 5);
                switch (b)
                {
                    case 0:
                        audio.PlaySoundAtTransform("inactive1", transform);
                        break;
                    case 1:
                        audio.PlaySoundAtTransform("inactive2", transform);
                        break;
                    case 2:
                        audio.PlaySoundAtTransform("inactive3", transform);
                        break;
                    case 3:
                        audio.PlaySoundAtTransform("inactive4", transform);
                        break;
                    case 4:
                        audio.PlaySoundAtTransform("inactive5", transform);
                        break;
                }
                tend = false;
            }
            anim2 = false;
            StartCoroutine(AnimateTend());
        }
    }
   
}
