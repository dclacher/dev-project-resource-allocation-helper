<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="ConsultarProjetos.aspx.cs"
    Inherits="SistemaApoioAlocacaoRH.ConsultarProjetos" Culture="pt-BR" %>

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
                        <asp:Label ID="lblTitle" runat="server" Text="Consultar Projeto" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Consultar os projetos existentes" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Cadastrar Projeto" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Selecionar Filtros" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblNomeProjeto" runat="server" Text="Nome do projeto" AssociatedControlID="txtNomeProjeto" />
                        <asp:TextBox ID="txtNomeProjeto" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblNomeGerente" runat="server" Text="Gerente do projeto" AssociatedControlID="ddlNomeGerente" />
                        <asp:DropDownList ID="ddlNomeGerente" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblStatusProjeto" runat="server" Text="Status do projeto" AssociatedControlID="ddlStatusProjeto" />
                        <asp:DropDownList ID="ddlStatusProjeto" runat="server" CssClass="form-control">
                            <asp:ListItem Text="" Value="" />
                            <asp:ListItem Text="Ativo" Value="Ativo" />
                            <asp:ListItem Text="Parado" Value="Parado" />
                            <asp:ListItem Text="Encerrado" Value="Encerrado" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblPrioridade" runat="server" Text="Prioridade do projeto" AssociatedControlID="ddlPrioridadeProjeto" />
                        <asp:DropDownList ID="ddlPrioridadeProjeto" runat="server" CssClass="form-control">
                            <asp:ListItem Text="" Value="" />
                            <asp:ListItem Text="Baixa" Value="Baixa" />
                            <asp:ListItem Text="Média" Value="Media" />
                            <asp:ListItem Text="Alta" Value="Alta" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblDataInicio" runat="server" Text="Data de início" AssociatedControlID="txtDataInicio" />
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" data-datepicker="true" />
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblDataTermino" runat="server" Text="Data de término" AssociatedControlID="txtDataTermino" />
                        <asp:TextBox ID="txtDataTermino" runat="server" CssClass="form-control" data-datepicker="true" />
                        <asp:CompareValidator ID="cvDataTermino" runat="server" ControlToCompare="txtDataInicio" CultureInvariantValues="true" Display="None" EnableClientScript="true"
                            ControlToValidate="txtDataTermino" ErrorMessage="A data de inicio não pode ser maior ou igual a data de término!" Type="Date" Operator="GreaterThanEqual" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="btn btn-primary" OnClick="btnLimpar_Click" CausesValidation="false" />
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-mg-12">
                    <asp:GridView ID="grvGridProjetos" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" 
                        AutoGenerateColumns="false" OnRowCommand="grvGridProjetos_RowCommand" OnRowDataBound="grvGridProjetos_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="proj_id" Visible="false" />
                            <asp:BoundField HeaderText="Nome" DataField="proj_nome" />
                            <asp:BoundField HeaderText="Status" DataField="proj_status" />
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
            <hr />
            <div class="row">
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group text-center">
                        <asp:ValidationSummary ID="vldCadastrarProjeto" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="14px" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
        <div class="modal fade" id="modalRecursos" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header headerNoBorder">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblModalTitle" runat="server" Text="Recursos do projeto" />
                        </h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblPopupMsg" runat="server" Text="Este projeto não possui recursos alocados!" Visible="false" />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grvRecursos" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
                                        <asp:TemplateField HeaderText="Nome Completo">
                                            <ItemTemplate>
                                                <%#Eval("Nome") + " " + Eval("Sobrenome") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Inicio" DataField="DataInicio" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField HeaderText="Término" DataField="DataTermino" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField HeaderText="Email" DataField="Email" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer footerNoBorder">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>

    <script type="text/javascript">
        function ShowModal() {
            $("#modalRecursos").modal('show');
        }
    </script>
</asp:Content>
