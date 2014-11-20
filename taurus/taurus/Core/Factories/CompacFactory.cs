using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using SDKCONTPAQNGLib;
using taurus.Core.Services;
using taurus.Core.Constants;

namespace taurus.Core.Factories
{
    public class CompacFactory : ICompac
    {
        public void polizaEntrada(Entities.Stock stock)
        {
            if (stock.Proveedor.CodigoCuenta == null || stock.Proveedor.CodigoCuenta.Trim() == "") {
                throw new Exception("El proveedor no tiene una cuenta asignada");
            }

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
                    session.firmaUsuarioParams(ConfigurationService.Instance.getProperty(ConfigurationConstants.CONTPAQ_USER),
                        ConfigurationService.Instance.getProperty(ConfigurationConstants.CONTPAQ_PWD));
                if (session.conexionActiva == 1 && session.ingresoUsuario == 1)
                    session.abreEmpresa(ConfigurationService.Instance.getProperty(ConfigurationConstants.CONTPAQ_EMPRESA));

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
                poliza.Numero = (stock.Id + 80000);
                poliza.Clase = ECLASEPOLIZA.CLASE_AFECTAR;
                poliza.Impresa = 0;
                poliza.Concepto = stock.polizaConcepto;
                poliza.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

                int movNum = 0;
                int mov = 0;
                foreach (var item in stock.Items)
                {
                    movNum += 1;
                    movimiento.setSesion(session);
                    movimiento.iniciarInfo();
                    movimiento.NumMovto = movNum;
                    movimiento.CodigoCuenta = item.Cuenta.Codigo;
                    movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                    movimiento.Importe = item.Importe;
                    movimiento.Concepto = "Cargo a Almacen";
                    mov = poliza.agregaMovimiento(movimiento);
                }

                movNum += 1;
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = ConfigurationService.Instance.getProperty(ConfigurationConstants.CUENTA_IVA); 
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                movimiento.Importe = (stock.importeTotal() * (decimal)0.16);
                movimiento.Concepto = stock.Proveedor.Description;
                mov = poliza.agregaMovimiento(movimiento);

                movNum += 1;
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = stock.Proveedor.CodigoCuenta;
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_ABONO;
                movimiento.Importe = 116;
                movimiento.Concepto = string.Format("Abono a {0}", stock.Proveedor.Description);
                mov = poliza.agregaMovimiento(movimiento);

                if (mov == 0)
                {
                    throw new Exception("No se pudo agregar el movimiento");
                }

                int polizaId = poliza.crea();
                if (polizaId == 0)
                {
                    throw new Exception("No se pudo crear la poliza: " + poliza.UltimoMsjError);
                }else {
                    stock.PolizaId = polizaId;
                    stock.SaveAndFlush();
                    stock.Refresh();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                session.cierraEmpresa();
                session.finalizaConexion();
            }
        }

        public void polizaSalida(Entities.Stock stock)
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
                    session.firmaUsuarioParams(ConfigurationService.Instance.getProperty(ConfigurationConstants.CONTPAQ_USER),
                        ConfigurationService.Instance.getProperty(ConfigurationConstants.CONTPAQ_PWD));
                if (session.conexionActiva == 1 && session.ingresoUsuario == 1)
                    session.abreEmpresa(ConfigurationService.Instance.getProperty(ConfigurationConstants.CONTPAQ_EMPRESA));

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
                poliza.Numero = (stock.Id + 80000); ;
                poliza.Clase = ECLASEPOLIZA.CLASE_AFECTAR;
                poliza.Impresa = 0;
                poliza.Concepto = stock.polizaConcepto;
                poliza.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

                int movNum = 0;
                int mov = 0;
                foreach (var item in stock.Items)
                {
                    movNum += 1;
                    movimiento.setSesion(session);
                    movimiento.iniciarInfo();
                    movimiento.NumMovto = movNum;
                    movimiento.CodigoCuenta = item.Cuenta.Codigo;
                    movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_CARGO;
                    movimiento.Importe = item.Importe;
                    movimiento.Concepto = string.Format("Cargo a {0}", item.Cuenta.Codigo);
                    mov = poliza.agregaMovimiento(movimiento);
                }

               

                movNum += 1;
                movimiento.iniciarInfo();
                movimiento.NumMovto = movNum;
                movimiento.CodigoCuenta = ConfigurationService.Instance.getProperty(ConfigurationConstants.CUENTA_ALMACEN); 
                movimiento.TipoMovto = ETIPOIMPORTEMOVPOLIZA.MOVPOLIZA_ABONO;
                movimiento.Importe = stock.importeTotal();
                movimiento.Concepto = "Abono a almacen";
                mov = poliza.agregaMovimiento(movimiento);

                if (mov == 0)
                {
                    throw new Exception("No se pudo agregar el movimiento");
                }

                int polizaId = poliza.crea();
                if (polizaId == 0)
                {
                    throw new Exception("No se pudo crear la poliza: " + poliza.UltimoMsjError);
                }
                else {
                    stock.PolizaId = polizaId;
                    stock.SaveAndFlush();
                    stock.Refresh();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                session.cierraEmpresa();
                session.finalizaConexion();
            }
        }
    }
}