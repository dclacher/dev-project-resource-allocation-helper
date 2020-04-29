<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="ConsultarCompetencias.aspx.cs" Inherits="SistemaApoioAlocacaoRH.ConsultarCompetencias" Culture="pt-BR" %>
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
                        <asp:Label ID="lblTitle" runat="server" Text="Consultar Competências" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Consultar as competências já existentes" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Consultar Competências" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Selecione o tipo da competência" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblTipoCompetencia" runat="server" Text="Tipo Competencia" AssociatedControlID="ddlTipoCompetencia" />
                        <asp:DropDownList ID="ddlTipoCompetencia" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code"
                            OnTextChanged="ddlTipoCompetencia_TextChanged" AutoPostBack="true" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="grvCompetencias" runat="server" CssClass="table table-bordered table-hover table-responsive" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="CompID" Visible="false" />
                            <asp:BoundField HeaderText="Nome" DataField="CompNome" />
                            <asp:BoundField HeaderText="Descrição" DataField="CompDesc" />
                            <asp:BoundField HeaderText="Tipo" DataField="CompTipo" />
                        </Columns>
                    </asp:GridView>                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
