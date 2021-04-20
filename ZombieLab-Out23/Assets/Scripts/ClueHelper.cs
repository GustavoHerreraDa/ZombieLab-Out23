using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ClueHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Clue> pistas;
    public TMP_Text clueText;
    public GameObject panelClue;

    public int actualNumberEnigma;
    public int actualClueNumber;

    void Awake()
    {
        actualNumberEnigma = 1;
        actualClueNumber = 0;
        pistas = new List<Clue>();

        pistas.Add(new Clue() { EnigmaNumber = 1, ClueDescription = "Las letras forman una palabra, para formarla debes mover algunas letras. Recuerda que tienes que poner los tubos en el apoya tubos de abajo." });
        pistas.Add(new Clue() { EnigmaNumber = 1, ClueDescription = "La palabra empieza con P" });
        pistas.Add(new Clue() { EnigmaNumber = 1, ClueDescription = "La palabra es PACIENTE0", isSolution = true });
        pistas.Add(new Clue() { EnigmaNumber = 2, ClueDescription = "Para saber qué probetas colocar debes revisar la sala y habrá dos objetos principales que te ayudarán con eso." });
        pistas.Add(new Clue() { EnigmaNumber = 2, ClueDescription = "Los dos objetos son las banderas de Argentina y China." });
        pistas.Add(new Clue() { EnigmaNumber = 2, ClueDescription = "Las probetas que hay que poner son las mismas que los colores de las banderas de Argentina y China. Recuerda que debes colocar las probetas en el apoya probetas que se encuentra debajo. Por lo tanto las probetas que se deben encastrar son: Bandera de china: Rojo y Amarillo Argentina: Celeste y Blanco ", isSolution = true });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "El personaje que ves a continuación se llama Federico Moura. " });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "Este personaje pertenecía a una banda llamada virus" });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "¿Qué tiene en la cabeza Federico Moura en la imagen?" });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "Tiene una corona de flores" });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "coronavirus." });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "Debes ver la letra de las casillas y compararla con la cantidad de letras de cada bandera." });
        pistas.Add(new Clue() { EnigmaNumber = 3, ClueDescription = "los números 12 - 12 - 5 indican cantidad de letras es decir hay que buscar números con esa cantidad de letras. Las banderas correctas serían: Estados Unidos, Nueva Zelanda e India.", isSolution = true });
        pistas.Add(new Clue() { EnigmaNumber = 4, ClueDescription = "Debes ver algún objeto para compararlo con los Post It con nombres." });
        pistas.Add(new Clue() { EnigmaNumber = 4, ClueDescription = "Los post it los debes comparar con la tabla periódica." });
        pistas.Add(new Clue() { EnigmaNumber = 4, ClueDescription = "Para poder llegar al resultado, debes tomar las dos primeras letras del nombre y con eso ver con que elementos encajan. " });
        pistas.Add(new Clue() { EnigmaNumber = 4, ClueDescription = "Los elementos que debes usar para resolver el acertijo son: BR, NA, FA, AL, AU, IR.Debes usar el número atómico de cada elemento y hacer la operación. Solución: Seria: 35 + 11 + 87 + 13 + 47 x 77 = 3.765", isSolution = true });
        pistas.Add(new Clue() { EnigmaNumber = 5, ClueDescription = "Lo primero será resolver el sudoku, luego deberás sumar los números del primer cuadrado y hacer lo mismo con todos" });
        pistas.Add(new Clue() { EnigmaNumber = 5, ClueDescription = "El resultado de 1 es 45 debes multiplicar 45 por 12 y tendrás el resultado. Solución: 405, 45 x 9 = 405 ", isSolution = true });
        pistas.Add(new Clue() { EnigmaNumber = 6, ClueDescription = "Primero debes resolver la pirámide, luego sumar los resultados de cada casilla. Solución: El resultado sumando los números de la pirámide es 1192.", isSolution = true });
        pistas.Add(new Clue()
        {
            EnigmaNumber = 7,
            ClueDescription = "Debes interpretar las secuencias que se ven en el televisor, Si te sirve puedes agarrar una lapicera y un papel. Recuerda que cuando hay una indicación de x2  es que debes presionar 2 veces seguidas el mismo botón. Solución: "
             + "Secuencia 1: Azul, Negro, Negro, Verde, Amarillo, Amarillo, Rosa. Secuencia 2: Marron, Azul, Verde, Azul, Rojo, Rojo, Negro, Rosa, Verde, Negro, Negro. Secuencia 3: Rojo, Rojo,  Marron, Azul, Azul, Azul, Verde, Verde, Verde, Marron. "
             + "Secuencia 4: Rojo, Rojo, Rojo, Rojo, Negro, Negro, Negro, Marron, Amarillo, Rosa."
             + "Secuencia 5: Negro, Rojo, Rosa, Azul, Azul, Amarillo, Verde, Rosa, Verde, Verde, Verde, Negro, Rojo, Azul, Marron, Marron, Amarillo, Verde, Azul, Amarillo, Negro, Rosa, Negro, Marron, Rosa, Rosa, Rosa, Amarillo, Negro, Rojo, Rojo, Rojo, Negro, Negro, Verde, Azul, Verde. "
             + "Secuencia 6: Rojo, Azul, Verde, Rosa, Marron, Azul. "
             + "Secuencia 7: Rojo, Rojo. ",
            isSolution = true
        });

        pistas.Add(new Clue() { EnigmaNumber = 8, ClueDescription = "Debes comparar los informes de las vacunas en base a: Reaccion Positiva del paciente Efectos Secundarios ,Porcentaje de efectividad en mayores de 60, Porcentaje de efectividad en menores de 10, Mutaciones y ordenar las vacunas de la más peligrosa a la menos peligrosa, colocándolas en las cajas de cristal.", isSolution = true });
        pistas.Add(new Clue() { EnigmaNumber = 8, ClueDescription = "Vacuna Norcoreana Sputnik V Vacuna Chilena Vacuna Pfizer Vacuna China Vacuna Mexicana Vacuna Indica Vacuna Astrazeneca Vacuna Argentina    Vacuna de Microsoft "});
        pistas.Add(new Clue() { EnigmaNumber = 8, ClueDescription = "Si tuviste algún inconveniente técnico con el puzzle presiona el botón ́ ́Resolver Automáticamente ́ ́ y las cajas se cerraran automáticamente y la puerta de salida se abrirá."});

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateClueText()
    {
        panelClue.SetActive(true);
        GetStringNumberEnigma();
    }

    public void CloseClueText()
    {
        clueText.text = "";
        panelClue.SetActive(false);
    }


    private void GetStringNumberEnigma()
    {
        var clue = pistas.Where(x => x.EnigmaNumber == actualNumberEnigma).ToList();

        clueText.text = clue[actualClueNumber].ClueDescription;
        
        actualClueNumber++;
        
        if (clue[actualClueNumber].isSolution)
        {
            actualClueNumber = 0;
            actualNumberEnigma++;
        }

    }
}

public class Clue
{
    public int EnigmaNumber;
    public string ClueDescription;
    public bool isSolution;
}