using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/PlayerSelectionData")]
public class PlayerSelectionData : ScriptableObject
{
    [SerializeField]
    private int numberOfPlayers;
    //[SerializeField]
    //public Map chosenMap;
    //[SerializeField]
    //public Character[] chosenCharacters;
    //[SerializeField]
    //public Kart[] chosenKarts;

    private void OnEnable()
    {
        numberOfPlayers = 0;
    }

    public void SetNumberOfPlayers(int players)
    {
        numberOfPlayers = players;
    }

    //public void SetMap(Map map)
    //{
    //    chosenMap = map;
    //}

    //public void SetCharacter(int playerIndex, Character character)
    //{
    //    chosenCharacters[playerIndex] = character;
    //}

    //public void SetKart(int playerIndex, Kart kart)
    //{
    //    chosenKarts[playerIndex] = kart;
    //}

    public int GetNumberOfPlayers()
    {
        return numberOfPlayers;
    }

    //public Map GetMap()
    //{
    //    return chosenMap;
    //}

    //public Character GetCharacter(int playerIndex)
    //{
    //    return chosenCharacters[playerIndex];
    //}

    //public Kart GetKart(int playerIndex)
    //{
    //    return chosenKarts[playerIndex];
    //}

}
