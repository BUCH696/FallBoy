using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    [SerializeField] private float speedRotation;

    [SerializeField] private Vector3 DirectionRotation;
    private Transform attachedElement;
    private Rigidbody Rigidbody { get; set; }
    private int directionValue = 1;

    public Component rotationComponent = Component.NULL;

    public Figure figureType = Figure.Pendulum;

    public enum Component
    {
        NULL,
        Rotation,
        Physics,
    }

    public enum Figure
    {
        Pendulum,
    }
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ApplyFigureChanges();
    }

    private void FixedUpdate()
    {
        switch (rotationComponent)
        {

            case Component.Rotation:
                transform.localRotation *= Quaternion.Euler(DirectionRotation * speedRotation * Time.fixedDeltaTime);
                break;

            case Component.Physics:
                {

                    Rigidbody.angularVelocity = DirectionRotation * speedRotation * Time.fixedDeltaTime;
                }
                break;
            default:
                break;

        }

    }
    private void ApplyFigureChanges()
    {
        switch (figureType)
        {
            case Figure.Pendulum:
                //Debug.Log($"{Rigidbody.transform.rotation.eulerAngles}   { DirectionRotation * 90}  {Vector3.Angle(Rigidbody.transform.rotation.eulerAngles, DirectionRotation * 90)}");
                //Vector3 targetDir = attachedElement.position - Rigidbody.transform.position;

                Debug.Log(Quaternion.Inverse(transform.rotation));
                if (transform.rotation.x == 270)
                {
                    Debug.Log(transform.rotation.eulerAngles.x);

                    directionValue *= -1;
                    DirectionRotation = DirectionRotation * directionValue;

                }

                break;
        }

    }

}
