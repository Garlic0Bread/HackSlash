using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManagerPlayer2 : MonoBehaviour
{
    public BeatManager beatManagerScript;
    public UIManager UIM;
    public TMP_Text textP2;
    public TMP_Text counterTextP2;
    public TMP_Text HitCounterTextP2;
    public TMP_Text TimeTillDecreaseTextP2;
    public float hitCounterP2;
    public float interval_CountP2;
    public string intertvalCountStringP2;
    public Intervals intervalP2;
    public float bpmP2;
    public float stepCountP2;
    public float counterTickP2;
    public float songIntervalP2;
    public float startDecreasing;
    public GameObject Player2ComboFX1;
    public GameObject Player2ComboFX2;
    public GameObject Player2ComboFX3;
    
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
        if(UIM == null)
        {
            UIM = FindObjectOfType<UIManager>();
        }
    }

    private void Start()
    {
        if (beatManagerScript != null)
        {
            bpmP2 = beatManagerScript._bpm;
        }
        interval_CountP2 = UIM.interval_Count;
        intertvalCountStringP2 = UIM.intertvalCountString;
        stepCountP2 = UIM.stepCount;
        counterTickP2 = UIM.counterTick;
        songIntervalP2 = UIM.songInterval;

        interval_CountP2 = (60f / (bpmP2 * stepCountP2));
        intertvalCountStringP2 = interval_CountP2.ToString();
        hitCounterP2 = 0;
        StartCoroutine(DecreaseScoreP2());
        startDecreasing = interval_CountP2 * 16f;
    }

    public void EnablecomboFXP2()
    {
        if (hitCounterP2 >= 5f && hitCounterP2 < 10f)
        {
            Player2ComboFX1.SetActive(true);
        }
        if (hitCounterP2 >= 10f && hitCounterP2 < 15f)
        {
            Player2ComboFX2.SetActive(true);
            Player2ComboFX1.SetActive(false);
        }
        if (hitCounterP2 >= 15f)
        {
            Player2ComboFX3.SetActive(true);
            Player2ComboFX2.SetActive(false);
        }
        if (hitCounterP2 < 5f)
        {
            Player2ComboFX1.SetActive(false);
            Player2ComboFX2.SetActive(false);
            Player2ComboFX3.SetActive(false);
        }
    }

    public void intervalCounterP2()
    {
        counterTickP2 += Time.deltaTime;
        if (counterTickP2 / songIntervalP2 >= interval_CountP2)
        {
            counterTickP2 = 0;
        }
    }

    public void CheckIfPressedP2()
    {
        
                Debug.Log("P22222222222222222222222222 script");
                if ((_InputManager1.isAttacking) && ((counterTickP2 > 0f) && (counterTickP2 < ((interval_CountP2 * songIntervalP2) - ((interval_CountP2 * songIntervalP2) * 0.75f)))))
                {
                    hitCounterP2 ++;
                    startDecreasing = interval_CountP2 * 16f;
                }
                if (_InputManager1.isAttacking && (counterTickP2 > ((interval_CountP2 * songIntervalP2) - ((interval_CountP2 * songIntervalP2) * 0.25f))) && (counterTickP2 < (interval_CountP2 * songIntervalP2)))
                {
                    hitCounterP2 ++;
                    startDecreasing = interval_CountP2 * 16f;
                }
                if (_InputManager1.isAttacking && counterTickP2 == 0f)
                {
                    hitCounterP2 = hitCounterP2 + 2;
                    startDecreasing = interval_CountP2 * 16f;
                }
                if (_InputManager1.isAttacking && (counterTickP2 > ((interval_CountP2 * songIntervalP2) - ((interval_CountP2 * songIntervalP2) * 0.75f))) && (counterTickP2 < ((interval_CountP2 * songIntervalP2) - ((interval_CountP2 * songIntervalP2) * 0.25f))))
                {
                    hitCounterP2 = 0f;
                }
                
    }

    // Update is called once per frame
    void Update()
    {
        

        textP2.SetText(intertvalCountStringP2);
        counterTextP2.SetText(counterTickP2.ToString());
        HitCounterTextP2.SetText(hitCounterP2.ToString());
        TimeTillDecreaseTextP2.SetText(startDecreasing.ToString());
        intervalCounterP2();
        CheckIfPressedP2();
        startDecreasing -= Time.deltaTime;
        EnablecomboFXP2();

    }



    IEnumerator DecreaseScoreP2()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            if (hitCounterP2 > 0f && startDecreasing <= 0f)
            {
                hitCounterP2--;
            }
        }
    }
}
