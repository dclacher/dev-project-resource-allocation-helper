<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="ConsultarPerfilPessoal.aspx.cs" Inherits="SistemaApoioAlocacaoRH.ConsultarPerfilPessoal" %>
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
                        <asp:Label ID="lblTitle" runat="server" Text="Consultar Perfil Pessoal" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Consultar perfil de competências de um funcionário" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-dashboard"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Dashboard" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Consultar Perfil Pessoal" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Buscar por nome, sobrenome ou nome de usuário" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Label ID="lblBusca" runat="server" Text="Buscar funcionário" AssociatedControlID="txtBusca" />
                        <asp:TextBox ID="txtBusca" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvBusca" runat="server" ControlToValidate="txtBusca" ErrorMessage="Esse campo não pode ser vazio!" Display="None" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="btn btn-primary" OnClick="btnLimpar_Click" />
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-mg-12">
                    <asp:GridView ID="grvGridCompetencias" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" 
                        AutoGenerateColumns="false" OnRowCommand="grvGridCompetencias_RowCommand" OnRowDataBound="grvGridCompetencias_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="comp_id" Visible="false" />
                            <asp:BoundField HeaderText="Nome" DataField="comp_nome" />
                            <asp:BoundField HeaderText="Descrição" DataField="proj_status" />
                            <asp:BoundField HeaderText="Prioridade" DataField="proj_prioridade" />
                            <asp:BoundField HeaderText="Data Início" DataField="proj_data_inicio" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Data Término" DataField="proj_data_termino" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditarProjeto" runat="server" CssClass="linkDisabled" Enabled="false" CommandName="alterarProjeto" ToolTip="Alterar projeto" 
                                        CommandArgument='<%#Eval("proj_id")%>'>
                                         <i class="fa fa-fw fa-edit"></i>                                         
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnVisualizarRecursos" runat="server" CommandName="verRecursos" ToolTip="Visualizar recursos do projeto"
                                        CommandArgument='<%#Eval("proj_id")%>'>
                                        <i class="fa fa-fw fa-users"></i> 
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
