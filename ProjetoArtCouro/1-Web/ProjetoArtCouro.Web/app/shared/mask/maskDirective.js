(function () {
    "use strict";

    function cpfCnpjMaskBehavior(val) {
        return val.replace(/\D/g, "").length > 11 ? "00.000.000/0000-00" : "000.000.000-00999";
    };
    var documentoOptions = {
        onKeyPress: function (val, e, field, options) {
            field.mask(cpfCnpjMaskBehavior.apply({}, arguments), options);
        }
    };

    function spMaskBehavior(val) {
        return val.replace(/\D/g, "").length === 11 ? "(00) 00000-0000" : "(00) 0000-00009";
    }
    var spOptions = {
        onKeyPress: function (val, e, field, options) {
            field.mask(spMaskBehavior.apply({}, arguments), options);
        }
    };

    function validationCPF(value) {
        value = value.replace(/[^0-9]/gi, "");
        //vazio
        if (value.length === 0) {
            return true;
        }

        if (value.length !== 11 ||
            value === "00000000000" ||
            value === "11111111111" ||
            value === "22222222222" ||
            value === "33333333333" ||
            value === "44444444444" ||
            value === "55555555555" ||
            value === "66666666666" ||
            value === "77777777777" ||
            value === "88888888888" ||
            value === "99999999999")
            return false;

        var add = 0;
        for (var i = 0; i < 9; i++) {
            add += parseInt(value.charAt(i)) * (10 - i);
        }

        var rev = 11 - (add % 11);
        if (rev === 10 || rev === 11) {
            rev = 0;
        }

        if (rev !== parseInt(value.charAt(9))) {
            return false;
        }

        add = 0;
        for (i = 0; i < 10; i++) {
            add += parseInt(value.charAt(i)) * (11 - i);
        }

        rev = 11 - (add % 11);
        if (rev === 10 || rev === 11) {
            rev = 0;
        }

        if (rev !== parseInt(value.charAt(10))) {
            return false;
        }

        //cpf válido
        return true;
    }

    function validationCNPJ(value) {
        value = value.replace(/[^0-9]/gi, "");
        //vazio
        if (value.length === 0) {
            return true;
        }
        
        if (value.length !== 14) {
            return false;
        }
        // Elimina CNPJs invalidos conhecidos
        if (value === "00000000000000" ||
            value === "11111111111111" ||
            value === "22222222222222" ||
            value === "33333333333333" ||
            value === "44444444444444" ||
            value === "55555555555555" ||
            value === "66666666666666" ||
            value === "77777777777777" ||
            value === "88888888888888" ||
            value === "99999999999999") {
            return false;
        }
        // Valida DVs
        var tamanho = value.length - 2;
        var numeros = value.substring(0, tamanho);
        var digitos = value.substring(tamanho);
        var soma = 0;
        var pos = tamanho - 7;
        for (var i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }
        var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado.toString() !== digitos.charAt(0)) {
            return false;
        }

        tamanho = tamanho + 1;
        numeros = value.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }

        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado.toString() !== digitos.charAt(1)) {
            return false;
        }

        return true;
    }

    angular.module("sbAdminApp")
        .directive("cpfMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("000.000.000-00");
                }
            }
        }).directive("cnpjMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("00.000.000/0000-00");
                }
            }
        }).directive("cpfCnpjMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask(cpfCnpjMaskBehavior, documentoOptions);
                }
            }
        }).directive("cepMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("00000-000");
                }
            }
        }).directive("telefoneMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("(00) 0000-0000");
                }
            }
        }).directive("dataMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("00/00/0000");
                }
            }
        }).directive("somenteLetraMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                }
            }
        }).directive("somenteCinquentaLetrasMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                }
            }
        }).directive("somenteDoisDigitos", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("00");
                }
            }
        }).directive("somenteVinteDigitos", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("00000000000000000000");
                }
            }
        }).directive("milharMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask("0.000.000", { reverse: true });
                }
            }
        }).directive("celularMask", function() {
            return {
                restrict: "A",
                link: function(scope, element) {
                    $(element).mask(spMaskBehavior, spOptions);
                }
            }
        }).directive("cpfValido", function() {
            return {
                restrict: "A",
                require: "ngModel",
                link: function(scope, element, attrs, ctrl) {
                    scope.$watch(attrs.ngModel, function() {
                        ctrl.$setValidity("cpfValido", validationCPF(element[0].value));
                    });
                }
            }
        }).directive("cnpjValido", function() {
            return {
                restrict: "A",
                require: "ngModel",
                link: function(scope, element, attrs, ctrl) {
                    scope.$watch(attrs.ngModel, function() {
                        ctrl.$setValidity("cnpjValido", validationCNPJ(element[0].value));
                    });
                }
            }
        }).directive("emailValido", function() {
            return {
                restrict: "A",
                require: "ngModel",
                link: function(scope, element, attrs, ctrl) {
                    scope.$watch(attrs.ngModel, function () {
                        var regexPattern = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/g;
                        ctrl.$setValidity("emailValido", regexPattern.test(element[0].value));
                    });
                }
            }
        });
})();