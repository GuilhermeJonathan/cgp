(function (campeonatoGeral, $) {
    "use strict";

    campeonatoGeral.AdicionarMensagemDeSucesso = function (mensagem) {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right"
        };

        toastr.success(mensagem, "Sucesso");
    };

    campeonatoGeral.AdicionarMensagemDeErro = function (mensagem) {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right"
        };
        toastr.error(mensagem, "OPS! Algo deu errado");
    };

    campeonatoGeral.BloquearTela = function () {
        
        $.blockUI({ message: "<div style='color: white'> <i class='fa fa-spinner fa-pulse fa-3x'></i> </div>", css: { border: '0px', backgroundColor: 'none' } });

        $(".blockOverlay").css("z-index", "2001");
        $(".blockUI").css("z-index", "2002");
    };

    campeonatoGeral.DesbloquearTela = function () {
        $.unblockUI();
    };

    campeonatoGeral.ajaxGet = function (url, params, sucesso, falha) {
        if (!params)
            return;

        $.get(url, params, function (resultado) {
            if (sucesso) {
                sucesso(resultado.retorno);
            }
        }).fail(function (erro) {
            if (falha) {
                falha();
            }
        }).always(function () {
            campeonatoGeral.DesbloquearTela();
        });
    };

    campeonatoGeral.preencherCombo = function atualizarCombo($combo, valores, aoPreencher, valorSelecionado) {
        $combo.empty();
        $combo.append("<option value=''> Selecione </option>");

        if (aoPreencher)
            aoPreencher();

        if (!valores || valores.length <= 0)
            return;

        jQuery.each(valores, function (i, valor) {
            var opcao = jQuery('<option>').text(valor.Nome).attr('value', valor.Valor);

            if (valorSelecionado && valorSelecionado.toString() === valor.Valor.toString())
                opcao.attr("selected", true);

            $combo.append(opcao);
        });
    };

    $(function () {
        $('.autoComplete').select2();
        $('.data').mask('00/00/0000');

        $('.data').datepicker({
            language: "pt-BR", autoclose: true,
            zIndexOffset: 99999999
        });

        $('.mascaraData').datepicker({
            language: "pt-BR", autoclose: true,
            zIndexOffset: 99999999
        });

        $(".voltar").on("click", function () {
            history.back();
        });

        $("a:not(.no-load)").on("click", function () {
            campeonatoGeral.BloquearTela();
        });

        $(this).ajaxStart(function () {
            campeonatoGeral.BloquearTela();
        });

        $(this).ajaxComplete(function () {
            campeonatoGeral.DesbloquearTela();
        });

        $("form").attr("novalidate", "novalidate");

        $("form").on("submit", function () {
            debugger;
            if (!$(document.activeElement).is('.no-load')) {
                campeonatoGeral.BloquearTela();
            }

            var temDados = $(this).attr("data-bloquear-tela");
            
            if (temDados !== "nao")
                campeonatoGeral.BloquearTela();

            //else if (!$(this).valid()) {
            //    var $primeiroInputComErro = $(this).find("input.error:first");

            //    if ($primeiroInputComErro.parents(".collapsed-box")) {
            //        $primeiroInputComErro.parents(".collapsed-box").find(".btn-box-tool").click();
            //    }

            //    $primeiroInputComErro.focus();

            //    campeonatoGeral.DesbloquearTela();
            //    return false;
            //}

        });

        $(document).ready(function () {
            $.ajax({
                type: "GET",
                url: "/Usuario/QuantidadeDeUsuariosNovos",
                success: function (retorno) {
                    if (retorno.quantidade > 0) {
                        $(".numeroDeUsuariosNovos").text(retorno.quantidade);
                    } else {
                        $(".numeroDeUsuariosNovos").text("");
                    }
                }
            });
        });
    });

})(window.campeonatoGeral = window.campeonatoGeral || {}, jQuery)