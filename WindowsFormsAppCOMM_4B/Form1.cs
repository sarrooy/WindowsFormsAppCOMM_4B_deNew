using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
namespace WindowsFormsAppCOMM_4B
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //agregar aqui todo lo que requieras al inicializar la app
            string[] Nombres_de_puertos_disponible = SerialPort.GetPortNames();
            foreach (string mostrar_puerto in Nombres_de_puertos_disponible)
                comboBox_PORT_NAME.Items.Add(mostrar_puerto);

            label_STATUS_PORT.Text = "puerto NO seleccionado";
        }

        private void button_EXIT_Click(object sender, EventArgs e)
        {
            //aqui cerraremos y liveraremos el recurso del puerto
            serialPort1.Close();
            serialPort1.Dispose();
            Close();
        }

        private void comboBox_PORT_NAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            //de lo que selecciono el usuario como puerto serie
            //que me lo guarde en una variable 
            string Selected_PORT= comboBox_PORT_NAME.Text;
            serialPort1.PortName = Selected_PORT;
        }

        private void button_OPEN_PORT_Click(object sender, EventArgs e)
        {
            //garantizar puerto
            serialPort1.Open();
            CheckForIllegalCrossThreadCalls = false;
            if (serialPort1.IsOpen == true)
                label_STATUS_PORT.Text = "acceso correcto";
            else
            {
                label_STATUS_PORT.Text = "acceso denegado";
                return;
            }


        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //aqui tengo que declarar una variable
            string leo_linea_de_comm = serialPort1.ReadLine();
            double numero = Convert.ToDouble(leo_linea_de_comm);
            double voltjae = Math.Round((numero * 5) / 1023, 2);
            label_VOLTAGE.Text = voltjae.ToString();
        }
    }
}
