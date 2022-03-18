using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Prediccion
{
    public string comentario;

    public string comentarioEsp; // o tipo null quiza porque esta vacio

    public string dataActualizacion; // fechas en tipo string

    public string dataPredicion;

    public int dia;

    public List<MeteoMain> listaMapas;
    



}
