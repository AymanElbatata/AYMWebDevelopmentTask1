//Adjust Slider Hight
//var winH    = $(window).height(),
  //  upperH  = $('.container-1').innerHeight(),
//    navH    = $('.navbar').innerHeight();

//$('.slider').height(winH - (upperH + navH));
//$('.carousel-item').height(winH - (upperH + navH));

$(function () {
  'use strict';
  // Adjust Slider Height
  var winH    = $(window).height(),
      upperH  = $('.container-1').innerHeight(),
      navH    = $('.navbar').innerHeight();
  $('.slider, .carousel-item').height(winH - ( upperH + navH ));
    
});
