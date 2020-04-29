<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaApoioAlocacaoRH.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .jumbotron {
            background-color: #ffffff;
        }
        .imgThumb {
            margin-top:15px;
        }

        .rowThumbs {
            margin-top:25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        <asp:Label ID="lblTitle" runat="server" Text="Home" />
                        <%--<small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Bem vindo ao SAARH!" />
                        </small>--%>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="jumbotron">
                    <div class="col-md-6 col-sm-12">
                        <asp:Image ID="imgLogoSaarh" runat="server" ImageUrl="~/Images/SAARHLogoHome.png" CssClass="img-responsive" style="margin-top:5%; margin-left:10%;" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <h1>
                            <asp:Label ID="lblBemVindo" runat="server" Text="Bem Vindo!" />
                        </h1>
                        <p>
                            <asp:Label ID="lblBemVindoTexto" runat="server"
                                Text="Seja bem vindo ao SAA-RH, um sistema que permite ao gerente de projetos selecionar de uma forma rápida e objetiva quais 
                                os recursos humanos que farão parte de uma equipe para um projeto de software." />
                        </p>
                    </div>
                </div>
            </div>
            <div class="row rowThumbs">
                <div class="col-md-4 col-sm-12">
                    <div class="thumbnail">
                        <asp:Image ID="imgEncontreRecursos" runat="server" ImageUrl="~/Images/encontrarRecursos.png" CssClass="imgThumb" />
                        <div class="caption">
                            <h3>
                                <asp:Label ID="lblEncontreRecursos" runat="server" Text="Encontre recursos" CssClass="text-center" />
                            </h3>
                            <p>
                                <asp:Label ID="lblEncontrarRecursosTexto" runat="server" CssClass="text-center"
                                    Text="O sistema permite encontrar facilmente recursos humanos que contenham determinada competência. " />
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="thumbnail">
                        <asp:Image ID="imgBuscarEquipe" runat="server" ImageUrl="~/Images/Buscarequipe.png" CssClass="imgThumb" />
                        <div class="caption">
                            <h3>
                                <asp:Label ID="lblSugestaoEquipe" runat="server" Text="Busque por equipes" />
                            </h3>
                            <p>
                                <asp:Label ID="lblSugestaoEquipeTexto" runat="server"
                                    Text="O sistema permite que seja encontrada a melhor equipe para um determinado projeto, identificando os melhores recursos de acordo com a necessidade." />
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="thumbnail">
                        <asp:Image ID="imgAvaliarEquipe" runat="server" ImageUrl="~/Images/AvaliarEquipe.png" CssClass="imgThumb" />
                        <div class="caption">
                            <h3>
                                <asp:Label ID="lblAvaliarEquipe" runat="server" Text="Avalie as equipes" />
                            </h3>
                            <p>
                                <asp:Label ID="lblAvaliarEquipeTexto" runat="server"
                                    Text="Quando o projeto termina o gerente deve realizar uma avaliação dos recursos, mantendo os dados sempre atualizados." />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </div>
</asp:Content>
