using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension {

    public static string Change(this string source, int pos, int word)
    {
        string tmp = string.Empty;

        for (int i = 0; i < source.Length; i++)
        {
            if (i == pos)
            {
                tmp += (char)word;
                continue;
            }

            tmp += source[i];
        }

        return tmp;
    }

}
