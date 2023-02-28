using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    float fDeltatime;
    float fMoveSpeed;

    float fMouseSensitivityX;
    float fMouseSensitivityY;
    float fSensitivitySpeedup;
    float fLookRotation;

    bool bCursorVisable;

    Transform tfPlayer;
    GameObject playerCam;
    Rigidbody rig;

    void Start()
    {
        fDeltatime = Time.deltaTime;
        fMoveSpeed = 2f;
        tfPlayer = transform;

        fMouseSensitivityX = 2f;
        fMouseSensitivityY = 3f;
        fSensitivitySpeedup = 60f;

        bCursorVisable = false;
        Cursor.visible = bCursorVisable;
        Cursor.lockState = CursorLockMode.Locked;

        GetField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
            SetCursor();
    }

    void FixedUpdate()
    {
        Movement();
        View();
    }

    void GetField()
    {
        playerCam = tfPlayer.GetChild(0).gameObject;
        rig = GetComponent<Rigidbody>();
    }

    void SetCursor()
    {
        bCursorVisable = !bCursorVisable;

        Cursor.visible = bCursorVisable;
        Cursor.lockState = bCursorVisable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void Movement()
    {
        if (bCursorVisable)
            return;

        Vector3 v3Target = tfPlayer.forward * Input.GetAxisRaw("Vertical") +
                           tfPlayer.right * Input.GetAxisRaw("Horizontal");

        rig.MovePosition(tfPlayer.position + v3Target * fDeltatime * fMoveSpeed);
    }

    void View()
    {
        if (bCursorVisable)
            return;

        tfPlayer.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * fMouseSensitivityX * fDeltatime * fSensitivitySpeedup);

        fLookRotation += Input.GetAxisRaw("Mouse Y") * fMouseSensitivityY * fDeltatime * fSensitivitySpeedup;
        fLookRotation = Mathf.Clamp(fLookRotation, -80, 75);

        playerCam.transform.localEulerAngles = -Vector3.right * fLookRotation;
    }
}
