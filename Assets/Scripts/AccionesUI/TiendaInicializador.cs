using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiendaInicializador : MonoBehaviour
{
    // variables públicas
    public GameObject prefabEspacioTienda;
    public int columnas = 5;
    public int filas = 7;
    public int anchoEspacio = 32;
    public int altoEspacio = 32;

    // variables privadas
    GameObject EspaciosTienda;
    List<Sprite> IconosObjetos;

    // Start is called before the first frame update
    void Start()
    {
        EspaciosTienda = GameObject.FindGameObjectWithTag(Tags.EspaciosTienda);
        IconosObjetos = GameObject.FindGameObjectWithTag(Tags.PanelMenu).GetComponent<PanelAcciones>().IconosObjetos;

        // corregimos cualquier valor incorrecto que venga como parámetro de unity
        NormalizarValoresEntrada();

        // actualizamos los espacios en la tienda
        ActualizarEspaciosTienda();
    }

    private void NormalizarValoresEntrada()
    {
        // si se puso algún valor en 0 o negativo lo ponemos por defecto en 1, sino lo dejamos como está
        columnas = columnas < 1 ? 1 : columnas;
        filas = filas < 1 ? 1 : filas;
        anchoEspacio = anchoEspacio < 1 ? 1 : anchoEspacio;
        altoEspacio = altoEspacio < 1 ? 1 : altoEspacio;
    }

    private void ActualizarEspaciosTienda()
    {
        // incializamos los ejes en la posición que esté el parent en pantalla
        float posicionEjeX = EjeXInicial();
        float posicionEjeY = EjeYInicial();

        // inicializamos el índice de la tienda para asignar el objeto existente allí
        int indiceTienda = 0;

        // recorremos filas y columnas para rellenarlas con el prefab
        for (int x = 0; x < filas; x++)
        {
            for (int y = 0; y < columnas; y++)
            {
                // creamos un duplicado del prefab
                // le asignamos al duplicado su nuevo parent (la tienda)
                GameObject espacioNuevo = GameObject.Instantiate(prefabEspacioTienda, EspaciosTienda.transform);
                // establecemos en que ejes se encontrará este espacio
                espacioNuevo.transform.position = new Vector3(posicionEjeX, posicionEjeY);

                // asignamos el objeto de la tienda en caso de haber uno para el índice actual
                AgregarObjetoExistente(indiceTienda, espacioNuevo);

                // incrementamos la posición del eje 'x' ya que pasamos a la columna siguiente
                posicionEjeX += anchoEspacio;

                // incrementamos el índice de la tienda
                indiceTienda++;
            }

            // reiniciamos el eje 'x' ya que iniciaremos en la columna 1
            posicionEjeX = EjeXInicial();

            // incrementamos la posición del eje 'y' ya que pasamos a la fila siguiente
            posicionEjeY -= altoEspacio;
        }
    }

    private float EjeXInicial()
    {
        // calculamos el eje 'x' donde empezarán a dibujarse los espacios
        return EspaciosTienda.transform.position.x - (anchoEspacio * 2);
    }

    private float EjeYInicial()
    {
        // calculamos el eje 'y' donde empezarán a dibujarse los espacios
        return EspaciosTienda.transform.position.y + (altoEspacio * (columnas - 2));
    }

    private void AgregarObjetoExistente(int indice, GameObject espacio)
    {
        // obtenemos los objetos de la tienda
        List<Objeto> tienda = GameManager.Instance.Tienda;

        // si hay objetos suficientes para el índice indicado procedemos
        if (tienda.Count > indice)
        {
            // seleccionamos el objeto correspondiente al índice
            Objeto tiendaObjeto = tienda[indice];

            // actualizamos la cantidad
            ActualizarCantidad(espacio, 1);

            // actualizamos el tooltip
            ActualizarTooltip(espacio, tiendaObjeto.Nombre, tiendaObjeto.Descripcion, tiendaObjeto.Caracteristica.PrecioCompra);

            // obtenemos el componente que contiene la imagen para poder quitar el fondo vacío y asignar la imagen del objeto
            Image imagenFondo = espacio.GetComponentsInChildren<Image>().First(image => image.CompareTag(Tags.EspacioTiendaFondo));
            // actualizamos la imagen vacía por la del objeto
            ActualizarImagenEspacio(tiendaObjeto.RutaImagen, imagenFondo);
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

    private void ActualizarCantidad(GameObject espacio, int cantidad)
    {
        // obtenemos el componente que contiene la cantidad de objetos para poder actualizarla
        TextMeshProUGUI textoCantidad = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.EspacioTiendaCantidad));
        // esta se usa para ponerle un "color de fondo" xD
        TextMeshProUGUI textoCantidadFondo = espacio.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.EspacioTiendaCantidadFondo));
        // actualizamos la cantidad
        /*textoCantidad.text = cantidad.ToString();
        textoCantidadFondo.text = $"<mark=#000000>{textoCantidad.text}</mark>";*/
        textoCantidad.text = "";
        textoCantidadFondo.text = "";
    }

    private void ActualizarTooltip(GameObject espacio, string titulo, string descripcion, int precioCompra)
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
        textoPrecio.text = $"${precioCompra}";
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