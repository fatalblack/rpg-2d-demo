using System.Collections.Generic;

public static class CriterioNiveles
{
    public static List<NivelPorExperiencia> ObtenerListaNiveles()
    {
        // establecemos la tabla de niveles y experiencias para llegar a ellos
        List<NivelPorExperiencia> nivelesPorExperiencia = new List<NivelPorExperiencia>
        {
            new NivelPorExperiencia { Nivel = 1, ExperienciaNecesaria = 0 },
            new NivelPorExperiencia { Nivel = 2, ExperienciaNecesaria = 900 },
            new NivelPorExperiencia { Nivel = 3, ExperienciaNecesaria = 1400 },
            new NivelPorExperiencia { Nivel = 4, ExperienciaNecesaria = 2100 },
            new NivelPorExperiencia { Nivel = 5, ExperienciaNecesaria = 2800 },
            new NivelPorExperiencia { Nivel = 6, ExperienciaNecesaria = 3600 },
            new NivelPorExperiencia { Nivel = 7, ExperienciaNecesaria = 4500 },
            new NivelPorExperiencia { Nivel = 8, ExperienciaNecesaria = 5400 },
            new NivelPorExperiencia { Nivel = 9, ExperienciaNecesaria = 6500 },
            new NivelPorExperiencia { Nivel = 10, ExperienciaNecesaria = 7600 },
        };

        // retornamos la tabla de niveles
        return nivelesPorExperiencia;
    }

    public static int ObtenerNivelPorExperiencia(int experienciaActual)
    {
        // obtenemos la tabla de niveles para comparar más adelante
        List<NivelPorExperiencia> nivelesPorExperiencia = ObtenerListaNiveles();
        int nivelActual = 1;

        // si tiene experiencia 0 suponemos que tiene nivel 1
        if (nivelesPorExperiencia.Count == 0)
        {
            // retornamos el nivel actual
            return nivelActual;
        }

        // evaluamos por cada nivel si la experiencia es suficiente para estar en dicho nivel
        for (int i = 0; i < nivelesPorExperiencia.Count; i++)
        {
            NivelPorExperiencia nivelPorExperiencia = nivelesPorExperiencia[i];

            // si la experiencia es suficiente establecemos el nivel evaluado como nivel actual
            if (nivelPorExperiencia.ExperienciaNecesaria <= experienciaActual && nivelPorExperiencia.Nivel > nivelActual)
            {
                nivelActual = nivelPorExperiencia.Nivel;
            }
        }

        // retornamos el nivel actual
        return nivelActual;
    }
}