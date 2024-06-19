using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventarioInicializador : MonoBehaviour
{
    // variables públicas
    public GameObject prefabEspacioInventario;
    public int columnas = 5;
    public int filas = 7;
    public int anchoEspacio = 32;
    public int altoEspacio = 32;

    // variables privadas
    GameObject EspaciosInventario;
    List<Sprite> IconosObjetos;

    // Start is called before the first frame update
    void Start()
    {
        EspaciosInventario = GameObject.FindGameObjectWithTag(Tags.EspaciosInventario);
        IconosObjetos = GameObject.FindGameObjectWithTag(Tags.PanelMenu).GetComponent<PanelAcciones>().IconosObjetos;

        // corregimos cualquier valor incorrecto que venga como parámetro de unity
        NormalizarValoresEntrada();

        // actualizamos los espacios en el inventario
        ActualizarEspaciosInventario();
    }

    // Update is called once per frame
    void Update()
    {
		// actualizamos los espacios en el inventario si es necesario
		if (GameManager.Instance.SeDebeSincronizarInventario())
        {
            LimpiarInventario();
            ActualizarEspaciosInventario();
        }
    }

    private void NormalizarValoresEntrada()
	{
        // si se puso algún valor en 0 o negativo lo ponemos por defecto en 1, sino lo dejamos como está
        columnas = columnas < 1 ? 1 : columnas;
        filas = filas < 1 ? 1 : filas;
        anchoEspacio = anchoEspacio < 1 ? 1 : anchoEspacio;
        altoEspacio = altoEspacio < 1 ? 1 : altoEspacio;
    }

    private void LimpiarInventario()
	{
        // seleccionamos todos los espacios dentro del inventario
        GameObject[] espaciosALimpiar = GameObject.FindGameObjectsWithTag(Tags.EspacioInventario);
		
        // destruimos cada espacio
        foreach (GameObject espacio in espaciosALimpiar)
		{
            GameObject.Destroy(espacio);
		}
    }

    private void ActualizarEspaciosInventario()
	{
        // incializamos los ejes en la posición que esté el parent en pantalla
        float posicionEjeX = EjeXInicial();
        float posicionEjeY = EjeYInicial();

        // inicializamos el índice del inventario en el GameManager para asignar el objeto existente allí
        int indiceInventario = 0;

        // recorremos filas y columnas para rellenarlas con el prefab
        for (int x = 0; x < filas; x++)
		{
            for (int y = 0; y < columnas; y++)
			{
                // creamos un duplicado del prefab
                // le asignamos al duplicado su nuevo parent (el inventario)
                GameObject espacioNuevo = GameObject.Instantiate(prefabEspacioInventario, EspaciosInventario.transform);
                // establecemos en que ejes se encontrará este espacio
                espacioNuevo.transform.position = new Vector3(posicionEjeX, posicionEjeY);

                // asignamos el objeto del inventario en caso de haber uno para el índice actual
                AgregarObjetoExistente(indiceInventario, espacioNuevo);

                // incrementamos la posición del eje 'x' ya que pasamos a la columna siguiente
                posicionEjeX += anchoEspacio;

                // incrementamos el índice del inventario
                indiceInventario++;
            }

            // reiniciamos el eje 'x' ya que iniciaremos en la columna 1
            posicionEjeX = EjeXInicial();

            // incrementamos la posición del eje 'y' ya que pasamos a la fila siguiente
            posicionEjeY -= altoEspacio;
        }

        // marcamos que ya sincronizamos el inventario
        GameManager.Instance.SeSincronizoInventario();
    }

    private float EjeXInicial()
	{
        // calculamos el eje 'x' donde empezarán a dibujarse los espacios
        return EspaciosInventario.transform.position.x - (anchoEspacio * 2);
    }

    private float EjeYInicial()
    {
        // calculamos el eje 'y' donde empezarán a dibujarse los espacios
        return EspaciosInventario.transform.position.y + (altoEspacio * (columnas-2));
    }

    private void AgregarObjetoExistente(int indice, GameObject espacio)
	{
        // obtenemos el inventario del Jugador
        List<PersonajeInventario> inventario = GameManager.Instance.Jugador.Inventario;

        // si hay objetos suficientes para el índice indicado procedemos
        if (inventario.Count > indice)
		{
            // seleccionamos el objeto correspondiente al índice
            PersonajeInventario inventarioObjeto = inventario[indice];

            // actualizamos la cantidad
            ActualizarCantidad(espacio, inventarioObjeto.Cantidad, inventarioObjeto.Objeto.Caracteristica.Apilable, inventarioObjeto.Equipado);

            // actualizamos el tooltip
            ActualizarTooltip(espacio, inventarioObjeto.Objeto.Nombre, inventarioObjeto.Objeto.Descripcion, inventarioObjeto.Objeto.Caracteristica.PrecioVenta);

            // obtenemos el componente que contiene la imagen para poder quitar el fondo vacío y asignar la imagen del objeto
            Image imagenFondo = espacio.GetComponentsInChildren<Image>().First(image => image.CompareTag(Tags.EspacioInventarioFondo));
            // actualizamos la imagen vacía por la del objeto
            ActualizarImagenEspacio(inventarioObjeto.Objeto.RutaImagen, imagenFondo);
		}
		else
		{
            // si el espacio está vacío quitamos el tooltip
            QuitarTooltip(espacio);
        }
	}

    private void ActualizarImagenEspacio(string rutaImagen, Image imagenFondo)
	{
        // buscamos el sprite correspondiente en la lista de iconos de PanelAcciones
        imagenFondo.sprite = ObtenerSpritePorNombre(rutaImagen);
    }

    private void ActualizarCantidad(GameObject espacio, int cantidad, bool apilable, bool equipado)
	{
        // obtenemos el componente que contiene la cantidad de objetos para poder actualizarla
        TextMeshProUGUI textoCantidad = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.EspacioInventarioCantidad));
        // esta se usa para ponerle un "color de fondo" xD
        TextMeshProUGUI textoCantidadFondo = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.EspacioInventarioCantidadFondo));
		// actualizamos la cantidad si el objeto es apilable
		if (apilable)
		{
            textoCantidad.text = cantidad.ToString();
            textoCantidadFondo.text = $"<mark=#000000>{textoCantidad.text}</mark>";
		}
        // caso contrario dejamos vacío
        else
        {
			if (equipado)
			{
                textoCantidad.text = "E";
                textoCantidadFondo.text = $"<mark=#000000>E</mark>";
            }
			else
			{
                textoCantidad.text = "";
                textoCantidadFondo.text = "";
            }
        }
    }

    private void ActualizarTooltip(GameObject espacio, string titulo, string descripcion, int precioVenta)
    {
        // obtenemos el componente que contiene el título del tooltip
        TextMeshProUGUI textoTitulo = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.Titulo));
        // obtenemos el componente que contiene la descripción del tooltip
        TextMeshProUGUI textoDescripcion = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.Descripcion));
        // obtenemos el componente que contiene el precio del tooltip
        TextMeshProUGUI textoPrecio = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.Precio));
        // actualizamos el tooltip
        textoTitulo.text = titulo;
        textoDescripcion.text = descripcion;
        textoPrecio.text = $"${precioVenta}";
    }

    private void QuitarTooltip(GameObject espacio)
    {
        // obtenemos el componente que contiene el título del tooltip
        GameObject.Destroy(espacio.GetComponentsInChildren<RectTransform>().First(objeto => objeto.CompareTag(Tags.Tooltip)).gameObject);
    }

    private Sprite ObtenerSpritePorNombre(string nombre)
    {
        return IconosObjetos.FirstOrDefault(icono => icono.name == nombre);
    }
}