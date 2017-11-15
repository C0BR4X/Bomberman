using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public partial class PlayerTest
{
    private Player player;
    private GameObject playerGameObject;
    private IUnityInput unityInput;

    [SetUp]
    public void SetUp()
    {

        playerGameObject =
            Object.Instantiate(Resources.Load("Player"), Vector3.zero, Quaternion.identity) as GameObject;
        player = playerGameObject.GetComponent<Player>();
        unityInput = Substitute.For<IUnityInput>();
    }
}
