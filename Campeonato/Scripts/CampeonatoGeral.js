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
        $.blockUI.defaults.border = 'none';
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

    //$(document).on("ready", function () {

    //    $("body").tooltip({ selector: '[data-toggle=tooltip]' });

    //    $("input[required], select[required]").closest(".form-group").find("label.control-label").append("<span class='obrigatorio'>*</span>");

    //    $("a:not(.no-load)").on("click", function () {
    //        campeonatoGeral.BloquearTela();
    //    });

    //    $(this).ajaxStart(function () {
    //        campeonatoGeral.BloquearTela();
    //    });

    //    $(this).ajaxComplete(function () {
    //        campeonatoGeral.DesbloquearTela();
    //    });


    //    $("form").attr("novalidate", "novalidate");

    //    $("form").on("submit", function () {

    //        var temDados = $(this).attr("data-bloquear-tela");
    //        console.log(temDados);

    //        if (temDados !== "nao")
    //            campeonatoGeral.BloquearTela();

    //        if (!$(this).valid()) {
    //            var $primeiroInputComErro = $(this).find("input.error:first");

    //            if ($primeiroInputComErro.parents(".collapsed-box")) {
    //                $primeiroInputComErro.parents(".collapsed-box").find(".btn-box-tool").click();
    //            }

    //            $primeiroInputComErro.focus();

    //            campeonatoGeral.DesbloquearTela();
    //            return false;
    //        }

    //        $("form input:not(.data)").unmask();
    //        $("form .dinheiro").maskMoney("destroy");
    //        $("form .porcentagem").maskMoney("destroy");

    //    });
    //});

})(window.campeonatoGeral = window.campeonatoGeral || {});