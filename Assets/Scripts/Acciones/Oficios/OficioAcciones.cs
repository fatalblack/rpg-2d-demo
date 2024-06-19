using System;
using System.Collections.Generic;

public static class OficioAcciones
{
    private static Personaje _personaje = GameManager.Instance.Jugador;

    public static void ExtraerDeRecolectable(Recolectable recolectable)
    {
        // inicializamos la variable para agregar experiencia en caso de ser necesario
        int experienciaGanada = 0;
        // evaluamos si será tala o minería y si contamos con las herramientas
        bool esTala = recolectable.Caracteristica.SePuedeTalar;
        bool esMineria = recolectable.Caracteristica.SePuedeMinar;

        // recorremos la lista de recurso que se pueden recolectar
        foreach (RecolectableRecurso recurso in recolectable.Recursos)
        {
            // si las probabilidades de recolectar son favorables procedemos
            if (EsRecursoRecolectado(recurso.Probabilidad))
            {
                // calculamos la cantidad de objetos a recibir (cantidad base + (nivel personaje / 2))
                int cantidadRecolectada = recurso.Cantidad + (int)Math.Ceiling((double)_personaje.Nivel / 2);

                // agregamos a la lista el recurso recolectado
                PersonajeInventarioAcciones.AgregarObjetoInventario(recurso.Objeto, cantidadRecolectada);

                // incrementamos la experiencia ganada
                experienciaGanada += recurso.Experiencia;

                // informamos al visor de eventos que recolectamos algo
                EventosAcciones.Instancia.AgregarEventoInfo($"Obtuviste {recurso.Objeto.Nombre} x {cantidadRecolectada}");
            }
        }

        // si se logró recolectar algún objeto actualizamos el inventario del personaje
        if (experienciaGanada > 0)
        {
            // actualizamos experiencia y nivel de oficio
            if (esTala)
            {
                PersonajeOficioAcciones.ActualizarTala(experienciaGanada);
            }
            if (esMineria)
            {
                PersonajeOficioAcciones.ActualizarMineria(experienciaGanada);
            }
        }
    }

    private static bool EsRecursoRecolectado(int probabilidad)
    {
        // armamos una lista de posibilidades, donde se acumularán cuantas chances buenas o malas tenemos (100 en total)
        List<bool> listaPosibilidades = new List<bool>();
        // en base a un número aleatorio marcaremos cuantas posibilidades son buenas (a modo length)
        int itemPosibilidadElegido = new Random().Next(0, 99);

        // creamos las 100 posibilidades, estableciendo como exitosas las primeras X (itemPosibilidadElegido)
        for (int i = 0; i < 100; i++)
        {
            listaPosibilidades.Add(i <= probabilidad);
        }

        // retornamos el resultado de probabilidad
        return listaPosibilidades[itemPosibilidadElegido];
    }
}