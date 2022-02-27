﻿using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRoomMenu : MenuPanel, IUIRoom, IUIRoomMenu
{
    [SerializeField] private TextMeshProUGUI _titleNameTxt;
    [SerializeField] private GameObject _playerNameContainer;
    [SerializeField] private GameObject _playerNamePrefab;
    [SerializeField] private Button _leaveRoomBtn;

    [SerializeField] private List<UIPlayerName> _playerNameLists = new List<UIPlayerName>();

    private void OnEnable()
    {
        if (PhotonManager.Instance != null)
        {
            PhotonManager.Instance.GetCurrentRoomName();
        }
    }

    private void Start()
    {
        _leaveRoomBtn.onClick.AddListener(handleLeaveRoom);
    }

    private void handleLeaveRoom()
    {
        clearPlayersNameList();
        PhotonManager.Instance.LeaveRoom();
    }

    public void CreatePlayer(Player player)
    {
        var playerPrefab = Instantiate(_playerNamePrefab, _playerNameContainer.transform);
        var playerItem = playerPrefab.GetComponent<UIPlayerName>();
        playerItem.Setup(player);
        _playerNameLists.Add(playerItem);
    }

    public void SetName(string value)
    {
        _titleNameTxt.text = value;
    }

    public List<UIPlayerName> GetPlayersList()
    {
        return _playerNameLists;
    }

    private void clearPlayersNameList()
    {
        if (_playerNameLists.Count > 0)
        {
            _playerNameLists.Where(x => x != null).ToList().ForEach(x => x.PlayerLeftRoom());
            _playerNameLists.Clear();
        }
    }
}