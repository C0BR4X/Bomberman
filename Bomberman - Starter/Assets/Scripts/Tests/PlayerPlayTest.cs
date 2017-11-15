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

    [UnityTest]
    public IEnumerator MoveUpOnW()
    {
        yield return null;

        unityInput.KeyPressed(KeyCode.W).Returns(true);
        unityInput.KeyPressed(KeyCode.UpArrow).Returns(true);
        player.Contruct(unityInput);

        Assert.AreEqual(new Vector3(0, 0, 0), player.GetComponent<Rigidbody>().velocity);
        yield return null;

        Rigidbody dini = playerGameObject.GetComponent<Rigidbody>();

        Assert.AreEqual(new Vector3(0, 0, player.moveSpeed), playerGameObject.GetComponent<Rigidbody>().velocity);
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
