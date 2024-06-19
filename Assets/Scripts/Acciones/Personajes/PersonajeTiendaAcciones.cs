using System.Collections.Generic;
using System.Linq;

public static class PersonajeTiendaAcciones
{
    private static Personaje _personaje = GameManager.Instance.Jugador;
    private static List<PersonajeInventario> _personajeInventario = GameManager.Instance.Jugador.Inventario;
    private static List<Objeto> _tienda = GameManager.Instance.Tienda;

    public static bool VenderObjetoInventario(int idPersonajeInventario, int cantidad = 1)
    {
        // verificamos y obtenemos el objeto en el inventario del personaje en base al idPersonajeInventario
        PersonajeInventario personajeInventario = _personajeInventario
            .FirstOrDefault(item => item.Id == idPersonajeInventario);

        // si el objeto no existe no hacemos nada y retornamos false
        if (personajeInventario == null)
        {
            return false;
        }

        // establecemos la cantidad de oro ganada por la venta
        int oroVenta = personajeInventario.Objeto.Caracteristica.PrecioVenta;

        // actualizamos la cantidad de oro
        PersonajeAcciones.IncrementarOro(oroVenta);

        // restamos el item vendido, en caso de quedar 0 lo borramos del inventario
        PersonajeInventarioAcciones.ActualizarCantidadObjetoInventario(personajeInventario.Id, (-cantidad));

        // marcamos que el inventario se modificó y deberá ser sincronizado por la UI de inventario
        GameManager.Instance.InventarioModificado();

        // retornos que se pudo efectuar la transacción
        return true;
    }

    public static Objeto ObtenerObjetoTiendaPorIndice(int indice)
    {
        // inicializamos en nulo el objeto, en caso de que no se encuentre coincidencia
        Objeto objeto = null;

        // si el índice provisto sobrepasa el rango de nuestra tienda, retornamos nulo
        if (_tienda.Count < indice)
        {
            return objeto;
        }

        // retornamos el objeto requerido
        return _tienda[indice];
    }

    public static bool ComprarItemPorIndice(int indice, int cantidad)
    {
        // obtenemos el objeto a comprar
        Objeto objeto = ObtenerObjetoTiendaPorIndice(indice);

        // si el objeto no existe no compramos y retornamos false
        if (objeto == null)
        {
            return false;
        }

        // verificamos que el personaje tenga suficiente oro para comprar
        if (objeto.Caracteristica.PrecioCompra > _personaje.Oro)
        {
            // si no tiene oro suficiente no hacemos nada y retornamos false
            return false;
        }

        // restamos oro por la compra
        PersonajeAcciones.RestarOro(objeto.Caracteristica.PrecioCompra);

        // agregamos el objeto al inventario
        PersonajeInventarioAcciones.AgregarObjetoInventario(objeto, cantidad);

        // avisamos que se pudo efectuar la compra
        return true;
    }
}