using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public BeatManager beatManagerScript;
    public TMP_Text text;
    public TMP_Text counterText;
    public TMP_Text HitCounterText;
    public TMP_Text TimeTillDecreaseText;
    public float hitCounter;
    public float interval_Count;
    public string intertvalCountString;
    public Intervals interval;
    public float bpm;
    public float stepCount;
    public float counterTick;
    public float songInterval;
    public float startDecreasing;
    public GameObject Player1ComboFX1;
    public GameObject Player1ComboFX2;
    public GameObject Player1ComboFX3;


    //if step count = 0.25 and interval = 1 you have to hit notes 1 & 3 in 1,2,3,4 (in halftime)
    //if step count = 0.5 and interval = 1 you have to hit notes 1,2,3 & 4 (in halftime or 4 beats per bar)
    //if step count = 1 and interval = 1 you have to hit notes 1,2,3,4,5,6,7 & 8 (in 8 beats per bar)
    //if step count = 1 and interval = 0.5 you have to hit every 16th note in the bar.

    // Start is called before the first frame update

    void Awake()
    {
        if (beatManagerScript == null)
        {
            beatManagerScript = FindObjectOfType<BeatManager>();
            if (beatManagerScript == null)
            {
                Debug.LogError("No BeatManager found in the scene!");
            }
        }
    }
    
    void Start()
    {
        if (beatManagerScript != null)
        {
            bpm = beatManagerScript._bpm;
        }
        else
        {
            Debug.LogError("beatManagerScript is not assigned!");
        }
        interval_Count = (60f / (bpm * stepCount));
        intertvalCountString = interval_Count.ToString();
        hitCounter = 0;
        StartCoroutine(DecreaseScore());
        startDecreasing = interval_Count * 16f;
    }

    // Update is called once per frame
    void Update()
    {
        //interval_Count = interval.GetIntervalLength(bpm).ToString();
        text.SetText(intertvalCountString);
        counterText.SetText(counterTick.ToString());
        HitCounterText.SetText(hitCounter.ToString());
        TimeTillDecreaseText.SetText(startDecreasing.ToString());
        intervalCounter();
        CheckIfPressed();
        startDecreasing -= Time.deltaTime;
        Enablecombo();
    }
    

    public void Enablecombo()
    {
        if (hitCounter >= 5f && hitCounter < 10f)
        {
            Player1ComboFX1.SetActive(true);
        }
        if(hitCounter >= 10f && hitCounter < 15f)
        {
            Player1ComboFX2.SetActive(true);
            Player1ComboFX1.SetActive(false);
        }
        if(hitCounter >= 15f)
        {
            Player1ComboFX3.SetActive(true);
            Player1ComboFX2.SetActive(false);
        }
        if(hitCounter < 5f)
        {
            Player1ComboFX1.SetActive(false);
            Player1ComboFX2.SetActive(false);
            Player1ComboFX3.SetActive(false);
        }
            
    }



    public void GetTimeTillDecrease()
    {
        //return startDecreasing;
    }

    public void intervalCounter()
    {
        counterTick += Time.deltaTime;
        if(counterTick/songInterval >= interval_Count)
        {
            counterTick = 0;
        }
    }

    public void CheckIfPressed()
    {
        
                Debug.Log("P111111111111111111111111111111111111 script");
                if ((_InputManager.isAttacking) && ((counterTick > 0f) && (counterTick < ((interval_Count * songInterval) - ((interval_Count * songInterval) * 0.75f)))))
                {
                    hitCounter++;
                    startDecreasing = interval_Count * 16f;
                }
                if (_InputManager.isAttacking && (counterTick > ((interval_Count * songInterval) - ((interval_Count * songInterval) * 0.25f))) && (counterTick < (interval_Count * songInterval)))
                {
                    hitCounter ++;
                    startDecreasing = interval_Count * 16f;
                }
                if (_InputManager.isAttacking && counterTick == 0f)
                {
                    hitCounter = hitCounter + 2;
                    startDecreasing = interval_Count * 16f;
                }
                if (_InputManager.isAttacking && (counterTick > ((interval_Count * songInterval) - ((interval_Count * songInterval) * 0.75f))) && (counterTick < ((interval_Count * songInterval) - ((interval_Count * songInterval) * 0.25f))))
                {
                    hitCounter = 0f;
                }
                
            
 
        
    }

    IEnumerator DecreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            if(hitCounter > 0f && startDecreasing <= 0f)
            {
                hitCounter--;
            }
        }
    }
}
