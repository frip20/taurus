using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDKCONTPAQNGLib;

namespace contpaq
{
    public partial class Form1 : Form
    {
        private string cuenta_almacen = "1007000000000";
        private string cuenta_proveedor = "2218005003000";
        private string cuenta_iva = "1005007000000";

        public Form1()
        {
            InitializeComponent();
            Label.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbl.Text = "Procesando Entrada....\n";
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TSdkPoliza poliza = new TSdkPoliza();
            TSdkTipoPoliza tipoPoliza = new TSdkTipoPoliza();
            TSdkSesion session = new TSdkSesion();
            TSdkMovimientoPoliza movimiento = new TSdkMovimientoPoliza();
            TSdkCuenta cuenta = new TSdkCuenta();
            TSdkEmpresa empresa = new TSdkEmpresa();
            TSdkControlIVA iva = new TSdkControlIVA();
            try
            {
                if (session.conexionActiva < 1)
                    session.iniciaConexion();
                if (session.conexionActiva == 1 && session.ingresoUsuario == 0)
                    session.firmaUsuario();
                if (session.conexionActiva == 1 && session.ingresoUsuario == 1)
                    session.abreEmpresa(textBox1.Text);

                empresa.setSesion(session);
                poliza.setSesion(session);
                cuenta.setSesion(session);
                tipoPoliza.setSesion(session);

                lbl.Text += "Empresa: " + empresa.Nombre + "\n";
                int empresaId = empresa.IdEmpresa;

                tipoPoliza.iniciarInfo();
                tipoPoliza.Tipo = ETIPOPOLIZA.TIPO_DIARIO;
                poliza.iniciarInfo();
                poliza.Fecha = DateTime.Now;
                poliza.Tipo = tipoPoliza.Tipo;
                poliza.Numero = 98767;
                poliza.Clase = ECLASEPOLIZA.CLASE_AFECTAR;
                poliza.Impresa = 0;
                poliza.Concepto = "E0001 FolioF";
                poliza.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

                int movNum = 1;
                movimiento.setSesion(session);
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = cuenta_almacen;//"5500017005002"
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                movimiento.Importe = 100;
                movimiento.Concepto = "Cargo a Almacen";
                int mov = poliza.agregaMovimiento(movimiento);
                lbl.Text += "Registrar movimiento de Cargo\n";

                movNum += 1;
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = cuenta_iva;
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                movimiento.Importe = 16;
                movimiento.Concepto = "Cargo a Almacen";
                mov = poliza.agregaMovimiento(movimiento);
                lbl.Text += "Registrar movimiento de IVA\n";

                movNum+=1;
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = cuenta_proveedor;
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_ABONO;
                movimiento.Importe = 116;
                movimiento.Concepto = "Abono a nombre del proveedor";
                mov = poliza.agregaMovimiento(movimiento);
                lbl.Text += "Registrar movimiento de Abono\n";
                
                if (mov == 0)
                {
                    throw new Exception("No se pudo agregar el movimiento");
                }

                int polizaId = poliza.crea();
                if (polizaId == 0)
                {
                    lbl.Text += "# Movimientos: " + poliza.NumeroMovtos + "\n";
                    throw new Exception("No se pudo crear la poliza: " + poliza.UltimoMsjError);
                }
                lbl.Text += "Poliza creada: " + polizaId + "\n";
                lbl.Text += "Generando IVA\n";

            }
            catch (Exception ex)
            {
                lbl.Text += ex.Message +  poliza.UltimoMsjError + "\n";
            }
            finally
            {
                session.cierraEmpresa();
                session.finalizaConexion();
                lbl.Text += "Proceso completado";
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            TSdkPoliza poliza = new TSdkPoliza();
            TSdkTipoPoliza tipoPoliza = new TSdkTipoPoliza();
            TSdkSesion session = new TSdkSesion();
            TSdkMovimientoPoliza movimiento = new TSdkMovimientoPoliza();
            TSdkCuenta cuenta = new TSdkCuenta();
            TSdkEmpresa empresa = new TSdkEmpresa();
            TSdkControlIVA iva = new TSdkControlIVA();
            try
            {
                if (session.conexionActiva < 1)
                    session.iniciaConexion();
                if (session.conexionActiva == 1 && session.ingresoUsuario == 0)
                    session.firmaUsuario();
                if (session.conexionActiva == 1 && session.ingresoUsuario == 1)
                    session.abreEmpresa(textBox1.Text);

                empresa.setSesion(session);
                poliza.setSesion(session);
                cuenta.setSesion(session);
                tipoPoliza.setSesion(session);

                lbl.Text += "Empresa: " + empresa.Nombre + "\n";
                int empresaId = empresa.IdEmpresa;

                tipoPoliza.iniciarInfo();
                tipoPoliza.Tipo = ETIPOPOLIZA.TIPO_DIARIO;
                poliza.iniciarInfo();
                poliza.Fecha = DateTime.Now;
                poliza.Tipo = tipoPoliza.Tipo;
                poliza.Numero = 98766;
                poliza.Clase = ECLASEPOLIZA.CLASE_AFECTAR;
                poliza.Impresa = 0;
                poliza.Concepto = "S001 Prueba de poliza";
                poliza.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

                int movNum = 1;
                movimiento.setSesion(session);
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = "5500017005002";
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                movimiento.Importe = 100;
                movimiento.Concepto = "Cargo a Descripcion de la cuenta";
                int mov = poliza.agregaMovimiento(movimiento);
                lbl.Text += "Registrar movimiento de Cargo\n";

                movNum += 1;
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = cuenta_almacen;
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_ABONO;
                movimiento.Importe = 100;
                movimiento.Concepto = "Abono a almacen";
                mov = poliza.agregaMovimiento(movimiento);
                lbl.Text += "Registrar movimiento de Abono\n";

                if (mov == 0)
                {
                    throw new Exception("No se pudo agregar el movimiento");
                }

                int polizaId = poliza.crea();
                if (polizaId == 0)
                {
                    lbl.Text += "# Movimientos: " + poliza.NumeroMovtos + "\n";
                    throw new Exception("No se pudo crear la poliza: " + poliza.UltimoMsjError);
                }
                lbl.Text += "Poliza creada: " + polizaId + "\n";

            }
            catch (Exception ex)
            {
                lbl.Text += ex.Message + poliza.UltimoMsjError + "\n";
            }
            finally
            {
                session.cierraEmpresa();
                session.finalizaConexion();
                lbl.Text += "Proceso completado";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbl.Text = "Procesando Salida....\n";
            backgroundWorker2.RunWorkerAsync();
        }
    }
}
