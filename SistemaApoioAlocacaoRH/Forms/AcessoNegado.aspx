<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcessoNegado.aspx.cs" Inherits="SistemaApoioAlocacaoRH.Forms.AcessoNegado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
     
    <title>Acesso Negado!!!</title>

    <style type="text/css">
        .acessoNegado{
            font-family:Helvetica, Arial, sans-serif;
            background-repeat:no-repeat;
            background-image: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#D2E9FF), to(#FFFFFF));
	        background-image: -webkit-linear-gradient(top, #D2E9FF, #FFFFFF);								/* Safari 5.1+, Mobile Safari, Chrome 10+ */
	        background-image:    -moz-linear-gradient(top, #D2E9FF, #FFFFFF);								/* Firefox 3.6+ */
	        background-image:      -o-linear-gradient(top, #D2E9FF, #FFFFFF);								/* Opera 11.10+ */
	        background-image:     -ms-linear-gradient(top, #D2E9FF, #FFFFFF);
            background-color:#fff;
        }

        .acessoNegadoContainer{
            height:100%;
            margin-top:150px;
        }

        h1{
            font-size:50px;
        }
    </style>
</head>
<body class="acessoNegado">
    <form id="form1" runat="server">
    <div class="container-fluid acessoNegadoContainer">
       <div class="row">
           <div class="col-xs-12 text-center">
               <asp:Label ID="lblExclamationIcon" runat="server" CssClass="glyphicon glyphicon-exclamation-sign" Font-Size="200" />
           </div>
           <div class="col-xs-12 text-center">
               <h1>
                    <asp:Label ID="lblAccessDenied" runat="server" Text="Acesso Negado!"/>
               </h1>
               <asp:Label ID="lblMessage" runat="server" Text="Você não possuí permissão necessária passa acessar esta página!" />
               <br/>
               <asp:Label ID="lblMessage2" runat="server" Text="Em caso de dúvidas consulte o Administrador do sistema ou retorne para a" />
               <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />  
           </div>
       </div> 
    </div>
    </form>
</body>
</html>
