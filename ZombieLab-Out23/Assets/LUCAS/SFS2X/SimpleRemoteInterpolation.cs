using TMPro;
using UnityEngine;

/**
 * Extremely simple and dumb interpolation script.
 * But it works for this example.
 */
public class SimpleRemoteInterpolation : MonoBehaviour
{
	public int modelType = 0;
	private Vector3 desiredPos;
	private Quaternion desiredRot;

	private float dampingFactor = 5f;

	private Animator Playeranim;

	public float rottt = 1;

	public void Start()
	{
		transform.GetChild(modelType).gameObject.SetActive(true);
		Playeranim = transform.GetChild(modelType).GetComponent<Animator>();
		desiredPos = this.transform.position;
		//desiredRot = this.transform.rotation;
	}

	public void SetPosition(Vector3 pos, bool interpolate)
	{
		if (interpolate)
		{
			desiredPos = pos;
		}
		else
		{
			this.transform.position = pos;
		}
	}

	public void SetRotation(float rotY)
    {
		Vector3 v = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(v.x, rotY * rottt, v.z);
	}

	public void JumpAnim(string jumpName)
    {
		if (Playeranim == null)
			return;

		Playeranim.SetTrigger(jumpName);
	}

	public void WalkAnim(string xName, float xFloat, string yName, float yFloat)
    {
		if (Playeranim == null)
			return;
		
		Playeranim.SetFloat(xName, xFloat);
		Playeranim.SetFloat(yName, yFloat);
	}


	void Update()
	{
		this.transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * dampingFactor);
	}
}
