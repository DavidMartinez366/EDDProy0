﻿using EDDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;


//using GraphVizWrapper;
//using GraphVizWrapper.Queries;
//using GraphVizWrapper.Commands;

//using csdot;
//using csdot.Attributes.DataTypes;

namespace EDDemo.Estructuras_No_Lineales
{
    public partial class frmArboles : Form
    {
        ArbolBusqueda miArbol;
        NodoBinario miRaiz;

        public frmArboles()
        {
            InitializeComponent();
            miArbol = new ArbolBusqueda();
            miRaiz = null;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            int nuevoValor;
            if (int.TryParse(txtDato.Text, out nuevoValor))

                //Obtenemos el nodo Raiz del arbol
                miRaiz = miArbol.RegresaRaiz();

            if (!miArbol.BuscarNodo(nuevoValor, miRaiz))
            {
                // Si el valor no existe, se inserta
                miArbol.InsertaNodo(nuevoValor, ref miRaiz);

                //Limpiamos la cadena donde se concatenan los nodos del arbol 
                miArbol.strArbol = "";

                //Se inserta el nodo con el dato capturado
                miArbol.InsertaNodo(int.Parse(txtDato.Text),
                                    ref miRaiz);

                //Leer arbol completo y mostrarlo en caja de texto
                miArbol.MuestraArbolAcostado(1, miRaiz);
                txtArbol.Text = miArbol.strArbol;

                txtDato.Text = "";
            }
            else
            {
                // Si el valor ya existe mostrar un mensaje
                MessageBox.Show("El valor ya existe en el árbol.");
                txtDato.Text = "";
            }

        }
    
        

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            miArbol = null;
            miRaiz = null;
            miArbol = new ArbolBusqueda();
            txtArbol.Text = "";
            txtDato.Text = "";
            lblRecorridoPreOrden.Text = "";
            lblRecorridoInOrden.Text = "";
            lblRecorridoPostOrden.Text = "";
        }

        private void btnGrafica_Click(object sender, EventArgs e)
        {
            String graphVizString;

            miRaiz = miArbol.RegresaRaiz();
            if (miRaiz == null)
            {
                MessageBox.Show("El arbol esta vacio");
                return;
            }

            StringBuilder b = new StringBuilder();
            b.Append("digraph G { node [shape=\"circle\"]; " + Environment.NewLine);
            b.Append(miArbol.ToDot(miRaiz));
            b.Append("}");
            graphVizString = b.ToString();

            //graphVizString = @" digraph g{ label=""Graph""; labelloc=top;labeljust=left;}";
            //graphVizString = @"digraph Arbol{Raiz->60; 60->40. 60->90; 40->34; 40->50;}";
            Bitmap bm = FileDotEngine.Run(graphVizString);


            frmGrafica graf = new frmGrafica();
            graf.ActualizaGrafica(bm);
            graf.MdiParent = this.MdiParent;
            graf.Show();
        }


        private void btnRecorrer_Click(object sender, EventArgs e)
        {
            //Recorrido en PreOrden
            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();
            miArbol.strRecorrido = "";

            if (miRaiz == null)
            {
                lblRecorridoPreOrden.Text = "El arbol esta vacio";
                return;
            }
            lblRecorridoPreOrden.Text = "";
            miArbol.PreOrden(miRaiz);

            lblRecorridoPreOrden.Text = miArbol.strRecorrido;


            //Recorrido en InOrden
            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();
            miArbol.strRecorrido = "";

            if (miRaiz == null)
            {
                lblRecorridoPostOrden.Text = "El arbol esta vacio";
                return;
            }
            lblRecorridoInOrden.Text = "";
            miArbol.InOrden(miRaiz);
            lblRecorridoInOrden.Text = miArbol.strRecorrido;


            //Recorrido en PostOrden
            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();
            miArbol.strRecorrido = "";

            if (miRaiz == null) {
                lblRecorridoPostOrden.Text = "El arbol esta vacio";
                return;
            }
            lblRecorridoPostOrden.Text = "";
            miArbol.PostOrden(miRaiz);
            lblRecorridoPostOrden.Text = miArbol.strRecorrido;
        }

        private void btnCrearArbol_Click(object sender, EventArgs e)
        {
            //Limpiamos los objetos y clases del anterior arbol
            miArbol = null;
            miRaiz = null;
            miArbol = new ArbolBusqueda();
            txtArbol.Text = "";
            txtDato.Text = "";

            miArbol.strArbol = "";

            Random rnd = new Random();

            for (int nNodos = 1; nNodos <= txtNodos.Value; nNodos++)
            {
                int Dato = rnd.Next(1, 100);

                // Obtenemos el nodo Raiz del arbol
                miRaiz = miArbol.RegresaRaiz();

                // Verificar si el dato ya existe en el árbol
                if (!miArbol.BuscarNodo(Dato, miRaiz))
                {
                    // Si el dato no existe, se inserta
                    miArbol.InsertaNodo(Dato, ref miRaiz);
                }
                else
                {
                    // Si el dato ya existe, se salta la inserción y pasa al siguiente número
                    nNodos--; // Disminuir el contador para intentar agregar un nodo adicional
                }
            }

            // Leer arbol completo y mostrarlo en caja de texto
            miArbol.MuestraArbolAcostado(1, miRaiz);
            txtArbol.Text = miArbol.strArbol;

            txtDato.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int valor;

            // Intentar convertir el valor ingresado en el TextBox a entero
            if (int.TryParse(txtBuscar.Text, out valor))
            {
                // Llamar al método BuscarNodo y obtener el resultado
                bool encontrado = miArbol.BuscarNodo(valor, miArbol.RegresaRaiz());

                // Mostrar el resultado en un Label
                if (encontrado)
                    MessageBox.Show("valor si existe en el arbol");
                else
                    MessageBox.Show("Valor no encontrado en el árbol.");
            }
            else
            {
                lblDatos.Text = "Ingrese un número válido.";
            }
        }
    }
}