public static class CreadorObjetos
{
    public static Objeto CrearPorIdObjeto(int idObjeto)
    {
        // inicializamos el objeto vacío por defecto
        Objeto objeto = new Objeto();

        // dependiendo el idObjeto que hayamos recibido crearemos el indicado
        switch (idObjeto)
        {
            case (int)ObjetoIndices.PocionVidaMenor:
                objeto = CrearPocionVidaMenor();
                break;
            case (int)ObjetoIndices.PocionVida:
                objeto = CrearPocionVida();
                break;
            case (int)ObjetoIndices.KitTala:
                objeto = CrearKitTala();
                break;
            case (int)ObjetoIndices.KitMineria:
                objeto = CrearKitMineria();
                break;
            case (int)ObjetoIndices.SetNovato:
                objeto = CrearSetNovato();
                break;
            case (int)ObjetoIndices.SetPaladin:
                objeto = CrearSetPaladin();
                break;
            case (int)ObjetoIndices.SetGuerrero:
                objeto = CrearSetGuerrero();
                break;
            case (int)ObjetoIndices.SetMatadragones:
                objeto = CrearSetMatadragones();
                break;
            case (int)ObjetoIndices.MaderaRoble:
                objeto = CrearMaderaRoble();
                break;
            case (int)ObjetoIndices.FragmentoBronce:
                objeto = CrearFragmentoBronce();
                break;
        }

        // retornamos el objeto creado
        return objeto;
    }

