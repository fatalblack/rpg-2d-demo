using System.Collections.Generic;
using System.Linq;

public static class PersonajeInventarioAcciones
{
	private static Personaje _personaje = GameManager.Instance.Jugador;
	private static List<PersonajeInventario> _personajeInventario = GameManager.Instance.Jugador.Inventario;
    private static PersonajeEstadistica _personajeEstadistica = GameManager.Instance.Jugador.Estadistica;

    public static List<PersonajeInventario> AgregarObjetoInventario(Objeto objeto, int cantidad)
    {
        // verificamos si el objeto es apilable, de ser así no hay que agregar uno nuevo sino incrementar la cantidad
        bool esApilable = objeto.Caracteristica.Apilable;

        if (esApilable && _personajeInventario.Count > 0)
        {
            bool apilado = false;

            for (int i = 0; i < _personajeInventario.Count; i++)
            {
                // si encontramos el objeto en el inventario, procedemos a apilar
                if (_personajeInventario[i].IdObjeto == objeto.Id)
                {
                    // incrementamos la cantidad en la pila
                    _personajeInventario[i].Cantidad += cantidad;

                    // ajustamos la cantidad por si se pasó del límite
                    _personajeInventario[i].Cantidad = AjustarMaximoObjetoApilable(_personajeInventario[i].Cantidad);

                    apilado = true;
                }
            }

            // retornamos el personaje actualizado en caso de haber apilado los objetos
            if (apilado)
            {
                // marcamos que el inventario se modificó y deberá ser sincronizado por la UI de inventario
                GameManager.Instance.InventarioModificado();

                // ajustamos el inventario por si se pasa del máximo permitido de espacios
                return AjustarMaximoEspaciosInventario(_personajeInventario);
            }
        }

        // en caso de no existir el objeto lo agregamos al inventario
        // obtenemos el siguiente id a insertar
        int siguientIdInventario = 1;
        if (_personajeInventario.Count > 0)
        {
            siguientIdInventario = _personajeInventario.Max(inventario => inventario.Id) + 1;
        }

        // agregamos el objeto al inventario
        _personajeInventario.Add(new PersonajeInventario
        {
            Id = siguientIdInventario,
            IdPersonaje = _personaje.Id,
            IdObjeto = objeto.Id,
            Equipado = false,
            Cantidad = cantidad,
            Objeto = objeto
        });

        // marcamos que el inventario se modificó y deberá ser sincronizado por la UI de inventario
        GameManager.Instance.InventarioModificado();

        // retornamos el personaje actualizado con el objeto agregado al inventario
        // ajustamos el inventario por si se pasa del máximo permitido de espacios
        return AjustarMaximoEspaciosInventario(_personajeInventario);
    }

    public static PersonajeEstadistica EquiparObjetoInventario(int idPersonajeInventario)
    {
        // validamos si estamos heridos
        int vidaAntesDelCambio = _personajeEstadistica.VidaActualCalculada;
        bool personajeHerido = _personajeEstadistica.VidaActualCalculada < _personajeEstadistica.VidaMaximaCalculada;

        // verificamos y obtenemos el objeto en el inventario del personaje en base al idPersonajeInventario
        PersonajeInventario objetoInventario = _personajeInventario
            .FirstOrDefault(item => item.Id == idPersonajeInventario);

        // en caso de que el objeto no se encuentre ya en el inventario o bien no sea equipable no hacemos nada
        if (objetoInventario == null || !objetoInventario.Objeto.Caracteristica.Equipable)
        {
            // retornamos las estadísticas sin actualizar ya que no se equipó el objeto y no cambió nada
            return _personajeEstadistica;
        }

        // detectamos si lo que queremos realmente es desequipar
		if (objetoInventario.Equipado)
		{
            // desequipamos
            objetoInventario.Equipado = false;
		}
		else
		{
            // recorremos todo el inventario para dejar equipado solo el objeto requerido
            foreach (PersonajeInventario item in _personajeInventario)
            {
                // si el objeto evaluado está equipado, lo desequipamos
                if (item.Equipado)
                {
                    item.Equipado = false;
                }

                // si el objeto evaluado corresponde al idPersonajeInventario provisto, lo equipamos
                if (item.Id == idPersonajeInventario)
                {
                    item.Equipado = true;
                }
            }
        }

        PersonajeEstadisticaAcciones.ActualizarEstadisticas();

        // si estamos heridos mantenemos esta vida y no la calculada
        if (personajeHerido)
        {
            _personajeEstadistica.VidaActualCalculada = vidaAntesDelCambio;
        }

        // marcamos que el inventario se modificó y deberá ser sincronizado por la UI de inventario
        GameManager.Instance.InventarioModificado();

        // actualizamos y retornamos las estadísticas del personaje sumándole las que nos provee el objeto
        return _personajeEstadistica;
    }

