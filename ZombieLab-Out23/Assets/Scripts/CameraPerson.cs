using UnityEngine;

public class CameraPerson : MonoBehaviour
{
    private Transform objetivo;
    public float alturaJugador = 1f;
    public float distanciaPared = 0.1f;
    public float maxDistancia = 14f;
    public float minDistancia = 1.87f;

    public float xVelocidad = 200.0f;
    public float yVelocidad = 200.0f;
    public int yMinLimite = -80;
    public int yMaxLimite = 80;

    public int velocidadZoom = 40;
    public float amortiguacionRotacion = 3.0f;
    public float amortiguacionZoom = 5.0f;

    public LayerMask collisionLayers = -1;

    public float xDeg = 0.0f;
    public float yDeg = 0.0f;

    public float distanciaBase = 5.0f;
    public float distanciaActual;
    public float distanciaDeseada;
    public float distanciaCorregida;

    // Use this for initialization
    void Start()
    {
        GameObject pc = GameObject.Find("Player");
        if (pc != null)
        {
            objetivo = pc.transform;
        }

        Vector3 angulos = transform.eulerAngles;
        xDeg = angulos.x;
        yDeg = angulos.y;

        distanciaActual = distanciaBase;
        distanciaDeseada = distanciaBase;
        distanciaCorregida = distanciaBase;

        //Bloquea la Rotacion.
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject pc = GameObject.Find("Player");
        if (pc == null)
        {
            return;
        }
        else
        {
            objetivo = pc.transform;
        }

        Vector3 vTargetOffset;

        //Si el objetivo no esta definido no hara nada en este ciclo alv :v
        if (!objetivo)
            return;

        //si el boton derecho o el izquierdo del raton esta pulsado la camara rotara con el movimiento del raton.

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            xDeg += Input.GetAxis("Mouse X") * xVelocidad * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * yVelocidad * 0.02f;
        }

        //si no , y si ademas el objetivo se esta moviendo, utilizara la rotacion del objetivo para crregir la propia
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float anguloRotacionObjetivo = objetivo.eulerAngles.y;
            float anguloRotacionActualCamara = transform.eulerAngles.y;
            xDeg = Mathf.LerpAngle(anguloRotacionActualCamara, anguloRotacionObjetivo, amortiguacionRotacion * Time.deltaTime);

        }

        yDeg = CorregirAngulo(yDeg, yMinLimite, yMaxLimite);

        //Establece la rotacion de la camara.
        Quaternion rotacionFinalCamara = Quaternion.Euler(yDeg, xDeg, 0);

        // calcula la distancia deseada (distanciaDeseada) al objeto.
        distanciaDeseada -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * velocidadZoom * Mathf.Abs(distanciaDeseada);
        distanciaDeseada = Mathf.Clamp(distanciaDeseada, minDistancia, maxDistancia);
        distanciaCorregida = distanciaDeseada;

        //asigna la posicion final de la camara al nuevo vector.

        vTargetOffset = new Vector3(0, -alturaJugador, 0);
        Vector3 posicionFinalCamara = objetivo.position - (rotacionFinalCamara * Vector3.forward * distanciaDeseada + vTargetOffset);

        // comprueba si hay colision y crea un nuevo vector con la posicion del objeto mas su altura.
        RaycastHit collisionHit;
        Vector3 posicionRealObjeto = new Vector3(objetivo.position.x, objetivo.position.y + alturaJugador, objetivo.position.z);

        // si hubo una colision corrige la posicion de la camara y calcula la distancia correcta.

        bool isCorrected = false;
        if (Physics.Linecast(posicionRealObjeto, posicionFinalCamara, out collisionHit, collisionLayers.value))

        {
            //calcula la distancia desde la posicion real del objetivo hasta la ubicacion de la colision
            distanciaCorregida = Vector3.Distance(posicionRealObjeto, collisionHit.point) - distanciaPared;
            isCorrected = true;
        }

        //corrige la distancia actual.

        distanciaActual = !isCorrected || distanciaCorregida > distanciaActual ? Mathf.Lerp(distanciaActual, distanciaCorregida, Time.deltaTime * amortiguacionZoom) : distanciaCorregida;

        //Se aseura de que la distancia este dentro de los limites
        distanciaActual = Mathf.Clamp(distanciaActual, minDistancia, maxDistancia);

        //recalcula la posicion final de la camara.

        posicionFinalCamara = objetivo.position - (rotacionFinalCamara * Vector3.forward * distanciaActual + vTargetOffset);


        //establece la posicion y rotacon finales de la camara
        transform.rotation = rotacionFinalCamara;
        transform.position = posicionFinalCamara;

    }

    private static float CorregirAngulo(float angulo, float min, float max)
    {
        if (angulo < -360)
            angulo += 360;
        if (angulo > 360)
            angulo -= 360;
        return Mathf.Clamp(angulo, min, max);
    }
}