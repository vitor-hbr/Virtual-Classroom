using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChairGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tableChairPair;
    [SerializeField] private int columns = 8;
    [SerializeField] private int numberOfPairs = 10;
    [SerializeField] private float maxWidth = 10f;
    [SerializeField] private float rowSpacing = 2f;

    void Start()
    {
      InstantiateTableChairPairs();
    }

    void InstantiateTableChairPairs()
    {
        int rows = (int) Mathf.Ceil((float) numberOfPairs / (float) columns);  
        int remainder = numberOfPairs;

        for(int row = 0; row < rows; row++)
        {
            int numberOfPairsInRow = remainder - columns > 0 ? columns : remainder;
            remainder -= numberOfPairsInRow;

            for(int column = 0; column < numberOfPairsInRow; column++)
            {
                float x = numberOfPairsInRow > 1 ? (column - (numberOfPairsInRow - 1) / 2f) * maxWidth / (numberOfPairsInRow - 1) : 0f;
                float y = 0;
                float z = -row * rowSpacing;

                Vector3 position = new Vector3(x, y, z);
                GameObject newPair = Instantiate(tableChairPair, transform, false);
                newPair.transform.localPosition = position;
            }
        }
    }
}
