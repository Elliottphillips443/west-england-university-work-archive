using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerInfoStruct
{
    public int playerNumber;
    public STAR_SIGN sign;
}

public static class PlayerInformation
{
	public static List<PlayerInfoStruct> list_of_players = new List<PlayerInfoStruct>();

    public static void addToList(PlayerInfoStruct p)
    {
        list_of_players.Add(p);
    }

    public static void removeFromList(int index)
    {
        list_of_players.RemoveAt(index);
    }

    public static PlayerInfoStruct getListItem(int index)
    {
        return list_of_players[index];
    }

    public static void overwriteListItem(PlayerInfoStruct p, int index)
    {
        list_of_players[index] = p;
    }
}