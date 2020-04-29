<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="True" CodeBehind="ConsultarRecursos.aspx.cs" Inherits="SistemaApoioAlocacaoRH.ConsultarRecursos" %>

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
                        <asp:Label ID="lblTitle" runat="server" Text="Consultar Recursos" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Consultar recursos humanos através de competências" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Consultar Recursos" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Selecionar Filtros" />
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Label ID="lblConhecimentos" runat="server" Text="Conhecimentos" AssociatedControlID="ddlConhecimentos" />
                        <asp:DropDownList ID="ddlConhecimentos" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlConhecimento_TextChanged" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblHabilidade" runat="server" Text="Habilidades" AssociatedControlID="ddlHabilidades" />
                        <asp:DropDownList ID="ddlHabilidades" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlHabilidades_TextChanged" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblAtitudes" runat="server" Text="Atitudes" AssociatedControlID="ddlAtitudes" />
                        <asp:DropDownList ID="ddlAtitudes" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlAtitudes_TextChanged" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Label ID="lblSelecionados" runat="server" Text="Filtros selecionados" AssociatedControlID="grvSelecionados" />
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
                        <p class="help-block">
                            <em>
                                <asp:Label ID="lblLimitacao" runat="server" Text="É possível selecionar no máximo 10 tipo de competência como filtro para a busca." />
                            </em>                            
                        </p>
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
                <div class="col-lg-12">
                    <asp:GridView ID="grvGridResultado" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="IdUsuario" Visible="false" />
                            <asp:BoundField HeaderText="Nome" DataField="Nome" />
                            <asp:BoundField HeaderText="SobreNome" DataField="SobreNome" />
                            <asp:BoundField DataField="Nivel0" Visible="false"/>
                            <asp:BoundField DataField="Nivel1"  Visible="false" />
                            <asp:BoundField DataField="Nivel2"  Visible="false" />
                            <asp:BoundField DataField="Nivel3"  Visible="false"/>
                            <asp:BoundField DataField="Nivel4"  Visible="false" />
                            <asp:BoundField DataField="Nivel5"  Visible="false"/>
                            <asp:BoundField DataField="Nivel6"  Visible="false"/>
                            <asp:BoundField DataField="Nivel7"  Visible="false"/>
                            <asp:BoundField DataField="Nivel8"  Visible="false"/>
                            <asp:BoundField DataField="Nivel9"  Visible="false"/>                            
                        </Columns>
                    </asp:GridView>
                </div>
            </div>            
        </div>
    </div>  

</asp:Content>
