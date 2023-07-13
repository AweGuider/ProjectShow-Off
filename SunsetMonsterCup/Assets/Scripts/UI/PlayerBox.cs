using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerBox : MonoBehaviour
{
    [SerializeField]
    private GameObject _backgroundImage;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private GameObject _playerImageObject;
    private Image _playerImage;
    private Color _playerImageColor;

    void Start()
    {
        //Canvas setting
        if (_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.5f;
        }

        if (_backgroundImage == null) _backgroundImage = transform.GetChild(0).gameObject;
        if (_playerImageObject == null) _playerImageObject = transform.GetChild(1).gameObject;

        _playerImage = _playerImageObject.GetComponent<Image>();
        SetPlayerImageColor(0.5f);

        gameObject.SetActive(false);
    }

    public void JoinPlayer()
    {
        gameObject.SetActive(true);
    }
    private void SetPlayerImage(Sprite sprite)
    {
        if (sprite == null)
        {
            _playerImageObject.SetActive(false);
            return;
        }
        _playerImageObject.SetActive(true);
        _playerImage.sprite = sprite;
    }

    private void SetPlayerImageColor(float alpha)
    {
        _playerImageColor = _playerImage.color;
        _playerImageColor.a = alpha;
        _playerImage.color = _playerImageColor;
    }
    public void Select(Sprite sprite)
    {
        SetPlayerImage(sprite);
    }
    public void Ready(bool isReady)
    {
        SetPlayerImageColor(isReady ? 1f : 0.5f);

        //_canvasGroup.alpha = isReady ? 1f : 0.5f;
    }
}
