﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="fTransferencias.aspx.cs" Inherits="Finanzas.ControlPanel.Consultas.fTransferencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <div class="container" style="margin-left:100px;">
            <div class="modal-content" style="width:1150px;">
                <div class="modal-header">
                    <h1>Consulta De Transferencias</h1>
                </div>

                
                <div>
                    <table class="table-condensed">
                <tr>  
                <td>
                    <asp:TextBox ID="TbBuscar" class="form-control" runat="server" placeholder="Texto"></asp:TextBox>
                    </td>
                        <td>
                            <asp:DropDownList ID="TipoDropDownList" Class="form-control" runat="server" AutoPostBack="True">
                                <asp:ListItem>IdTransferencia</asp:ListItem>
                                <asp:ListItem>Fecha</asp:ListItem>
                                <asp:ListItem>IdCuenta Origen</asp:ListItem>
                                <asp:ListItem>IdCuenta Destino</asp:ListItem>
                                <asp:ListItem>Descripcion</asp:ListItem>
                                <asp:ListItem>Valor</asp:ListItem>


                            </asp:DropDownList>

                    </td>

                        <td>
                            <asp:TextBox ID="TbDesde" Class="form-control"  runat="server" TextMode="Date" Visible="False"></asp:TextBox>

                    </td>

                        <td>
                            <asp:Label ID="Lde" Class="form-control"  runat="server" Text="De" Visible="False"></asp:Label>

                    </td>

                        <td>
                            <asp:TextBox ID="TbHasta" Class="form-control"  runat="server" TextMode="Date" Visible="False"></asp:TextBox>

                    </td>

                        <td>
                        <asp:Button class="btn btn-default" ID="BtnConsulta" runat="server" Text="Buscar" OnClick="BtnConsulta_Click" />

                    </td>

                        <td>
                        <asp:Button class="btn btn-default" ID="BtnLimpiar" runat="server" Text="Limpiar" OnClick="BtnLimpiar_Click" />

                    </td>
                </tr>  
                    </table>
           
                    <asp:GridView ID="GridViewVista" class="scroll" runat="server" Width="90%" Height="100%" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="IdTransferencia" DataNavigateUrlFormatString="~/ControlPanel/Registros/rTransferencias.aspx?IdTransferencia={0}" Text="Editar" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>

                </div>

            </div>

        </div>
        <br /><br />

    </div>
</asp:Content>