    public static List<PersonajeInventario> ActualizarCantidadObjetoInventario(int idPersonajeInventario, int modificadorCantidad)
    {
        // verificamos y obtenemos el objeto en el inventario del personaje en base al idPersonajeInventario
        PersonajeInventario personajeInventario = _personajeInventario
            .FirstOrDefault(item => item.Id == idPersonajeInventario);

        // validamos que el item exista
        if (personajeInventario == null)
        {
            // retornamos el inventario sin actualizar
            return _personajeInventario;
        }

        // restamos el item consumido, en caso de quedar 0 lo borramos del inventario
        personajeInventario.Cantidad += modificadorCantidad;

        if (personajeInventario.Cantidad == 0)
        {
            _personajeInventario.Remove(personajeInventario);
        }

        // marcamos que el inventario se modificó y deberá ser sincronizado por la UI de inventario
        GameManager.Instance.InventarioModificado();

        // retornamos el personaje actualizado
        // ajustamos el inventario por si se pasa del máximo permitido de espacios
        return AjustarMaximoEspaciosInventario(_personajeInventario);
    }

    public static int ConsumirCuraPersonajeInventario(int idPersonajeInventario)
    {
        // verificamos y obtenemos el objeto en el inventario del personaje en base al idPersonajeInventario
        PersonajeInventario personajeInventario = _personajeInventario
            .FirstOrDefault(item => item.Id == idPersonajeInventario);

        // validamos que sea un item que nos permita curar
        if (
            personajeInventario == null ||
            !personajeInventario.Objeto.Caracteristica.Consumible ||
            personajeInventario.Objeto.Estadistica.VidaActual <= 0)
        {
            // retornamos la vida actual sin curar ya que no había poción para tomar
            return _personajeEstadistica.VidaActualCalculada;
        }

        // obtenemos la información de la vida máxima y actual
        // adicionalmente obtenemos el valor de cuanto cura la poción
        int cura = personajeInventario.Objeto.Estadistica.VidaActual;

        // aplicamos la cura
        PersonajeAcciones.CurarVida(cura);

        // restamos el item consumido, en caso de quedar 0 lo borramos del inventario
        ActualizarCantidadObjetoInventario(personajeInventario.Id, -1);

        // marcamos que el inventario se modificó y deberá ser sincronizado por la UI de inventario
        GameManager.Instance.InventarioModificado();

        // retornamos la vida actualizada
        return _personajeEstadistica.VidaActualCalculada;
    }

    public static PersonajeInventario ObtenerPersonajeInventarioPorIndice(int indice)
    {
        // inicializamos en nulo el personaje inventario, en caso de que no se encuentre coincidencia
        PersonajeInventario personajeInventario = null;

        // si el índice provisto sobrepasa el rango de nuestro inventario, retornamos nulo
        if (_personajeInventario.Count < indice)
        {
            return personajeInventario;
        }

        // retornamos el personajeInventario requerido
        return _personajeInventario[indice];
    }

    private static int AjustarMaximoObjetoApilable(int cantidad)
    {
        // si la cantidad nueva es mayor que la permitida devolvemos la permitida
        return cantidad <= GameManager.Instance.CantidadMaximaObjetosApilables ? cantidad : GameManager.Instance.CantidadMaximaObjetosApilables;
    }

    private static List<PersonajeInventario> AjustarMaximoEspaciosInventario(List<PersonajeInventario> inventario)
    {
        int cantidadObjetosInventario = inventario.Count;

        // si la cantidad de espacios ocupados en el inventario pasa la máxima permitida devolvemos la lista cortada
        return cantidadObjetosInventario > GameManager.Instance.CantidadMaximaEspaciosInventario ?
            inventario.Take(GameManager.Instance.CantidadMaximaEspaciosInventario).ToList() :
            inventario;
    }
}