using UnityEngine;

/// <summary>
/// Drag a rigidbody with the mouse using a spring joint.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DragRigidbody : MonoBehaviour
{
	public float force = 600;
	public float damping = 6;

	Transform jointTrans;
	float dragDepth;
	GameObject touched_trash;

	void OnMouseDown()
	{
		HandleInputBegin(Input.mousePosition);
	}

	void OnMouseUp()
	{
		HandleInputEnd(Input.mousePosition);
	}

	void OnMouseDrag()
	{
		HandleInput(Input.mousePosition);
	}

	public void HandleInputBegin(Vector3 screenPosition)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Trash")))
		{
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Trash"))
			{
				dragDepth = CameraPlane.CameraToPointDepth(Camera.main, hit.point);
				jointTrans = AttachJoint(hit.rigidbody, hit.point);
				hit.transform.gameObject.layer = LayerMask.NameToLayer("Interactive");
				touched_trash = hit.transform.gameObject;
			}
		}
	}

	public void HandleInput(Vector3 screenPosition)
	{
		if (jointTrans == null)
			return;
		if (touched_trash != null)
			dragDepth = CameraPlane.CameraToPointDepth(Camera.main, touched_trash.transform.position);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
		jointTrans.position = CameraPlane.ScreenToWorldPlanePoint(Camera.main, dragDepth, screenPosition);
		float height_pos = 1.5f;
		if (touched_trash != null)
			height_pos = touched_trash.GetComponent<TrashScript>().height;
		jointTrans.position = new Vector3(jointTrans.position.x, height_pos, jointTrans.position.z);
	}

	public void HandleInputEnd(Vector3 screenPosition)
	{
		if (touched_trash != null)
		{
			touched_trash.layer = LayerMask.NameToLayer("Trash");
			touched_trash = null;
		}
		Destroy(jointTrans.gameObject);
	}

	Transform AttachJoint(Rigidbody rb, Vector3 attachmentPosition)
	{
		GameObject go = new GameObject("Attachment Point");
		go.hideFlags = HideFlags.HideInHierarchy;
		go.transform.position = attachmentPosition;

		var newRb = go.AddComponent<Rigidbody>();
		newRb.isKinematic = true;

		var joint = go.AddComponent<ConfigurableJoint>();
		joint.connectedBody = rb;
		joint.configuredInWorldSpace = true;
		joint.xDrive = NewJointDrive(force, damping);
		joint.yDrive = NewJointDrive(force, damping);
		joint.zDrive = NewJointDrive(force, damping);
		joint.slerpDrive = NewJointDrive(force, damping);
		joint.rotationDriveMode = RotationDriveMode.Slerp;

		return go.transform;
	}

	private JointDrive NewJointDrive(float force, float damping)
	{
		JointDrive drive = new JointDrive();
		drive.mode = JointDriveMode.Position;
		drive.positionSpring = force;
		drive.positionDamper = damping;
		drive.maximumForce = Mathf.Infinity;
		return drive;
	}
}