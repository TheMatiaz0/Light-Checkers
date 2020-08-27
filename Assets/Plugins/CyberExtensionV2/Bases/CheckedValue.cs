#if UNITY_EDITOR || UNITY_STANDALONE ||UNITY_WII|| UNITY_IOS || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS4 || UNITY_XBOXONE || UNITY_LUMIN || UNITY_TIZEN ||UNITY_TVOS || UNITY_WSA || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_10_0 ||UNITY_WEBGL ||UNITY_FACEBOOK||UNITY_FACEBOOK||UNITY_ADS || UNITY_ANALYTICS ||UNITY_ASSERTIONS ||UNITY_64
#define ANY_UNITY
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;

[Serializable]
public class CheckedValue
{

    public enum Action
    {
        Add,
        Remove
    }


#if ANY_UNITY
    [SerializeField]  
#endif
    private int _Max;
#if ANY_UNITY
    [SerializeField]
#endif
    private int _Min;
#if ANY_UNITY
    [SerializeField]
#endif
    private int _Current;

    public int Current
    {
        get => _Current;
        set
        {
            if (value == _Current)
                return;
            int before = _Current;
            _Current = CheckedGet(_Min, _Max, value);
            OnValueChanged(this, new CheckedValueEventArgs
                (this,
                before,
                _Current,
                (_Current > before) ? Action.Add : Action.Remove,
                value));
              
        }
    }
    public int Max => _Max;
    public int Min => _Min;

    public event EventHandler<CheckedValueEventArgs> OnValueChanged = delegate { };
    public event EventHandler<CheckedValueEventArgs> OnValueAdd = delegate { };
    public event EventHandler<CheckedValueEventArgs> OnValueRemove = delegate { };

    public CheckedValue(int min, int max, int current)
    {
        if (max > min == false)
            throw new ArgumentException("\"max\" have to be bigger than min", nameof(max));
        _Max = max;
        _Min = min;
        Current = current;
        OnValueChanged +=
            (s, e) =>
            {
                if (e.Action == CheckedValue.Action.Add)
                    OnValueAdd(this, e);
                else if (e.Action == CheckedValue.Action.Remove)
                    OnValueRemove(this, e);
            };
    }
    public void Add(uint val)
    {
        Current += (int)val;
    }
    public void Remove(uint val)
    {
        Current -= (int)val;
    }

    public static int CheckedGet(int min, int max, int value)
    {
        if (value > max)
            return max;
        else if (value < min)
            return min;
        else return value;
    }
}