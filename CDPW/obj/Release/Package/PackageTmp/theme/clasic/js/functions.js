//window.onload = redirect;



//window.onunload = ConfirmDeleteData;
///window.onbeforeunload = ConfirmDeleteData; 

function redirect() {
    var loc = location.href.split(':');
    if (loc[0] == 'http') {
        location.href = 'https:' + loc[1];
    }
}

function ChangeAlert() {
    var sendserver = 'valchange';
    CallServer(sendserver, "");
}

function IfPrint() {
    var purl = window.location.href;
    if (purl.indexOf("DUSAForm.aspx") > 0) {
        if (!document.getElementById("opt1").checked && !document.getElementById("opt2").checked && 
            !document.getElementById("opt11").checked && !document.getElementById("opt22").checked){

                alert("Please select Page(s) to be printed");
                return;
        }
    }

    var sendserver = 'ifprint';
    CallServer(sendserver, "");
}

function ConfirmDeleteData() {
    var purl = window.location.href;
    //window.closed &&
    alert(window.self);
    alert(window.screen);
    if ((purl.indexOf("DUSAForm.aspx") > 0 || purl.indexOf("DCANForm.aspx") > 0 || purl.indexOf("cdpprintpreview.aspx") > 0)) {
        var r = confirm("Do you want your data to be deleted ? The page will also be closed...");
        //if (r == true) {
        //var sendserver = 'ifdeletedata';
        var sendserver = 'deletedata';
            CallServer(sendserver, "");
        //}
    }

} 



function ReceiveServerData(rValue) {
    //document.getElementById("ResultsSpan").innerHTML = rValue;
    //alert('call back');
    //documnet.getElementById("print-2").setAttribute('href', rValue);
    if (rValue.toLowerCase() == "ifprint") {
        printCF();
        return;
    }

    if (rValue.toLowerCase() == "#24") {
        //document.getElementById("discl_CAN").style = "disclaimer-4-update";
        $('#discl_CAN').addClass('disclaimer-4-update');
        document.getElementById("discl_CAN").innerHTML = '<span class="message-save">Please Save first, then Print</span>';
        
    }

    if (rValue.toLowerCase() == "#1600") {
        //document.getElementById("discl_USA").style = "disclaimer-4-update";
        $('#discl_USA').addClass('disclaimer-4-update');
        document.getElementById("discl_USA").innerHTML = '<span class="message-save">Please Save first, then Print</span>';
    }

    if (rValue.toLowerCase()=="msgdeletedata"){
        var purl = window.location.href;
        if (purl.indexOf("DUSAForm.aspx") > 0 || purl.indexOf("DCANForm.aspx") > 0 || purl.indexOf("cdpprintpreview.aspx") > 0) {
        var r = confirm("Do you want your data to be deleted ? The page will also be closed.");
            if (r == true) {
                var sendserver = 'deletedata';
                CallServer(sendserver, "");
            }
        }
    }


}

function numValF(field, rules, i, options) {
    var fId = field[0].id;
    var numf = document.getElementById(fId).value;
    //var numf = document.getElementById("txtArticlesValue1").value;

    var nA = numf.split(",");
    var nS = "";
    for (i = 0; i < nA.length; i++) {
        nS = nS + nA[i];
    }

    if (!isNaN(nS)) {
        var nN = new Number(nS);
        document.getElementById(field[0].id).value = $.number(nN, 2);
        //field.value = $.number(nN, 2);
        if (!(nN >= 0 && nN <= 214748.3647)) {
            return "The value should be between 0 and 214,748.3647";
        }
    }
    else {
        return "The value should be between 0 and 214,748.3647";
    }
}

function externalLinks() {
	if (!document.getElementsByTagName) return;
	var anchors = document.getElementsByTagName("a");
	for (var i=0; i<anchors.length; i++) {
		var anchor = anchors[i];
		if (anchor.getAttribute("href") &&
		anchor.getAttribute("rel") == "blank")
		anchor.target = "_blank";
		}
}

function esteData(txtDate) {
    var currVal = txtDate;

    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[1];
    dtDay = dtArray[3];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}

$(function () {
    $('a.edit').tipsy({ gravity: 's' });
    $('a.apply').tipsy({ gravity: 's' });
});

