<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Historico.aspx.cs" Inherits="SistemaApoioAlocacaoRH.Historico" Culture="pt-BR" %>
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
                        <asp:Label ID="lblTitle" runat="server" Text="Histórico" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Acessar o histórico de evolução da competência" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-dashboard"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Histórico de evolução da competência" />
                        </li>
                    </ol>
                </div>                
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-long-arrow-right"></i>
                                <asp:Label ID="lblPaneltitle" runat="server" Text="Gráfico de Competências" />
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div id="divGrafico"></div>
                        </div>
                    </div>
                </div>
            </div>            
            <%--<hr />
            <div class="row">
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group text-center">
                        <asp:ValidationSummary ID="vldHistorico" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="14px" ForeColor="Red" />
                    </div>
                </div>
            </div>--%>
        </div>        
    </div>
    <script type="text/javascript">
        //$(function() {
        //    Morris.Bar({
        //        element: 'divGrafico',
        //        data: [
        //          { y: '25/08/2006', a: 1 },
        //          { y: '12/04/2007', a: 2 },
        //          { y: '18/09/2008', a: 3 },
        //          { y: '11/11/2010', a: 4 },
        //          { y: '14/12/2012', a: 5 },
        //          { y: '17/06/2013', a: 4 },
        //          { y: '21/09/2015', a: 5 },
        //        ],
        //        xkey: 'y',
        //        ykeys: ['a'],
        //        labels: ['IIS'],
        //        hideHover: true,
        //        resize: true
        //    });
        //});
    </script>
</asp:Content>
