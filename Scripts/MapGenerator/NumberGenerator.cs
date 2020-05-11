using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberGenerator
{
    public static int[] GenerateNumber(int numberOfTiles)
    {
        var number = new int[] { 2, 3, 4, 5, 6, 8, 9, 10, 11, 12 };
        var finishNumber = new int[numberOfTiles];
        number.Shuffle();
        int k = 0;

        for (int i = 0; i < numberOfTiles; i++)
        {
            finishNumber[i] = number[k];

            k++;
            if (k == number.Length)
                k = 0;
        }

        return finishNumber;
    }
}

