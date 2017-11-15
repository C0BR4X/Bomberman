using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public partial class PlayerTest
{
    private Player player;
    private MonoBehaviour playerGameObject;
    private IUnityInput unityInput;

    [SetUp]
    public void SetUp()
    {

        playerGameObject = MonoBehaviour.Instantiate(Resources.Load("Player"), Vector3.zero, Quaternion.identity) as MonoBehaviour;
        player = playerGameObject.GetComponent<Player>();
    }

	[UnityTest]
	public IEnumerator MoveUpOnW()
    { 

        unityInput = Substitute.For<IUnityInput>();
        unityInput.KeyPressed(KeyCode.W).Returns(true);
        unityInput.KeyPressed(KeyCode.UpArrow).Returns(true);

        Assert.AreEqual(new Vector3(0, 0, 0), player.GetComponent<Rigidbody>().velocity);
        yield return null;
        yield return null;

        Rigidbody dini  =playerGameObject.GetComponent<Rigidbody>();

        Assert.AreEqual(new Vector3(0,0,player.moveSpeed), playerGameObject.GetComponent<Rigidbody>().velocity);
    }

// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PlayerTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
