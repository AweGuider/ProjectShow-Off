using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImageSetter : MonoBehaviour
{
    public bool SetImages(List<Sprite> sprites)
    {
        if (sprites == null)
        {
            Debug.LogError($"Sprites are NULL");
            return false;
        }
        if (sprites == null || sprites.Count != transform.childCount)
        {
            Debug.LogError($"Amount of characters != amount of character boxes.");
            return false;
        }

        for (int i = 0; i < sprites.Count; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = sprites[i];
        }
        return true;
    }

}
