using System.Collections.Generic;

public static class CreadorRecolectables
{
    public static Recolectable CrearPorIdRecolectable(int idRecolectable)
    {
        // inicializamos el recolectable vacío por defecto
        Recolectable recolectable = new Recolectable();

        // dependiendo el idRecolectable que hayamos recibido crearemos el indicado
        switch (idRecolectable)
        {
            case (int)RecolectableIndices.ArbolRoble:
                recolectable = CrearArbolRoble();
                break;
            case (int)RecolectableIndices.MenaBronce:
                recolectable = CrearMenaBronce();
                break;
        }

        // retornamos el recolectable creado
        return recolectable;
    }

    public static Recolectable CrearArbolRoble()
    {
        int idRecolectable = 1;

        // determinamos que objetos podremos recolectar de la entidad recolectable
        List<RecolectableRecurso> recursos = new List<RecolectableRecurso>
        {
            CrearRecolectableRecurso(idRecolectable, 10, 1, 100, CreadorObjetos.CrearMaderaRoble())
        };

        // armamos la entidad recolectable
        Recolectable recolectable = new Recolectable
        {
            Id = idRecolectable,
            Nombre = "Roble",
            Caracteristica = TalaCaracteristica(idRecolectable, 0),
            Recursos = recursos
        };

        // retornamos el recolectable
        return recolectable;
    }

    public static Recolectable CrearMenaBronce()
    {
        int idRecolectable = 2;

        // determinamos que objetos podremos recolectar de la entidad recolectable
        List<RecolectableRecurso> recursos = new List<RecolectableRecurso>
        {
            CrearRecolectableRecurso(idRecolectable, 10, 1, 100, CreadorObjetos.CrearFragmentoBronce())
        };

        // armamos la entidad recolectable
        Recolectable recolectable = new Recolectable
        {
            Id = idRecolectable,
            Nombre = "Mena de bronce",
            Caracteristica = MineriaCaracteristica(idRecolectable, 0),
            Recursos = recursos
        };

        // retornamos el recolectable
        return recolectable;
    }

    private static RecolectableCaracteristica TalaCaracteristica(int idRecolectable, int tiempoRecargaMilisegundos)
    {
        // armamos y retornamos las características correspondiente al oficio deseado
        return new RecolectableCaracteristica
        {
            IdRecolectable = idRecolectable,
            SePuedeTalar = true,
            SePuedeAgotar = tiempoRecargaMilisegundos > 0,
            TiempoRecargaMilisegundos = tiempoRecargaMilisegundos
        };
    }

    private static RecolectableCaracteristica MineriaCaracteristica(int idRecolectable, int tiempoRecargaMilisegundos)
    {
        // armamos y retornamos las características correspondiente al oficio deseado
        return new RecolectableCaracteristica
        {
            IdRecolectable = idRecolectable,
            SePuedeMinar = true,
            SePuedeAgotar = tiempoRecargaMilisegundos > 0,
            TiempoRecargaMilisegundos = tiempoRecargaMilisegundos
        };
    }

    private static RecolectableRecurso CrearRecolectableRecurso(int idRecolectable, int experiencia, int cantidad, int probabilidad, Objeto objeto)
    {
        // creamos y retornamos un recurso
        return new RecolectableRecurso
        {
            IdRecolectable = idRecolectable,
            IdObjeto = objeto.Id,
            Experiencia = experiencia,
            Cantidad = cantidad,
            Probabilidad = probabilidad,
            Objeto = objeto
        };
    }
}