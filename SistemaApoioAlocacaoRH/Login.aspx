<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SistemaApoioAlocacaoRH.Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>SAA-RH - Login</title>
    <!-- Bootstrap Core CSS -->
    <link href="~/CSS/bootstrap.css" rel="stylesheet" type="text/css" />

    <!-- Custom CSS -->
    <link href="~/CSS/sb-admin.css" rel="stylesheet" type="text/css" />

    <!-- Custom Fonts -->
    <link href="~/font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery Version 1.11.0 -->
    <script src="Scripts/jquery-1.11.0.js" type="text/javascript"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

    <style type="text/css">
        .checkbox input[type="checkbox"] {
            margin-left: 0;
        }

        .imgContainer{            
            overflow-x:hidden;
            overflow-y:hidden;
            position:static;
        }
        .navbar-brand{
            padding:0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" role="form" defaultbutton="btnPopupLogin">
        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner">
                <div class="container">
                    <div class="row">
                        <div class="col-md-11 col-xs-12">
                            <asp:Image ID="imgBrand" runat="server" ImageUrl="~/Images/LogoSAARH.png" />
                        </div>
                        <div class="col-md-1 col-xs-12">
                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary navbar-btn" Text="Login" OnClick="btnLogin_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="imgContainer">
            <asp:Image ID="ImgTeamBanner" runat="server" CssClass="img-responsive" ImageUrl="~/Images/TeamworkBanner.jpg" />
        </div>        
        <footer class="loginFooter navbar navbar-fixed-bottom">
            <asp:Label ID="lblDesc" runat="server" Text="Uma maneira ágil e simples de montar sua equipe para projetos de software!" />
        </footer>

        <div class="modal fade" id="modalLogin" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-vertical-centered-login modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblSignIn" runat="server" Text="SAA-RH Login" />
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:Label ID="lblUserName" runat="server" CssClass="control-label" Text="Nome de Usuário:" />
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rqvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="Nome de usuário não pode ser vazio!" Display="None" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" CssClass="control-label" Text="Senha:" />
                            <asp:TextBox ID="txtPasword" runat="server" CssClass="form-control" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="rqvPassword" runat="server" ControlToValidate="txtPasword" ErrorMessage="Senha não pode ser vazia!" Display="None" />
                        </div>
                        <div class="form-group">
                            <div class="checkbox">
                                <asp:CheckBox ID="chkRememberMe" runat="server" Text="Lembrar-me" Visible="false" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblLoginFail" runat="server" Text="Nome de usuário ou senha inválidos!" Visible="false" ForeColor="Red" />
                            <asp:ValidationSummary ID="vldLogin" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="Smaller" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnPopupLogin" runat="server" CssClass="btn btn-primary" Text="Entrar" OnClick="btnPopupLogin_Click" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

        <div class="modal fade" id="modalErro" role="dialog">
            <div class="modal-dialog modal-vertical-centered">
                <div class="modal-content error">
                    <div class="modal-header headerError">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblErro" runat="server" Text="Erro!" />
                        </h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblErroMsg" runat="server" />
                    </div>
                    <div class="modal-footer footerError">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <script type="text/javascript">
            function showLoginPopup() {
                // backdrop 'static' previne que a popup feche quando clicado fora dela
                $('#modalLogin').modal({ backdrop: 'static' })
                $("#modalLogin").modal('show');
            }

            function showError() {
                $("#modalErro").modal('show');
            }
        </script>
    </form>
</body>
</html>
