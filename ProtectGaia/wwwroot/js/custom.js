/**	
	* Template Name: Be-Hero
	* Version: 1.0	
	* Template Scripts
	* Author: MarkUps
	* Author URI: http://www.markups.io/

	Custom JS
	
	1. MAIN SLIDER
	2. VIDEO POPUP
	3. TESTIMONIALS (SLICK SLIDER)
	4. SCROLL TOP BUTTON
	5. CLIENTS SLIDEER ( SLICK SLIDER )
	6. PORTFOLIO GALLERY
	7. PORTFOLIO POPUP VIEW ( IMAGE LIGHTBOX )
	
	
**/



(function( $ ){

	
	/*  2. VIDEO POPUP
	/* ----------------------------------------------------------- */

   $('.mu-video-play-btn').on('click', function(event) {
	   
        event.preventDefault();
        
        $('.mu-video-iframe-area').addClass('mu-video-iframe-display');
       
    });
   
    // when click the close btn

    // disappear iframe window
    
    $('.mu-video-close-btn').on('click', function(event) {
	    
	    event.preventDefault();
	    
		$('.mu-video-iframe-area').removeClass('mu-video-iframe-display');
		
    });

    // stop iframe if it is play while close the iframe window

    $('.mu-video-close-btn').click(function(){

        $('.mu-video-iframe').attr('src', $('.mu-video-iframe').attr('src',''));

    });

    // when click overlay area

     $('.mu-video-iframe-area').on('click', function(event) {
	    
	    event.preventDefault();
	    
		$('.mu-video-iframe-area').removeClass('mu-video-iframe-display');
		
    });

	$('.mu-video-iframe-area, .mu-video-iframe').on('click', function(e){
	    e.stopPropagation();
	});


  	/*  4. SCROLL TOP BUTTON
  	/* ----------------------------------------------------------- */

	  //Check to see if the window is top if not then display button

	    jQuery(window).scroll(function(){
	      if (jQuery(this).scrollTop() > 300) {
	        jQuery('.scrollToTop').fadeIn();
	      } else {
	        jQuery('.scrollToTop').fadeOut();
	      }
	    });
	     
	    //Click event to scroll to top

	    jQuery('.scrollToTop').click(function(){
	      jQuery('html, body').animate({scrollTop : 0},800);
	      return false;
	    });
	 
	
	/* ----------------------------------------------------------- */
	/*  6. PORTFOLIO GALLERY
	/* ----------------------------------------------------------- */ 
	
		

	/* ----------------------------------------------------------- */
	/*  7. PORTFOLIO POPUP VIEW ( IMAGE LIGHTBOX )
	/* ----------------------------------------------------------- */ 

	$('.mu-imglink').magnificPopup({
	  type: 'image',
	  mainClass: 'mfp-fade',
	  gallery:{
	    enabled:true
	  }
	});

	
	
})( jQuery );



  
	