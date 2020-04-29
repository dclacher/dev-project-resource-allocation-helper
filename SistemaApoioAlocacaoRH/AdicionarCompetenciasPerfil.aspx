<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AdicionarCompetenciasPerfil.aspx.cs" Inherits="SistemaApoioAlocacaoRH.AdicionarCompetenciaPerfil" %>

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
                        <asp:Label ID="lblTitle" runat="server" Text="Adicionar Competências" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="Acessar perfil pessoal para adicionar competências" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Acessar perfil pessoal para adicionar competências" />
                        </li>
                    </ol>
                </div>                
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Perfil atual" />
                    </h3>
                </div>
            </div>
            <asp:Panel ID="divPerfilAtual" runat="server">
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblNomeFuncionario" runat="server" Text="Nome" AssociatedControlID="txtNomeFuncionario" />
                            <asp:TextBox ID="txtNomeFuncionario" runat="server" CssClass="form-control" Enabled="false" />
                            <asp:DropDownList ID="ddlNomeFuncionario" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code" AutoPostBack="true" OnTextChanged="ddlNomeFuncionario_TextChanged" Visible="false" />
                        </div>
                    </div>                                        
                </div>
                <div class="row">                    
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblConhecimentos" runat="server" Text="Conhecimentos" AssociatedControlID="ddlConhecimentos" />
                            <asp:DropDownList ID="ddlConhecimentos" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code"
                                AutoPostBack="true" OnTextChanged="ddlConhecimentos_TextChanged" />
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblHabilidades" runat="server" Text="Habilidades" AssociatedControlID="ddlHabilidades" />
                            <asp:DropDownList ID="ddlHabilidades" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code"
                                AutoPostBack="true" OnTextChanged="ddlHabilidades_TextChanged" />
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblAtitudes" runat="server" Text="Atitudes" AssociatedControlID="ddlAtitudes" Visible="false" />
                            <asp:DropDownList ID="ddlAtitudes" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code"
                                AutoPostBack="true" OnTextChanged="ddlAtitudes_TextChanged" Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblCompFuncionario" runat="server" Text="Suas competências" AssociatedControlID="grvCompFuncionario" />
                            <asp:GridView ID="grvCompFuncionario" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="false" AutoGenerateColumns="false"
                                OnRowCommand="grvCompFuncionario_RowCommand" OnRowDataBound="grvCompFuncionario_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CompID" Visible="false" />
                                    <asp:BoundField HeaderText="Competência" DataField="CompNome" />                                    
                                    <asp:BoundField HeaderText="CompNivel" DataField="CompNivel" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAjustarnivel" runat="server" CommandName="AjustarNivel" ToolTip="ajustar nível da competência" CommandArgument='<%#Eval("CompID") + "~" + Eval("CompNome")%>'
                                                CausesValidation="false">
                                                <i id="iconNivel" runat="server" class="fa fa-fw fa-sort"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:CustomValidator ID="custCompFuncionario" runat="server" ErrorMessage="Todas as competências devem possuir níveis configurados maiores que zero!" Display="None"
                                OnServerValidate="custCompFuncionario_ServerValidate" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnLimparDados" runat="server" Text="Limpar" CssClass="btn btn-primary" OnClick="btnLimparDados_Click" CausesValidation="false" />
                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" CausesValidation="true"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="divHistorico" runat="server" Visible="false">
                
            </asp:Panel>
            <hr />
            <div class="row">
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group text-center">
                        <asp:ValidationSummary ID="vldPerfil" runat="server" ShowSummary="true" DisplayMode="BulletList" Font-Size="14px" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>        
    </div>
    <div class="modal fade" id="modalAjusteNível" role="dialog">
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
    <div class="modal fade" id="modalConfirmar" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header headerNoBorder">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblTituloConfirmarPerfil" runat="server" Text="Confirmar Perfil" />
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblConfirmarPerfilMsg" runat="server" Text="Confirma a inclusão da(s) nova(s) competência(s)?" />
                </div>
                <div class="modal-footer footerNoBorder">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarPerfil" runat="server" Text="Atualizar" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnConfirmarPerfil_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        function ShowModalNivel() {
            $("#modalAjusteNível").modal('show');
        }

        function ShowModalConfirmar() {
            $("#modalConfirmar").modal('show');
        }
    </script>
</asp:Content>
