using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Task01 : MonoBehaviour
{
    public string[] A = { "1", "10", "9", "4", "8", "7" };
    public int[] B = { 2, 7, 4 };
    public int[] combined;
    int[] AInt;

    int tortoise;
    int hare;

    //untuk menemukan duplikasi pada angka diatas maka digunakanlah algoritma Floyd's Tortoise and Hare

    void Update()
    {
        combined = new int[A.Length + B.Length];
        AInt = new int[A.Length];

        for (int i = 0; i < AInt.Length; i++)
        {
            AInt[i] = int.Parse(A[i]);
        }

        combined = AInt.Concat(B).ToArray();

        Debug.Log("All Number is = [" + string.Join(",", new List<int>(combined).ConvertAll(i => i.ToString()).ToArray()) + "]");
        Debug.Log( FindDuplicate(combined.ToArray()) );
    
    }

    int FindDuplicate(int[] nums)
    {

        tortoise = nums[0];
        hare = nums[0];

        while (true) { 
            
            tortoise = nums[tortoise];
            hare = nums[nums[hare]];

            if (tortoise == hare)
            {
                break;
            }
        
        }
        int pointer1 = nums[0];
        int pointer2 = tortoise;
        while (pointer1 != pointer2)
        {
            pointer1 = nums[pointer1];
            pointer2 = nums[pointer2];
        }

        return pointer1;

    }

}
