using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using SDKCONTPAQNGLib;

namespace taurus.Core.Factories
{
    public class CompacFactory : ICompac
    {
        public void addPoliza(Entities.Stock stock)
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
                    session.abreEmpresa("ActDRM_2011");

                empresa.setSesion(session);
                poliza.setSesion(session);
                cuenta.setSesion(session);
                tipoPoliza.setSesion(session);

                int empresaId = empresa.IdEmpresa;

                tipoPoliza.iniciarInfo();
                tipoPoliza.Tipo = ETIPOPOLIZA.TIPO_DIARIO;
                poliza.iniciarInfo();
                poliza.Fecha = DateTime.Now;
                poliza.Tipo = tipoPoliza.Tipo;
                poliza.Numero = 98765;
                poliza.Clase = ECLASEPOLIZA.CLASE_AFECTAR;
                poliza.Impresa = 0;
                poliza.Concepto = "Prueba de poliza";
                poliza.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

                int movNum = 1;
                movimiento.setSesion(session);
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = "6000090";
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                movimiento.Importe = 100;
                movimiento.Concepto = "Movimiento 1";

                if (poliza.agregaMovimiento(movimiento) == 0)
                {
                    //
                }
                if (poliza.crea() == 0)
                {
                    //
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {
                session.cierraEmpresa();
                session.finalizaConexion();
            }
        }
    }
}