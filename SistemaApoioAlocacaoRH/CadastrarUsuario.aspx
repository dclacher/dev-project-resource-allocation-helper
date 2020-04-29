<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="CadastrarUsuario.aspx.cs" Inherits="SistemaApoioAlocacaoRH.CadastrarUsuario" %>
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
                        <asp:Label ID="lblTitle" runat="server" Text="Cadastrar Usuário" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Cadastrar novos usuários no sistema" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Cadastrar Usuário" />
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
                        <asp:Label ID="lblNomeUsuario" runat="server" Text="Nome de usuário" AssociatedControlID="txtNomeUsuario" />
                        <asp:TextBox ID="txtNomeUsuario" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvNomeUsuario" runat="server" ControlToValidate="txtNomeUsuario" ErrorMessage="Nome de usuário não pode ser vazio!" Display="None" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblNome" runat="server" Text="Nome" AssociatedControlID="txtNome" />
                        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome não pode ser vazio!" Display="None" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSobrenome" runat="server" Text="Sobrenome" AssociatedControlID="txtSobrenome" />
                        <asp:TextBox ID="txtSobrenome" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvSobrenome" runat="server" ControlToValidate="txtSobrenome" ErrorMessage="Sobrenome não pode ser vazio!" Display="None" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" Text="E-mail" AssociatedControlID="txtEmail" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rqvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="E-mail não pode ser vazio!" Display="None" />
                        <asp:RegularExpressionValidator ID="rgvEmailSintax" runat="server" ControlToValidate="txtEmail" ErrorMessage="O endereço de E-mail deve ser válido" 
                            ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$" Display="None" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo de usuário" AssociatedControlID="ddlTipo" />
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" />
                        <asp:RequiredFieldValidator ID="rqvTipo" runat="server" ControlToValidate="ddltipo" ErrorMessage="Tipo não pode ser vazio!" Display="None" />
                    </div>
                    <div class="form-group">
                        <!-- inputs falsos para prevenir alguns navegadores de colocar valores de senha armazenados em cookies (Chrome e Opera)-->
                        <input style="display:none;" type="text" name="fakeText" />
                        <input style="display:none;" type="password" name="fakeSenha" />

                        <asp:Label ID="lblSenha" runat="server" Text="Senha" AssociatedControlID="txtSenha" />
                        <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="tqvSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Senha não pode ser vazia!" Display="None" />
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
                        <asp:ValidationSummary ID="vldCadastrarUsuario" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="14px" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
