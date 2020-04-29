<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="BuscarEquipe.aspx.cs" Inherits="SistemaApoioAlocacaoRH.BuscarEquipe" Culture="pt-BR" %>

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
                        <asp:Label ID="lblTitle" runat="server" Text="Buscar Sugestões de Equipe" />
                        <small>
                            <asp:Label ID="lblSubTitle" runat="server" Text="O sistema busca uma sugestão de equipe" />
                        </small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-home"></i>
                            <asp:HyperLink ID="lnkHome" runat="server" Text="Home" NavigateUrl="~/Default.aspx" />
                        </li>
                        <li class="active">
                            <i class="fa fa-file"></i>
                            <asp:Label ID="lblPageName" runat="server" Text="Buscar Sugestões de Equipe" />
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblLegend" runat="server" Text="Configure a Pesquisa" />
                    </h3>
                </div>
            </div>
            <asp:Panel ID="divConfiguraPesquisa" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblNomeGerente" runat="server" Text="Gerente do projeto" AssociatedControlID="txtNomeGerente" />
                            <asp:TextBox ID="txtNomeGerente" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblProjeto" runat="server" Text="Projeto" AssociatedControlID="ddlProjeto" />
                            <asp:DropDownList ID="ddlProjeto" runat="server" CssClass="form-control" DataTextField="Desc" DataValueField="Code"
                                AutoPostBack="true" OnTextChanged="ddlProjeto_TextChanged" />
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblNumeroRecursos" runat="server" Text="Número de recursos" AssociatedControlID="txtNumeroRecursos" />
                            <asp:TextBox ID="txtNumeroRecursos" runat="server" CssClass="form-control" TextMode="Number" min="1" />
                            <asp:RequiredFieldValidator ID="rqvNumeroRecursos" runat="server" ControlToValidate="txtNumeroRecursos"
                                ErrorMessage="O Número de recursos para este projeto não pode ser vazio!" ValidationGroup="ConfigPesquisa" Display="None" />
                        </div>
                    </div>
                    <hr />
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblCompetências" runat="server" Text="Competências do projeto" AssociatedControlID="grvCompetenciasProj" />
                            <asp:GridView ID="grvCompetenciasProj" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="false" AutoGenerateColumns="false"
                                OnRowCommand="grvCompetenciasProj_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="CompID" Visible="false" />
                                    <asp:BoundField HeaderText="Competência" DataField="CompNome" />
                                    <asp:BoundField HeaderText="Descrição" DataField="CompDesc" />
                                    <asp:BoundField HeaderText="Nivel da Competência" DataField="CompNivel" />
                                    <asp:TemplateField HeaderText="Ajustar Nível" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAjustarnivel" runat="server" CommandName="AjustarNivel" ToolTip="ajustar nível da competência" CommandArgument='<%#Eval("CompID") + "~" + Eval("CompNome")%>'
                                                CausesValidation="false">
                                                <i class="fa fa-fw fa-sort"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:CustomValidator ID="custNivelComp" runat="server" ErrorMessage="Todas as competências devem possuir níveis configurados maiores que zero!" Display="None"
                                OnServerValidate="custNivelComp_ServerValidate" ValidationGroup="ConfigPesquisa" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnAvançar" runat="server" Text="Avançar" CssClass="btn btn-primary" OnClick="btnAvançar_Click" ValidationGroup="ConfigPesquisa" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="divResultadoPesquisa" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <asp:Label ID="lblTipoPesquisa" runat="server" Text="Tipo de pesquisa" />
                        <div class="radio">
                            <asp:RadioButton ID="radMelhores" runat="server" GroupName="tipoPesquisa" Checked="true" AutoPostBack="true"
                                OnCheckedChanged="radMelhores_CheckedChanged" CausesValidation="false" Style="margin-left: 0px" />
                            <asp:Label ID="lblMelhores" runat="server" Text="Melhores" AssociatedControlID="radMelhores" />
                        </div>
                        <div class="radio">
                            <asp:RadioButton ID="radMelhorAjuste" runat="server" GroupName="tipoPesquisa" AutoPostBack="true"
                                OnCheckedChanged="radMelhorAjuste_CheckedChanged" Style="margin-left: 0px" />
                            <asp:Label ID="lblMelhorAjuste" runat="server" Text="Melhor ajuste" AssociatedControlID="radMelhorAjuste" />
                        </div>
                        <p class="help-block">
                            <em>
                                <asp:Label ID="lblTipoPesqDesc" runat="server" Text="Seleciona os melhores recursos de acordo com as competências" />
                            </em>
                        </p>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:GridView ID="grvResultadoPesquisa" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" AutoGenerateColumns="false"
                                OnRowCommand="grvResultadoPesquisa_RowCommand" OnRowDataBound="grvResultadoPesquisa_RowDataBound" DataKeyNames="UsuarioID">
                                <Columns>
                                    <asp:BoundField DataField="UsuarioID" Visible="false" />
                                    <asp:BoundField HeaderText="Nome" DataField="NomeUsuario" />
                                    <asp:BoundField HeaderText="Sobrenome" DataField="SobrenomeUsuario" />
                                    <asp:BoundField HeaderText="E-mail" DataField="Email" />
                                    <asp:BoundField HeaderText="Alocado" DataField="Alocado" />
                                    <asp:TemplateField HeaderText="Indicação" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkMarca" runat="server" ToolTip="Recurso comum" Enabled="false" Style="cursor: default">
                                                <i id="marcaIco" runat="server" class="fa fa-fw fa-thumbs-o-up"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Competências" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCompetenciaRecurso" runat="server" ToolTip="Visualizar Competências do Recurso" 
                                                CommandName="MostrarCompetencias" CommandArgument='<%#Eval("UsuarioID") + "~" + Eval("NomeUsuario") + " " + Eval("SobrenomeUsuario") %>'>
                                                <i class="fa fa-fw fa-bars"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Selecionar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelecionarRecurso" runat="server" ToolTip="Selecionar recurso" CausesValidation="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Classificação" DataField="classificacao" Visible="false" />                                    
                                </Columns>
                            </asp:GridView>
                            <asp:CustomValidator ID="custRecursos" runat="server" Display="None"
                                OnServerValidate="custRecursos_ServerValidate" ValidationGroup="ResultadoPesquisa" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-primary" OnClick="btnVoltar_Click" CausesValidation="false" />
                            <asp:Button ID="btnSelecionarEquipe" runat="server" Text="Selecionar" CssClass="btn btn-primary" OnClick="btnSelecionarEquipe_Click" ValidationGroup="ResultadoPesquisa" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="divSelecionados" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblEquipeSelecionada" runat="server" />
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:GridView ID="grvEquipeSelecionada" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" AutoGenerateColumns="false"
                                OnRowCommand="grvEquipeSelecionada_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="UsuarioID" Visible="false" />
                                    <asp:BoundField HeaderText="Nome" DataField="NomeUsuario" />
                                    <asp:BoundField HeaderText="Sobrenome" DataField="SobrenomeUsuario" />
                                    <asp:BoundField HeaderText="E-mail" DataField="Email" />
                                    <asp:BoundField HeaderText="Alocado" DataField="Alocado" />
                                    <asp:BoundField HeaderText="Data Início" DataField="DataInicio" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Data Término" DataField="DataTermino" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAjustarDatas" runat="server" CommandName="AjustarDatas" ToolTip="Ajustar datas de início e término do recurso " 
                                                CommandArgument='<%#Eval("UsuarioID") + "~" + Eval("NomeUsuario") + " " + Eval("SobrenomeUsuario") + "~" + Eval("DataInicio") + "~" + Eval("DataTermino") %>' CausesValidation="false">
                                                <i class="fa fa-fw fa-calendar"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>                            
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnVoltarResultadoPesquisa" runat="server" Text="Voltar" CssClass="btn btn-primary" OnClick="btnVoltarResultadoPesquisa_Click" CausesValidation="false" />
                            <asp:Button ID="btnConfirmarEquipe" runat="server" Text="Confirmar" CssClass="btn btn-primary" OnClick="btnConfirmarEquipe_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <hr />
            <div class="row">
                <div class="col-md-offset-3 col-md-6">
                    <div class="form-group text-center">
                        <asp:ValidationSummary ID="vldSelecionarEquipe" runat="server" ShowSummary="true" DisplayMode="BulletList" ValidationGroup="ConfigPesquisa" Font-Size="14px" ForeColor="Red" />
                        <asp:ValidationSummary ID="vldResultadoPesquisa" runat="server" ShowSummary="true" DisplayMode="BulletList" ValidationGroup="ResultadoPesquisa" Font-Size="14px" ForeColor="Red" />
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
    <div class="modal fade" id="modalCompetenciasRecurso" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header headerNoBorder">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title">
                        <asp:Label ID="Label1" runat="server" Text="Competências do Recurso" />
                    </h4>
                </div>
                <div class="modal-body">                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblNomeRecurso" runat="server" />
                            </div>                            
                            <asp:GridView ID="grvCompetenciaRecurso" runat="server" CssClass="table table-bordered table-hover table-responsive" AllowSorting="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CompID" Visible="false" />
                                    <asp:BoundField HeaderText="Competência" DataField="CompNome" />
                                    <asp:BoundField HeaderText="Nível" DataField="CompNivel" />                             
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer footerNoBorder">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <div class="modal fade" id="modalAjusteDatas" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header headerNoBorder">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title">
                        <asp:Label ID="Label2" runat="server" Text="Ajustar Datas" />
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label ID="lblNomeRecursoDatas" runat="server" />
                        <asp:HiddenField ID="hdnRecursoId" runat="server" />
                    </div>       
                    <div class="form-group">
                        <asp:Label ID="lblDataInicio" runat="server" Text="Data de início" AssociatedControlID="txtDataInicio" />
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" data-datepicker="true" />                        
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblDataTermino" runat="server" Text="Data de término" AssociatedControlID="txtDataTermino" />
                        <asp:TextBox ID="txtDataTermino" runat="server" CssClass="form-control" data-datepicker="true" />
                    </div>
                </div>
                <div class="modal-footer footerNoBorder">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    <asp:Button ID="btnAjustarData" runat="server" Text="Ajustar" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnAjustarData_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <div class="modal fade" id="modalAtualizarEquipe" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header headerNoBorder">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title">
                        <asp:Label ID="Label3" runat="server" Text="Atualizar Equipe" />
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblAtualizarEquipeMsg" runat="server" Text="Este projeto já possuí uma equipe! Você deseja atualizar os membros desta equipe?" />
                </div>
                <div class="modal-footer footerNoBorder">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnAtualizarEquipe" runat="server" Text="Atualizar" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnAtualizarEquipe_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=radMelhores.ClientID%>').click(function () {
                $('#<%=lblTipoPesqDesc.ClientID%>').text('Seleciona os melhores recursos de acordo com as competências');
            });
            $('#<%=radMelhorAjuste.ClientID%>').click(function () {
                $('#<%=lblTipoPesqDesc.ClientID%>').text('Seleciona os recursos que estão mais próximos aos níves de competências selecionados');
            });
        });

        function ShowModal() {
            $("#modalAjusteNível").modal('show');
        }

        function ShowCompetenciaModal() {
            $("#modalCompetenciasRecurso").modal('show');
        }

        function ShowDatasModal() {
            $("#modalAjusteDatas").modal('show');
        }

        function ShowConfirmarModal() {
            $('#modalAtualizarEquipe').modal('show');
        }

    </script>
</asp:Content>
