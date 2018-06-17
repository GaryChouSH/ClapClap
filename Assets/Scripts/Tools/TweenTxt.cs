using UnityEngine;
using UnityEngine.UI;

public class TweenTxt : MonoBehaviour
{
    Text mTxt;
    public bool isStart = false;
    public long endValue;
    public double executionValue;
    public double speed;
    private bool isStrFormat;
    private bool hasPlusImg;

    // Use this for initialization
    void Start()
    {
        mTxt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
            upChangeTxt();

    }

    public static void valueTo(GameObject _obj, long _start, long _end, float _time, bool _isFormat = true, bool _hasPlusImg = false)
    {
        if (_start - _end == 0)
        {
            return;
        }
        TweenTxt tween = _obj.GetComponent<TweenTxt>();

        if (!tween)
        {
            tween = _obj.AddComponent<TweenTxt>();
        }
        /*
        if (tween.isStart)
        {
            if(_end > tween.endValue)
            return;
        }*/
        tween.endValue = _end;
        tween.speed = (_end - _start) / _time;
        tween.executionValue = _start;
        tween.isStart = true;
        tween.isStrFormat = _isFormat;
        tween.hasPlusImg = _hasPlusImg;
    }

    public static void stop(GameObject _obj)
    {
        TweenTxt tween = _obj.GetComponent<TweenTxt>();
        if (tween)
        {
            tween.isStart = false;
            tween.speed = 0;
        }
    }

    void upChangeTxt()
    {

        executionValue += speed * Time.deltaTime;
        if (speed > 0)
        {
            if (executionValue > endValue)
            {
                executionValue = endValue;
                isStart = false;
            }
        }
        else
        {
            if (executionValue < endValue)
            {
                executionValue = endValue;
                isStart = false;
            }
        }

        if (isStrFormat)
        {
            string str = ((long)executionValue).ToString("#,##0");
            if (hasPlusImg) {
                str = "+" + str;
            }
            mTxt.text = str;
        }
        else
        {
            mTxt.text = ((long)executionValue).ToString();
        }
    }

   
}

