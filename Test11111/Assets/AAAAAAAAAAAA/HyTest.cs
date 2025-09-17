using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test(new List<int>() { 5, 4, 3, 2, 1 });
    }    
    public static void Test(List<int> list)
    {
        for (int i = 1; i < list.Count - 1; i++)
        {
            int a = list[i];
    
            int j = i - 1;
            while (j >= 0 && list[j] > a)
            {
                list[j + 1] = list[j];
                j--;
            }
            list[j + 1] = a;
        }
    }

}
