﻿using Dominio;
using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MED_Download_Requisiciones_RQT
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

            try
            {
                string strConexionSQL;
                string nomConexionProceso;

                //Definir ambiente
                strConexionSQL = Properties.Settings.Default.strConexionSQL;
                nomConexionProceso = Properties.Settings.Default.nomConexionProceso;

                Download_Configuracion_Inicial.configuracionInicial(strConexionSQL, nomConexionProceso);
                Crear_OEX.ejecutarProceso();

            }
            catch (Exception ex)
            {

            }

            this.Close();

        }
    }
}
