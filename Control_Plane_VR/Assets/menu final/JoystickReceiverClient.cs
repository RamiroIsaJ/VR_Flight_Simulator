using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.SceneManagement;


public class JoystickReceiverClient : MonoBehaviour
{
    [SerializeField] int listenPort = 50012; // Puerto para escuchar
    [SerializeField] string serverIP = "172.25.102.7"; // Dirección IP del servidor
    private UdpClient udpClient;

    void Start()
    {
        InitializeUDPClient();
    }

    private void InitializeUDPClient()
    {
        try
        {
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient = null;
            }

            udpClient = new UdpClient(listenPort);
            udpClient.BeginReceive(ReceiveData, null);
            Debug.Log("Cliente UDP iniciado en el puerto: " + listenPort);
        }
        catch (SocketException ex)
        {
            Debug.LogError("Error al iniciar el cliente UDP en el puerto " + listenPort + ": " + ex.Message);
        }
    }

    private void ReceiveData(IAsyncResult result)
    {
        try
        {
            IPEndPoint sourceEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedBytes = udpClient.EndReceive(result, ref sourceEndPoint);
            string receivedData = Encoding.ASCII.GetString(receivedBytes);
            Debug.Log("Datos recibidos: " + receivedData); // Mensaje de depuración
            string[] dataParts = receivedData.Split(',');

            if (dataParts.Length >= 4)
            {
                int buttons;
                if (int.TryParse(dataParts[3], out buttons))
                {
                    // Cambiar de escena basado en el botón presionado
                    switch (buttons)
                    {
                        case 128:
                            ChangeScene(2); // Cambiar a la primera escena (índice 0)
                            break;
                        case 64:
                            ChangeScene(1); // Cambiar a la segunda escena (índice 1)
                            break;
                        default:
                            Debug.Log("Botón no asignado para cambiar de escena.");
                            break;
                    }
                }
                else
                {
                    Debug.Log("Error al convertir los datos recibidos.");
                }
            }
            else
            {
                Debug.Log("Datos recibidos no tienen el formato esperado.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error al recibir datos: " + e.Message);
        }
        finally
        {
            // Continuar escuchando para recibir más datos
            if (udpClient != null)
                udpClient.BeginReceive(ReceiveData, null);
        }
    }

    private void ChangeScene(int sceneIndex)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(sceneIndex); // Ajusta el índice para cargar la escena 1 y 2
            }
            else
            {
                Debug.LogError("Índice de escena fuera de los límites: " + sceneIndex);
            }
        });
    }

    private void OnApplicationQuit()
    {
        if (udpClient != null)
        {
            udpClient.Close();
            Debug.Log("Cliente UDP cerrado correctamente en el puerto: " + listenPort);
        }
    }
}
