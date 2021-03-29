

var $cnfrmAlrtButton= null;
var $inputModalVal = null;
var ERROR_MSG_DELAY = 2000;

var inputModalCallback = null;

var confirmModalCallback = null;

var alertModalCallback = null;

function confirm_modal(msg, callback){
  $('#confirm-modal .modal-body').html(msg);
  confirmModalCallback = callback;
  $('#confirm-modal').modal('show');
}

function input_modal(title, msg, callback, pattern, error, type)
{
  _pattern = (pattern!=undefined)?pattern:"";

  _error = (error!=undefined)?error:"input error";

  _type = (type!=undefined)?type:"text";

  $('#input-modal .modal-title').html(title);
  $('#input-modal label').html(msg);
  $('#input-modal input').attr("pattern",_pattern);
  $('#input-modal input').prop({type:_type});

  $('#input-modal input').val('');
  $("#any-input-error").html(_error);
  inputModalCallback = callback;
  $("#input-modal").modal("show");
}

function alert_modal(msg, callback)
{
  $('#alert-modal .modal-body').html(msg);
  $("#alert-modal").modal("show");

  alertModalCallback = null;

  if(callback!=undefined)
  {
    alertModalCallback = callback;
  }
}

function androidLogonDevice()
{
  return Android.logonDevice();
}

function showAndroidToast(toast) {
    Android.showToast(toast);
}

$(document).ready( function() {

$("#input-modal-id").click(function (e) {

  var t = $("#any-input").attr("pattern");
  var regex = new RegExp(t);
  $inputModalVal= $("#any-input").val();
  if(regex.test($inputModalVal))
  {
    $("#input-modal").modal("hide");
    inputModalCallback();
  }else {
    $inputModalVal = null;
    $("#any-input-error").show();
    $("#any-input-error").delay(ERROR_MSG_DELAY).fadeOut(300);
  }
});

$("#confirm-modal-id").click(function (e) {
  $("#confirm-modal").modal("hide");
  confirmModalCallback();
});

$("#alert-modal-id").click(function (e) {
  $("#alert-modal").modal("hide");

  if(alertModalCallback!=null)
  {
    alertModalCallback();
  }
});

 });
