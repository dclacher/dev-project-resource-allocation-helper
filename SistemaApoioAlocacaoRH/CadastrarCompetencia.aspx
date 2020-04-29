<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="CadastrarCompetencia.aspx.cs"
     Inherits="SistemaApoioAlocacaoRH.CadastrarCompetencia" Culture="pt-BR" %>
<%@ MasterType VirtualPath="~/Master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-md-12">
                    <h1 class="page-header">
                        <asp:Label ID="lblTitle" runat="server" Text="Cadastrar Competência" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Cadastrar novos conhecimentos, habilidades e atitudes no sistema" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Cadastrar Competência" />
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
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo de competência" AssociatedControlID="ddlTipo" />
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" />
                        <asp:RequiredFieldValidator ID="rqvTipo" runat="server" ControlToValidate="ddltipo" ErrorMessage="Tipo não pode ser vazio!" Display="None" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblNome" runat="server" Text="Nome" AssociatedControlID="txtNome" />
                        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome não pode ser vazio!" Display="None" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição" AssociatedControlID="txtDescricao" />
                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control" />
                    </div>                    
                    <div class="form-group pull-right">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group text-center">
                        <asp:ValidationSummary ID="vldCadastrarCompetencia" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="14px" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
