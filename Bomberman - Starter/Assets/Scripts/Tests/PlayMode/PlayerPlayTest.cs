using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class PlayerPlayTest {

    private Player player;
    private GameObject playerGameObject;
    private IUnityInput unityInput;

    [SetUp]
    public void SetUp()
    {

        playerGameObject = Object.Instantiate(Resources.Load("Player"), Vector3.zero, Quaternion.identity) as GameObject;
        player = playerGameObject.GetComponent<Player>();
        unityInput = Substitute.For<IUnityInput>();
        
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(playerGameObject);
        player = null;
    }

    [UnityTest]
    public IEnumerator MoveUpOnW()
    {
        yield return null;

        unityInput.KeyPressed(KeyCode.W).Returns(true);
        player.Contruct(unityInput);

        Assert.AreEqual(new Vector3(0, 0, 0), player.GetComponent<Rigidbody>().velocity);

        yield return null;

        Assert.AreEqual(new Vector3(0, 0, player.moveSpeed), playerGameObject.GetComponent<Rigidbody>().velocity);
    }

    [UnityTest]
    public IEnumerator MoveRightOnD()
    {
        yield return null;

        unityInput.KeyPressed(KeyCode.D).Returns(true);
        player.Contruct(unityInput);

        Assert.AreEqual(new Vector3(0, 0, 0), player.GetComponent<Rigidbody>().velocity);

        yield return null;

        Assert.AreEqual(new Vector3(player.moveSpeed, 0, 0), playerGameObject.GetComponent<Rigidbody>().velocity);
    }

    [UnityTest]
    public IEnumerator MoveLeftOnA()
    {
        yield return null;

        unityInput.KeyPressed(KeyCode.A).Returns(true);
        player.Contruct(unityInput);

        Assert.AreEqual(new Vector3(0, 0, 0), player.GetComponent<Rigidbody>().velocity);

        yield return null;

        Assert.AreEqual(new Vector3(-player.moveSpeed, 0, 0), playerGameObject.GetComponent<Rigidbody>().velocity);
    }

    [UnityTest]
    public IEnumerator MoveDownOnS()
    {
        yield return null;

        unityInput.KeyPressed(KeyCode.S).Returns(true);
        player.Contruct(unityInput);

        Assert.AreEqual(new Vector3(0, 0, 0), player.GetComponent<Rigidbody>().velocity);

        yield return null;

        Assert.AreEqual(new Vector3(0, 0, -player.moveSpeed), playerGameObject.GetComponent<Rigidbody>().velocity);
    }

    [UnityTest]
    public IEnumerator CanDropBombDropBomb()
    {
        yield return null;

        unityInput.KeyDown(KeyCode.Space).Returns(true);
        player.Contruct(unityInput);

        Assert.AreEqual(GameObject.FindObjectsOfType<Bomb>().Length, 0);

        yield return null;

        Assert.AreEqual(GameObject.FindObjectsOfType<Bomb>().Length, 1);

    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator PlayerPlayTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
