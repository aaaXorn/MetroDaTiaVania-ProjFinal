using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyLobby : MonoBehaviourPunCallbacks
{
    public string PlayerName;
    public GameObject roomPanel;
    public InputField ifName;
    public GameObject required;
    public PlayerName[] playersNames;
   
    //funçao chamada pelo botao play
    public void PlayGame()
    {
        //se a quantidade de letras dentro do inputfield for maior q 0 
        if (ifName.text.Length > 0)
        {
            //copia o nome pra variavel
            PlayerName = ifName.text;
            //coloca o nome como nickname da photon
            PhotonNetwork.LocalPlayer.NickName = PlayerName;
            //Conecta nos servidores da photon
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            //liga o texto de requerido
            required.SetActive(true);
        }

    }
    //quando conectar a photon chama esta funçao 
    public override void OnConnectedToMaster()
    {
        //liga a tela da sala
        roomPanel.SetActive(true);
    }

    public void JoinRoom2D()
    {
        //guarda a informaçao q o game é 2D
        GameRoutines.gameType = GameRoutines.GameType.Game2D;
        //cria um objeto tipo RoomOptions para setar parametros da sala
        RoomOptions rOp = new RoomOptions();
        //quantidade maxima de jogadores
        rOp.MaxPlayers = 10;
        //cria a sala com o nome entre aspas
        PhotonNetwork.JoinOrCreateRoom("AmongRoom", rOp, TypedLobby.Default, null);
    }

    public void JoinRoomFPS()
    {
        GameRoutines.gameType = GameRoutines.GameType.FPS;
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("FPSRoom01",rOp,TypedLobby.Default,null);
        //PhotonNetwork.JoinRandomRoom();
    }
     public void JoinRoomTank()
    {
        GameRoutines.gameType = GameRoutines.GameType.Tank;
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("TankRoom01",rOp, TypedLobby.Default, null);
    }  
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Não encontrou sala, Criando uma...");
        string roomName = "Sala00";
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(roomName, rOp);
    }

    //depois q cria ou entra na sala 
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);

        InvokeRepeating("CheckAllReady", 1, 1);
       

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("O jogador "+newPlayer.NickName+" Entrou na Sala");
       
        
    }

    //checa se todomundo clicou no botao ready
    void CheckAllReady() 
    {
        print("Checando...");
        //pega todo mundo q tem o script playername
        playersNames = FindObjectsOfType<PlayerName>();
        bool allready = true;
        if (playersNames.Length >= 1)
        {
            allready = playersNames.All(param => param.ready);
           
            if (allready)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false; //fecha a sala pra ninguem entrar

                switch (GameRoutines.gameType)
                {
                    case GameRoutines.GameType.Tank:
                        PhotonNetwork.LoadLevel("Level1");
                        break;
                    case GameRoutines.GameType.FPS:
                        PhotonNetwork.LoadLevel("Level2");
                        break;
                    case GameRoutines.GameType.Game2D:
                        PhotonNetwork.LoadLevel("Level3");
                        break;
                }
            }
        }
    }
}
