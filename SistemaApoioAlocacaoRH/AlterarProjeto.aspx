<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AlterarProjeto.aspx.cs" Inherits="SistemaApoioAlocacaoRH.AlterarProjeto" Culture="pt-BR" %>
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
                        <asp:Label ID="lblTitle" runat="server" Text="Alterar Projeto" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Alterar um projeto existente no sistema " />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Alterar Projeto" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Preencha o Formulário" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblNomeGerente" runat="server" Text="Gerente do projeto" AssociatedControlID="txtNomeGerente" />
                        <asp:TextBox ID="txtNomeGerente" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblNomeProjeto" runat="server" Text="Nome do projeto" AssociatedControlID="txtNomeProjeto" />
                        <asp:TextBox ID="txtNomeProjeto" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvNomeProjeto" runat="server" ControlToValidate="txtNomeProjeto" ErrorMessage="Nome do projeto não pode ser vazio!" 
                            Display="None" EnableClientScript="false" />
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblDescProjeto" runat="server" Text="Descrição do projeto" AssociatedControlID="txtDescProjeto" />
                        <asp:TextBox ID="txtDescProjeto" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" MaxLength="200" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblDataInicio" runat="server" Text="Data de início" AssociatedControlID="txtDataInicio" />
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" data-datepicker="true" />
                        <asp:RequiredFieldValidator ID="rqvDataInicio" runat="server" ControlToValidate="txtDataInicio" 
                            ErrorMessage="Data de Início não pode ser vazia!" Display="None" EnableClientScript="false" />
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblDataTermino" runat="server" Text="Data de término" AssociatedControlID="txtDataTermino" />
                        <asp:TextBox ID="txtDataTermino" runat="server" CssClass="form-control" data-datepicker="true" />
                        <asp:RequiredFieldValidator ID="rqvDataTermino" runat="server" ControlToValidate="txtDataTermino" ErrorMessage="Data de Término não pode ser vazia!" 
                            Display="None" EnableClientScript="false" /> 
                        <asp:CompareValidator ID="cvDataTermino" runat="server" ControlToCompare="txtDataInicio" CultureInvariantValues="true" Display="None" EnableClientScript="false"
                            ControlToValidate="txtDataTermino" ErrorMessage="A data de inicio não pode ser maior ou igual a data de término!" Type="Date" Operator="GreaterThanEqual"  />                                                    
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
                        <asp:RequiredFieldValidator ID="rqvStatusProjeto" runat="server" ControlToValidate="ddlStatusProjeto" ErrorMessage="Status do Projeto não pode ser vazio!" 
                            Display="None" EnableClientScript="false" />
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
                        <asp:RequiredFieldValidator ID="rqvPrioridadeProjeto" runat="server" ControlToValidate="ddlPrioridadeProjeto" ErrorMessage="Prioridade do Projeto não pode ser vazio!" 
                            Display="None" EnableClientScript="false" />
                    </div>
                </div>               
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblConhecimentos" runat="server" Text="Conhecimentos necessários" AssociatedControlID="ddlConhecimentos" />
                        <asp:DropDownList ID="ddlConhecimentos" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlConhecimentos_TextChanged"/>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblHabilidades" runat="server" Text="Habilidades necessárias" AssociatedControlID="ddlHabilidades" />
                        <asp:DropDownList ID="ddlHabilidades" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlHabilidades_TextChanged" /> 
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblAtitudes" runat="server" Text="Atitudes necessárias" AssociatedControlID="ddlAtitudes" />
                        <asp:DropDownList ID="ddlAtitudes" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlAtitudes_TextChanged" /> 
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblSelecionados" runat="server" Text="Competências selecionadas" AssociatedControlID="grvSelecionados" />                        
                        <asp:GridView ID="grvSelecionados" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="false" AutoGenerateColumns="false"
                            OnRowCommand="grvSelecionados_RowCommand" EmptyDataText="Não há filtros selecionados!" EmptyDataRowStyle-ForeColor="#a94442" EmptyDataRowStyle-HorizontalAlign="Center" 
                            ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="Code" Visible="false" />
                                <asp:BoundField HeaderText="Competências" DataField="Desc" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRemoverComp" runat="server" CommandName="RemoverComp" ToolTip="Remover Competência" CommandArgument='<%#Eval("Code")%>' 
                                            CausesValidation="false">
                                         <i class="fa fa-fw fa-times"></i>                                         
                                    </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>
                        </asp:GridView>                        
                        <asp:CustomValidator ID="custValiSelecionados" runat="server" ErrorMessage="Um projeto deve conter pelo menos uma competência!" Display="None" 
                            OnServerValidate="custValiSelecionados_ServerValidate"/>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnLimparSelecionados" runat="server" Text="Limpar competências" CssClass="btn btn-primary" OnClick="btnLimparSelecionados_Click" CausesValidation="false" />                        
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="btn btn-primary" OnClick="btnLimpar_Click" CausesValidation="false" />
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                    </div>
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
    </div>
</asp:Content>
