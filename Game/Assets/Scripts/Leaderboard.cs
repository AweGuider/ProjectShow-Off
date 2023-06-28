using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] Transform playerBoxesParent;

    [SerializeField]
    private List<Sprite> charactersTextures;

    // Start is called before the first frame update
    void Start()
    {
        GameData data = GameData.Instance;

        for (int i = 0; i < playerBoxesParent.childCount; i++)
        {
            if (i > data.PlayersPlaying - 1)
            {
                playerBoxesParent.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            var player = data.LeaderboardList[i];
            var character = data.PlayersConfigurations[player].character;

            playerBoxesParent.GetChild(i).GetComponent<Image>().sprite = charactersTextures[character];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