    public static Objeto CrearPocionVidaMenor()
    {
        int idObjeto = (int)ObjetoIndices.PocionVidaMenor;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Poción de vida menor",
            Descripcion = "Restaura 100 puntos de Vida de tu personaje.",
            Caracteristica = ConsumibleCaracteristica(idObjeto, true, 20, 18),
            Estadistica = ConsumibleEstadistica(idObjeto, 100),
            RutaImagen = "pocion-vida-menor"
        };
    }

    public static Objeto CrearPocionVida()
    {
        int idObjeto = (int)ObjetoIndices.PocionVida;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Poción de vida",
            Descripcion = "Restaura 200 puntos de Vida de tu personaje.",
            Caracteristica = ConsumibleCaracteristica(idObjeto, true, 34, 30),
            Estadistica = ConsumibleEstadistica(idObjeto, 200),
            RutaImagen = "pocion-vida"
        };
    }

    public static Objeto CrearKitTala()
    {
        int idObjeto = (int)ObjetoIndices.KitTala;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Kit de tala",
            Descripcion = "Todo lo necesario para poder talar árboles.",
            Caracteristica = TalaCaracteristica(idObjeto, true, 100, 90),
            Estadistica = TalaEstadistica(idObjeto),
            RutaImagen = "kit-tala"
        };
    }

    public static Objeto CrearKitMineria()
    {
        int idObjeto = (int)ObjetoIndices.KitMineria;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Kit de minería",
            Descripcion = "Todo lo necesario para poder minar menas.",
            Caracteristica = MineriaCaracteristica(idObjeto, true, 100, 90),
            Estadistica = MineriaEstadistica(idObjeto),
            RutaImagen = "kit-mineria"
        };
    }

    public static Objeto CrearSetNovato()
    {
        int idObjeto = (int)ObjetoIndices.SetNovato;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Set de novato",
            Descripcion = "Equipamiento de batalla útil para iniciar una aventura.",
            Caracteristica = BatallaCaracteristica(idObjeto, true, 2000, 1800),
            Estadistica = BatallaEstadistica(idObjeto, 15, 15, 15, 15, 100),
            RutaImagen = "set-novato"
        };
    }

    public static Objeto CrearSetPaladin()
    {
        int idObjeto = (int)ObjetoIndices.SetPaladin;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Set de Paladín",
            Descripcion = "Equipamiento ideal para personajes defensivos.",
            Caracteristica = BatallaCaracteristica(idObjeto, true, 10000, 9000),
            Estadistica = BatallaEstadistica(idObjeto, 100, 50, 100, 350, 250),
            RutaImagen = "set-paladin"
        };
    }

    public static Objeto CrearSetGuerrero()
    {
        int idObjeto = (int)ObjetoIndices.SetGuerrero;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Set de Guerrero",
            Descripcion = "Equipamiento ideal para personajes ofensivos.",
            Caracteristica = BatallaCaracteristica(idObjeto, true, 10000, 9000),
            Estadistica = BatallaEstadistica(idObjeto, 350, 100, 50, 100, 250),
            RutaImagen = "set-guerrero"
        };
    }

    public static Objeto CrearSetMatadragones()
    {
        int idObjeto = (int)ObjetoIndices.SetMatadragones;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Set Matadragones xD",
            Descripcion = "Equipamiento ideal para lo que sea, muchos stats ;D.",
            Caracteristica = BatallaCaracteristica(idObjeto, true, 50000, 45000),
            Estadistica = BatallaEstadistica(idObjeto, 1000, 1000, 1000, 1000, 500),
            RutaImagen = "set-matadragones"
        };
    }

    public static Objeto CrearMaderaRoble()
    {
        int idObjeto = (int)ObjetoIndices.MaderaRoble;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Madera de roble",
            Descripcion = "Recurso obtenido a través de la tala de robles.",
            Caracteristica = RecursoCaracteristica(idObjeto, 50, 50),
            Estadistica = RecursoEstadistica(idObjeto),
            RutaImagen = "madera-roble"
        };
    }

    public static Objeto CrearFragmentoBronce()
    {
        int idObjeto = (int)ObjetoIndices.FragmentoBronce;

        // creamos y retornamos el objeto con sus propiedades, características y estadísticas
        return new Objeto
        {
            Id = idObjeto,
            Nombre = "Fragmento de bronce",
            Descripcion = "Recurso obtenido a través de la minería de menas de bronce.",
            Caracteristica = RecursoCaracteristica(idObjeto, 50, 50),
            Estadistica = RecursoEstadistica(idObjeto),
            RutaImagen = "fragmento-bronce"
        };
    }

    private static ObjetoCaracteristica ConsumibleCaracteristica(int idObjeto, bool ofrecerEnTienda, int precioCompra, int precioVenta)
    {
        // creamos y retornamos las características predefinidas para el tipo de objeto
        return new ObjetoCaracteristica
        {
            IdObjeto = idObjeto,
            Consumible = true,
            Apilable = true,
            OfrecerEnTienda = ofrecerEnTienda,
            PrecioCompra = precioCompra,
            PrecioVenta = precioVenta
        };
    }

    private static ObjetoEstadistica ConsumibleEstadistica(int idObjeto, int vidaActual)
    {
        // creamos y retornamos las estadísticas predefinidas para el tipo de objeto
        return new ObjetoEstadistica
        {
            IdObjeto = idObjeto,
            VidaActual = vidaActual,
        };
    }

    private static ObjetoCaracteristica TalaCaracteristica(int idObjeto, bool ofrecerEnTienda, int precioCompra, int precioVenta)
    {
        // creamos y retornamos las características predefinidas para el tipo de objeto
        return new ObjetoCaracteristica
        {
            IdObjeto = idObjeto,
            ParaTalar = true,
            Equipable = false,
            OfrecerEnTienda = ofrecerEnTienda,
            PrecioCompra = precioCompra,
            PrecioVenta = precioVenta
        };
    }

    private static ObjetoEstadistica TalaEstadistica(int idObjeto)
    {
        // creamos y retornamos las estadísticas predefinidas para el tipo de objeto
        return new ObjetoEstadistica
        {
            IdObjeto = idObjeto
        };
    }

    private static ObjetoCaracteristica MineriaCaracteristica(int idObjeto, bool ofrecerEnTienda, int precioCompra, int precioVenta)
    {
        // creamos y retornamos las características predefinidas para el tipo de objeto
        return new ObjetoCaracteristica
        {
            IdObjeto = idObjeto,
            ParaMinar = true,
            Equipable = false,
            OfrecerEnTienda = ofrecerEnTienda,
            PrecioCompra = precioCompra,
            PrecioVenta = precioVenta
        };
    }

    private static ObjetoEstadistica MineriaEstadistica(int idObjeto)
    {
        // creamos y retornamos las estadísticas predefinidas para el tipo de objeto
        return new ObjetoEstadistica
        {
            IdObjeto = idObjeto
        };
    }

    private static ObjetoCaracteristica BatallaCaracteristica(int idObjeto, bool ofrecerEnTienda, int precioCompra, int precioVenta)
    {
        // creamos y retornamos las características predefinidas para el tipo de objeto
        return new ObjetoCaracteristica
        {
            IdObjeto = idObjeto,
            Equipable = true,
            OfrecerEnTienda = ofrecerEnTienda,
            PrecioCompra = precioCompra,
            PrecioVenta = precioVenta
        };
    }

    private static ObjetoEstadistica BatallaEstadistica(int idObjeto, int fuerza, int agilidad, int inteligencia, int vitalidad, int durabilidadMaxima)
    {
        // creamos y retornamos las estadísticas predefinidas para el tipo de objeto
        return new ObjetoEstadistica
        {
            IdObjeto = idObjeto,
            Fuerza = fuerza,
            Agilidad = agilidad,
            Inteligencia = inteligencia,
            Vitalidad = vitalidad,
            DurabilidadMaxima = durabilidadMaxima,
            DurabilidadActual = durabilidadMaxima
        };
    }

    private static ObjetoCaracteristica RecursoCaracteristica(int idObjeto, int precioCompra, int precioVenta)
    {
        // creamos y retornamos las características predefinidas para el tipo de objeto
        return new ObjetoCaracteristica
        {
            IdObjeto = idObjeto,
            Apilable = true,
            PrecioCompra = precioCompra,
            PrecioVenta = precioVenta
        };
    }

    private static ObjetoEstadistica RecursoEstadistica(int idObjeto)
    {
        // creamos y retornamos las estadísticas predefinidas para el tipo de objeto
        return new ObjetoEstadistica
        {
            IdObjeto = idObjeto
        };
    }
}