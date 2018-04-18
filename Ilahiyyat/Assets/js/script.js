$("document").ready(function($){
    var nav = $('.menu');

    $(window).scroll(function () {
        if ($(this).scrollTop() > 200) {
            nav.addClass("fixed");
        } else {
            nav.removeClass("fixed");
        }
    });
})
$(window).scroll(function(event){
	if($(document).scrollTop() > 300){
		$(".circle").css({
			display : "block",
			bottom: "40px"

			
		});
		
	}else{
		$(".circle").css({
			display: "none"
		
		});

	}
})

$(".circle").click(function(event){
	 console.log($(document).scrollTop())
	var currentPos = $(document).scrollTop();
	setInterval(function(){
		if(currentPos > 0){
			currentPos -= 15;
			$(document).scrollTop(currentPos);
		}
	},1)
	
	
	
})