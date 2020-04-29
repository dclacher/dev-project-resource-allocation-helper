<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="VisualizarHistorico.aspx.cs" Inherits="SistemaApoioAlocacaoRH.VisualizarHistorico" Culture="pt-BR" %>
<%@ MasterType VirtualPath="~/Master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:Label ID="lblTitle" runat="server" Text="Visualizar Histórico" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Visualizar a evolução das competências de um funcionário" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="evolução das competências de um funcionário" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Selecionar funcionário" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblNomeFuncionario" runat="server" Text="Nome do Funcionário" AssociatedControlID="ddlNomeFuncionario" />
                        <asp:DropDownList ID="ddlNomeFuncionario" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlNomeFuncionario_TextChanged" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-mg-12">
                    <asp:GridView ID="grvGridCompetencias" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true"
                        AutoGenerateColumns="false" OnRowCommand="grvGridCompetencias_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="CompID" Visible="false" />
                            <asp:BoundField HeaderText="Competência" DataField="CompNome" />                                    
                            <asp:BoundField HeaderText="CompNivel" DataField="CompNivel" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnHistoricoComp" runat="server" CommandName="HistoricoComp" ToolTip="Visualizar histórico da competência" CommandArgument='<%#Eval("CompID") + "~" + Eval("CompNome")%>'
                                        CausesValidation="false">
                                        <i class="fa fa-fw fa-signal"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>           
</asp:Content>
