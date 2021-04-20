using Sfs2X;
using Sfs2X.Entities;
using UnityEngine;

/**
 * Singleton class with static fields to hold a reference to SmartFoxServer connection.
 * It is useful to access the SmartFox class from anywhere in the game.
 */
public class SmartFoxConnection : MonoBehaviour
{
	private static SmartFoxConnection mInstance;
	private static SmartFox sfs;
	private static Room room;

	public static SmartFox SFS
	{
		get
		{
			return sfs;
		}
		set
		{
			sfs = value;
		}
	}
	public static Room Room
	{
		get
		{
			return room;
		}
		set
		{
			room = value;
		}
	}

	// Handle disconnection automagically
	// ** Important for Windows users - can cause crashes otherwise
	void OnApplicationQuit()
	{
		if (sfs.IsConnected)
		{
			sfs.Disconnect();
		}
	}
}