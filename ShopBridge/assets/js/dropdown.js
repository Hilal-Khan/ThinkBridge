$(document).ready(function(){ 
  $('#button1').click(function(){ 
    alert($('#combo :selected').text());
  });
});