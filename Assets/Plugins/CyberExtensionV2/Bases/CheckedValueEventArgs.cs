using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public class CheckedValueEventArgs : EventArgs
{
   

    public CheckedValue CheckedValue { get; }
    public int Before { get; }
    public int Current { get; }
    public CheckedValue.Action Action { get; }
    public int ValueWhichObjectTriedToPut { get; }
    public CheckedValueEventArgs(CheckedValue checkedValue, int before, int current, CheckedValue.Action action, int valueWhichObjectTriedToPut)
    {
        CheckedValue = checkedValue ?? throw new ArgumentNullException(nameof(checkedValue));
        Before = before;
        Current = current;
        Action = action;
        ValueWhichObjectTriedToPut = valueWhichObjectTriedToPut;
    }




}