using System;
using System.Data;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace AsuFit.Reportes
{
    public class GeneradorPDF
    {
        // ========================================================================
        // 1. MÉTODO PARA EL TICKET TÉRMICO (Impresoras chicas de 80mm)
        // ========================================================================
        public void GenerarTicketTermico(DataTable detalles, decimal total, string cliente, string ci, string ruc, string metodoPago, decimal montoRecibido, decimal vuelto, string cajero, string nroTicket)
        {
            Rectangle tamañoPapel = new Rectangle(226f, 800f);
            Document doc = new Document(tamañoPapel, 2f, 2f, 10f, 10f);

            string rutaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string rutaArchivo = Path.Combine(rutaDescargas, $"Comprobante_Venta_{nroTicket}.pdf");

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                Font fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 7);
                Font fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7);

                Paragraph cabeceraGym = new Paragraph("ASUFIT GYM\nR.U.C.: XXXXXXXX-X\n", fuenteNormal);
                cabeceraGym.Alignment = Element.ALIGN_CENTER;
                doc.Add(cabeceraGym);

                Paragraph cabeceraFiscal = new Paragraph(
                    "Av. Principal 123 - Asunción\n" +
                    "Tel: 0972910196\n" +
                    "Timbrado Nro.: xxxxxxxx\n" +
                    "Fecha Inicio : xx/xx/xxxx\n" +
                    "Fecha Fin    : xx/xx/xxxx\n" +
                    "Factura Nro. : xxx-xxx-xxxxxxx\n\n", fuenteNormal);
                cabeceraFiscal.Alignment = Element.ALIGN_CENTER;
                doc.Add(cabeceraFiscal);

                Paragraph info = new Paragraph();
                info.Font = fuenteNormal;
                info.Add($"Cliente: {cliente}\n");
                info.Add($"C.I.: {ci}\n");
                info.Add($"R.U.C.: {ruc}\n");
                info.Add("Tipo Factura: Contado\n");
                info.Add($"Cond. Pago: {metodoPago}\n");
                info.Add($"Fecha: {DateTime.Now.ToString("dd'/'MM'/'yyyy")}\n");
                info.Add($"Hora: {DateTime.Now.ToString("HH:mm:ss")}\n");
                info.Add($"Transacción Nro°: {nroTicket}\n");
                info.Add("------------------------------------------------------------\n");
                doc.Add(info);

                // --- TABLA DEL TICKET (4 Columnas) ---
                PdfPTable tabla = new PdfPTable(4);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 2.5f, 4.7f, 1.7f, 1.1f });

                tabla.AddCell(new PdfPCell(new Phrase("CODIGO\nCANTIDAD", fuenteNegrita)) { Border = Rectangle.BOTTOM_BORDER, PaddingBottom = 4f });
                tabla.AddCell(new PdfPCell(new Phrase("DESCRIPCION ARTICULO\nPRECIO UNITARIO", fuenteNegrita)) { Border = Rectangle.BOTTOM_BORDER, PaddingBottom = 4f });
                tabla.AddCell(new PdfPCell(new Phrase("\nIMPORTE", fuenteNegrita)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 4f });
                tabla.AddCell(new PdfPCell(new Phrase("\n% IVA", fuenteNegrita)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 4f });

                DataTable dtAgrupado = detalles.Clone();
                Dictionary<string, DataRow> diccAgrupado = new Dictionary<string, DataRow>();

                foreach (DataRow fila in detalles.Rows)
                {
                    string clave = fila["Concepto"].ToString() + "|" + fila["PrecioUnitario"].ToString();
                    if (diccAgrupado.ContainsKey(clave))
                    {
                        diccAgrupado[clave]["Cantidad"] = Convert.ToInt32(diccAgrupado[clave]["Cantidad"]) + Convert.ToInt32(fila["Cantidad"]);
                        diccAgrupado[clave]["SubTotal"] = Convert.ToDecimal(diccAgrupado[clave]["SubTotal"]) + Convert.ToDecimal(fila["SubTotal"]);
                    }
                    else
                    {
                        DataRow nuevaFila = dtAgrupado.NewRow();
                        nuevaFila.ItemArray = fila.ItemArray.Clone() as object[];
                        diccAgrupado.Add(clave, nuevaFila);
                        dtAgrupado.Rows.Add(nuevaFila);
                    }
                }

                int totalItems = dtAgrupado.Rows.Count;
                int totalArticulos = 0;

                foreach (DataRow fila in dtAgrupado.Rows)
                {
                    int cantidadItem = Convert.ToInt32(fila["Cantidad"]);
                    decimal precioUnit = Convert.ToDecimal(fila["PrecioUnitario"]);
                    string subtotal = Convert.ToDecimal(fila["SubTotal"]).ToString("N0");
                    totalArticulos += cantidadItem;

                    string ivaProducto = fila.Table.Columns.Contains("IVA") && fila["IVA"] != DBNull.Value ? fila["IVA"].ToString() : "10";
                    if (!ivaProducto.EndsWith("%"))
                    {
                        ivaProducto += "%";
                    }

                    string codBarra = fila.Table.Columns.Contains("CodigoBarras") && fila["CodigoBarras"] != DBNull.Value ? fila["CodigoBarras"].ToString() : "0";

                    // FILA 1
                    tabla.AddCell(new PdfPCell(new Phrase(codBarra, fuenteNormal)) { Border = 0, PaddingTop = 4f });
                    tabla.AddCell(new PdfPCell(new Phrase(fila["Concepto"].ToString(), fuenteNormal)) { Border = 0, PaddingTop = 4f });
                    tabla.AddCell(new PdfPCell(new Phrase(subtotal, fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 4f });
                    tabla.AddCell(new PdfPCell(new Phrase(ivaProducto, fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 4f });

                    // FILA 2
                    string textoUnidad = cantidadItem == 1 ? "1 unidad" : $"{cantidadItem} unidades";

                    tabla.AddCell(new PdfPCell(new Phrase(textoUnidad, fuenteNormal)) { Border = 0 });
                    tabla.AddCell(new PdfPCell(new Phrase($"x Gs. {precioUnit.ToString("N0")}", fuenteNormal)) { Border = 0 });
                    tabla.AddCell(new PdfPCell(new Phrase("", fuenteNormal)) { Border = 0 });
                    tabla.AddCell(new PdfPCell(new Phrase("", fuenteNormal)) { Border = 0 });
                }

                doc.Add(tabla);
                doc.Add(new Paragraph("------------------------------------------------------------\n", fuenteNormal));

                // --- SECCIÓN DE TOTALES ---
                PdfPTable tablaTotales = new PdfPTable(2);
                tablaTotales.WidthPercentage = 100;
                tablaTotales.SetWidths(new float[] { 6f, 4f });

                tablaTotales.AddCell(new PdfPCell(new Phrase("TOTAL:", fuenteNegrita)) { Border = 0, PaddingBottom = 3f });
                tablaTotales.AddCell(new PdfPCell(new Phrase($"Gs. {total.ToString("N0")}", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 3f });

                if (metodoPago.ToUpper() == "EFECTIVO")
                {
                    tablaTotales.AddCell(new PdfPCell(new Phrase("EFECTIVO RECIBIDO:", fuenteNegrita)) { Border = 0, PaddingBottom = 3f });
                    tablaTotales.AddCell(new PdfPCell(new Phrase($"Gs. {montoRecibido.ToString("N0")}", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 3f });

                    tablaTotales.AddCell(new PdfPCell(new Phrase("SU VUELTO:", fuenteNegrita)) { Border = 0, PaddingBottom = 3f });
                    tablaTotales.AddCell(new PdfPCell(new Phrase($"Gs. {vuelto.ToString("N0")}", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 3f });
                }
                doc.Add(tablaTotales);

                doc.Add(new Paragraph("------------------------------------------------------------\n", fuenteNormal));

                // --- SECCIÓN DE LIQUIDACIÓN DE IVA ---
                decimal iva10 = Math.Round(total / 11);

                PdfPTable tablaIva = new PdfPTable(3);
                tablaIva.WidthPercentage = 100;
                tablaIva.SetWidths(new float[] { 4f, 3f, 3f });

                // Encabezados en negrita
                tablaIva.AddCell(new PdfPCell(new Phrase("SUB TOTALES", fuenteNegrita)) { Border = 0, PaddingBottom = 3f });
                tablaIva.AddCell(new PdfPCell(new Phrase("LIQUIDACION", fuenteNegrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 3f });
                tablaIva.AddCell(new PdfPCell(new Phrase("IVA", fuenteNegrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 3f });

                // Exentas
                tablaIva.AddCell(new PdfPCell(new Phrase("Exentas E   :", fuenteNormal)) { Border = 0 });
                tablaIva.AddCell(new PdfPCell(new Phrase("0", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                tablaIva.AddCell(new PdfPCell(new Phrase("0", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                // Gravado 5%
                tablaIva.AddCell(new PdfPCell(new Phrase("Gravado 5%  :", fuenteNormal)) { Border = 0 });
                tablaIva.AddCell(new PdfPCell(new Phrase("0", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                tablaIva.AddCell(new PdfPCell(new Phrase("0", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                // Gravado 10%
                tablaIva.AddCell(new PdfPCell(new Phrase("Gravado 10% :", fuenteNormal)) { Border = 0 });
                tablaIva.AddCell(new PdfPCell(new Phrase(total.ToString("N0"), fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                tablaIva.AddCell(new PdfPCell(new Phrase(iva10.ToString("N0"), fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                // Total Final IVA
                tablaIva.AddCell(new PdfPCell(new Phrase(" ", fuenteNormal)) { Border = 0 });
                tablaIva.AddCell(new PdfPCell(new Phrase("TOTAL:", fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                tablaIva.AddCell(new PdfPCell(new Phrase(iva10.ToString("N0"), fuenteNormal)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                doc.Add(tablaIva);

                Paragraph pie = new Paragraph();
                pie.Font = fuenteNormal;
                // Quitado el \n inicial de la siguiente línea
                pie.Add("------------------------------------------------------------\n");
                pie.Add($"Total ítems: {totalItems}\n");
                pie.Add($"Total artículos vendidos: {totalArticulos}\n");
                pie.Add($"Atendido por: {cajero}\n\n");
                doc.Add(pie);

                Paragraph saludo = new Paragraph("¡Gracias por su preferencia!\nGuarde este ticket como comprobante.", fuenteNormal);
                saludo.Alignment = Element.ALIGN_CENTER;
                doc.Add(saludo);

                doc.Close();
                Process.Start(rutaArchivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error al generar el PDF del Ticket: " + ex.Message);
            }
        }

        // ========================================================================
        // 2. MÉTODO PARA LA FACTURA LEGAL (Hoja A4)
        // ========================================================================
        public void GenerarFacturaLegalA4(DataTable detalles, decimal total, string cliente, string ci, string ruc, string correoCliente, string metodoPago, decimal montoRecibido, decimal vuelto, string cajero, string nroFactura)
        {
            Document doc = new Document(PageSize.A4, 40f, 40f, 40f, 40f);

            string rutaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string rutaArchivo = Path.Combine(rutaDescargas, $"Factura_Legal_{nroFactura}.pdf");

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                Font fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);
                Font fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.BLACK);
                Font fuenteTabla = FontFactory.GetFont(FontFactory.HELVETICA, 7, BaseColor.BLACK);
                Font fuenteTablaNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.BLACK);
                BaseColor colorBorde = BaseColor.BLACK;

                // --- CABECERA Y DATOS DEL CLIENTE ---
                PdfPTable tablaCabecera = new PdfPTable(3);
                tablaCabecera.WidthPercentage = 100;
                tablaCabecera.SetWidths(new float[] { 65f, 5f, 30f });

                PdfPCell celdaEmpresa = new PdfPCell();
                celdaEmpresa.Border = Rectangle.BOX;
                celdaEmpresa.BorderColor = colorBorde;
                celdaEmpresa.Padding = 10f;
                celdaEmpresa.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaEmpresa.AddElement(new Paragraph("ASUFIT GYM", fuenteTitulo) { Alignment = Element.ALIGN_CENTER });
                celdaEmpresa.AddElement(new Paragraph("Av. 123 esq. Calle 123", fuenteNormal) { Alignment = Element.ALIGN_CENTER });
                celdaEmpresa.AddElement(new Paragraph("Tel.: (021) xxx xxx", fuenteNormal) { Alignment = Element.ALIGN_CENTER });
                celdaEmpresa.AddElement(new Paragraph("www.asufitgym.com.py", fuenteNormal) { Alignment = Element.ALIGN_CENTER });
                tablaCabecera.AddCell(celdaEmpresa);

                PdfPCell celdaVacia = new PdfPCell(new Phrase(""));
                celdaVacia.Border = 0;
                tablaCabecera.AddCell(celdaVacia);

                PdfPCell celdaFiscal = new PdfPCell();
                celdaFiscal.Border = Rectangle.BOX;
                celdaFiscal.BorderColor = colorBorde;
                celdaFiscal.Padding = 8f;
                celdaFiscal.AddElement(new Paragraph("R.U.C.: xxxxxxxx-x", fuenteNormal));
                celdaFiscal.AddElement(new Paragraph("Timbrado N°: xxxxxxxx", fuenteNormal));
                celdaFiscal.AddElement(new Paragraph($"Fecha vigencia: xx/xx/xxxx", fuenteNormal));
                celdaFiscal.AddElement(new Paragraph("Factura electrónica", fuenteNormal));
                celdaFiscal.AddElement(new Paragraph($"N° xxx-xxx-xxxxxxx", fuenteNormal));
                tablaCabecera.AddCell(celdaFiscal);

                doc.Add(tablaCabecera);
                doc.Add(new Paragraph("\n"));

                PdfPTable tablaCliente = new PdfPTable(2);
                tablaCliente.WidthPercentage = 100;
                tablaCliente.SetWidths(new float[] { 40f, 60f });

                PdfPCell celdaClienteIzq = new PdfPCell();
                celdaClienteIzq.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                celdaClienteIzq.BorderColor = colorBorde;
                celdaClienteIzq.Padding = 8f;
                celdaClienteIzq.AddElement(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm")}", fuenteNormal));
                celdaClienteIzq.AddElement(new Paragraph($"Condición de venta: {metodoPago}", fuenteNormal));

                PdfPCell celdaClienteDer = new PdfPCell();
                celdaClienteDer.Border = Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                celdaClienteDer.BorderColor = colorBorde;
                celdaClienteDer.Padding = 8f;
                celdaClienteDer.AddElement(new Paragraph($"R.U.C.: {ruc}", fuenteNormal));
                celdaClienteDer.AddElement(new Paragraph($"Nombre o Razón Social: {cliente}", fuenteNormal));
                celdaClienteDer.AddElement(new Paragraph("Dirección: No especificada", fuenteNormal));
                celdaClienteDer.AddElement(new Paragraph("Tel.: 0972-910-196", fuenteNormal));
                celdaClienteDer.AddElement(new Paragraph($"Correo: {correoCliente}", fuenteNormal));

                tablaCliente.AddCell(celdaClienteIzq);
                tablaCliente.AddCell(celdaClienteDer);

                doc.Add(tablaCliente);
                doc.Add(new Paragraph("\n"));

                // --- DETALLES DE PRODUCTOS ---
                PdfPTable tablaDetalles = new PdfPTable(9);
                tablaDetalles.WidthPercentage = 100;
                tablaDetalles.SetWidths(new float[] { 7f, 6f, 25f, 11f, 11f, 13f, 10f, 8f, 9f });

                string[] encabezados = { "CANT.", "COD.", "DESCRIPCIÓN", "PRECIO\nUNITARIO", "PRECIO\nTOTAL", "DESCUENTO", "EXENTAS", "IVA 5%", "IVA 10%" };
                foreach (string texto in encabezados)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(texto, fuenteTablaNegrita));
                    celda.BorderColor = colorBorde;
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.Padding = 5f;
                    tablaDetalles.AddCell(celda);
                }

                foreach (DataRow fila in detalles.Rows)
                {
                    int cantidad = Convert.ToInt32(fila["Cantidad"]);
                    string codigoStr = fila.Table.Columns.Contains("CodigoBarras") && fila["CodigoBarras"] != DBNull.Value ? fila["CodigoBarras"].ToString() : "0";
                    decimal precio = Convert.ToDecimal(fila["PrecioUnitario"]);
                    decimal subtotal = Convert.ToDecimal(fila["SubTotal"]);

                    int bordesLaterales = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;

                    tablaDetalles.AddCell(new PdfPCell(new Phrase(cantidad.ToString(), fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_CENTER });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(codigoStr, fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_CENTER });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(fila["Concepto"].ToString(), fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(precio.ToString("N0"), fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(subtotal.ToString("N0"), fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase("0", fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase("0", fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase("0", fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(subtotal.ToString("N0"), fuenteTabla)) { Border = bordesLaterales, BorderColor = colorBorde, HorizontalAlignment = Element.ALIGN_RIGHT });
                }

                // --- FILAS DE TOTALES ---
                PdfPCell celdaSubTotalTitulo = new PdfPCell(new Phrase("SUBTOTAL", fuenteTablaNegrita));
                celdaSubTotalTitulo.Colspan = 8;
                celdaSubTotalTitulo.BorderColor = colorBorde;
                celdaSubTotalTitulo.BorderWidthTop = 1f;
                tablaDetalles.AddCell(celdaSubTotalTitulo);

                PdfPCell celdaSubTotalMonto = new PdfPCell(new Phrase(total.ToString("N0"), fuenteTabla));
                celdaSubTotalMonto.BorderColor = colorBorde;
                celdaSubTotalMonto.BorderWidthTop = 1f;
                celdaSubTotalMonto.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaDetalles.AddCell(celdaSubTotalMonto);

                string montoEnLetras = ConvertirMontoALetras(total);
                PdfPCell celdaTotalLetras = new PdfPCell(new Phrase($"TOTAL A PAGAR ({montoEnLetras} GUARANÍES)", fuenteTablaNegrita));
                celdaTotalLetras.Colspan = 8;
                celdaTotalLetras.BorderColor = colorBorde;
                tablaDetalles.AddCell(celdaTotalLetras);

                PdfPCell celdaTotalPagarMonto = new PdfPCell(new Phrase(total.ToString("N0"), fuenteTabla));
                celdaTotalPagarMonto.BorderColor = colorBorde;
                celdaTotalPagarMonto.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaDetalles.AddCell(celdaTotalPagarMonto);

                if (metodoPago.Equals("Efectivo", StringComparison.OrdinalIgnoreCase))
                {
                    PdfPCell celdaEfectivoTex = new PdfPCell(new Phrase("EFECTIVO RECIBIDO", fuenteTablaNegrita));
                    celdaEfectivoTex.Colspan = 8;
                    celdaEfectivoTex.BorderColor = colorBorde;
                    tablaDetalles.AddCell(celdaEfectivoTex);

                    PdfPCell celdaEfectivoMon = new PdfPCell(new Phrase(montoRecibido.ToString("N0"), fuenteTabla));
                    celdaEfectivoMon.BorderColor = colorBorde;
                    celdaEfectivoMon.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tablaDetalles.AddCell(celdaEfectivoMon);

                    PdfPCell celdaVueltoTex = new PdfPCell(new Phrase("VUELTO", fuenteTablaNegrita));
                    celdaVueltoTex.Colspan = 8;
                    celdaVueltoTex.BorderColor = colorBorde;
                    tablaDetalles.AddCell(celdaVueltoTex);

                    PdfPCell celdaVueltoMon = new PdfPCell(new Phrase(vuelto.ToString("N0"), fuenteTabla));
                    celdaVueltoMon.BorderColor = colorBorde;
                    celdaVueltoMon.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tablaDetalles.AddCell(celdaVueltoMon);
                }

                decimal iva10 = Math.Round(total / 11);
                PdfPCell celdaIva = new PdfPCell(new Phrase($"LIQUIDACIÓN DEL IVA                (5%): 0                (10%): {iva10.ToString("N0")}                TOTAL IVA: {iva10.ToString("N0")}", fuenteTablaNegrita));
                celdaIva.Colspan = 9;
                celdaIva.BorderColor = colorBorde;
                celdaIva.Padding = 5f;
                tablaDetalles.AddCell(celdaIva);

                doc.Add(tablaDetalles);

                doc.Close();
                Process.Start(rutaArchivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error al generar la Factura A4: " + ex.Message);
            }
        }

        // ========================================================================
        // 3. MÉTODO AUXILIAR: NÚMEROS A LETRAS
        // ========================================================================
        private string ConvertirMontoALetras(decimal numero)
        {
            if (numero == 0) return "CERO";
            long entero = Convert.ToInt64(Math.Truncate(numero));
            return NumeroALetras(entero).Trim().ToUpper();
        }

        private string NumeroALetras(long value)
        {
            string num2Text;
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UN";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = NumeroALetras((value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras(value / 100) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = NumeroALetras((value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras(value / 1000) + " MIL";
                if ((value % 1000) > 0) num2Text = num2Text + " " + NumeroALetras(value % 1000);
            }
            else if (value == 1000000) num2Text = "UN MILLON";
            else if (value < 2000000) num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras(value / 1000000) + " MILLONES ";
                if ((value - (value / 1000000) * 1000000) > 0) num2Text = num2Text + " " + NumeroALetras(value - (value / 1000000) * 1000000);
            }
            else num2Text = "NUMERO MUY GRANDE";

            return num2Text.Replace("  ", " ");
        }
    }
}