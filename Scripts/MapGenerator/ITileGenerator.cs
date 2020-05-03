using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileGenerator
{
    Dictionary<Tuple<int, int>, Transform> Generate();
}
