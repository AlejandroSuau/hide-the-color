using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EggColor
{
    Yellow,
    Green,
    Red,
    Blue
}

public interface IEgg
{
    void Touch();
}
