<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AvaliarEquipe.aspx.cs" Inherits="SistemaApoioAlocacaoRH.AvaliarEquipe" %>

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
                        <asp:Label ID="lblTitle" runat="server" Text="Avaliar Equipe" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Avaliar a equipe de um projeto" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Avaliar Equipe" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Selecionar Projeto" />
                    </h3>
                </div>
            </div>



            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblNomeGerente" runat="server" Text="Gerente do projeto" AssociatedControlID="txtNomeGerente" />
                        <asp:TextBox ID="txtNomeGerente" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblProjeto" runat="server" Text="Projeto" AssociatedControlID="ddlProjeto" />
                        <asp:DropDownList ID="ddlProjeto" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code"
                            AutoPostBack="true" OnTextChanged="ddlProjeto_TextChanged" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblNomeFuncionario" runat="server" Text="Nome do Funcionário" AssociatedControlID="ddlNomeFuncionario" Visible="false" />
                        <asp:DropDownList ID="ddlNomeFuncionario" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" Visible="false" OnTextChanged="ddlNomeFuncionario_TextChanged" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-mg-12">
                    <asp:GridView ID="grvGridCompetencias" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true"
                        AutoGenerateColumns="false" OnRowCommand="grvGridCompetencias_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="compID" Visible="false" />
                            <asp:BoundField HeaderText="Nome" DataField="compNome" />
                            <asp:BoundField HeaderText="Descrição" DataField="compDesc" />
                            <asp:BoundField HeaderText="Nível" DataField="compNivel" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAjustarnivel" runat="server" CommandName="AjustarNivel" ToolTip="ajustar nível da competência" CommandArgument='<%#Eval("CompID") + "~" + Eval("CompNome")%>'
                                        CausesValidation="false">
                                                <i class="fa fa-fw fa-sort"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnSalvar" runat="server" Text="Atualizar este Funcionário" CssClass="btn btn-primary" OnClick="btnSalvar_Click" Visible="false" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group text-center">
                        <asp:ValidationSummary ID="vldAtualizarCompetencias" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="14px" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnFinalizarAvaliacao" runat="server" Text="Finalizar Avaliação da Equipe" CssClass="btn btn-primary" OnClick="btnFinalizarAvaliacao_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalAjusteNivel" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header headerNoBorder">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblModalTitle" runat="server" Text="Ajustar Nível" />
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:Label ID="lblNomeComp" runat="server" Text="Competência" AssociatedControlID="txtNomeComp" />
                            <asp:TextBox ID="txtNomeComp" runat="server" CssClass="form-control" Enabled="false" />
                            <asp:HiddenField ID="hdnCompID" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblNivel" runat="server" Text="Nível" AssociatedControlID="txtNivelValor" />
                            <asp:TextBox ID="txtNivelValor" runat="server" CssClass="form-control" TextMode="Number" min="1" max="5" />
                        </div>
                    </div>
                    <div class="modal-footer footerNoBorder">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                        <asp:Button ID="btnAjustarNivel" runat="server" Text="Ajustar" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnAjustarNivel_Click" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    <div class="modal fade" id="modalConfirmaFinalizar" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header headerNoBorder">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblConfirmaFinalizar" runat="server" Text="Finalizar Avaliação" />
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblConfirmaFinalizarMsg" runat="server" />
                </div>
                <div class="modal-footer footerNoBorder">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmaFinalizar" runat="server" Text="Finalizar" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnConfirmaFinalizar_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

        <script type="text/javascript">
            function ShowModal() {
                $("#modalAjusteNivel").modal('show');
            }
            function ShowConfirmarModal() {
                $('#modalConfirmaFinalizar').modal('show');
            }
        </script>
    </div>
</asp:Content>
