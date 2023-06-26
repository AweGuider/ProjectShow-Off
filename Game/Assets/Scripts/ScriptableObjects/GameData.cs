using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Manager/GameData")]
public class GameData : SingletonScriptableObject<GameData>
{
    [SerializeField]
    private int _numberOfPlayers;
    [SerializeField]
    private int _numberOfPlayersPlaying;
    public int PlayersPlaying { get => _numberOfPlayersPlaying; set => _numberOfPlayersPlaying = value; }

    [SerializeField]
    private bool _testing;
    public bool IsTesting { get => _testing; }

    //[SerializeField]
    //public List<(InputDevice device, int character)> PlayersConfigurations;
    [SerializeField]
    public Dictionary<int, (InputDevice device, int character)> PlayersConfigurations = new Dictionary<int, (InputDevice device, int character)>();
    //[SerializeField]
    //public Map chosenMap;
    //[SerializeField]
    //public Kart[] chosenKarts;

    private void Awake()
    {
        _numberOfPlayers = 2;
        PlayersConfigurations = new Dictionary<int, (InputDevice device, int character)>();
        DontDestroyOnLoad(this);
    }

    public void AddOrSetPlayerConfiguration(int playerID, InputDevice device = null, int character = -1)
    {
        if (!PlayersConfigurations.ContainsKey(playerID))
        {

            var newConfig = (device, character); // Create a new configuration
            PlayersConfigurations[playerID] = newConfig;
        }
        else
        {
            var playerConfig = PlayersConfigurations[playerID];

            if (device != null) playerConfig.device = device;

            if (character != -1) playerConfig.character = character;

            PlayersConfigurations[playerID] = playerConfig; // Update the dictionary entry
        }
    }

    public void RemovePlayerConfiguration(int playerID)
    {
        PlayersConfigurations.Remove(playerID);
    }

    public void SetNumberOfPlayers(int players)
    {
        _numberOfPlayers = players;
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
        return _numberOfPlayers;
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
