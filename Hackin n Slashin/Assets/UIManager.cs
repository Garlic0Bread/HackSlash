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
        if ((Input.anyKeyDown) && ((counterTick > 0f) && (counterTick < ((interval_Count*songInterval) - ((interval_Count *songInterval) * 0.75f)))))
        {
            hitCounter++;
            startDecreasing = interval_Count * 16f;
        }
        if (Input.anyKeyDown && (counterTick > ((interval_Count*songInterval) - ((interval_Count*songInterval) * 0.25f))) && (counterTick < (interval_Count* songInterval)))
        {
            hitCounter++;
            startDecreasing = interval_Count * 16f;
        }
        if(Input.anyKeyDown && counterTick == 0f)
        {
            hitCounter = hitCounter + 2;
            startDecreasing = interval_Count * 16f;
        }
        if(Input.anyKeyDown && (counterTick > ((interval_Count* songInterval) -((interval_Count*songInterval) *0.75f))) && (counterTick < ((interval_Count * songInterval) - ((interval_Count * songInterval) * 0.25f)))) 
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