$(document).ready(function () {

    $("#DCAN").colorbox();
    $("#DUSA").colorbox();

    $('#mainSettings div .gender input').attr('disabled', true);

    //$('.translate').click(function () {
    $('.translate').hover(function () {
        $('#translate').css('display', 'compact').fadeIn('fast');
        // $('a#open-btn').css('display', 'none').fadeOut("slow");
        // $('a#close-btn').css('display', 'compact').fadeIn("slow");
        $('a#open-btn').addClass('color-1').fadeIn('fast');
    },
		function () {
		    //$('#translate').css('display', 'none').fadeOut('4500');
		    //$('a#open-btn').removeClass('color-1').fadeIn('fast');
		}
	);
    $('a#close_translate').click(function () {
        $('#translate').css('display', 'none').fadeOut('slow');
        $('a#open-btn').removeClass('color-1').fadeIn('fast');
    
    })
    /*$('a#close-btn').click(function () {
    $('#translate').css('display', 'none').fadeOut("slow");
    $('a#open-btn').css('display', 'compact').fadeIn("slow");
    $('a#close-btn').css('display', 'none').fadeOut("slow");
    });*/

    $('a.edit').click(function () {
        $(this).parent().children('a.edit').removeClass('visible').addClass('hidden');
        //$(this).parent().children('a.apply').removeClass('hidden').addClass('visible');
        $(this).parent().children('input').removeClass('text-1-disable').addClass('text-1');
        $(this).parent().children('input#txtUName').addClass('validate[minSize[6]]');
        $(this).parent().children('input#txtEmail').addClass('validate[required,custom[email]]');
        $(this).parent().children('input#txtPwd').addClass('validate[required,minSize[6]]');
        //$(this).parent().children('input#txtPwdConf').addClass('validate[required,equals[txtPwd]]');
        $(this).parent().children('input#txtAEmail').addClass('validate[custom[email]]');
        $(this).parent().children('input#txtDOB').addClass('validate[custom[date]]');
        $(this).parent().children('input#txtFName').addClass('validate[custom[onlyLetterSp]]');
        $(this).parent().children('input#txtMName').addClass('validate[custom[onlyLetterSp]]');
        $(this).parent().children('input#txtLName').addClass('validate[custom[onlyLetterSp]]');
        $(this).parent().children('input#txtStrNo').addClass('validate[custom[pos_smallint]]');
        $(this).parent().children('input#txtUNo').addClass('validate[custom[pos_smallint]]');
        $(this).parent().children('input#txtCity').addClass('validate[custom[onlyLetterSp]]');
        $(this).parent().children('input').attr('disabled', false);
        $(this).parent().children('select.select-1').attr('disabled', false);
        $(this).parent().children('span').children('input').attr('disabled', false);
    })
    $('a.edit-pass').click(function () {
        $('p.hidden').removeClass('hidden');
        $('#txtPwdConf').attr('disabled', false);
        $('#txtPwdConf').removeClass("text-1-disable").addClass("text-1 validate[required,equals[txtPwd]]");
    })


    // for print
    $('#print').hover(function () {
        $(this).children('ul').fadeIn('fast');
    },
		function () {
		    $(this).children('ul').fadeOut('fast');
		}
	);
    $('#print > ul').hover(function () {
        $(this).parent('li').children('a').addClass("selected");
    },
		function () {
		    $(this).parent('li').children('a').removeClass("selected");
		}
    );

    // for bottom print
    $('#print-2-container').hover(function () {
        $('ul#print-choice-bottom').fadeIn('fast');
    },
	    function () {
	        $('ul#print-choice-bottom').fadeOut('fast');
	    }
	);

    $('ul#print-choice-bottom').hover(function () {
        $('#print-2').css("background-position", "right top");
    },
		function () {
		    $('#print-2').css("background-position", "left top");
		}
    );

    $('#print-2').hover(function () {
        $('#print-2').css("background-position", "right top");
    },
		function () {
		    $('#print-2').css("background-position", "left top");
		}
    );

    //    $('a.apply').click(function () {
    //        //$(this).parent().children('a.edit').removeClass('hidden').addClass('visible');
    //        $(this).parent().children('a.apply').removeClass('visible').addClass('hidden');
    //        $(this).parent().children('input').removeClass('text-1').addClass('text-1-disable');
    //        $(this).parent().children('input').attr('disabled', true);
    //        alert($(this).parent().children('input').val());

    //    })

    //    $('a.show-confirm-password').click(function () {
    //        $('p.hidden').removeClass('hidden').addClass('visible');
    //    })
    //    $('a.hide-confirm-password').click(function () {
    //        $('p.visible').removeClass('visible').addClass('hidden');
    //    })
});

